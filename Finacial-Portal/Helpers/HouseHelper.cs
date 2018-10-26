using Finacial_Portal.Models;
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
            Household house = db.Households.Find(HouseholdId);
            var newUser = db.Users.Find(userId);

            house.Users.Add(newUser);
            db.SaveChanges();
        }
        
        public ICollection<Household> ListUserHouseholds(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            var households = db.Households.Where(u => u.Id == db.Users.Find(user).HouseholdId).ToList();
            return households;
        }

        public bool RemoveUserFromHouse(string userId, int HouseholdId)
        {
            var result = db.Users.Find(userId).HouseholdId.Equals(null);
            return result;
        }
    }
}