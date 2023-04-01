namespace DoAn2023.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_HoSoXinViec
    {
        [Key]
        public int PK_iMaHoSo { get; set; }

        [StringLength(200)]
        public string sTenHoSo { get; set; }

        public int FK_iMaUngVien { get; set; }

        [Required]
        [StringLength(50)]
        public string FK_sMaCN { get; set; }

        [StringLength(50)]
        public string FK_sMaLoaiCV { get; set; }

        [Required]
        [StringLength(50)]
        public string FK_sMaCapBac { get; set; }

        [Column(TypeName = "ntext")]
        public string sMucTieuNgheNghiep { get; set; }

        [Column(TypeName = "ntext")]
        public string sKinhNghiem { get; set; }

        [StringLength(200)]
        public string sKyNang { get; set; }

        [Column(TypeName = "ntext")]
        public string sHocVan { get; set; }

        [StringLength(200)]
        public string sKyNangMem { get; set; }

        [StringLength(200)]
        public string sGiaiThuong { get; set; }

        public virtual tbl_CapBac tbl_CapBac { get; set; }

        public virtual tbl_ChuyenNganh tbl_ChuyenNganh { get; set; }

        public virtual tbl_LoaiCongViec tbl_LoaiCongViec { get; set; }

        public virtual tbl_UngVien tbl_UngVien { get; set; }
    }
}
