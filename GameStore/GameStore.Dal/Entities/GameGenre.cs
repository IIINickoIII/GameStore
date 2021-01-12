using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class GameGenre : BaseEntity
    {
        [Required] public int GameId { get; set; }

        public Game Game { get; set; }

        [Required] public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}