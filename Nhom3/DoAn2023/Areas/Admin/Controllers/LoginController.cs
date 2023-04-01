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
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            Session[CommonConstants.ADMIN_SESSION] = null;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var taiKhoanDAL = new TaiKhoanDAL();

                var ketQua = await taiKhoanDAL
                    .Login(model.UserName, model.PassWord, CommonConstants.QUAN_TRI);
                
                if(ketQua == 1)
                {
                    var userLogin = await taiKhoanDAL.LayTaiKhoanAdminTheoEmail(model.UserName);
                    Session[CommonConstants.ADMIN_SESSION] = userLogin;
                    TempData["AlertMessage"] = "Đăng nhập thành công !";
                    TempData["AlertType"] = "alert-success";
                    return RedirectToAction("Index", "Home");
                }
                else if(ketQua == 0)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa");
                }else if(ketQua == -1)
                {
                    ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
                }
            }
            return View();
        }
    }
}