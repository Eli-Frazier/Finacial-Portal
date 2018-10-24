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

        //Things this has
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public bool Reconciled { get; set; }
        public int ReconAmount { get; set; }

        //Parents
        public virtual Account Account { get; set; }
        public virtual ApplicationUser EnteredByUser { get; set; }
    }
}