using GameStore.Bll.Payment.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Payment.Dto
{
    [ExcludeFromCodeCoverage]
    public class BankPaymentResult : IPaymentResult
    {
        public bool IsSuccessful { get; set; }
    }
}
