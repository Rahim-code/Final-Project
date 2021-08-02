using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public DateTime Created_At { get; set; }
        public int UserId { get; set; }


    }
}
