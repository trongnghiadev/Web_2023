using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn2023.Models.ViewModels.User
{
    public class UserCreate
    {
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận chưa đúng")]
        public string Password_Confirm { get; set; }
        public int idQuyen { get; set; }
        public DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }
    }
}