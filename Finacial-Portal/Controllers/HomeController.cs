using Finacial_Portal.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finacial_Portal.Controllers
{
    public class HomeController : Controller
    {
        UserHelper userHelper = new UserHelper();
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var hhId = UserHelper.GetHouseholdId();

            switch (hhId)
            {
                case -1:
                    return RedirectToAction("Lobby", "Home");
                default:
                    break;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Lobby()
        {
            return View();
        }
    }
}