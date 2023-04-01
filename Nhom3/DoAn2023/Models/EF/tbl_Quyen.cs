namespace DoAn2023.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Quyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Quyen()
        {
            tbl_TaiKhoan = new HashSet<tbl_TaiKhoan>();
        }

        [Key]
        public int PK_iMaQuyen { get; set; }

        [StringLength(100)]
        public string sTenQuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_TaiKhoan> tbl_TaiKhoan { get; set; }
    }
}
