using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Dtos
{
    public class AddBlogDto
    {
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public DateTime Created_At { get; set; }
    }
}
