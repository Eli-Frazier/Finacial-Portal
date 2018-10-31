using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Finacial_Portal.Models;
using Finacial_Portal.Helpers;
using Microsoft.AspNet.Identity;
using static Finacial_Portal.Models.Household;
using Finacial_Portal.ViewModels;
using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Finacial_Portal.Controllers
{
    [Authorize]
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private HouseHelper houseHelper = new HouseHelper();
        private UserHelper userHelper = new UserHelper();

        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Household household)
        {
            var user = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Households.Add(household);
                db.SaveChanges();

                if(userHelper.IsUserInRole(user, "Household Member"))
                {
                    userHelper.RemoveUserFromRole(user, "Household Member");
                }

                var currentHouse = UserHelper.GetHouseholdId();
                if (currentHouse != -1)
                {
                    houseHelper.RemoveUserFromHouse();
                }

                houseHelper.AddUserToHouse(user, household.Id);
                userHelper.AddUserToRole(User.Identity.GetUserId(), "Head of Household");

                return RedirectToAction("Details", "Households", new { id = household.Id });
            }

            return View(household);
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string AjaxLeave(string userId, string houseId)
        {
            ApplicationUser user = db.Users.Find(userId);
            Household household = db.Households.Find(Convert.ToInt32(houseId));

            user.HouseholdId = null;
            db.SaveChanges();

            if (household.Users.Count == 0)
                household.Condemned = true;

            db.SaveChanges();
            return "Success";
        }

        [Authorize(Roles ="Head of Household")]
        public ActionResult Invite(int id)
        {
            var invitation = new InviteVM
            {
                HouseholdId = id
            };

            return View(invitation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Invite(Invitation invite)
        {
            var newInvite = new Invitation
            {
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(3),
                HouseholdId = invite.HouseholdId,
                Email = invite.Email,
                Code = Guid.NewGuid()
            };

            try
            {
                var from = ConfigurationManager.AppSettings["emailfrom"];

                var callbackUrl = Url.Action("RegisterFromInvite", "Households", new { email = newInvite.Email, code = newInvite.Code }, protocol: Request.Url.Scheme);

                var email = new MailMessage(from, newInvite.Email)
                {
                    Subject = "Household Invite",
                    Body = "You have been invited to join a household on the Financial Portal-EF. please click <a href=\"" + callbackUrl + "\">here</a> to register an join the household",
                    IsBodyHtml = true
                };
                var svc = new PersonalEmail();
                await svc.SendAsync(email);

                db.Invitations.Add(newInvite);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }

            return RedirectToAction("Details", new { id = invite.HouseholdId });
        }

        public ActionResult RegisterFromInvite(string email, Guid code)
        {
            var accept = new AcceptVM
            {
                Email = email,
                Code = code
            };
            return View(accept);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
    }
}
