using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoAn2023.Models.EF
{
    public partial class TuyenDungDbContext : DbContext
    {
        public TuyenDungDbContext()
            : base("name=TuyenDungDbContext")
        {
            Database.SetInitializer<TuyenDungDbContext>(null);
        }

        public virtual DbSet<tbl_BaiViet> tbl_BaiViet { get; set; }
        public virtual DbSet<tbl_CapBac> tbl_CapBac { get; set; }
        public virtual DbSet<tbl_ChuyenNganh> tbl_ChuyenNganh { get; set; }
        public virtual DbSet<tbl_DiaChi> tbl_DiaChi { get; set; }
        public virtual DbSet<tbl_HoSoXinViec> tbl_HoSoXinViec { get; set; }
        public virtual DbSet<tbl_LoaiCongViec> tbl_LoaiCongViec { get; set; }
        public virtual DbSet<tbl_MucLuong> tbl_MucLuong { get; set; }
        public virtual DbSet<tbl_NhaTuyenDung> tbl_NhaTuyenDung { get; set; }
        public virtual DbSet<tbl_Quyen> tbl_Quyen { get; set; }
        public virtual DbSet<tbl_TaiKhoan> tbl_TaiKhoan { get; set; }
        public virtual DbSet<tbl_TinTuyenDung> tbl_TinTuyenDung { get; set; }
        public virtual DbSet<tbl_UngTuyen> tbl_UngTuyen { get; set; }
        public virtual DbSet<tbl_UngVien> tbl_UngVien { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_CapBac>()
                .HasMany(e => e.tbl_HoSoXinViec)
                .WithRequired(e => e.tbl_CapBac)
                .HasForeignKey(e => e.FK_sMaCapBac)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CapBac>()
                .HasMany(e => e.tbl_TinTuyenDung)
                .WithRequired(e => e.tbl_CapBac)
                .HasForeignKey(e => e.FK_sMaCapBac)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_ChuyenNganh>()
                .HasMany(e => e.tbl_HoSoXinViec)
                .WithRequired(e => e.tbl_ChuyenNganh)
                .HasForeignKey(e => e.FK_sMaCN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_ChuyenNganh>()
                .HasMany(e => e.tbl_TinTuyenDung)
                .WithRequired(e => e.tbl_ChuyenNganh)
                .HasForeignKey(e => e.FK_sMaCN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DiaChi>()
                .HasMany(e => e.tbl_TinTuyenDung)
                .WithOptional(e => e.tbl_DiaChi)
                .HasForeignKey(e => e.FK_sMaDiaChi);

            modelBuilder.Entity<tbl_LoaiCongViec>()
                .HasMany(e => e.tbl_HoSoXinViec)
                .WithOptional(e => e.tbl_LoaiCongViec)
                .HasForeignKey(e => e.FK_sMaLoaiCV);

            modelBuilder.Entity<tbl_LoaiCongViec>()
                .HasMany(e => e.tbl_TinTuyenDung)
                .WithRequired(e => e.tbl_LoaiCongViec)
                .HasForeignKey(e => e.FK_sMaLoaiCV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_MucLuong>()
                .HasMany(e => e.tbl_TinTuyenDung)
                .WithOptional(e => e.tbl_MucLuong)
                .HasForeignKey(e => e.FK_sMaMucLuong);

            modelBuilder.Entity<tbl_NhaTuyenDung>()
                .HasMany(e => e.tbl_TinTuyenDung)
                .WithRequired(e => e.tbl_NhaTuyenDung)
                .HasForeignKey(e => e.FK_iMaNTD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Quyen>()
                .HasMany(e => e.tbl_TaiKhoan)
                .WithOptional(e => e.tbl_Quyen)
                .HasForeignKey(e => e.FK_iMaQuyen);

            modelBuilder.Entity<tbl_TaiKhoan>()
                .HasMany(e => e.tbl_BaiViet)
                .WithOptional(e => e.tbl_TaiKhoan)
                .HasForeignKey(e => e.FK_iMaTaiKhoan);

            modelBuilder.Entity<tbl_TaiKhoan>()
                .HasOptional(e => e.tbl_NhaTuyenDung)
                .WithRequired(e => e.tbl_TaiKhoan);

            modelBuilder.Entity<tbl_TaiKhoan>()
                .HasOptional(e => e.tbl_UngVien)
                .WithRequired(e => e.tbl_TaiKhoan);

            modelBuilder.Entity<tbl_TinTuyenDung>()
                .HasMany(e => e.tbl_UngTuyen)
                .WithRequired(e => e.tbl_TinTuyenDung)
                .HasForeignKey(e => e.FK_iMaTTD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_UngVien>()
                .HasMany(e => e.tbl_HoSoXinViec)
                .WithRequired(e => e.tbl_UngVien)
                .HasForeignKey(e => e.FK_iMaUngVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_UngVien>()
                .HasMany(e => e.tbl_UngTuyen)
                .WithRequired(e => e.tbl_UngVien)
                .WillCascadeOnDelete(false);
        }
    }
}
