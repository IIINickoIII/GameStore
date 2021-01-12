using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Dto
{
    [ExcludeFromCodeCoverage]
    public class GameCreate
    {
        [Required] public string Name { get; set; }

        [Required] public string Key { get; set; }

        [Required] public string Description { get; set; }

        [Required] public int PublisherId { get; set; }

        public float Discount { get; set; }

        public IEnumerable<int> GenreIds { get; set; }

        [Required] public IEnumerable<int> PlatformTypeIds { get; set; }

        [Required] public decimal Price { get; set; }

        [Required] public short UnitInStock { get; set; }

        public bool Discontinued { get; set; }
    }
}