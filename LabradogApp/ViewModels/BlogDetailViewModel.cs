using LabradogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.ViewModels
{
    public class BlogDetailViewModel
    {
        public Blog Blog { get; set; }
        public Setting Setting { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Category> Categories { get; set; }
    }
}
