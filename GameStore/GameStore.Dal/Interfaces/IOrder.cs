using GameStore.Dal.Entities;
using GameStore.Dal.Entities.Enums;
using System;
using System.Collections.Generic;

namespace GameStore.Dal.Interfaces
{
    public interface IOrder
    {
        public int Id { get; set; }

        int CustomerId { get; set; }

        DateTime Date { get; set; }

        bool IsClosedForEdit { get; set; }

        IEnumerable<OrderItem> OrderItems { get; set; }

        bool IsPaid { get; set; }

        decimal TotalSum { get; set; }

        PaymentMethod PaymentMethod { get; set; }
    }
}
