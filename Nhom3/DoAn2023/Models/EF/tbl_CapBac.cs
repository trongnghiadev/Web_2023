namespace DoAn2023.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CapBac
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CapBac()
        {
            tbl_HoSoXinViec = new HashSet<tbl_HoSoXinViec>();
            tbl_TinTuyenDung = new HashSet<tbl_TinTuyenDung>();
        }

        [Key]
        [StringLength(50)]
        public string PK_sMaCapBac { get; set; }

        [StringLength(100)]
        public string sTenCapBac { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_HoSoXinViec> tbl_HoSoXinViec { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_TinTuyenDung> tbl_TinTuyenDung { get; set; }
    }
}
