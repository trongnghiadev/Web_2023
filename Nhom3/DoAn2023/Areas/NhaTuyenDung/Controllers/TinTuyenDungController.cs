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
using DoAn2023.Models.ViewModels.User;

namespace DoAn2023.Areas.NhaTuyenDung.Controllers
{
    public class TinTuyenDungController : BaseController
    {
        private readonly TinTuyenDungDAL dao;
        public TinTuyenDungController()
        {
            dao = new TinTuyenDungDAL();
        }

        // GET: NhaTuyenDung/TinTuyenDung
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPaging(bool? hetHan, bool trangThai, string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var session = (UserLogin)Session[CommonConstants.EMPLOYER_SESSION];
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = dao.GetList(hetHan, request, trangThai, session.Id);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChoDuyet()
        {
            return View();
        }

        public ActionResult HetHan()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TinTuyenDungCreate item)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.EMPLOYER_SESSION];
                var result = await dao.Create(item, session.Id);
                if (result > 0)
                {
                    SetAlert("Tạo tin tuyển dụng thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
                }
            }
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var member = await dao.GetById(id);
            return View(member);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TinTuyenDungEdit member)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.EMPLOYER_SESSION];
                var result = await dao.EditTTD(member, session.Id);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int maTTD)
        {
            var result = await dao.Delete(maTTD);
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