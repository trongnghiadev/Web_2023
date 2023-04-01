using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn2023.Models.ViewModels.HoSoXinViec
{
    public class HoSoXinViecCreate
    {
        [Required(ErrorMessage = "Chưa nhập tên hồ sơ")]
        public string TenHoSo { get; set; }

        [Required(ErrorMessage = "Chưa chọn chuyên ngành")]
        public string MaCN { get; set; }

        [Required(ErrorMessage = "Chưa chọn hình thức làm việc")]
        public string MaLoaiCV { get; set; }

        [Required(ErrorMessage = "Chưa chọn cấp bậc")]
        public string MaCapBac { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Bạn nên nhập mục tiêu nghề nghiệp")]
        public string MucTieuNgheNghiep { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Nêu một số dự án bạn từng tham gia")]
        public string KinhNghiem { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Chưa nhập kỹ năng")]
        public string KyNang { get; set; }

        [AllowHtml]
        public string HocVan { get; set; }

        [AllowHtml]
        public string KyNangMem { get; set; }

        [AllowHtml]
        public string GiaiThuong { get; set; }
    }
}