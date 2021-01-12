using GameStore.Bll.Dto.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Web.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class UserBanViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please, select ban duration")]
        [Display(Name = "Ban duration")]
        public BanDurationEnum BanDurationEnum { get; set; }
    }
}
