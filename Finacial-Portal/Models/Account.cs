using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finacial_Portal.Models
{
    public class Account
    {
        public int Id { get; set; }

        public int HouseholdId { get; set; }

        public string Name { get; set; }
        public decimal Balance { get; set; }
        public int? ReconBalance { get; set; } 

        //children
        public virtual ICollection<Transaction> Transactions { get; set; }

        //parents
        public virtual Household Household { get; set; }

        
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}