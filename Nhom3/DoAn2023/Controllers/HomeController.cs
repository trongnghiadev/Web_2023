using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Common;
using DoAn2023.Models.DAL;
using DoAn2023.Models.ViewModels.Common;
using DoAn2023.Models.ViewModels.TinTuyenDung;
using DoAn2023.Models.ViewModels.UngVien;
using DoAn2023.Models.ViewModels.User;

namespace DoAn2023.Controllers
{
    public class HomeController : BaseController
    {
        private readonly TaiKhoanDAL taiKhoanDao;
        private readonly UngVienDAL ungVienDao;
        private readonly TinTuyenDungDAL TinTuyenDungDAL;

        public HomeController()
        {
            taiKhoanDao = new TaiKhoanDAL();
            ungVienDao = new UngVienDAL();
            TinTuyenDungDAL = new TinTuyenDungDAL();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Index()
        {
            var model = new TinTuyenDungDAL().GetListItemHot(9);
            return View(model);
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        public JsonResult GetPaging(string KeyWord, int pageIndex, string CapBac, string DiaChi, string ChuyenNganh, string LoaiCV, string MucLuong)
        {
            var request = new TinTuyenDungSearch()
            {
                CapBac = CapBac,
                DiaChi = DiaChi,
                ChuyenNganh = ChuyenNganh,
                LoaiCV = LoaiCV,
                MucLuong = MucLuong
            };

            var paging = new GetListPaging()
            {
                keyWord = KeyWord.ToLower(),
                PageIndex = pageIndex,
                PageSize = 5
            };
            var data = TinTuyenDungDAL.GetListSearch(paging, request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / paging.PageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult _ViewSearch()
        {
            var search = new TinTuyenDungSearch()
            {
                CapBac = Request["CapBac"],
                DiaChi = Request["DiaChi"]
            };
            return PartialView(search);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            string data = "";
            if (ModelState.IsValid)
            {
                var result = await taiKhoanDao.Login(model.login_email, model.login_password, CommonConstants.UNG_VIEN);
                if (result > 0)
                {
                    var userLogin = await taiKhoanDao.GetByEmail_UngVien(model.login_email);
                    Session[CommonConstants.USER_SESSION] = userLogin;
                    SetAlert("Đăng nhập thành công !", "success");
                    return Json(new
                    {
                        success = true
                    });
                }
                else if (result == 0)
                {
                    data = "Tài khoản đã bị khóa";
                }
                else if (result == -1)
                {
                    data = "Sai tài khoản hoặc mật khẩu";
                }
            }
            return Json(new
            {
                data = data,
                success = false
            });
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            string data = "";
            if (ModelState.IsValid)
            {
                if(model.register_password == model.password_confirm)
                {
                    var result = await taiKhoanDao.Register_UngVien(model);
                    if (result > 0)
                    {
                        SetAlert("Đăng ký thành công !", "success");
                        return Json(new
                        {
                            success = true
                        });
                    }
                    else if (result == 0)
                    {
                        data = "Email này đã tồn tại";
                    }
                    else if (result == -1)
                    {
                        data = "Đã có lỗi xảy ra. Vui lòng thử lại";
                    }
                }else
                {
                    data = "Mật khẩu xác nhận chưa đúng";
                }
            }
            return Json(new
            {
                data = data,
                success = false
            });
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            SetAlert("Đăng xuất thành công !", "success");
            return Redirect("/");
        }

        public async Task<ActionResult> Info()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null) return RedirectToAction("Index");
            var member = await ungVienDao.GetByIdClient(session.Id);
            ViewBag.AnhDaiDien = member.AnhDaiDien;
            ViewBag.AnhBia = member.AnhBia;
            return View(member);
        }

        [HttpPost]
        public async Task<ActionResult> Info(UngVienEditClient member)
        {
            var item = await ungVienDao.GetByIdClient(member.MaUngVien);
            ViewBag.AnhDaiDien = item.AnhDaiDien;
            ViewBag.AnhBia = item.AnhBia;
            if (ModelState.IsValid)
            {
                if((DateTime.Now - DateTime.Parse(member.NgaySinh)).TotalDays / 365 < 18)
                {
                    ModelState.AddModelError("", "Bạn chưa đủ 18 tuổi");
                    return View();
                }
                var result = await ungVienDao.UpdateClient(member, Server);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoiMatKhau(UserPassword model)
        {
            if (ModelState.IsValid)
            {
                var result = taiKhoanDao.UpdatePass(model, UserLogin().Id);
                if (result > 0)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Mật khẩu cũ chưa chính xác");
                }
                else
                {
                    SetAlert("Đã có lỗi xảy ra!", "error");
                }
            }
            return View();
        }

        public async Task<ActionResult> BaiViet(int id)
        {
            var model = await new BaiVietDAL().GetByIdView(id);
            new BaiVietDAL().UpdateCount(id);
            return View(model);
        }
    }
}