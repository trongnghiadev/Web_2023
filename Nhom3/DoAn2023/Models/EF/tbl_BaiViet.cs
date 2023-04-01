namespace DoAn2023.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_BaiViet
    {
        [Key]
        public int PK_iMaBaiViet { get; set; }

        [StringLength(200)]
        public string sTenBaiViet { get; set; }

        [StringLength(100)]
        public string sTieuDe { get; set; }

        [StringLength(200)]
        public string sAnhChinh { get; set; }

        [Column(TypeName = "ntext")]
        public string sNoiDung { get; set; }

        public int? FK_iMaTaiKhoan { get; set; }

        public DateTime? dThoiGian { get; set; }

        public int? iLuotXem { get; set; }

        [StringLength(500)]
        public string sGhiChu { get; set; }

        public bool? bTrangThai { get; set; }

        public virtual tbl_TaiKhoan tbl_TaiKhoan { get; set; }
    }
}
