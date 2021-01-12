using GameStore.Dal.Interfaces;

namespace GameStore.Bll.Payment.Interfaces
{
    public interface IPaymentContext
    {
        IPaymentResult Pay(IOrder order);

        void SetStrategy(IPaymentStrategy paymentStrategy);
    }
}
