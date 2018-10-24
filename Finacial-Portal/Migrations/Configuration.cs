namespace Finacial_Portal.Migrations
{
    using Finacial_Portal.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Finacial_Portal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Finacial_Portal.Models.ApplicationDbContext";
        }

        protected override void Seed(Finacial_Portal.Models.ApplicationDbContext context)
        {
            //Create Roles
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }



            //seed a household
            context.Households.AddOrUpdate(p => p.Name,
                new Household { Id = 100, Name = "The Frazier Household" }
            );


            //Create Users
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "eli_frazier@yahoo.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    HouseholdId = 100,
                    UserName = "eli_frazier@yahoo.com",
                    Email = "eli_frazier@yahoo.com",
                    FirstName = "Eli",
                    LastName = "Frazier",
                }, "LavabombAdmin1");
            }


            //Put users in Roles
            var adminId = userManager.FindByEmail("eli_frazier@yahoo.com").Id;
            userManager.AddToRole(adminId, "Admin");

           
        }
    }
}
