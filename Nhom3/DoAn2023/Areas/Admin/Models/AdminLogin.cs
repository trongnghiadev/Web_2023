using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2023.Areas.Admin.Models
{
    public class AdminLogin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}