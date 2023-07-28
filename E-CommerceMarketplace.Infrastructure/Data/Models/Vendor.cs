using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
		[ForeignKey(nameof(User))]
		public string User_Id { get; set; } = null!;
        
        public ApplicationUser User { get; set; } = null!;
    }
}