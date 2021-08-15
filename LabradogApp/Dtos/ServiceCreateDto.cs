using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Dtos
{
    public class ServiceCreateDto
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string Main { get; set; }
    }
}
