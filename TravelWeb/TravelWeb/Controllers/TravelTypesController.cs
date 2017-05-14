using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelWeb.DAL;
using TravelWeb.Models;

namespace TravelWeb.Controllers
{
    public class TravelTypesController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: TravelTypes
        public ActionResult Index()
        {
            return View(db.TravelTypes.ToList());
        }

        // GET: TravelTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelType travelType = db.TravelTypes.Find(id);
            if (travelType == null)
            {
                return HttpNotFound();
            }
            return View(travelType);
        }

        // GET: TravelTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TravelTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TravelTypeID,Name")] TravelType travelType)
        {
            if (ModelState.IsValid)
            {
                db.TravelTypes.Add(travelType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(travelType);
        }

        // GET: TravelTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelType travelType = db.TravelTypes.Find(id);
            if (travelType == null)
            {
                return HttpNotFound();
            }
            return View(travelType);
        }

        // POST: TravelTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TravelTypeID,Name")] TravelType travelType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travelType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(travelType);
        }

        // GET: TravelTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelType travelType = db.TravelTypes.Find(id);
            if (travelType == null)
            {
                return HttpNotFound();
            }
            return View(travelType);
        }

        // POST: TravelTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TravelType travelType = db.TravelTypes.Find(id);
            db.TravelTypes.Remove(travelType);
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
