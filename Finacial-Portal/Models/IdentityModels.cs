﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Finacial_Portal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarPath { get; set; }
        public int? HouseholdId { get; set; }

        public virtual Household Household { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }

        public ApplicationUser()
        {
            Transactions = new HashSet<Transaction>();
            UserNotifications = new HashSet<UserNotification>();
        }


            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<BudgetItem> BudgetItems { get; set; }

        public DbSet<Budget> Budgets { get; set; }

        public DbSet<Household> Households { get; set; }

        public DbSet<TransationType> transationTypes { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }

        public System.Data.Entity.DbSet<Finacial_Portal.Models.Invitation> Invitations { get; set; }
    }
}