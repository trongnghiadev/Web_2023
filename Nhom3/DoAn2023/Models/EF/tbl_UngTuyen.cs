namespace DoAn2023.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_UngTuyen
    {
        [Key]
        [Column(Order = 0)]
        public int PK_iMaUngVien { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_iMaTTD { get; set; }

        [StringLength(150)]
        public string sLinkHoSo { get; set; }

        public DateTime? dNgayUngTuyen { get; set; }

        [StringLength(50)]
        public string sTrangThai { get; set; }

        public virtual tbl_TinTuyenDung tbl_TinTuyenDung { get; set; }

        public virtual tbl_UngVien tbl_UngVien { get; set; }
    }
}
