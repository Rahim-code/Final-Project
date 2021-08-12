using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.ViewModels
{
    public class BasketItemViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
