using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn2023.Areas.Admin.Models
{
    public class LoginModel
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Mời nhập user name")]
        public string UserName { set; get; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mời nhập password")]
        public string PassWord { set; get; }

        public bool RememberMe { set; get; }
    }
}