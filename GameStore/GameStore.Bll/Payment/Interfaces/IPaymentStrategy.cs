using GameStore.Dal.Interfaces;

namespace GameStore.Bll.Payment.Interfaces
{
    public interface IPaymentStrategy
    {
        IPaymentResult Pay(IOrder order);
    }
}
