using System.Web.Mvc;

namespace DoAn2023.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { areas = "admin", action = "Index", id = UrlParameter.Optional },
                new[] {"DoAn2023.Areas.Admin.Controllers"}
            );
        }
    }
}