using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finacial_Portal.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Target { get; set; }

        public int HouseholdId { get; set; }

        //chidren
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }//---
                                                                        //  |  
        //parents                                                       //  | 
        public virtual Household Household { get; set; }                //  |
                                                                        //  | 
        public Budget()                                                 //  |
        {                                                               //  |
            BudgetItems = new HashSet<BudgetItem>(); //<---------------------
        }
    }
}