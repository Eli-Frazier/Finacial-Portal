using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finacial_Portal.Models
{
    public class Transaction
    {
        //PK
        public int Id { get; set; }

        //FK
        public int AccountId { get; set; }
        public string EnteredById { get; set; }
        public int TransactionTypeId { get; set; }
        public int? BudgetItemId { get; set; }


        //Things this has
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool Reconciled { get; set; }
        public decimal? ReconAmount { get; set; }
        public bool Void { get; set; }

        //Parents
        public virtual Account Account { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
    }
}