using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bangazon.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        public virtual List<Product> Products { get; set; }

        public virtual List<Order> Orders { get; set; }

        public virtual List<PaymentType> PaymentTypes { get; set; }

    }
}
