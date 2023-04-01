using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2023.Models.ViewModels.User
{
    public class RegisterModel
    {
        public string register_name { get; set; }
        public string register_email { get; set; }
        public string register_password { get; set; }
        public string password_confirm { get; set; }
    }
}