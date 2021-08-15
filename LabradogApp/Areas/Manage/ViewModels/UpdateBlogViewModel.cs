using LabradogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Areas.Manage.ViewModels
{
    public class UpdateBlogViewModel
    {
        public List<Category> Categories { get; set; }
        public Blog Blog { get; set; }
    }
}
