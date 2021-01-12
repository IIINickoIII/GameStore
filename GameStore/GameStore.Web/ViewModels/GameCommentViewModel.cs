using GameStore.Bll.Dto;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Web.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class GameCommentViewModel
    {
        public GameDto Game { get; set; }

        public CommentCreateViewModel CommentCreate { get; set; }

        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
