using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class Comment : BaseEntity
    {
        public int? ParentCommentId { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Body { get; set; }

        [Required] public DateTime Time { get; set; }
    }
}