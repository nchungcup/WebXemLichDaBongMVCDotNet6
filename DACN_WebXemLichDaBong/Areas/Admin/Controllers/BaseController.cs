using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DACN_WebXemLichDaBong.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var adminses = HttpContext.Session.GetString("Admin");
            var clientses = HttpContext.Session.GetString("Client");
            if (adminses == null && clientses == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Signin", controller = "SigninSignup", area = "default" }));
            }
            else if (adminses == null && clientses != null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}