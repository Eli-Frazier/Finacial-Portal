using Finacial_Portal.Models;
using Finacial_Portal.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finacial_Portal.Controllers
{
    public class MorrisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult GetBudgetDataForBarChart()
        {
            var budgetData = new List<MorrisBudgetBar>();

            var userId = User.Identity.GetUserId();

            var houseId = db.Users.Find(userId).HouseholdId;
            if (houseId == null)
                return Content(JsonConvert.SerializeObject(budgetData), "application/json");

            foreach (var budget in db.Households.Find(houseId).Budgets.ToList())
            {
                budgetData.Add(new MorrisBudgetBar
                {
                    Label = budget.Name,
                    Target = budget.Target,
                    Actual = budget.CurrentBalance
                });
            }

            return Content(JsonConvert.SerializeObject(budgetData), "application/json");
        }
    }
}