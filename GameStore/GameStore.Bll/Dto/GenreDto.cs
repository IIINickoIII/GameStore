using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Dto
{
    [ExcludeFromCodeCoverage]
    public class GenreDto
    {
        [Required] public int Id { get; set; }

        public int? ParentGenreId { get; set; }

        [Required] public string Name { get; set; }

        public IEnumerable<GenreDto> ChildrenGenres { get; set; } = new List<GenreDto>();

        public bool IsDeleted { get; set; }
    }
}