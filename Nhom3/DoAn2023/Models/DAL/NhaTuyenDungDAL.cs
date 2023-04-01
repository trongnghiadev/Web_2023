using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DoAn2023.Areas.Admin.Models;
using DoAn2023.Common;
using DoAn2023.Models.EF;
using DoAn2023.Models.ViewModels.Common;
using DoAn2023.Models.ViewModels.Employer;

namespace DoAn2023.Models.DAL
{
    public class NhaTuyenDungDAL
    {
        private TuyenDungDbContext dbContext;

        public NhaTuyenDungDAL()
        {
            dbContext = new TuyenDungDbContext();
        }

        public async Task<EmployerEdit> GetById(int id)
        {
            try
            {
                var item = await dbContext.tbl_NhaTuyenDung.Where(x => x.PK_iMaNTD.Equals(id)).SingleOrDefaultAsync();
                if (item == null) return null;
                return new EmployerEdit()
                {
                    MaNTD = item.PK_iMaNTD,
                    DiaChi = item.sDiaChi,
                    QuyMo = item.sQuyMo,
                    SoDienThoai = item.sSoDienThoai,
                    TenNTD = item.sTenNTD,
                    ChucVuNDD = item.sChucVuNDD,
                    MoTa = item.sMoTa,
                    TenNDD = item.sTenNDD,
                    Website = item.sWebsite
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<EmployerEditClient> GetByIdClient(int id)
        {
            try
            {
                var item = await dbContext.tbl_NhaTuyenDung.Where(x => x.PK_iMaNTD.Equals(id)).SingleOrDefaultAsync();
                if (item == null) return null;
                return new EmployerEditClient()
                {
                    MaNTD = item.PK_iMaNTD,
                    AnhBia = item.sAnhBia,
                    AnhDaiDien = item.sAnhDaiDien,
                    ChucVuNDD = item.sChucVuNDD,
                    MoTa = item.sMoTa,
                    QuyMo = item.sQuyMo,
                    TenNDD = item.sTenNDD,
                    TenNTD = item.sTenNTD,
                    Website = item.sWebsite,
                    DiaChi = item.sDiaChi,
                    SoDienThoai = item.sSoDienThoai
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public EmployerEditClient GetNhaTuyenDung(int id)
        {
            try
            {
                var item = dbContext.tbl_NhaTuyenDung.Where(x => x.PK_iMaNTD.Equals(id)).SingleOrDefault();
                if (item == null) return null;
                return new EmployerEditClient()
                {
                    MaNTD = item.PK_iMaNTD,
                    AnhBia = item.sAnhBia,
                    AnhDaiDien = item.sAnhDaiDien,
                    ChucVuNDD = item.sChucVuNDD,
                    MoTa = item.sMoTa,
                    QuyMo = item.sQuyMo,
                    TenNDD = item.sTenNDD,
                    TenNTD = item.sTenNTD,
                    Website = item.sWebsite,
                    DiaChi = item.sDiaChi,
                    SoDienThoai = item.sSoDienThoai
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateClient(EmployerEditClient item, HttpServerUtilityBase httpServer)
        {
            try
            {
                var member = await dbContext.tbl_NhaTuyenDung.Where(x => x.PK_iMaNTD.Equals(item.MaNTD)).SingleOrDefaultAsync();
                if (member == null) return false;

                if (item.ImageMain != null && item.ImageMain.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item.ImageMain.FileName);
                    string extension = Path.GetExtension(item.ImageMain.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    item.AnhDaiDien = "/Images/NhaTuyenDung/" + fileName;
                    fileName = Path.Combine(httpServer.MapPath("~/Images/NhaTuyenDung/"), fileName);
                    item.ImageMain.SaveAs(fileName);
                    member.sAnhDaiDien = item.AnhDaiDien;
                }

                if (item.ImageCover != null && item.ImageCover.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item.ImageCover.FileName);
                    string extension = Path.GetExtension(item.ImageCover.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    item.AnhBia = "/Images/NhaTuyenDung/" + fileName;
                    fileName = Path.Combine(httpServer.MapPath("~/Images/NhaTuyenDung/"), fileName);
                    item.ImageCover.SaveAs(fileName);
                    member.sAnhBia = item.AnhBia;
                }

                member.sTenNDD = item.TenNDD;
                member.sTenNTD = item.TenNTD;
                member.sChucVuNDD = item.ChucVuNDD;
                member.sDiaChi = item.DiaChi;
                member.sWebsite = item.Website;
                member.sSoDienThoai = item.SoDienThoai;
                member.sQuyMo = item.QuyMo;
                member.sMoTa = item.MoTa;

                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResultPaging<EmployerVm>> GetList(GetListPaging paging)
        {
            IQueryable<tbl_NhaTuyenDung> model = dbContext.tbl_NhaTuyenDung;
            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.sTenNTD.Contains(paging.keyWord.Trim()));
            }

            int total = await model.CountAsync();

            var items = await model.OrderBy(x => x.PK_iMaNTD)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .Select(item => new EmployerVm()
                {
                    MaNTD = item.PK_iMaNTD,
                    DiaChi = item.sDiaChi,
                    QuyMo = item.sQuyMo,
                    SoDienThoai = item.sSoDienThoai,
                    TenNTD = item.sTenNTD,
                    Website = item.sWebsite
                }).ToListAsync();
            return new ResultPaging<EmployerVm>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public async Task<int> Create(EmployerEdit item)
        {
            try
            {
                var existAccount = await dbContext.tbl_TaiKhoan.Where(x=>x.PK_iMaTaiKhoan.Equals(item.MaNTD) 
                                    && x.FK_iMaQuyen == CommonConstants.NHA_TUYEN_DUNG).SingleOrDefaultAsync();
                if (existAccount == null) return -2;
                var check = await dbContext.tbl_NhaTuyenDung.FindAsync(item.MaNTD);
                if (check != null) return 0;
                var member = new tbl_NhaTuyenDung()
                {
                    PK_iMaNTD = item.MaNTD,
                    sTenNDD = item.TenNDD,
                    sMoTa = item.MoTa,
                    sChucVuNDD = item.ChucVuNDD,
                    sDiaChi = item.DiaChi,
                    sQuyMo = item.QuyMo,
                    sSoDienThoai = item.SoDienThoai,
                    sTenNTD = item.TenNTD,
                    sWebsite = item.Website
                };
                dbContext.tbl_NhaTuyenDung.Add(member);
                var result = await dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> Delete(int maNTD)
        {
            try
            {
                var member = await dbContext.tbl_NhaTuyenDung.FindAsync(maNTD);
                if (member == null) return false;
                dbContext.tbl_NhaTuyenDung.Remove(member);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateNTD(EmployerEdit item)
        {
            try
            {
                var member = await dbContext.tbl_NhaTuyenDung.FindAsync(item.MaNTD);
                if (member == null) return false;
                member.PK_iMaNTD = item.MaNTD;
                member.sTenNTD = item.TenNTD;
                member.sTenNDD = item.TenNDD;
                member.sChucVuNDD = item.ChucVuNDD;
                member.sSoDienThoai = item.SoDienThoai;
                member.sQuyMo = item.QuyMo;
                member.sMoTa = item.MoTa;
                member.sDiaChi = item.DiaChi;
                member.sWebsite = item.Website;
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}