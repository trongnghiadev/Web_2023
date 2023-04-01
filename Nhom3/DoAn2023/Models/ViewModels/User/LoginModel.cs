using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn2023.Models.ViewModels.User
{
    //[Serializable]
    public class LoginModel
    {
        public string login_email { set; get; }

        public string login_password { set; get; }
    }
}