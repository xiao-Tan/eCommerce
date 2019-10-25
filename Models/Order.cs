using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Orderquantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateddAt { get; set; } = DateTime.Now;

        public Customer Customer { get; set; }

        public Product Product { get; set; }

        [NotMapped]
        public string TimeDiff
        {
            get{
                TimeSpan timeSince = DateTime.Now.Subtract(CreatedAt);
                if (timeSince.TotalMilliseconds < 1)
                    return "not yet";
                if (timeSince.TotalMinutes < 1)
                    return "just now";
                if (timeSince.TotalMinutes < 2)
                    return "1 minute ago";
                if (timeSince.TotalMinutes < 60)
                    return string.Format("{0} minutes ago", timeSince.Minutes);
                if (timeSince.TotalMinutes < 120)
                    return "1 hour ago";
                if (timeSince.TotalHours < 24)
                    return string.Format("{0} hours ago", timeSince.Hours);
                if (timeSince.TotalDays == 1)
                    return "1 day ago";
                if (timeSince.TotalDays < 7)
                    return string.Format("{0} days ago", timeSince.Days);
                if (timeSince.TotalDays < 14)
                    return "last week";
                if (timeSince.TotalDays < 21)
                    return "2 weeks ago";
                if (timeSince.TotalDays < 28)
                    return "3 weeks ago";
                if (timeSince.TotalDays < 60)
                    return "last month";
                if (timeSince.TotalDays < 365)
                    return string.Format("{0} months ago", Math.Round(timeSince.TotalDays / 30));
                if (timeSince.TotalDays < 730)
                    return "last year";
                return string.Format("{0} years ago", Math.Round(timeSince.TotalDays / 365));
            }
        }
    }
}