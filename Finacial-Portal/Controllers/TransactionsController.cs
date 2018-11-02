using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Finacial_Portal.Helpers;
using Finacial_Portal.Models;
using Microsoft.AspNet.Identity;

namespace Finacial_Portal.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Account);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var houseId = db.Users.Find(userId).HouseholdId;
            ViewBag.AccountId = new SelectList(db.Accounts.Where(u => u.HouseholdId == houseId), "Id", "Name");
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Name");
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems.Where(d => d.Deleted == false), "Id", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccountId,Description,Amount,Type,TransactionTypeId,BudgetItemId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Date = DateTime.Now;
                transaction.EnteredById = User.Identity.GetUserId();
                
                db.Transactions.Add(transaction);
                db.SaveChanges();

                TransactionHelper.AdjustAccountBalance(transaction.Id);

                var balance = db.Accounts.Find(transaction.AccountId).Balance;
                if (balance < 0)
                    TempData["overdraft"] = "true";
                else
                    TempData["overdraft"] = "false";

                return RedirectToAction("Details", "Households", new { id = db.Accounts.Find(transaction.AccountId).HouseholdId });
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", transaction.AccountId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", transaction.AccountId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AccountId,EnteredById,Description,Date,Amount,Type,Reconciled,ReconAmount")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", transaction.AccountId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string Void(int id)
        {
            TransactionHelper.VoidATransaction(id);

            return "Success";
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
