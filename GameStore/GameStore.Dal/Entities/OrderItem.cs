using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class OrderItem : BaseEntity
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public decimal SumWithoutDiscount { get; set; }

        public decimal SumWithDiscount { get; set; }
    }
}
