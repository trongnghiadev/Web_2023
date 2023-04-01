using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Areas.Admin.Models;
using DoAn2023.Common;
using DoAn2023.Models.DAL;

namespace DoAn2023.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.SlUngVien = new TaiKhoanDAL().SlUngVien();
            ViewBag.SlNhaTuyenDung = new TaiKhoanDAL().SlNhaTuyenDung();
            ViewBag.SlTinTuyenDung = new TinTuyenDungDAL().SlTinTuyenDung(null, true);
            ViewBag.SlBaiViet = new BaiVietDAL().SlBaiViet(null, true);
            ViewBag.TopView = new TinTuyenDungDAL().GetListTopView(5, null);
            return View();
        }

        public ActionResult Logout()
        {
            SetAlert("Đăng xuất thành công", "success");
            Session[CommonConstants.ADMIN_SESSION] = null;
            return RedirectToAction("Index","Login");
        }

        [ChildActionOnly]
        public ActionResult _SidebarMenu()
        {
            return PartialView();
        }
    }
}