using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Models
{
    public class ReviewProduct:BaseEntity
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }

        [Required]
        [StringLength(maximumLength: 500)]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
