using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAn2023.Common;

namespace DoAn2023.Models.ViewModels.UngTuyen
{
    public class UngTuyen
    {
        public int MaUngVien { get; set; }
        public int MaTTD { get; set; }
        public int MaNTD { get; set; }
        public string TenNTD { get; set; }
        public string TieuDeNTD => StringHelper.ToUnsignString(TenNTD).ToLower();
        public string AnhDaiDien { get; set; }
        public string TenCongViec { get; set; }
        public string TieuDeTTD => StringHelper.ToUnsignString(TenCongViec).ToLower();
        public string NgayUngTuyen { get; set; }
        public string TrangThai { get; set; }
        public string LinkHoSo { get; set; }
    }
}