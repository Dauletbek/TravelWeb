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
    public class TravelsController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: Travels
        public ActionResult Index()
        {
            var travels = db.Travels.Include(t => t.guider).Include(t => t.TravelType);
            return View(travels.ToList());
        }

        // GET: Travels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travel travel = db.Travels.Find(id);
            if (travel == null)
            {
                return HttpNotFound();
            }
            return View(travel);
        }

        // GET: Travels/Create
        public ActionResult Create()
        {
            ViewBag.guiderID = new SelectList(db.Guides, "ID", "firstName");
            ViewBag.TravelTypeID = new SelectList(db.TravelTypes, "TravelTypeID", "Name");
            return View();
        }

        // POST: Travels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,StartFrom,EndTo,TravelTypeID,guiderID")] Travel travel)
        {
            if (ModelState.IsValid)
            {
                db.Travels.Add(travel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.guiderID = new SelectList(db.Guides, "ID", "firstName", travel.guiderID);
            ViewBag.TravelTypeID = new SelectList(db.TravelTypes, "TravelTypeID", "Name", travel.TravelTypeID);
            return View(travel);
        }

        // GET: Travels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travel travel = db.Travels.Find(id);
            if (travel == null)
            {
                return HttpNotFound();
            }
            ViewBag.guiderID = new SelectList(db.Guides, "ID", "firstName", travel.guiderID);
            ViewBag.TravelTypeID = new SelectList(db.TravelTypes, "TravelTypeID", "Name", travel.TravelTypeID);
            return View(travel);
        }

        // POST: Travels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,StartFrom,EndTo,TravelTypeID,guiderID")] Travel travel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.guiderID = new SelectList(db.Guides, "ID", "firstName", travel.guiderID);
            ViewBag.TravelTypeID = new SelectList(db.TravelTypes, "TravelTypeID", "Name", travel.TravelTypeID);
            return View(travel);
        }

        // GET: Travels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travel travel = db.Travels.Find(id);
            if (travel == null)
            {
                return HttpNotFound();
            }
            return View(travel);
        }

        // POST: Travels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Travel travel = db.Travels.Find(id);
            db.Travels.Remove(travel);
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
