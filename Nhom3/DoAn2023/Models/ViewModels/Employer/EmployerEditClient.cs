using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DoAn2023.Common;

namespace DoAn2023.Models.ViewModels.Employer
{
    public class EmployerEditClient
    {
        public int MaNTD { get; set; }

        [Required(ErrorMessage = "Chưa nhập tên nhà tuyển dụng")]
        public string TenNTD { get; set; }

        public string TieuDe => StringHelper.ToUnsignString(TenNTD).ToLower();

        [Required(ErrorMessage = "Chưa nhập tên người đại diện")]
        public string TenNDD { get; set; }

        [Required(ErrorMessage = "Chưa nhập chức vụ người đại diện")]
        public string ChucVuNDD { get; set; }

        public string AnhDaiDien { get; set; }

        public string AnhBia { get; set; }

        [Required(ErrorMessage = "Chưa nhập số điện thoại")]
        public string SoDienThoai { get; set; }

        public string QuyMo { get; set; }

        [MaxLength(length:250, ErrorMessage = "Hãy mô tả dưới 250 ký tự")]
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Chưa nhập địa chỉ công ty")]
        public string DiaChi { get; set; }

        public string Website { get; set; }

        public HttpPostedFileBase ImageMain { get; set; }
        public HttpPostedFileBase ImageCover { get; set; }
    }
}