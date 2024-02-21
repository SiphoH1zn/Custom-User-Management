using Custom_User_Management.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Custom_User_Management.Data
{
    public class ContextSeed
    {
        public class CustomRoles
        {
            public enum Roles
            {
                SuperAdmin,
                Admin,
                //Moderator,
                //Basic
            }
        }

        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            foreach (var role in System.Enum.GetValues(typeof(CustomRoles.Roles)))
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                }
            }
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "Admin@GambuseOmkhulu.co.za",
                FirstName = "Mukesh",
                LastName = "Murugan",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    var result = await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    if (result.Succeeded)
                    {
                        //await userManager.AddToRoleAsync(defaultUser, CustomRoles.Roles.Basic.ToString());
                        //await userManager.AddToRoleAsync(defaultUser, CustomRoles.Roles.Moderator.ToString());
                        await userManager.AddToRoleAsync(defaultUser, CustomRoles.Roles.Admin.ToString());
                        await userManager.AddToRoleAsync(defaultUser, CustomRoles.Roles.SuperAdmin.ToString());
                    }
                }
            }
        }
    }
}

