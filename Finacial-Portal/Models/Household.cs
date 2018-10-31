using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finacial_Portal.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Condemned { get; set; }

        //chidren
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

        public Household()
        {
            Budgets = new HashSet<Budget>();
            Accounts = new HashSet<Account>();
            Users = new HashSet<ApplicationUser>();
            UserNotifications = new HashSet<UserNotification>();
            Invitations = new HashSet<Invitation>();
        }

       

    }
}