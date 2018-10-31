using Finacial_Portal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finacial_Portal.Helpers
{
    public class HouseHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void AddUserToHouse(string userId, int HouseholdId)
        {
            var newUser = db.Users.Find(userId);

            db.Users.Attach(newUser);
            newUser.HouseholdId = HouseholdId;
            
            db.SaveChanges();
        }
        
        public ICollection<Household> ListUserHouseholds(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            var households = db.Households.Where(u => u.Id == db.Users.Find(user).HouseholdId).ToList();
            return households;
        }

        public bool RemoveUserFromHouse()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (userId == null)
                return true;

            var user = db.Users.Find(userId);
            db.Users.Attach(user);
            user.HouseholdId = null;
            db.SaveChanges();
            return true;
        }
    }
}