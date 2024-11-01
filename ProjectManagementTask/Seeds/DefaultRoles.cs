using Microsoft.AspNetCore.Identity;
using ProjectManagement.Helpers;


namespace ProjectManagement.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if(! roleManager.Roles.Any())
            {
               await roleManager.CreateAsync(new IdentityRole(AppRoles.Employee));
               await roleManager.CreateAsync(new IdentityRole(AppRoles.Manager));
               
            }
        }
    }
}
