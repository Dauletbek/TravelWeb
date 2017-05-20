using System.Web.Mvc;
using TravelWeb.DAL;
using System.Linq;

namespace TravelWeb.Controllers
{
    public class BaseController : Controller
    {
        private TravelContext db = new TravelContext();
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            ViewBag.Submenus = db.TravelTypes.ToList();
        }
    }
}