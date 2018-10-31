using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finacial_Portal.Models
{
    public class UserNotification
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int HouseholdId { get; set; }
        public string RecipientId { get; set; }
        public bool Read { get; set; }

        public virtual Household Household { get; set; }
        public virtual ApplicationUser Recipient { get; set; }
    }
}