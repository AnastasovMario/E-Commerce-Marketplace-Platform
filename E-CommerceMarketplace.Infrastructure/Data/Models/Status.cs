using System.ComponentModel.DataAnnotations;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Status
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Description { get; init; } = null!;
    }
}
