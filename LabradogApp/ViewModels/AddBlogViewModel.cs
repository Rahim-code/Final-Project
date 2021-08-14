using LabradogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.ViewModels
{
    public class AddBlogViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public Blog Blog { get; set; }
    }
}
