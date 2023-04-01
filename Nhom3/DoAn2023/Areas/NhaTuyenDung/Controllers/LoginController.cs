using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Common;
using DoAn2023.Models.DAL;

namespace DoAn2023.Areas.NhaTuyenDung.Controllers
{
    public class LoginController : Controller
    {
        private readonly TaiKhoanDAL taiKhoanDao;

        public LoginController()
        {
            taiKhoanDao = new TaiKhoanDAL();
        }

        // GET: Login
        public ActionResult Index()
        {
            Session[CommonConstants.EMPLOYER_SESSION] = null;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(Models.ViewModels.Employer.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await taiKhoanDao.Login(model.Email, model.Password, CommonConstants.NHA_TUYEN_DUNG);
                if (result > 0)
                {
                    var userLogin = await taiKhoanDao.GetByEmail_NhaTuyenDung(model.Email);
                    Session[CommonConstants.EMPLOYER_SESSION] = userLogin;
                    TempData["Notify"] = "Đăng nhập thành công!";
                    TempData["AlertType"] = "alert-success";
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(Models.ViewModels.Employer.RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await taiKhoanDao.Register_NhaTuyenDung(model);
                if (result > 0)
                {
                    TempData["notify"] = "Đăng ký thành công !";
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Email này đã tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Đã có lỗi xảy ra. Vui lòng thử lại");
                }
            }
            return View();
        }
    }
}