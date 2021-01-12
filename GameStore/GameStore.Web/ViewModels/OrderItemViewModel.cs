using GameStore.Bll.Dto;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Web.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        [Required]
        public int GameId { get; set; }

        public GameDto Game { get; set; }

        public int? OrderId { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please, select the number of copies")]
        public short Quantity { get; set; } = 1;

        [Display(Name = "Discount")]
        [Required(ErrorMessage = "Discount is required")]
        public float Discount { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        public bool IsInOrder { get; set; }
    }
}
