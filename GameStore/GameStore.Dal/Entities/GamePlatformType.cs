using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class GamePlatformType : BaseEntity
    {
        [Required] public int GameId { get; set; }

        public Game Game { get; set; }

        [Required] public int PlatformTypeId { get; set; }

        public PlatformType PlatformType { get; set; }
    }
}