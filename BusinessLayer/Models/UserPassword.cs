using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BusinessLayer.Models
{
    [MetadataType(typeof(UserPasswordConfig))]
    public class UserPassword
    {

        public UserPassword() 
        {
            // Start out with empty constructor
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string OriginalPassword { get; set; }
        
        public string Email { get; set; }
        public bool RememberMe { get; set; }


        
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateAccessed { get; set; }
        
    }
    public class UserPasswordConfig
    {
        [Display(Name = "User Name")]
        [Required, Remote("UserNameAlreadyExists", "User", AdditionalFields = "Id")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Repeat Password")]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Last Login")]
        public DateTime DateAccessed { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
 
    }
}
