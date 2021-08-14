using LabradogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.ViewModels
{
    public class AdminPageViewModel
    {
        public List<Order> Orders { get; set; }
        public List<Team> Teams { get; set; }
    }
}
