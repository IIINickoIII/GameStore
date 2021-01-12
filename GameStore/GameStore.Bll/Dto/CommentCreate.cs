using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Dto
{
    [ExcludeFromCodeCoverage]
    public class CommentCreate
    {
        public int? ParentCommentId { get; set; }

        [Required] public int GameId { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Body { get; set; }
    }
}