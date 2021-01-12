using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Dto
{
    [ExcludeFromCodeCoverage]
    public class OrderItemDto
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public GameDto Game { get; set; }

        public int OrderId { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public decimal SumWithoutDiscount { get; set; }

        public decimal SumWithDiscount { get; set; }
    }
}
