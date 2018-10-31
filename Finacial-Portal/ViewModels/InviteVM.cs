using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finacial_Portal.ViewModels
{
    public class InviteVM
    {
        
        public int HouseholdId { get; set; }
        public bool Accepted { get; set; }
        public Guid Code { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Expires { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}