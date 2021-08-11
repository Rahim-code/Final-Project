using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Models
{
    public class OrderProduct:BaseEntity
    {
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public int Count { get; set; }
        [StringLength(maximumLength: 100)]
        public string CategoryName { get; set; }
        [StringLength(maximumLength: 100)]
        public string AuthorName { get; set; }
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProducingPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
