using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Storefront.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your user name")]
        [StringLength(20)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your e-mail address")]
        [StringLength(100)]
        [Display(Name = "E-mail address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must be the same")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="Country [optional]")]
        [StringLength(100)]
        public string Country { get; set; }

        [Display(Name = "City [optional]")]
        [StringLength(100)]
        public string City { get; set; }
    }
}