using GameStore.Bll.Payment.Interfaces;
using GameStore.Dal.Interfaces;
using System;

namespace GameStore.Bll.Payment
{
    public class PaymentContext : IPaymentContext
    {
        private IPaymentStrategy _paymentStrategy;

        public IPaymentResult Pay(IOrder order)
        {
            return _paymentStrategy.Pay(order);
        }

        public void SetStrategy(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy ??
                               throw new ArgumentNullException(nameof(paymentStrategy), "PaymentStrategy can't be null");
        }
    }
}
