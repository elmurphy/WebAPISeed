using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class SeedDataBase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //context.Database.EnsureCreated();
            //if (context.Languages.FirstOrDefault(x => x.Name == "Türkçe") == null)
            //{
            //    context.Languages.AddAsync(new Models.DefaultModels.Language
            //    {
            //        Id = Guid.NewGuid(),
            //        isActive = true,
            //        isDeleted = false,
            //        CreatedOn = DateTime.Now,
            //        ModifiedOn = DateTime.Now,
            //        Name = "Türkçe",
            //        ShortName = "tr",
            //        Order = 1
            //    });
            //}

            //context.Database.Migrate();

            if (roleManager.RoleExistsAsync("Developer").Result == false)
            {
                IdentityRole roleDeveloper = new IdentityRole()
                {
                    Name = "Developer",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                };
                roleManager.CreateAsync(roleDeveloper).Wait();

                IdentityRole roleDeveloper2 = new IdentityRole()
                {
                    Name = "Manager",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                };
                roleManager.CreateAsync(roleDeveloper2);
            }

            if (userManager.FindByEmailAsync("info@ugurcanbulat.com").Result == null)
            {
                //Developer account
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "info@ugurcanbulat.com",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "ElMurphy",
                };
                userManager.CreateAsync(user, "YourPasswordEditHere").Wait();
                if (roleManager.RoleExistsAsync("Developer").Result && userManager.FindByEmailAsync("info@ugurcanbulat.com").Result != null)
                {
                    userManager.AddToRoleAsync(user, "Developer");
                }
            }
        }
    }
}
