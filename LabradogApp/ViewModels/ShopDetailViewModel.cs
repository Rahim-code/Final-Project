using LabradogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.ViewModels
{
    public class ShopDetailViewModel
    {
        public Product Product { get; set; }
        public List<Product> LastProducts { get; set; }
    }
}
