using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal Total { get; init; }

        [Required]
        public DateTime Date { get; set; }
    }
}
