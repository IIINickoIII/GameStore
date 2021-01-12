using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Dto
{
    [ExcludeFromCodeCoverage]
    public class GameDto
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public decimal Price { get; set; }

        public float Discount { get; set; }

        public short UnitInStock { get; set; }

        public bool Discontinued { get; set; }

        public int PublisherId { get; set; }

        public PublisherDto Publisher { get; set; }

        public IEnumerable<GenreDto> Genres { get; set; }

        public IEnumerable<PlatformTypeDto> PlatformTypes { get; set; }

        public override string ToString()
        {
            return $"Game: {Name}\n" +
                $"Key: {Key}\n" +
                $"Description: {Description}";
        }
    }
}