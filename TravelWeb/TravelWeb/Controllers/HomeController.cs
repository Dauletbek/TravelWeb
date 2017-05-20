using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelWeb.DAL;

namespace TravelWeb.Controllers
{
    public class HomeController : BaseController
    {
        private TravelContext db = new TravelContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Travels() {
            return View(db.Travels.ToList());
        }
        public ActionResult TravelDetail() {
            return View();
        }
        public ActionResult Step1() {
            return View();
        }
        public ActionResult Step2()
        {
            return View();
        }
        public ActionResult Step3()
        {
            return View();
        }
    }
}