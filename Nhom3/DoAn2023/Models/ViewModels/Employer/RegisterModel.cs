using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn2023.Models.ViewModels.Employer
{
    public class RegisterModel
    {
        [Display(Name = "Tên nhà tuyển dụng")]
        [Required(ErrorMessage = "Bạn chưa nhập tên nhà tuyển dụng")]
        public string Company_Name { get; set; }

        [Display(Name = "Email đăng nhập")]
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email chưa đúng định dạng")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Người liên hệ")]
        [Required(ErrorMessage = "Bạn chưa tên người liên hệ")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận chưa đúng")]
        public string Pasword_Confirm { get; set; }
    }
}