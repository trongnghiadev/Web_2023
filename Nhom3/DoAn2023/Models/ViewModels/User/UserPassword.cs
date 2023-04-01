using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn2023.Models.ViewModels.User
{
    public class UserPassword
    {
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu cũ")]
        public string MatKhauCu { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu mới")]
        public string MatKhauMoi { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập lại mật khẩu")]
        [Compare("MatKhauMoi", ErrorMessage = "Mật khẩu xác nhận chưa đúng")]
        public string XN_MatKhau { get; set; }
    }
}