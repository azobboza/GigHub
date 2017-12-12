using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace GigHub.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<ApplicationUser> Followers { get; set; }
        public ICollection<ApplicationUser> Followees { get; set; }

        public ApplicationUser()
        {
            Followers = new Collection<ApplicationUser>();
            Followees = new Collection<ApplicationUser>();
        }
    }
}