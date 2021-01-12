using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Web.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class PlatformTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [Display(Name = "Type")]
        public string Type { get; set; }
    }
}
