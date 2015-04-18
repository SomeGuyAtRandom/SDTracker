using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BusinessLayer.Models
{
    [MetadataType(typeof(UserDetailConfig))]
    public class UserDetail
    {
        
        public UserDetail() 
        {
            // Start out with empty constructor
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateAccessed { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Initials { get; set; }
        public string Phone { get; set; }

    }

    public class UserDetailConfig
    {
        [StringLength(5, MinimumLength = 1)]
        [Required, Remote("InitialsAlreadyExists", "User", AdditionalFields = "Id")]
        [RegularExpression(@"[^a-zA-Z]", ErrorMessage = "Use letters A-Z or a-z")]
        [Display(Name = "Initials")]
        public string Initials { get; set; }


        [RegularExpression(@"^\(\d{3}\)\s\d{3}-\d{4}", ErrorMessage = "Use format (###) ###-####")]
        [Required]
        [Display(Name = "Phone#")]
        public string Phone { get; set; }

        [RegularExpression(@"[^a-zA-Z]", ErrorMessage = "Use letters A-Z or a-z")]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }


        [RegularExpression(@"[^a-zA-Z]", ErrorMessage = "Use letters A-Z or a-z")]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string LastName { get; set; }

        [RegularExpression(@"^\(\d{3}\)\s\d{3}-\d{4}", ErrorMessage = "Use valid email.")]
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }


    }

}
