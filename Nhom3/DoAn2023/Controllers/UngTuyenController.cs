using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn2023.Common;
using DoAn2023.Models.DAL;

namespace DoAn2023.Controllers
{
    public class UngTuyenController : BaseController
    {
        private readonly UngTuyenDAL UngTuyenDAL;

        public UngTuyenController()
        {
            UngTuyenDAL = new UngTuyenDAL();
        }

        // GET: UngTuyen
        public ActionResult Index()
        {
            if (UserLogin() == null)
            {
                SetAlert("Bạn chưa đăng nhập", "warning");
                return RedirectToAction("Index", "Home");
            }
            var model = UngTuyenDAL.GetListByUser(UserLogin().Id);
            return View(model);
        }

    }
}