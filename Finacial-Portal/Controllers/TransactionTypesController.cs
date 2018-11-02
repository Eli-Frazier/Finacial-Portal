using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Finacial_Portal.Models;

namespace Finacial_Portal.Controllers
{
    public class TransactionTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TransationTypes
        //public ActionResult Index()
        //{
        //    return View(db.transationTypes.ToList());
        //}

        //// GET: TransationTypes/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransationType transationType = db.transationTypes.Find(id);
        //    if (transationType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(transationType);
        //}

        // GET: TransationTypes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: TransationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TransactionType transactionType)
        {
            if (ModelState.IsValid)
            {
                db.TransactionTypes.Add(transactionType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transactionType);
        }

        // GET: TransationTypes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransationType transationType = db.transationTypes.Find(id);
        //    if (transationType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(transationType);
        //}

        // POST: TransationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TransactionType transactionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactionType);
        }

        // GET: TransationTypes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransationType transationType = db.transationTypes.Find(id);
        //    if (transationType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(transationType);
        //}

        // POST: TransationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionType transactionType = db.TransactionTypes.Find(id);
            db.TransactionTypes.Remove(transactionType);
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
