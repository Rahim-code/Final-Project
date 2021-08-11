using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.ViewModels
{
    public class RegisterViewModel
    {
        [StringLength(maximumLength: 30), Required]
        public string FullName { get; set; }

        [StringLength(maximumLength: 20), Required]
        public string UserName { get; set; }

        [StringLength(maximumLength: 20), Required]
        public string Email { get; set; }

        [StringLength(maximumLength: 50), Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(maximumLength: 50), Required]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmedPassword { get; set; }
    }
}