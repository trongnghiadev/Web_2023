using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Models.DAL;
using DoAn2023.Models.ViewModels.Common;
using DoAn2023.Models.ViewModels.UngVien;

namespace DoAn2023.Areas.Admin.Controllers
{
    public class UngVienController : BaseController
    {
        UngVienDAL dao;

        public UngVienController()
        {
            dao = new UngVienDAL();
        }

        // GET: Admin/UngVien
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
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UngVienVm ungvien)
        {
            if (ModelState.IsValid)
            {
                var result = await dao.Create(ungvien);
                if (result > 0)
                {
                    SetAlert("Tạo ứng viên thành công", "success");
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Mã ứng viên đã tồn tại");
                }
                else if (result == -1)
                {
                    SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int id)
        {
            var ungvien = await dao.GetById(id);
            ViewBag.MaUngVien = ungvien.MaUngVien;
            return View(ungvien);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int maUngVien)
        {
            var result = await dao.Delete(maUngVien);
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
    }
}