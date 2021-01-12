using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class PlatformType : BaseEntity
    {
        [Required] public string Type { get; set; }

        public IEnumerable<GamePlatformType> Games { get; set; }
    }
}