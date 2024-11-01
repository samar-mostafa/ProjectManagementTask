using Microsoft.AspNetCore.Identity;

namespace ProjectManagement.Models
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            UserRoles = new HashSet<IdentityUserRole<int>>();
        }
        public ICollection<IdentityUserRole<int>> UserRoles { get; set; }
    }
}
