using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.ViewModels
{
    public class BasketViewModel
    {
        public List<ProductBasketItem> ProductBasketItems { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class ProductBasketItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
