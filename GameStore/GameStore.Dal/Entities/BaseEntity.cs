using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class BaseEntity
    {
        [Key] public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}