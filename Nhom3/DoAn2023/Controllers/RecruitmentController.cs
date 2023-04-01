using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Common;
using DoAn2023.Models.DAL;
using DoAn2023.Models.ViewModels.Common;

namespace DoAn2023.Controllers
{

    public class RecruitmentController : BaseController
    {
        private readonly TinTuyenDungDAL TinTuyenDungDAL;
        private readonly UngTuyenDAL UngTuyenDAL;

        public RecruitmentController()
        {
            TinTuyenDungDAL = new TinTuyenDungDAL();
            UngTuyenDAL = new UngTuyenDAL();
        }

        // GET: Recruitment
        public async Task<ActionResult> Index(int id)
        {
            var model = await TinTuyenDungDAL.GetViewById(id);
            TinTuyenDungDAL.UpdateCount(id);
            if(UserLogin() != null)
            {
                ViewBag.Status = UngTuyenDAL.GetStatus(UserLogin().Id, id);
                ViewBag.ListCV = new HoSoXinViecDAL().GetListByIdNguoiDung(UserLogin().Id);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UngTuyen(string HoSo, HttpPostedFileBase FileCV, int MaTTD)
        {
            var result = -1;

            if (UserLogin() == null)
            {
                SetAlert("Bạn chưa đăng nhập", "warning");
                return RedirectToAction("Index", "Home");
            }

            if (HoSo != null)
            {
                result = UngTuyenDAL.UngTuyenOnline(UserLogin().Id, MaTTD, HoSo);
            }
            if(FileCV != null)
            {
                result = UngTuyenDAL.UngTuyenFile(UserLogin().Id, MaTTD, FileCV, Server);
            }

            if(result > 0)
            {
                SetAlert("Ứng tuyển thành công", "success");
            }else if(result == 0)
            {
                SetAlert("Bạn đã ứng tuyển tin tuyển dụng này", "warning");
            }
            else
            {
                SetAlert("Đã có lỗi xảy ra", "warning");
            }
            return RedirectToAction("Index", "Recruitment", new { id = MaTTD});
        }

        [HttpPost]
        public ActionResult Save(int MaTTD)
        {
            if (UserLogin() == null)
            {
                SetAlert("Bạn chưa đăng nhập", "warning");
                return RedirectToAction("Index", "Home");
            }
            var result = UngTuyenDAL.Save(UserLogin().Id, MaTTD);
            if(result > 0)
            {
                SetAlert("Lưu thành công", "success");
            }else if(result == 0)
            {
                SetAlert("Bỏ lưu thành công", "success");
            }
            else
            {
                SetAlert("Bạn đã ứng tuyển tin tuyển dụng này", "warning");
            }
            return RedirectToAction("Index", "Recruitment", new { id = MaTTD });
        }

        public ActionResult DaLuu()
        {
            if (UserLogin() == null)
            {
                SetAlert("Bạn chưa đăng nhập", "warning");
                return RedirectToAction("Index", "Home");
            }
            var model = TinTuyenDungDAL.GetListSaveByIdUser(UserLogin().Id, TrangThaiUngTuyen.DALUU);
            return View(model);
        }

        public ActionResult PhuHop()
        {
            if (UserLogin() == null)
            {
                SetAlert("Bạn chưa đăng nhập", "warning");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult GetPaging(int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = null,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = new TinTuyenDungDAL().GetListByIdUser(request, UserLogin().Id);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);
        }
    }
}