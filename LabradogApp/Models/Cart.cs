using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }


    }
}
