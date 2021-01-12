using GameStore.Bll.Payment.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Payment.Dto
{
    [ExcludeFromCodeCoverage]
    public class BoxPaymentResult : IPaymentResult
    {
        public bool IsSuccessful { get; set; }

        public string BoxNumber { get; set; }

        public Guid InvoiceNumber { get; set; }

        public decimal Sum { get; set; }
    }
}
