using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Finacial_Portal.Models;

namespace Finacial_Portal.Controllers
{
    public class UserNotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: UserNotifications
        //public ActionResult Index()
        //{
        //    var userNotifications = db.UserNotifications.Include(u => u.Household).Include(u => u.Recipient);
        //    return View(userNotifications.ToList());
        //}

        //// GET: UserNotifications/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserNotification userNotification = db.UserNotifications.Find(id);
        //    if (userNotification == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userNotification);
        //}

        //// GET: UserNotifications/Create
        //public ActionResult Create()
        //{
        //    ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
        //    ViewBag.RecipientId = new SelectList(db.ApplicationUsers, "Id", "FirstName");
        //    return View();
        //}

        // POST: UserNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Body,HouseholdId,RecipientId,Read")] UserNotification userNotification)
        {
            if (ModelState.IsValid)
            {
                db.UserNotifications.Add(userNotification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", userNotification.HouseholdId);
            //ViewBag.RecipientId = new SelectList(db.ApplicationUsers, "Id", "FirstName", userNotification.RecipientId);
            return View(userNotification);
        }

        // GET: UserNotifications/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserNotification userNotification = db.UserNotifications.Find(id);
        //    if (userNotification == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", userNotification.HouseholdId);
        //    ViewBag.RecipientId = new SelectList(db.ApplicationUsers, "Id", "FirstName", userNotification.RecipientId);
        //    return View(userNotification);
        //}

        // POST: UserNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,HouseholdId,RecipientId,Read")] UserNotification userNotification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userNotification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", userNotification.HouseholdId);
            //ViewBag.RecipientId = new SelectList(db.ApplicationUsers, "Id", "FirstName", userNotification.RecipientId);
            return View(userNotification);
        }

        // GET: UserNotifications/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserNotification userNotification = db.UserNotifications.Find(id);
        //    if (userNotification == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userNotification);
        //}

        // POST: UserNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserNotification userNotification = db.UserNotifications.Find(id);
            db.UserNotifications.Remove(userNotification);
            db.SaveChanges();
            return RedirectToAction("Index");
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
