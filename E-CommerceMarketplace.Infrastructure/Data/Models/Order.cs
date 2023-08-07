using E_CommerceMarketplace.Infrastructure.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Order
    {
        public Order()
        {
            Items = new List<Item>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime? DateCompleted { get; set; }

        [ForeignKey(nameof(Sale))]
        public int? Sale_Id { get; set; }

        public Sale? Sale { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public string User_Id { get; set; }
        public ApplicationUser User { get; set; }

        public List<Item> Items { get; set; }
    }
}

