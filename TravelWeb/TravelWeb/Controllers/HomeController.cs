using System.Linq;
using System.Web.Mvc;
using TravelWeb.DAL;
using System.Net;
using TravelWeb.Models;
using System.Web.Security;
using System;
using System.Web;
using System.Data.Entity;

namespace TravelWeb.Controllers
{
    public class HomeController : BaseController
    {
        private TravelContext db = new TravelContext();
        public ActionResult Index()
        {
            return View(db.Plans.ToList());
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
            string travelPlanID = Request.Form["travelPlanId"];
            string travellID = Request.Form["travelId"];
            comment.createdDate = DateTime.Now;
            comment.detail = commentMessage;
            comment.TravelID = Convert.ToInt32(travellID);
            User user = db.Users.Where(o => o.email == HttpContext.User.Identity.Name).ToList()[0];
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
        public ActionResult Step1(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            BigViewModel bigModel = new BigViewModel();
            bigModel.Hotel = hotel;
            if (Request.IsAuthenticated) {
                User user = db.Users.Where(o => o.email == HttpContext.User.Identity.Name).ToList()[0];
                bigModel.User = user;
            }
            return View(bigModel);
        }
        [HttpPost]
        public ActionResult Step1(BigViewModel model) {
            if (model.User.userID == 0) {
                Role role = db.Roles.ToList()[0];
                model.User.roleID = role.ID;
                FormsAuthentication.SignOut();
                db.Users.Add(model.User);
                db.SaveChanges();
                //Шинээр бүртгүүлсэн хэрэглэгчийг системд нэвтрүүлж байна
                FormsAuthentication.SetAuthCookie(model.User.email, false);

                var authTicket = new FormsAuthenticationTicket(1, model.User.email, DateTime.Now, DateTime.Now.AddMinutes(20), false, model.User.role.name);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
            }
            return RedirectToAction("Step2", new { id=model.Hotel.hotelID});
        }
        public ActionResult Step2(int? id)
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
        [HttpPost]
        public ActionResult Step2(Hotel model)
        {
            string paidMoney = Request.Form["money"];
            Hotel hotel = db.Hotels.Find(model.hotelID);
            //Save to Order
            Order order = new Order();
            order.totalPrice = hotel.price + hotel.travel.travelPlan.First().price;
            if (Request.IsAuthenticated)
            {
                User user = db.Users.Where(o => o.email == HttpContext.User.Identity.Name).ToList()[0];
                order.userID = user.userID;
            }
            order.hotelID = model.hotelID;
            db.Orders.Add(order);
            db.SaveChanges();
            //Order save completed

            //Save to Payment
            Payment payment = new Payment();
            payment.balance = order.totalPrice - Convert.ToInt32(paidMoney);
            payment.totalPaid += Convert.ToInt32(paidMoney);
            payment.orderID = order.orderID;
            db.Payments.Add(payment);
            db.SaveChanges();
            //Save to payment completed

            //Save to PaymentHistory
            PaymentHistory paymentHistory = new PaymentHistory();
            paymentHistory.paidPrice = Convert.ToInt32(paidMoney);
            paymentHistory.paidType = PaidType.ONLINE;
            paymentHistory.date = DateTime.Now;
            paymentHistory.paymentID = payment.paymentID;
            db.PaymentHistory.Add(paymentHistory);
            db.SaveChanges();
            //Save to payment completed

            return RedirectToAction("Step3", new { id = model.hotelID, orderID=order.orderID });
        }
        public ActionResult Step3(int? id,int orderID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            Order order = db.Orders.Find(orderID);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            BigOrderHotelModel model = new BigOrderHotelModel();
            model.Order = order;
            model.Hotel = hotel;
            return View(model);
        }
    }
}