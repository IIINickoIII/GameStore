using GameStore.Bll.Payment.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Payment.Dto
{
    [ExcludeFromCodeCoverage]
    public class FailPaymentResult : IPaymentResult
    {
        public FailPaymentResult()
        {
            IsSuccessful = false;
        }

        public bool IsSuccessful { get; set; }
    }
}
