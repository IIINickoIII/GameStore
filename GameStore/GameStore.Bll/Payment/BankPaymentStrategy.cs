using GameStore.Bll.Payment.Dto;
using GameStore.Bll.Payment.Interfaces;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using System;

namespace GameStore.Bll.Payment
{
    public class BankPaymentStrategy : IPaymentStrategy
    {
        private readonly IUnitOfWork _uow;

        public BankPaymentStrategy(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IPaymentResult Pay(IOrder order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            order.IsPaid = true;
            order.IsClosedForEdit = true;
            _uow.OrderRepository.Update((Order)order);
            _uow.Save();
            var bankPaymentResult = new BankPaymentResult()
            {
                IsSuccessful = true
            };
            return bankPaymentResult;
        }
    }
}
