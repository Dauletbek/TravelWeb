﻿using System;
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
    public class DaysController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: Days
        public ActionResult Index()
        {
            var days = db.Days.Include(d => d.travelPlan);
            return View(days.ToList());
        }

        // GET: Days/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // GET: Days/Create
        public ActionResult Create()
        {
            ViewBag.travelPlanID = new SelectList(db.Plans, "ID", "detail");
            return View();
        }

        // POST: Days/Create
        
        [HttpPost]
        public ActionResult Create(Day dayy)
        {
            if (ModelState.IsValid)
            {
                db.Days.Add(dayy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.travelPlanID = new SelectList(db.Plans, "ID", "detail", dayy.travelPlanID);
            return View(dayy);
        }

        // GET: Days/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            ViewBag.travelPlanID = new SelectList(db.Plans, "ID", "detail", day.travelPlanID);
            return View(day);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dayID,day,detail,travelPlanID")] Day day)
        {
            if (ModelState.IsValid)
            {
                db.Entry(day).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.travelPlanID = new SelectList(db.Plans, "ID", "detail", day.travelPlanID);
            return View(day);
        }

        // GET: Days/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Day day = db.Days.Find(id);
            db.Days.Remove(day);
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
