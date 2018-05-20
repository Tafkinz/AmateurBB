using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Model
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(128)]
        [MinLength(3)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(128)]
        [MinLength(3)]
        [Required]
        public string LastName { get; set; }

        [Required]
        [DefaultValue(1)]
        public long PersonTypeId { get; set; }
        public PersonType PersonType { get; set; }

        public List<Contact> Contacts { get; set; } = new List<Contact>();

        public string DisplayName => $"{FirstName} {LastName}";

        public List<TeamPerson> TeamPersons = new List<TeamPerson>();
    }
}
