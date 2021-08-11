using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Models
{
    public class Review:BaseEntity
    {
        public int UserId { get; set; }
        public string Main { get; set; }
        public DateTime Created_At { get; set; }

        public User User { get; set; }
    }
}
