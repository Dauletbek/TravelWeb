using System.Linq;
using System.Web.Mvc;
using TravelWeb.DAL;
using System.Net;
using TravelWeb.Models;

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
            return View(db.Plans.ToList());
        }

        public ActionResult TravelDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelPlan detail = db.Plans.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }

            return View(detail);
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