using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class Game : BaseEntity
    {
        [Required] public string Key { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Description { get; set; }

        public decimal Price { get; set; }

        public float Discount { get; set; }

        public short UnitInStock { get; set; }

        public bool Discontinued { get; set; }

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<GameGenre> Genres { get; set; }

        public IEnumerable<GamePlatformType> PlatformTypes { get; set; }

    }
}