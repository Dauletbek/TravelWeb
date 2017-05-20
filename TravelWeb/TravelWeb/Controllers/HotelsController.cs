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
using System.IO;

namespace TravelWeb.Controllers
{
    public class HotelsController : Controller
    {
        private TravelContext db = new TravelContext();

        // GET: Hotels
        public ActionResult Index()
        {
            var hotels = db.Hotels.Include(h => h.travel);
            return View(hotels.ToList());
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        public ActionResult Create()
        {
            ViewBag.TravelID = new SelectList(db.Travels, "ID", "Name");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
            if (!(hotel.File.ContentType == "image/jpeg" || hotel.File.ContentType == "image/gif" || hotel.File.ContentType == "image/png"))
            {
                ModelState.AddModelError("CustomError", "File type allowed : jpeg and gif");
                RedirectToAction("Index");
            }
            else
            {
                string subPath = "~/UserImages";
                bool exists = Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    Directory.CreateDirectory(Server.MapPath(subPath));

                var fileName = Path.GetFileName(hotel.File.FileName);
                var path = Path.Combine(Server.MapPath(subPath), fileName);
                hotel.File.SaveAs(path);
                var pathName = subPath + "/" + Path.GetFileName(hotel.File.FileName);
                hotel.imagesrc = pathName;
                    db.Hotels.Add(hotel);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.TravelID = new SelectList(db.Travels, "ID", "Name", hotel.TravelID);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.TravelID = new SelectList(db.Travels, "ID", "Name", hotel.TravelID);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hotelID,name,detail,room,maxPerson,TravelID")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TravelID = new SelectList(db.Travels, "ID", "Name", hotel.TravelID);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
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
