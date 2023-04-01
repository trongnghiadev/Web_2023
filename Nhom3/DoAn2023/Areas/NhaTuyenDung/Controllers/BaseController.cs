using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DoAn2023.Common;
using DoAn2023.Models.DAL;
using DoAn2023.Models.ViewModels.User;

namespace DoAn2023.Areas.NhaTuyenDung.Controllers
{
    public class BaseController : Controller
    {
        protected UserLogin UserLogin()
        {
            return (UserLogin)Session[CommonConstants.EMPLOYER_SESSION];
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.EMPLOYER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            else
            {
                var userLogin = new NhaTuyenDungDAL().GetNhaTuyenDung(session.Id);
                TempData["AnhDaiDien"] = userLogin.AnhDaiDien;
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["Notify"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}