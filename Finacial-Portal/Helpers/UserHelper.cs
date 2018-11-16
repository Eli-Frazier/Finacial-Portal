using Finacial_Portal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finacial_Portal.Helpers
{
    public class UserHelper
    {
        private static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private static ApplicationDbContext db = new ApplicationDbContext();

        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }

        public static ICollection<string> ListUserRoles(string userId)
        {
            return userManager.GetRoles(userId).ToList();
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var result = userManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = userManager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }

        public ICollection<ApplicationUser> UsersInRole(string roleName)
        {
            var resultList = new List<ApplicationUser>(); var List = userManager.Users.ToList(); foreach (var user in List) { if (IsUserInRole(user.Id, roleName)) resultList.Add(user); }

            return resultList;
        }

        public ICollection<ApplicationUser> UsersNotInRole(string roleName)
        {
            var resultList = new List<ApplicationUser>();
            var List = userManager.Users.ToList();
            foreach (var user in List)
            {
                if (!IsUserInRole(user.Id, roleName))
                    resultList.Add(user);
            }

            return resultList;
        }

       public static string GetAvatarPath(string id)
        {
            var userId = db.Users.Find(id).Id; /*HttpContext.Current.User.Identity.GetUserId();*/
            var avatarPath = db.Users.Find(userId).AvatarPath;
            if (userId == null || string.IsNullOrEmpty(avatarPath))    
                return "/images/User.png";
            return avatarPath;
        }

        public static int GetHouseholdId()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (userId == null)
                return -1;

            var houseId = db.Users.Find(userId).HouseholdId;
            return houseId ?? -1;     
        }

        public ICollection<ApplicationUser> UsersNotInHouse()
        {
            var resultList = new List<ApplicationUser>();
            var List = userManager.Users.ToList();
            foreach (var user in List)
            {
                if (user.HouseholdId == null)
                    resultList.Add(user);
            }

            return resultList;
        }
    }
}