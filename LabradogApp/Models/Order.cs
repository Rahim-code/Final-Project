using LabradogApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Models
{
    public class Order:BaseEntity
    {
        public int UserId { get; set; }
        public string Adress { get; set; }
        public string FullName { get; set; }
        public string ContactPhone { get; set; }
        public string Note { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

    }
}
