using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Dto
{
    [ExcludeFromCodeCoverage]
    public class CommentDto
    {
        [Required] public int Id { get; set; }

        [Required] public int? ParentCommentId { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Body { get; set; }

        [Required] public DateTime Time { get; set; }

        public IEnumerable<CommentDto> ChildrenComments { get; set; } = new List<CommentDto>();

        public bool IsDeleted { get; set; }
    }
}