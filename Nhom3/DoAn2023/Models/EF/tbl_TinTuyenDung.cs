namespace DoAn2023.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_TinTuyenDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_TinTuyenDung()
        {
            tbl_UngTuyen = new HashSet<tbl_UngTuyen>();
        }

        [Key]
        public int PK_iMaTTD { get; set; }

        public int FK_iMaNTD { get; set; }

        [Required]
        [StringLength(50)]
        public string FK_sMaCN { get; set; }

        [Required]
        [StringLength(50)]
        public string FK_sMaLoaiCV { get; set; }

        [StringLength(50)]
        public string FK_sMaMucLuong { get; set; }

        [Required]
        [StringLength(50)]
        public string FK_sMaCapBac { get; set; }

        [StringLength(50)]
        public string FK_sMaDiaChi { get; set; }

        [StringLength(250)]
        public string sTenCongViec { get; set; }

        [StringLength(250)]
        public string sDiaChiLamViec { get; set; }

        public int? iSoLuong { get; set; }

        [StringLength(50)]
        public string sGioiTinhYC { get; set; }

        [Column(TypeName = "ntext")]
        public string sMoTaCongViec { get; set; }

        [Column(TypeName = "ntext")]
        public string sYeuCauUngVien { get; set; }

        [Column(TypeName = "ntext")]
        public string sKyNangLienQuan { get; set; }

        [Column(TypeName = "ntext")]
        public string sQuyenLoi { get; set; }

        public DateTime? dNgayDang { get; set; }

        public DateTime? dHanNop { get; set; }

        public int? iLuotXem { get; set; }

        [StringLength(500)]
        public string sGhiChu { get; set; }

        public bool? bTrangThai { get; set; }

        public virtual tbl_CapBac tbl_CapBac { get; set; }

        public virtual tbl_ChuyenNganh tbl_ChuyenNganh { get; set; }

        public virtual tbl_DiaChi tbl_DiaChi { get; set; }

        public virtual tbl_LoaiCongViec tbl_LoaiCongViec { get; set; }

        public virtual tbl_MucLuong tbl_MucLuong { get; set; }

        public virtual tbl_NhaTuyenDung tbl_NhaTuyenDung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_UngTuyen> tbl_UngTuyen { get; set; }
    }
}
