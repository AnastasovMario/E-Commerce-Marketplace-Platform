using E_CommerceMarketplace.Infrastructure.DatabseConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Status
    {
        [Key]
        public int Id { get; init; }

        [Required]
		[MaxLength(DbConstants.StatusLength)]
		public string Description { get; init; } = null!;

        [NotMapped]
        public static Status Unavailable { get; set; } = new() { Id = 1 };

        [NotMapped]
        public static Status Stocked { get; set; } = new() { Id = 2 };
    }
}
