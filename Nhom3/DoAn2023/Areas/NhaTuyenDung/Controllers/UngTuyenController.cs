using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Models.DAL;
using DoAn2023.Models.ViewModels.Common;
using DoAn2023.Models.ViewModels.HoSoXinViec;
using DoAn2023.Models.ViewModels.UngTuyen;

namespace DoAn2023.Areas.NhaTuyenDung.Controllers
{
    public class UngTuyenController : BaseController
    {
        private readonly UngTuyenDAL UngTuyenDAL;
        public UngTuyenController()
        {
            UngTuyenDAL = new UngTuyenDAL();
        }

        // GET: NhaTuyenDung/UngTuyen
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPaging(bool status, int month, string keyWord,  int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = new UngTuyenDAL().GetListByNTD(status, month, request, UserLogin().Id);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChuaXem()
        {
            return View();
        }

        public async Task<RedirectResult> XemCV(int maUV, int maTTD)
        {
            var link = await UngTuyenDAL.UpdateStatus(maUV, maTTD);
            return RedirectPermanent(link);
        }

        public ActionResult TimUngVien()
        {
            return View();
        }

        public ActionResult DeXuat()
        {
            var model = new HoSoXinViecDAL().DeXuatUngVien(UserLogin().Id);
            return View(model);
        }

        public ActionResult CapNhat(int maUV, int maTTD)
        {
            var model = UngTuyenDAL.GetItem(maUV, maTTD);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CapNhat(int MaUngVien, int MaTTD, string trangThai)
        {
            var result = await UngTuyenDAL.UpdateStatus(MaUngVien, MaTTD, trangThai);
            if (result)
            {
                SetAlert("Cập nhật thành công", "success");
            }
            else
            {
                SetAlert("Có lỗi xảy ra. Vui lòng thử lại!", "error");
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetSearchPaging(string KeyWord, int pageIndex, string CapBac, string ChuyenNganh, string LoaiCV)
        {
            var request = new HoSoXinViecSearch()
            {
                CapBac = CapBac,
                ChuyenNganh = ChuyenNganh,
                LoaiCV = LoaiCV
            };

            var paging = new GetListPaging()
            {
                keyWord = KeyWord.ToLower(),
                PageIndex = pageIndex,
                PageSize = 5
            };
            var data = new HoSoXinViecDAL().GetListSearch(paging, request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / paging.PageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }
    }
}