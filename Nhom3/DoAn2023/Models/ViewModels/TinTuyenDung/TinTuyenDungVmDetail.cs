using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAn2023.Common;

namespace DoAn2023.Models.ViewModels.TinTuyenDung
{
    public class TinTuyenDungVmDetail
    {
        public int MaTTD { get; set; }
        public string TieuDeTTD => StringHelper.ToUnsignString(TenCongViec).ToLower();
        public string TenNTD { get; set; }
        public int MaNTD { get; set; }
        public string TieuDeNTD => StringHelper.ToUnsignString(TenNTD).ToLower();
        public string MoTa { get; set; }
        public string QuyMo { get; set; }
        public string DiaChiNTD { get; set; }
        public string AnhDaiDien { get; set; }
        public string TenCongViec { get; set; }
        public string ChuyenNganh { get; set; }
        public string LoaiCV { get; set; }
        public string MucLuong { get; set; }
        public string DiaChi { get; set; }
        public string DiaChiLamViec { get; set; }
        public string CapBac { get; set; }
        public int SoLuong { get; set; }
        public string GioiTinhYC { get; set; }
        public string MoTaCongViec { get; set; }
        public string YeuCauUngVien { get; set; }
        public string KyNangLienQuan { get; set; }
        public string QuyenLoi { get; set; }
        public string HanNop { get; set; }
    }
}