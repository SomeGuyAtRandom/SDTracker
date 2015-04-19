//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
// using System.ComponentModel.DataAnnotations.ValidationAttribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
//using System.Web.DynamicData; ?

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


        [Remote("ValidateEmailAtServer", "Home")]
        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email Address")]
        public string Email { get; set; }


        [Remote("ValidatePasswordAtServer", "Home", AdditionalFields = "Password")]
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        //[Remote("ValidateConfirmPasswordAtServer", "Home", AdditionalFields = "Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        // Replaces a ValidateConfirmPasswordAtServer remote call
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        

        //[Remote("ValidateConfirmEmailAtServer", "Home", AdditionalFields = "Email")]
        [DisplayName("Confirm  Email")]
        [Display(Name = "Confirm Email")]
        // Replaces a ValidateConfirmEmailAtServer remote call
        [Compare("Email", ErrorMessage = "The Email and confirmation Email do not match.")]
        public string ConfirmEmail { get; set; }


    }
}
