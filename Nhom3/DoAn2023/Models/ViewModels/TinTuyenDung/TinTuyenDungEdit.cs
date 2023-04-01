using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn2023.Models.ViewModels.TinTuyenDung
{
    public class TinTuyenDungEdit
    {
        public int MaTTD { get; set; }

        [Required(ErrorMessage = "Chưa nhập tên công việc")]
        [MaxLength(length: 250, ErrorMessage = "Nên đặt tên công việc dưới 250 ký tự")]
        public string TenCongViec { get; set; }

        [Required(ErrorMessage = "Chưa chọn chuyên ngành")]
        public string MaCN { get; set; }

        [Required(ErrorMessage = "Chưa chọn loại công việc")]
        public string MaLoaiCV { get; set; }

        [Required(ErrorMessage = "Chưa chọn mức lương")]
        public string MaMucLuong { get; set; }

        [Required(ErrorMessage = "Chưa chọn cấp bậc")]
        public string MaCapBac { get; set; }

        [Required(ErrorMessage = "Chưa nhập địa chỉ làm việc")]
        public string MaDiaChi { get; set; }

        public string DiaChiLamViec { get; set; }

        public int? SoLuong { get; set; }

        [Required(ErrorMessage = "Chưa chọn yêu cầu giới tính")]
        public string GioiTinhYC { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Chưa nhập mô tả công việc")]
        public string MoTaCongViec { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Chưa nhập yêu cầu ứng viên")]
        public string YeuCauUngVien { get; set; }

        [AllowHtml]
        public string KyNangLienQuan { get; set; }

        [AllowHtml]
        public string QuyenLoi { get; set; }

        [Required(ErrorMessage = "Chưa nhập hạn nộp")]
        public string HanNop { get; set; }
        public bool TrangThai { get; set; }

        [AllowHtml]
        public string GhiChu { get; set; }
    }
}