﻿using E_CommerceMarketplace.Infrastructure.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Buyer))]
        public string? Buyer_Id { get; set; }

        public ApplicationUser? Buyer { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        public DateTime? DatePaid { get; set; }

        [ForeignKey(nameof(Status))]
        public int Status_Id { get; set; }

        public Status Status { get; set; } = null!;

        [ForeignKey(nameof(Payment))]
        public int? Payment_Id { get; set; }

        public Payment? Payment { get; set; } = null!;

        public List<Product> Products { get; set; }
    }
}

