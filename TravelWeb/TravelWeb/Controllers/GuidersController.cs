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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guider guider)
        {
            if (ModelState.IsValid)
            {
                if (!(guider.File.ContentType == "image/jpeg" || guider.File.ContentType == "image/gif" || guider.File.ContentType == "image/png"))
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

                    var fileName = Path.GetFileName(guider.File.FileName);
                    var path = Path.Combine(Server.MapPath(subPath), fileName);
                    guider.File.SaveAs(path);
                    var pathName = subPath + "/" + Path.GetFileName(guider.File.FileName);
                    guider.img = pathName;
                    db.Guides.Add(guider);
                    db.SaveChanges();
                }

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
