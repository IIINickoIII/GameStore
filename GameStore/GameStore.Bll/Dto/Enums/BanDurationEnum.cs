using System.ComponentModel.DataAnnotations;

namespace GameStore.Bll.Dto.Enums
{
    public enum BanDurationEnum
    {
        [Display(Name = "One Hour")]
        OneHour,
        [Display(Name = "One Day")]
        OneDay,
        [Display(Name = "One Week")]
        OneWeek,
        [Display(Name = "One Month")]
        OneMonth,
        [Display(Name = "Permanent")]
        Permanent
    }
}
