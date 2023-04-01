using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Common;
using DoAn2023.Models.DAL;
using DoAn2023.Models.ViewModels.Employer;
using DoAn2023.Models.ViewModels.User;

namespace DoAn2023.Areas.NhaTuyenDung.Controllers
{
    public class HomeController : BaseController
    {
        private readonly NhaTuyenDungDAL nhatuyendungDao;
        private readonly TinTuyenDungDAL TinTuyenDungDAL;

        public HomeController()
        {
            nhatuyendungDao = new NhaTuyenDungDAL();
            TinTuyenDungDAL = new TinTuyenDungDAL();
        }

        // GET: NhaTuyenDung/Home
        public ActionResult Index()
        {
            var userLogin = (UserLogin)Session[CommonConstants.EMPLOYER_SESSION];
            if (userLogin == null) return RedirectToAction("Index", "Login");
            ViewBag.SlTinTuyenDung = TinTuyenDungDAL.SlTinTuyenDung(userLogin.Id, true);
            ViewBag.SlTinChoDuyet = TinTuyenDungDAL.SlTinTuyenDung(userLogin.Id, false);
            ViewBag.SlBaiViet = new BaiVietDAL().SlBaiViet(userLogin.Id, true);
            ViewBag.SlUngVien = new UngTuyenDAL().SlUngTuyen(userLogin.Id);
            ViewBag.TopView = new TinTuyenDungDAL().GetListTopView(5, userLogin.Id);
            return View();
        }

        public ActionResult Logout()
        {
            SetAlert("Đăng xuất thành công", "success");
            Session[CommonConstants.EMPLOYER_SESSION] = null;
            return RedirectToAction("Index", "Login");
        }

        public async Task<ActionResult> Info()
        {
            var session = (UserLogin)Session[CommonConstants.EMPLOYER_SESSION];
            if (session == null) return RedirectToAction("Index");
            var member = await nhatuyendungDao.GetByIdClient(session.Id);
            ViewBag.AnhDaiDien = member.AnhDaiDien;
            ViewBag.AnhBia = member.AnhBia;
            return View(member);
        }

        [HttpPost]
        public async Task<ActionResult> Info(EmployerEditClient member)
        {
            var item = await nhatuyendungDao.GetByIdClient(member.MaNTD);
            ViewBag.AnhDaiDien = item.AnhDaiDien;
            ViewBag.AnhBia = item.AnhBia;
            if (ModelState.IsValid)
            {
                var result = await nhatuyendungDao.UpdateClient(member, Server);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [ChildActionOnly]
        public ActionResult _SidebarMenu()
        {
            var userLogin = (UserLogin)Session[CommonConstants.EMPLOYER_SESSION];
            ViewBag.SlTinChoDuyet = TinTuyenDungDAL.SlTinTuyenDung(userLogin.Id, false);
            ViewBag.BaiVietChuaDuyet = new BaiVietDAL().SlBaiViet(userLogin.Id, false);
            return PartialView();
        }
    }
}