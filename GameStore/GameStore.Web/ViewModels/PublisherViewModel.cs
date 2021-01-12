using GameStore.Bll.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Web.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class PublisherViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Home page")]
        [Required(ErrorMessage = "Home page is required")]
        public string HomePage { get; set; }

        public IEnumerable<GameDto> Games { get; set; }

        public bool IsDeleted { get; set; }
    }
}
