using GameStore.Bll.Interfaces;
using GameStore.Bll.Payment.Enums;
using GameStore.Web.Services.Interfaces;
using GameStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameStore.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IPdfCreator _pdfCreator;
        private const string FileType = "pdf";
        private const string FileMime = "application/pdf";
        private const string FileName = "Payment Invoice";

        public OrdersController(IOrderService orderService, IPdfCreator pdfCreator)
        {
            _orderService = orderService;
            _pdfCreator = pdfCreator;
        }

        [Route("/basket")]
        public IActionResult ShowMyBasket()
        {
            var order = _orderService.GetCurrentOrDefaultEmptyOrder();
            return View("BasketForm", order);
        }

        [Route("/basket/details")]
        public IActionResult MakeOrder()
        {
            var order = _orderService.GetCurrentOrDefaultEmptyOrder();
            return View("OrderDetails", order);
        }

        [Route("/order/{orderId}/payment/bank")]
        public IActionResult BankPayment(int orderId)
        {
            var paymentDetails = new PaymentDetailsViewModel()
            {
                OrderId = orderId,
                PaymentMethod = PaymentMethod.Bank
            };
            return View("../Payment/BankPayment", paymentDetails);
        }

        [Route("/order/{orderId}/payment/box")]
        public IActionResult BoxPayment(int orderId)
        {
            var orderSum = _orderService.GetCurrentOrDefaultEmptyOrder().TotalSum;
            var paymentDetails = new PaymentDetailsViewModel()
            {
                OrderId = orderId,
                BoxAccountNumber = Guid.NewGuid(),
                InvoiceNumber = Guid.NewGuid(),
                Sum = orderSum,
                PaymentMethod = PaymentMethod.Box
            };
            return View("../Payment/BoxPayment", paymentDetails);
        }

        [Route("/order/{orderId}/payment/visa")]
        public IActionResult VisaPayment(int orderId)
        {
            var orderSum = _orderService.GetCurrentOrDefaultEmptyOrder().TotalSum;
            var paymentDetails = new PaymentDetailsViewModel()
            {
                OrderId = orderId,
                Sum = orderSum,
                PaymentMethod = PaymentMethod.Visa
            };
            return View("../Payment/VisaPayment", paymentDetails);
        }

        public IActionResult PayOrder(int orderId, PaymentMethod paymentMethod)
        {
            var paymentResult = _orderService.PayForOrder(orderId, paymentMethod);

            return View(paymentResult.IsSuccessful ? "../Payment/SuccessPayment" : "../Payment/FailPayment");
        }

        public IActionResult GetBankInvoice(int orderId)
        {
            var fileStreamResult = new FileStreamResult(
                _pdfCreator.CreateStream(_orderService.GetBankInvoice(orderId)),
                new Microsoft.Net.Http.Headers.MediaTypeHeaderValue(FileMime))
            {
                FileDownloadName = $"{FileName}.{FileType}"
            };
            return fileStreamResult;
        }

        [Route("/orders")]
        public IActionResult GetAllOrders()
        {
            return View("OrdersReadOnlyList", _orderService.GetAllOrders());
        }
    }
}
