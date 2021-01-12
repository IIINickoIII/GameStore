using GameStore.Bll.Dto;
using GameStore.Bll.Payment.Enums;
using GameStore.Bll.Payment.Interfaces;
using System.Collections.Generic;

namespace GameStore.Bll.Interfaces
{
    public interface IOrderService
    {
        OrderDto GetCurrentOrDefaultEmptyOrder();

        IPaymentResult PayForOrder(int orderId, PaymentMethod paymentMethod);

        string GetBankInvoice(int orderId);

        IEnumerable<OrderDto> GetAllOrders();
    }
}
