using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DoAn2023.Areas.Admin.Models;
using DoAn2023.Models.EF;
using DoAn2023.Models.ViewModels.Common;
using DoAn2023.Models.ViewModels.UngVien;

namespace DoAn2023.Models.DAL
{
    public class UngVienDAL
    {
        private TuyenDungDbContext dbContext;

        public UngVienDAL()
        {
            dbContext = new TuyenDungDbContext();
        }

        public async Task<UngVienVm> GetById(int id)
        {
            try
            {
                var item = await dbContext.tbl_UngVien.Where(x => x.PK_iMaUngVien.Equals(id)).SingleOrDefaultAsync();
                if (item == null) return null;
                return new UngVienVm()
                {
                    MaUngVien = item.PK_iMaUngVien,
                    TenUngVien = item.sTenUngVien,
                    GioiTinh = item.sGioiTinh,
                    NgaySinh = item.dNgaySinh.GetValueOrDefault().ToString("yyyy-MM-dd"),
                    DiaChi = item.sDiaChi,
                    SoDienThoai = item.sSoDienThoai
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UngVienEditClient GetUngVien(int id)
        {
            try
            {
                var item = dbContext.tbl_UngVien.Where(x => x.PK_iMaUngVien.Equals(id)).SingleOrDefault();
                if (item == null) return null;
                return new UngVienEditClient()
                {
                    MaUngVien = item.PK_iMaUngVien,
                    AnhBia = item.sAnhBia,
                    AnhDaiDien = item.sAnhDaiDien,
                    TenUngVien = item.sTenUngVien,
                    GioiTinh = item.sGioiTinh,
                    NgaySinh = item.dNgaySinh.GetValueOrDefault().ToString("yyyy-MM-dd"),
                    DiaChi = item.sDiaChi,
                    SoDienThoai = item.sSoDienThoai
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResultPaging<UngVienVm>> GetList(GetListPaging paging)
        {
            IQueryable<tbl_UngVien> model = dbContext.tbl_UngVien;
            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.sTenUngVien.Contains(paging.keyWord.Trim()) || x.sSoDienThoai.Contains(paging.keyWord.Trim()));
            }

            int total = await model.CountAsync();

            var items = await model.OrderBy(x => x.PK_iMaUngVien)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .Select(item => new UngVienVm()
                {
                    MaUngVien = item.PK_iMaUngVien,
                    TenUngVien = item.sTenUngVien,
                    GioiTinh = item.sGioiTinh,
                    NgaySinh = item.dNgaySinh.ToString(),
                    DiaChi = item.sDiaChi,
                    SoDienThoai = item.sSoDienThoai
                }).ToListAsync();
            return new ResultPaging<UngVienVm>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public async Task<int> Create(UngVienVm item)
        {
            try
            {
                var check = await dbContext.tbl_UngVien.Where(x => x.PK_iMaUngVien.Equals(item.MaUngVien)).SingleOrDefaultAsync();
                if (check != null) return 0;
                var member = new tbl_UngVien()
                {
                    PK_iMaUngVien = item.MaUngVien,
                    sTenUngVien = item.TenUngVien,
                    sGioiTinh = item.GioiTinh,
                    sDiaChi = item.DiaChi,
                    dNgaySinh = DateTime.Parse(item.NgaySinh),
                    sSoDienThoai = item.SoDienThoai
                };
                dbContext.tbl_UngVien.Add(member);
                var result = await dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception )
            {               
                return -1;
            }
        }

        public async Task<bool> Delete(int maUngVien)
        {
            try
            {
                var member = await dbContext.tbl_UngVien.FindAsync(maUngVien);
                if (member == null) return false;
                dbContext.tbl_UngVien.Remove(member);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateUngVien(UngVienVm item)
        {
            try
            {
                var member = await dbContext.tbl_UngVien.FindAsync(item.MaUngVien);
                if (member == null) return false;
                member.sTenUngVien = item.TenUngVien;
                member.sGioiTinh = item.GioiTinh;
                member.sDiaChi = item.DiaChi;
                member.dNgaySinh = DateTime.Parse(item.NgaySinh);
                member.sSoDienThoai = item.SoDienThoai;
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Client
        public async Task<UngVienEditClient> GetByIdClient(int id)
        {
            try
            {
                var item = await dbContext.tbl_UngVien.Where(x => x.PK_iMaUngVien.Equals(id)).SingleOrDefaultAsync();
                if (item == null) return null;
                return new UngVienEditClient()
                {
                    MaUngVien = item.PK_iMaUngVien,
                    AnhBia = item.sAnhBia,
                    AnhDaiDien = item.sAnhDaiDien,
                    TenUngVien = item.sTenUngVien,
                    GioiTinh = item.sGioiTinh,
                    NgaySinh = item.dNgaySinh.GetValueOrDefault().ToString("yyyy-MM-dd"),
                    DiaChi = item.sDiaChi,
                    SoDienThoai = item.sSoDienThoai
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateClient(UngVienEditClient item, HttpServerUtilityBase httpServer)
        {
            try
            {
                var member = await dbContext.tbl_UngVien.Where(x => x.PK_iMaUngVien.Equals(item.MaUngVien)).SingleOrDefaultAsync();
                if (member == null) return false;

                if (item.ImageMain != null && item.ImageMain.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item.ImageMain.FileName);
                    string extension = Path.GetExtension(item.ImageMain.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    item.AnhDaiDien = "/Images/UngVien/" + fileName;
                    fileName = Path.Combine(httpServer.MapPath("~/Images/UngVien/"), fileName);
                    item.ImageMain.SaveAs(fileName);
                    member.sAnhDaiDien = item.AnhDaiDien;
                }

                if (item.ImageCover != null && item.ImageCover.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item.ImageCover.FileName);
                    string extension = Path.GetExtension(item.ImageCover.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    item.AnhBia = "/Images/UngVien/" + fileName;
                    fileName = Path.Combine(httpServer.MapPath("~/Images/UngVien/"), fileName);
                    item.ImageCover.SaveAs(fileName);
                    member.sAnhBia = item.AnhBia;
                }

                member.sTenUngVien = item.TenUngVien;
                member.sGioiTinh = item.GioiTinh;
                member.sDiaChi = item.DiaChi;
                member.dNgaySinh = DateTime.Parse(item.NgaySinh);
                member.sSoDienThoai = item.SoDienThoai;

                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}