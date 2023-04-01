using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Areas.Admin.Models;
using DoAn2023.Common;
using DoAn2023.Models.DAL;
using DoAn2023.Models.ViewModels.BaiViet;
using DoAn2023.Models.ViewModels.Common;

namespace DoAn2023.Areas.Admin.Controllers
{
    public class BaiVietController : BaseController
    {
        private readonly BaiVietDAL dao;

        public BaiVietController()
        {
            dao = new BaiVietDAL();
        }

        // GET: Admin/BaiViet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPaging(bool trangThai, string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = dao.GetList(request, trangThai, session.Id);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(BaiVietCreate member)
        {
            if (ModelState.IsValid)
            {
                var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                var result = await dao.Create(member, Server, session.Id);
                if (result > 0)
                {
                    SetAlert("Tạo bài viết thành công", "success");
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tên bài viết đã tồn tại");
                }
                else
                {
                    SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var member = await dao.GetById(id);
            ViewBag.MaBaiViet = member.MaBaiViet;
            ViewBag.AnhChinh = member.AnhChinh;
            return View(member);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(BaiVietEdit member)
        {
            var item = await dao.GetById(member.MaBaiViet);
            ViewBag.MaBaiViet = item.MaBaiViet;
            ViewBag.AnhChinh = item.AnhChinh;
            if (ModelState.IsValid)
            {
                var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                var result = await dao.UpdateBaiViet(member, Server, session.Id);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int maBaiViet)
        {
            var result = await dao.Delete(maBaiViet);
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

        public ActionResult ChoDuyet()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Duyet(int maBaiViet)
        {
            var result = await dao.Duyet(maBaiViet);
            if (result)
            {
                SetAlert("Duyệt thành công", "success");
            }
            else
            {
                SetAlert("Có lỗi xảy ra. Vui lòng thử lại!", "error");
            }
            return Json(result);
        }
    }
}