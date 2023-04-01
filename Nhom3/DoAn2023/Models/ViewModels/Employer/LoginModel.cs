using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn2023.Models.ViewModels.Employer
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string Password { get; set; }
    }
}