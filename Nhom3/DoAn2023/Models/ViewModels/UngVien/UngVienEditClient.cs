using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2023.Models.ViewModels.UngVien
{
    public class UngVienEditClient
    {
        public int MaUngVien { get; set; }

        public string TenUngVien { get; set; }

        public string SoDienThoai { get; set; }

        public string GioiTinh { get; set; }

        public string NgaySinh { get; set; }

        public string AnhDaiDien { get; set; }

        public string AnhBia { get; set; }

        public string DiaChi { get; set; }

        public HttpPostedFileBase ImageMain { get; set; }
        public HttpPostedFileBase ImageCover { get; set; }
    }
}