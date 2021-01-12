using GameStore.Bll.Payment.Enums;
using System;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Web.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class PaymentDetailsViewModel
    {
        public int OrderId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public Guid BoxAccountNumber { get; set; }

        public Guid InvoiceNumber { get; set; }

        public decimal Sum { get; set; }
    }
}
