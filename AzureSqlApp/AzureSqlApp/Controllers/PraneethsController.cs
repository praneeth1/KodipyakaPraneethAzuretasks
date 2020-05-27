using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AzureSqlApp.Models;

namespace AzureSqlApp.Controllers
{
    public class PraneethsController : Controller
    {
        private freeazuresqldbEntities1 db = new freeazuresqldbEntities1();

        // GET: Praneeths
        public ActionResult Index()
        {
            return View(db.Praneeths.ToList());
        }

        // GET: Praneeths/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Praneeth praneeth = db.Praneeths.Find(id);
            if (praneeth == null)
            {
                return HttpNotFound();
            }
            return View(praneeth);
        }

        // GET: Praneeths/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Praneeths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,email_id,address")] Praneeth praneeth)
        {
            if (ModelState.IsValid)
            {
                db.Praneeths.Add(praneeth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(praneeth);
        }

        // GET: Praneeths/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Praneeth praneeth = db.Praneeths.Find(id);
            if (praneeth == null)
            {
                return HttpNotFound();
            }
            return View(praneeth);
        }

        // POST: Praneeths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,email_id,address")] Praneeth praneeth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(praneeth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(praneeth);
        }

        // GET: Praneeths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Praneeth praneeth = db.Praneeths.Find(id);
            if (praneeth == null)
            {
                return HttpNotFound();
            }
            return View(praneeth);
        }

        // POST: Praneeths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Praneeth praneeth = db.Praneeths.Find(id);
            db.Praneeths.Remove(praneeth);
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
