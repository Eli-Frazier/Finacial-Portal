using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finacial_Portal.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }

        public int BudgetId { get; set; }

        public int Amount { get; set; }

        //parents
        public virtual Budget Budget { get; set; }
    }
}