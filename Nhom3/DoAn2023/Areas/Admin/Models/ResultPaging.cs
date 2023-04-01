using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2023.Areas.Admin.Models
{
    public class ResultPaging<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecord { get; set; }
    }
}