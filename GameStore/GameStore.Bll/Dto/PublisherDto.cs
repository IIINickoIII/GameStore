using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Dto
{
    [ExcludeFromCodeCoverage]
    public class PublisherDto
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public IEnumerable<GameDto> Games { get; set; }

        public bool IsDeleted { get; set; }
    }
}
