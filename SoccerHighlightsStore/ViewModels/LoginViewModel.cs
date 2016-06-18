using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Storefront.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your e-mail address")]
        [Display(Name = "E-mail address")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email address cannot be longer that 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(20, ErrorMessage = "Passsword cannot be longer that 20 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}