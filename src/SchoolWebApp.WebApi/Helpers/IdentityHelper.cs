using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Models.Identity;
using System;

namespace SchoolWebApp.WebApi.Helpers
{
    public class IdentityHelper
    {
        public static void ConfigureService(IServiceCollection service)
        {
            service.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SchoolDbContext>()
                .AddDefaultTokenProviders();

            service.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = false;

                // User settings
                options.User.RequireUniqueEmail = false;
            });
        }
    }
}
