using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Coin.Web.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Customer Customer { get; set; }
        public Vendor Vendor { get; set; }
        public Administrator Administrator { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}