using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAn2023.Common;

namespace DoAn2023.Models.ViewModels.BaiViet
{
    public class BaiVietVm
    {
        public int MaBaiViet { get; set; }

        public string TenBaiViet { get; set; }
        public string TieuDe => StringHelper.ToUnsignString(TenBaiViet).ToLower();

        public string AnhChinh { get; set; }

        public string NoiDung { get; set; }

        public string TenTacGia { get; set; }

        public string ThoiGian { get; set; }

        public int? LuotXem { get; set; }

        public string TrangThai { get; set; }
    }
}