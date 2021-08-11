using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Helpers
{
    public class FileManager
    {
        public static string Save(string rootPath, string folder, IFormFile file)
        {
            var filename = Guid.NewGuid().ToString() + file.FileName;
            var path = Path.Combine(rootPath, "uploads", filename);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return filename;
        }

        public static bool Delete(string rootPath, string folder, string filename)
        {
            var path = Path.Combine(rootPath, "uploads", filename);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }

            return false;
        }
    }
}