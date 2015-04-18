using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BusinessLayer.Models
{
    public class Engineer
    {
        public Engineer() 
        {
            // Start out with empty constructor
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Initials { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public Boolean IsDisabled { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Role> AllRoles { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
    public class EngineerConfig
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

    }
}
