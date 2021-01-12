using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class Genre : BaseEntity
    {
        public int? ParentGenreId { get; set; }

        [Required] public string Name { get; set; }

        public IEnumerable<GameGenre> Games { get; set; }
    }
}