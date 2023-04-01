using System.Web.Mvc;

namespace DoAn2023.Areas.NhaTuyenDung
{
    public class NhaTuyenDungAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NhaTuyenDung";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NhaTuyenDung_default",
                "nha-tuyen-dung/{controller}/{action}/{id}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}