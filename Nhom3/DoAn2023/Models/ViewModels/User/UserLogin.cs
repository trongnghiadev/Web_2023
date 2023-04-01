using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2023.Models.ViewModels.User
{
    public class UserLogin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}