using GameStore.Bll.Payment.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Dto
{
    [ExcludeFromCodeCoverage]
    public class OrderDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalSum { get; set; }

        public bool IsPaid { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime Date { get; set; }

        public bool IsClosedForEdit { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
