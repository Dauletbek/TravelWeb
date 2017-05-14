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
    public class GuidersController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: Guiders
        public ActionResult Index()
        {
            return View(db.Guides.ToList());
        }

        // GET: Guiders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guider guider = db.Guides.Find(id);
            if (guider == null)
            {
                return HttpNotFound();
            }
            return View(guider);
        }

        // GET: Guiders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guiders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,firstName,lastName,workedYear,img")] Guider guider)
        {
            if (ModelState.IsValid)
            {
                db.Guides.Add(guider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guider);
        }

        // GET: Guiders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guider guider = db.Guides.Find(id);
            if (guider == null)
            {
                return HttpNotFound();
            }
            return View(guider);
        }

        // POST: Guiders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,firstName,lastName,workedYear,img")] Guider guider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guider);
        }

        // GET: Guiders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guider guider = db.Guides.Find(id);
            if (guider == null)
            {
                return HttpNotFound();
            }
            return View(guider);
        }

        // POST: Guiders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guider guider = db.Guides.Find(id);
            db.Guides.Remove(guider);
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
