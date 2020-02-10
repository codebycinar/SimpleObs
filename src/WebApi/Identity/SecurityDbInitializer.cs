using Core.Data.Entity;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Identity
{
    public static class SecurityDbInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,SchoolDbContext schoolDb)
        {
            CreateRoles(roleManager);
            CreateLogins(userManager, schoolDb.Students.ToList());
        }

        #region Data Creation Helpers

        private static void CreateLogins(UserManager<IdentityUser> userManager, List<Student> students)
        {
            foreach (var student in students)
            {
                var studentUser = new IdentityUser
                {
                    Email = $"abc{student.Id.ToString()}@school.edu.tr",
                    EmailConfirmed = true,
                    NormalizedUserName = $"abc{student.Id.ToString()}",
                    PhoneNumber = "212 111 1111",
                    UserName = $"abc{student.Id.ToString()}",
                };

                if (userManager.FindByEmailAsync(studentUser.Email).Result == null)
                {
                    IdentityResult result = userManager.CreateAsync(studentUser, "123456").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(studentUser, "Student").Wait();
                    }
                }
            }
           
            if (userManager.FindByEmailAsync("admin@school.edu.tr").Result == null)
            {
                var adminUser = new IdentityUser
                {
                    Email = $"admin@school.edu.tr",
                    EmailConfirmed = true,
                    NormalizedUserName = $"admin",
                    PhoneNumber = "212 111 1111",
                    UserName = $"admin"
                };

                IdentityResult result = userManager.CreateAsync(adminUser, "123456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                }
            }
        }

        private static void CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Student";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }

        #endregion
    }
}