using LabradogApp.Dtos;
using LabradogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.ViewModels
{
    public class HomeViewModel
    {
        public Setting Setting { get; set; }
        public List<Fag> Fags { get; set; }
        public List<DidYouNow> DidYouNows { get; set; }
        public List<Service> Services { get; set; }
        public List<Product> Products { get; set; }
        public List<Team> Teams { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
