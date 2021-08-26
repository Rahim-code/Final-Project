using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Dtos
{
    public class GalleryImageCreateDto
    {
        public IFormFile DogImage { get; set; }
        public string DogName { get; set; }
        public int Old { get; set; }
        public bool IsMale { get; set; }
    }
}
