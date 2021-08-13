using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int CategoryId { get; set; }
        public string FeedClass { get; set; }
        public string CountryOfOrigin { get; set; }
        public string BreedSize { get; set; }
        public string TypeOfMeat { get; set; }
        public string PetsAge { get; set; }
        public string SpecialParameters { get; set; }
        public string DeliveryAndPaymentInfo { get; set; }

        public Category Category { get; set; }
        public List<ReviewProduct> ReviewProducts { get; set; }

    }
}
