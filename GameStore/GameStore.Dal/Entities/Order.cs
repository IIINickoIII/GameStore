using GameStore.Dal.Entities.Enums;
using GameStore.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class Order : BaseEntity, IOrder
    {
        public int CustomerId { get; set; }

        public DateTime Date { get; set; }

        public bool IsClosedForEdit { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public bool IsPaid { get; set; }

        public decimal TotalSum { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}
