using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Helpers
{
    public class FileManager
    {
        public static string Save(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var filename = Guid.NewGuid() + extension;
            var location = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/uploads/",filename);
            var stream = new FileStream(location, FileMode.Create);
            file.CopyTo(stream);

            return filename;
        }

        public static bool Delete(string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/", filename);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }
    }
}