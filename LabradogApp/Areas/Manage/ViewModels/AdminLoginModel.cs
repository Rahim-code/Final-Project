using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabradogApp.Areas.Manage.ViewModels
{
    public class AdminLoginModel
    {
        [StringLength(maximumLength: 25)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 25)]
        public string Password { get; set; }
    }
}
