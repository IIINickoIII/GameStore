using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Web.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class CommentCreateViewModel
    {
        public int? ParentCommentId { get; set; }

        [Required] public int GameId { get; set; }

        [Required(ErrorMessage = "Please, enter your name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Comment text is required")]
        [Display(Name = "Text")]
        public string Body { get; set; }
    }
}
