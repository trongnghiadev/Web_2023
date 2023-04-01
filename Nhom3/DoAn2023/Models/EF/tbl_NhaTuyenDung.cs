namespace DoAn2023.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_NhaTuyenDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_NhaTuyenDung()
        {
            tbl_TinTuyenDung = new HashSet<tbl_TinTuyenDung>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PK_iMaNTD { get; set; }

        [StringLength(200)]
        public string sTenNTD { get; set; }

        [StringLength(100)]
        public string sTenNDD { get; set; }

        [StringLength(50)]
        public string sChucVuNDD { get; set; }

        [StringLength(10)]
        public string sSoDienThoai { get; set; }

        [StringLength(200)]
        public string sAnhBia { get; set; }

        [StringLength(200)]
        public string sAnhDaiDien { get; set; }

        [StringLength(50)]
        public string sQuyMo { get; set; }

        [StringLength(250)]
        public string sMoTa { get; set; }

        [StringLength(250)]
        public string sDiaChi { get; set; }

        [StringLength(50)]
        public string sWebsite { get; set; }

        public virtual tbl_TaiKhoan tbl_TaiKhoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_TinTuyenDung> tbl_TinTuyenDung { get; set; }
    }
}
