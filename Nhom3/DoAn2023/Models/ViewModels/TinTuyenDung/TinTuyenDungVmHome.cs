using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAn2023.Common;

namespace DoAn2023.Models.ViewModels.TinTuyenDung
{
    public class TinTuyenDungVmHome
    {
        public int MaTTD { get; set; }
        public int MaNTD { get; set; }
        public string TenNTD { get; set; }
        public string TieuDeNTD => StringHelper.ToUnsignString(TenNTD).ToLower();
        public string AnhDaiDien { get; set; }
        public string TenCongViec { get; set; }
        public string TieuDeTTD => StringHelper.ToUnsignString(TenCongViec).ToLower();
        public string LoaiCV { get; set; }
        public string MucLuong { get; set; }
        public string DiaChi { get; set; }
        public string NgayDang { get; set; }
        public string HanNop { get; set; }
    }
}