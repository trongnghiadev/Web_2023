namespace DoAn2023.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_UngVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_UngVien()
        {
            tbl_HoSoXinViec = new HashSet<tbl_HoSoXinViec>();
            tbl_UngTuyen = new HashSet<tbl_UngTuyen>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PK_iMaUngVien { get; set; }

        [StringLength(200)]
        public string sTenUngVien { get; set; }

        [StringLength(10)]
        public string sSoDienThoai { get; set; }

        [StringLength(3)]
        public string sGioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dNgaySinh { get; set; }

        [StringLength(200)]
        public string sAnhDaiDien { get; set; }

        [StringLength(200)]
        public string sAnhBia { get; set; }

        [StringLength(200)]
        public string sDiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_HoSoXinViec> tbl_HoSoXinViec { get; set; }

        public virtual tbl_TaiKhoan tbl_TaiKhoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_UngTuyen> tbl_UngTuyen { get; set; }
    }
}
