using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Models
{
    public class Service:BaseEntity
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Main { get; set; }
    }
}
