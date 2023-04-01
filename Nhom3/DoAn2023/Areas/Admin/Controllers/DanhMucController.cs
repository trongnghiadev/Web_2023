using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Models.DAL;
using DoAn2023.Models.ViewModels.Common;
using DoAn2023.Models.ViewModels.DanhMuc;

namespace DoAn2023.Areas.Admin.Controllers
{
    public class DanhMucController : BaseController
    {
        private DanhMucDAL dao;

        public DanhMucController()
        {
            dao = new DanhMucDAL();
        }

        // GET: Admin/DanhMuc
        public ActionResult ChuyenNganh()
        {
            return View();
        }

        public ActionResult GetPagingChuyenNganh(string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = dao.LayDanhSachChuyenNganh(request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateChuyenNganh(ChuyenNganh item)
        {
            var result = await dao.CreateChuyenNganh(item);
            if (result > 0)
            {
                SetAlert("Tạo thành công", "success");
            }
            else if (result == 0)
            {
                SetAlert("Chuyên ngành đã tồn tại", "error");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> EditChuyenNganh(ChuyenNganh item)
        {
            var result = await dao.EditChuyenNganh(item);
            if (result)
            {
                SetAlert("Cập nhật thành công", "success");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteChuyenNganh(string MaCN)
        {
            var result = await dao.DeleteChuyenNganh(MaCN);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        public ActionResult CapBac()
        {
            return View();
        }

        public ActionResult GetPagingCapBac(string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = dao.GetListCapBac(request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCapBac(CapBac item)
        {
            var result = await dao.CreateCapBac(item);
            if (result > 0)
            {
                SetAlert("Tạo thành công", "success");
            }
            else if (result == 0)
            {
                SetAlert("Cấp bậc đã tồn tại", "error");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> EditCapBac(CapBac item)
        {
            var result = await dao.EditCapBac(item);
            if (result)
            {
                SetAlert("Cập nhật thành công", "success");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCapBac(string MaCB)
        {
            var result = await dao.DeleteCapBac(MaCB);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        public ActionResult DiaChi()
        {
            return View();
        }

        public ActionResult GetPagingDiaChi(string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = dao.GetListDiaChi(request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDiaChi(DiaChi item)
        {
            var result = await dao.CreateDiaChi(item);
            if (result > 0)
            {
                SetAlert("Tạo thành công", "success");
            }
            else if (result == 0)
            {
                SetAlert("Địa chỉ đã tồn tại", "error");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> EditDiaChi(DiaChi item)
        {
            var result = await dao.EditDiaChi(item);
            if (result)
            {
                SetAlert("Cập nhật thành công", "success");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDiaChi(string MaDC)
        {
            var result = await dao.DeleteDiaChi(MaDC);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        public ActionResult MucLuong()
        {
            return View();
        }

        public ActionResult GetPagingMucLuong(string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = dao.GetListMucLuong(request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMucLuong(MucLuong item)
        {
            var result = await dao.CreateMucLuong(item);
            if (result > 0)
            {
                SetAlert("Tạo thành công", "success");
            }
            else if (result == 0)
            {
                SetAlert("Mức lương đã tồn tại", "error");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> EditMucLuong(MucLuong item)
        {
            var result = await dao.EditMucLuong(item);
            if (result)
            {
                SetAlert("Cập nhật thành công", "success");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMucLuong(string MaML)
        {
            var result = await dao.DeleteMucLuong(MaML);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        public ActionResult LoaiCongViec()
        {
            return View();
        }

        public ActionResult GetPagingLoaiCV(string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = dao.GetListLoaiCV(request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateLoaiCV(LoaiCongViec item)
        {
            var result = await dao.CreateLoaiCV(item);
            if (result > 0)
            {
                SetAlert("Tạo thành công", "success");
            }
            else if (result == 0)
            {
                SetAlert("Loại công việc đã tồn tại", "error");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> EditLoaiCV(LoaiCongViec item)
        {
            var result = await dao.EditLoaiCV(item);
            if (result)
            {
                SetAlert("Cập nhật thành công", "success");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteLoaiCV(string MaLoaiCV)
        {
            var result = await dao.DeleteLoaiCV(MaLoaiCV);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
            }
            return Json(result);
        }
    }
}