using Microsoft.AspNetCore.Identity;
using ProjectManagement.Helpers;
using ProjectManagement.Models;
using Task = System.Threading.Tasks.Task;


namespace ProjectManagement.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<Role> roleManager)
        {
            if(! roleManager.Roles.Any())
            {
               await roleManager.CreateAsync(new Role(AppRoles.Employee));
               await roleManager.CreateAsync(new Role(AppRoles.Manager));
               
            }
        }
    }
}
