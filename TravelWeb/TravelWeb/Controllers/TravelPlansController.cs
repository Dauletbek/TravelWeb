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
    public class TravelPlansController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: TravelPlans
        public ActionResult Index()
        {
            return View(db.Plans.ToList());
        }

        // GET: TravelPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelPlan travelPlan = db.Plans.Find(id);
            if (travelPlan == null)
            {
                return HttpNotFound();
            }
            return View(travelPlan);
        }

        // GET: TravelPlans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TravelPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,price,startDate,endDate,detail,TravelID")] TravelPlan travelPlan)
        {
            if (ModelState.IsValid)
            {
                db.Plans.Add(travelPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(travelPlan);
        }

        // GET: TravelPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelPlan travelPlan = db.Plans.Find(id);
            if (travelPlan == null)
            {
                return HttpNotFound();
            }
            return View(travelPlan);
        }

        // POST: TravelPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,price,startDate,endDate,detail,TravelID")] TravelPlan travelPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travelPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(travelPlan);
        }

        // GET: TravelPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelPlan travelPlan = db.Plans.Find(id);
            if (travelPlan == null)
            {
                return HttpNotFound();
            }
            return View(travelPlan);
        }

        // POST: TravelPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TravelPlan travelPlan = db.Plans.Find(id);
            db.Plans.Remove(travelPlan);
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
