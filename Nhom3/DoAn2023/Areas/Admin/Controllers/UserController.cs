using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Models.DAL;
using DoAn2023.Models.ViewModels.Common;
using DoAn2023.Models.ViewModels.User;

namespace DoAn2023.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        TaiKhoanDAL dao;

        public UserController()
        {
            dao = new TaiKhoanDAL();
        }

        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetPaging(string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await dao.GetList(request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);

            // Datetime:  .NET JavaScriptSerializer
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.ListRole = await dao.GetAllRole();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserCreate userCreate)
        {
            ViewBag.ListRole = await dao.GetAllRole();
            if (ModelState.IsValid)
            {
                var result = await dao.Create(userCreate);
                if(result > 0)
                {
                    SetAlert("Tạo tài khoản thành công", "success");
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Email này đã tồn tại");
                }
                else if (result == -1)
                {
                    SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var user = await dao.GetById(id);
            ViewBag.ListRole = await dao.GetAllRole();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserEdit user)
        {
            if (ModelState.IsValid)
            {
                var result = await dao.UpdateUser(user);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int userId)
        {
            var result = await dao.Delete(userId);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
            }
            else
            {
                SetAlert("Có lỗi xảy ra. Vui lòng thử lại!", "error");
            }
            return Json(result);
        }

        public async Task<ActionResult> RoleAssign(int id)
        {
            var user = await dao.GetRoleById(id);
            ViewBag.ListRole = await dao.GetAllRole();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> RoleAssign(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                var result = await dao.UpdateRole(userRole);
                if (result)
                {
                    SetAlert("Phân quyền thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}