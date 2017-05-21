using System.Linq;
using System.Web.Mvc;
using TravelWeb.DAL;
using System.Net;
using TravelWeb.Models;
using System.Web.Security;
using System;
using System.Web;

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

        [HttpPost]
        public ActionResult Comment() {
            Comment comment = new Comment();

            string commentMessage = Request.Form["comment"];
            string travelPlanID = Request.Form["travelId"]; 
                string travellID = Request.Form["travelPlanId"];
            comment.createdDate = DateTime.Now;
            comment.detail = commentMessage;
            //Travel tra= db.Travels.Find(travellID);
            //comment.travel = tra;
            comment.TravelID = Convert.ToInt32(travellID);
            User user = db.Users.Where(o => o.email == HttpContext.User.Identity.Name).ToList()[0];
           //User user=(User) Session["loginstate"];
            comment.user = user;
            comment.userID = user.userID;
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("TravelDetail", new { id = travelPlanID });
        }

        [HttpPost]
        public ActionResult Login() {
            string name = Request.Form["email"];
            string password = Request.Form["password"];
            string remember = Request.Form["remember_me"];

            var useer = db.Users.Where(o => o.email == name);
            useer=useer.Where(o => o.password == password);
            if (useer.ToList().Count!=0) {
                FormsAuthentication.SetAuthCookie(useer.First().email, false);

                var authTicket = new FormsAuthenticationTicket(1, useer.First().email, DateTime.Now, DateTime.Now.AddMinutes(20), false, useer.First().role.name);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
            }
            return RedirectToAction("index");
        }
        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index");
        }
        public ActionResult Travels(int? id) {

            if (id != null)
            {
                return View(db.Plans.Where(o => o.travel.TravelType.TravelTypeID == id).ToList());
            }
            else {
                return View(db.Plans.ToList());
            }
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