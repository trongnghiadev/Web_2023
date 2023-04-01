namespace DoAn2023.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DiaChi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DiaChi()
        {
            tbl_TinTuyenDung = new HashSet<tbl_TinTuyenDung>();
        }

        [Key]
        [StringLength(50)]
        public string PK_sMaDiaChi { get; set; }

        [StringLength(50)]
        public string sTenDiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_TinTuyenDung> tbl_TinTuyenDung { get; set; }
    }
}
