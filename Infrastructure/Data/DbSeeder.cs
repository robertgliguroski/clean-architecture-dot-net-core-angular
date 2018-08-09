using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class DbSeeder
    {

        public static void Seed(AppDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (!dbContext.Users.Any())
            {
                CreateUsers(dbContext, roleManager, userManager).GetAwaiter().GetResult();
            }
        }

        private static async Task CreateUsers(AppDbContext dbContext, RoleManager<IdentityRole> roleManager,
UserManager<ApplicationUser> userManager)
        {
            DateTime createdDate = new DateTime(2018, 07, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;

            string role_Administrator = "Administrator";

            string role_RegisteredUser = "RegisteredUser";
            if (!await roleManager.RoleExistsAsync(role_Administrator))
            {
                await roleManager.CreateAsync(new
                IdentityRole(role_Administrator));
            }
            if (!await roleManager.RoleExistsAsync(role_RegisteredUser))
            {
                await roleManager.CreateAsync(new
                IdentityRole(role_RegisteredUser));
            }

            var user_Admin = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "robert.gliguroski@gmail.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            if (await userManager.FindByNameAsync(user_Admin.UserName) == null)
            {
                await userManager.CreateAsync(user_Admin, "Pass4Admin");
                await userManager.AddToRoleAsync(user_Admin,
                role_RegisteredUser);
                await userManager.AddToRoleAsync(user_Admin,
                role_Administrator);
                // Remove Lockout and E-Mail confirmation.
                user_Admin.EmailConfirmed = true;
                user_Admin.LockoutEnabled = false;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
