using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Models
{
    public class Team:BaseEntity
    {
        public string FullName { get; set; }
        public string Job { get; set; }
        public string Image { get; set; }
        public string Info { get; set; }
        public string FbLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
