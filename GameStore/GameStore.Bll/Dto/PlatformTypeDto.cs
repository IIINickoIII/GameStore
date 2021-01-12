using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Bll.Dto
{
    [ExcludeFromCodeCoverage]
    public class PlatformTypeDto
    {
        public int Id { get; set; }

        [Required] public string Type { get; set; }

        public bool IsDeleted { get; set; }
    }
}