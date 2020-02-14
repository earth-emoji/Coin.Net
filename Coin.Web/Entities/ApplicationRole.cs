using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Coin.Web.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}