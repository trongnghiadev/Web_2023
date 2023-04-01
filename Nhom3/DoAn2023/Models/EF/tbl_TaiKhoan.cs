namespace DoAn2023.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_TaiKhoan()
        {
            tbl_BaiViet = new HashSet<tbl_BaiViet>();
        }

        [Key]
        public int PK_iMaTaiKhoan { get; set; }

        [StringLength(100)]
        public string sEmail { get; set; }

        [StringLength(100)]
        public string sMatKhau { get; set; }

        public int? FK_iMaQuyen { get; set; }

        public DateTime? dNgayTao { get; set; }

        public bool? bTrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_BaiViet> tbl_BaiViet { get; set; }

        public virtual tbl_NhaTuyenDung tbl_NhaTuyenDung { get; set; }

        public virtual tbl_Quyen tbl_Quyen { get; set; }

        public virtual tbl_UngVien tbl_UngVien { get; set; }
    }
}
