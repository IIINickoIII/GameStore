using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Bll.Payment;
using GameStore.Bll.Payment.Dto;
using GameStore.Bll.Payment.Enums;
using GameStore.Bll.Payment.Interfaces;
using GameStore.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.Bll.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPaymentContext _paymentContext;
        private const int CalculatePercentValue = 100;
        private const int NumberOfDigitsAfterComaInPrice = 2;

        public OrderService(IUnitOfWork uow, IMapper mapper, IPaymentContext paymentContext)
        {
            _uow = uow;
            _mapper = mapper;
            _paymentContext = paymentContext;
        }

        public OrderDto GetCurrentOrDefaultEmptyOrder()
        {
            if (!_uow.OrderRepository.Any(x => x.IsClosedForEdit == false))
            {
                return new OrderDto()
                {
                    OrderItems = new List<OrderItemDto>()
                };
            }

            var orderInDb = _uow.OrderRepository
                .Single(x => x.IsClosedForEdit == false);
            var orderItems =
                _uow.OrderItemRepository.Find(x => x.OrderId == orderInDb.Id & x.IsDeleted == false,
                    orderItem => orderItem.Include(x => x.Game)).ToList();
            var orderSum = default(decimal);

            foreach (var orderItem in orderItems)
            {
                orderItem.SumWithoutDiscount = orderItem.Game.Price * orderItem.Quantity;
                var calculatedSumWithDiscount = orderItem.SumWithoutDiscount -
                                            orderItem.SumWithoutDiscount * (decimal)orderItem.Game.Discount / CalculatePercentValue;
                orderItem.SumWithDiscount = Math.Round(calculatedSumWithDiscount, NumberOfDigitsAfterComaInPrice);
                orderItem.Price = orderItem.Game.Price;
                orderItem.Discount = orderItem.Game.Discount;
                orderSum += orderItem.SumWithDiscount;
            }

            orderInDb.OrderItems = orderItems;
            orderInDb.TotalSum = orderSum;

            _uow.OrderItemRepository.UpdateRange(orderItems);
            _uow.OrderRepository.Update(orderInDb);
            _uow.Save();

            var orderDto = _mapper.Map<OrderDto>(orderInDb);
            return orderDto;
        }

        public IPaymentResult PayForOrder(int orderId, PaymentMethod paymentMethod)
        {
            try
            {
                var order = _uow.OrderRepository.Single(x =>
                    x.Id == orderId & x.IsPaid == false);
                order.OrderItems = _uow.OrderItemRepository.Find(x => x.OrderId == orderId & x.IsDeleted == false,
                    orderItem => orderItem.Include(x => x.Game)).ToList();
                _paymentContext.SetStrategy(paymentMethod switch
                {
                    PaymentMethod.Bank => new BankPaymentStrategy(_uow),
                    PaymentMethod.Box => new BoxPaymentStrategy(_uow),
                    PaymentMethod.Visa => new VisaPaymentStrategy(_uow),
                    _ => null
                });
                return _paymentContext.Pay(order);
            }
            catch (Exception)
            {
                return new FailPaymentResult();
            }
        }

        public string GetBankInvoice(int orderId)
        {
            if (!_uow.OrderRepository.Any(x => x.Id == orderId))
            {
                throw new ArgumentException($"No order with Id = {orderId} in the Database");
            }

            var order = _uow.OrderRepository.Single(x =>
                x.Id == orderId & x.IsPaid == false);
            order.OrderItems = _uow.OrderItemRepository.Find(x => x.OrderId == orderId & x.IsDeleted == false,
                orderItem => orderItem.Include(x => x.Game)).ToList();
            return GenerateInvoiceContentPdf(order);
        }

        public IEnumerable<OrderDto> GetAllOrders()
        {
            var ordersInDb = _uow.OrderRepository.GetAll();
            return _mapper.Map<IEnumerable<OrderDto>>(ordersInDb);
        }

        private static string GenerateInvoiceContentPdf(IOrder order)
        {
            var documentBody = new StringBuilder();
            documentBody.AppendLine("INVOICE");
            documentBody.AppendLine($"Invoice Number {Guid.NewGuid()}");
            documentBody.AppendLine("Mr./Ms. Default User Name");
            documentBody.AppendLine($"Order: {order.Id}");
            documentBody.AppendLine();
            documentBody.AppendLine("Your Order");
            var itemNumber = 1;
            foreach (var orderItem in order.OrderItems)
            {
                documentBody.Append($"{itemNumber}.\t");
                documentBody.AppendLine($"{orderItem.Game.Name}\t");
                documentBody.Append($"\t\t\tPrice: {orderItem.Price}$\t");
                documentBody.Append($"Quantity: {orderItem.Quantity}pcs.\t");
                documentBody.Append($"Total: {orderItem.SumWithoutDiscount}$\t");
                documentBody.Append($"Discount: {orderItem.Discount}%\t");
                documentBody.AppendLine($"Final: {orderItem.SumWithDiscount}$\t");
                documentBody.AppendLine();
                itemNumber++;
            }

            documentBody.AppendLine();
            documentBody.AppendLine($"Final: {order.TotalSum}$");
            return documentBody.ToString();
        }
    }
}
