using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Models.DAL;
using DoAn2023.Models.ViewModels.Common;
using DoAn2023.Models.ViewModels.Employer;

namespace DoAn2023.Areas.Admin.Controllers
{
    public class NhaTuyenDungController : BaseController
    {
        NhaTuyenDungDAL dao;

        public NhaTuyenDungController()
        {
            dao = new NhaTuyenDungDAL();
        }

        // GET: Admin/TuyenDung
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
        public async Task<ActionResult> Create(EmployerEdit member)
        {
            if (ModelState.IsValid)
            {
                var result = await dao.Create(member);
                if (result > 0)
                {
                    SetAlert("Tạo nhà tuyển dụng thành công", "success");
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Mã nhà tuyển dụng đã tồn tại");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mã nhà tuyển chưa trùng với mã tài khoản đăng ký nhà tuyển dụng");
                }
                else
                {
                    SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int id)
        {
            var member = await dao.GetById(id);
            ViewBag.MaNTD = member.MaNTD;
            return View(member);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int maNTD)
        {
            var result = await dao.Delete(maNTD);
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