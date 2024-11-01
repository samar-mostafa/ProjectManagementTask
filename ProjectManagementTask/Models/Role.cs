using Microsoft.AspNetCore.Identity;
using ProjectManagement.Helpers;

namespace ProjectManagement.Models
{
    public class Role : IdentityRole<int>
    {
        public Role() : base() { }

        
        public Role(string roleName) : base(roleName)
        {
            UserRoles = new HashSet<IdentityUserRole<int>>();
        }
        
        public ICollection<IdentityUserRole<int>> UserRoles { get; set; }
    }
}
