using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string Image { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int InitialQuantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateddAt { get; set; } = DateTime.Now;

        public List<Order> ManyOdersBy { get; set; }
    }
}