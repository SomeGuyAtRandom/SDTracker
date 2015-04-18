//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace BusinessLayer.Models
{
    [MetadataType(typeof(RegisterUserConfig))]
    public class RegisterUser
    {
        
        public RegisterUser()
        {
 
        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


    }
    class RegisterUserConfig
    {
        [Remote("ValidateUserAtServer", "Home")]
        [Required(ErrorMessage = "User name is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [Remote("EmailAlreadyExists", "Home")]
        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email Address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "The {0} field is required!")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        

        [DisplayName("Confirm  Email")]
        [Display(Name = "Confirm Email")]
        [Compare("Email", ErrorMessage = "The Email and confirmation Email do not match.")]
        public string ConfirmEmail { get; set; }


    }
}
