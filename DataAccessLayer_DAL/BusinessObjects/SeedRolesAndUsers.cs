using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer_DAL
{
    public class SeedRolesAndUsers
    {

        public static void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            // Add roles in database
            if (!roleManager.RoleExists("admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("admin"));
            }

            if (!roleManager.RoleExists("user"))
            {
                var roleresult = roleManager.Create(new IdentityRole("user"));
            }

            // Add new user(admin) to database

            string userEmail = "admin@admin.com";
            string userPassword = "Admin!123";
            string firstName = "John";
            string lastName = "Smith";
            string dealerName = "GT Line";
            ApplicationUser user = userManager.FindByEmail(userEmail);
            if(user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = userEmail,
                    FirstName = firstName,
                    LastName = lastName,
                    DealerName = dealerName,
                    Email = userEmail,
                    isActive = true
                };

                IdentityResult userResult = userManager.Create(user, userPassword);
                if (userResult.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "admin");
                }
            }
        }
    }
}