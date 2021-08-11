using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Models
{
    public class ReviewBlog:BaseEntity
    {
        public int ReviewId { get; set; }
        public int BlogId { get; set; }

        public Review Review { get; set; }
        public Blog Blog { get; set; }
    }
}
