using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn2023.Models.ViewModels.HoSoXinViec
{
    public class HoSoXinViecVm
    {
        public int MaHoSo { get; set; }

        public string TenHoSo { get; set; }

        public int MaUngVien { get; set; }

        [Required]
        public string MaCN { get; set; }

        public string MaLoaiCV { get; set; }

        [Required]
        public string MaCapBac { get; set; }

        public string MucTieuNgheNghiep { get; set; }

        public string KinhNghiem { get; set; }

        public string KyNang { get; set; }

        public string HocVan { get; set; }

        public string KyNangMem { get; set; }

        public string GiaiThuong { get; set; }
    }
}