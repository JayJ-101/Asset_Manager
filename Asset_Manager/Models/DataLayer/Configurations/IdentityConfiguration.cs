using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace Asset_Manager.Models  
{
    public class IdentityConfiguration
    {
        public static async Task CreatedAdminUserAsync(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<User>>();

            string username = "admin";
            string password = "Admin@12345";
            string roleName = "Admin";

            // Check if the role exists, if not create it
            if(await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            //if username doesnt exist, create it and add role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
