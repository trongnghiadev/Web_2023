using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DoAn2023.Areas.Admin.Models;
using DoAn2023.Common;
using DoAn2023.Models.EF;
using DoAn2023.Models.ViewModels.Common;
using DoAn2023.Models.ViewModels.DanhMuc;

namespace DoAn2023.Models.DAL
{
    public class DanhMucDAL
    {
        private TuyenDungDbContext dbContext;

        public DanhMucDAL()
        {
            dbContext = new TuyenDungDbContext();
        }

        public ResultPaging<ChuyenNganh> LayDanhSachChuyenNganh(GetListPaging paging)
        {
            var model = dbContext.tbl_ChuyenNganh.ToList()
                .Select(x => new ChuyenNganh()
                {
                    MaCN = x.PK_sMaCN,
                    TenCN = x.sTenCN
                });
            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.MaCN.Contains(paging.keyWord.Trim()) || x.TenCN.Contains(paging.keyWord.Trim()));
            }

            int total = model.Count();

            var items = model.OrderBy(x => x.MaCN)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<ChuyenNganh>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public async Task<int> CreateChuyenNganh(ChuyenNganh item)
        {
            try
            {
                item.MaCN = StringHelper.ToUnsignString(item.TenCN);
                var check = await dbContext.tbl_ChuyenNganh.FindAsync(item.MaCN);
                if (check != null) return 0;
                var member = new tbl_ChuyenNganh()
                {
                    PK_sMaCN = item.MaCN.ToLower(),
                    sTenCN = item.TenCN
                };
                dbContext.tbl_ChuyenNganh.Add(member);
                return await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                return -1;
            }
        }

        public async Task<bool> EditChuyenNganh(ChuyenNganh item)
        {
            try
            {
                var check = await dbContext.tbl_ChuyenNganh.FindAsync(item.MaCN);
                if (check == null) return false;
                dbContext.tbl_ChuyenNganh.Remove(check);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    var member = new tbl_ChuyenNganh()
                    {
                        PK_sMaCN = StringHelper.ToUnsignString(item.TenCN).ToLower(),
                        sTenCN = item.TenCN
                    };
                    dbContext.tbl_ChuyenNganh.Add(member);
                    return await dbContext.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteChuyenNganh(string maCN)
        {
            try
            {
                var check = await dbContext.tbl_ChuyenNganh.FindAsync(maCN);
                if (check == null) return false;
                dbContext.tbl_ChuyenNganh.Remove(check);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ResultPaging<CapBac> GetListCapBac(GetListPaging paging)
        {
            var model = dbContext.tbl_CapBac.ToList()
                .Select(x => new CapBac()
                {
                    MaCapBac = x.PK_sMaCapBac,
                    TenCapBac = x.sTenCapBac
                });
            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.MaCapBac.Contains(paging.keyWord.Trim()) || x.TenCapBac.Contains(paging.keyWord.Trim()));
            }

            int total = model.Count();

            var items = model.OrderBy(x => x.MaCapBac)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<CapBac>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public async Task<int> CreateCapBac(CapBac item)
        {
            try
            {
                item.MaCapBac = StringHelper.ToUnsignString(item.TenCapBac);
                var check = await dbContext.tbl_ChuyenNganh.FindAsync(item.MaCapBac);
                if (check != null) return 0;
                var member = new tbl_CapBac()
                {
                    PK_sMaCapBac = item.MaCapBac.ToLower(),
                    sTenCapBac = item.TenCapBac
                };
                dbContext.tbl_CapBac.Add(member);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> EditCapBac(CapBac item)
        {
            try
            {
                var check = await dbContext.tbl_CapBac.FindAsync(item.MaCapBac);
                if (check == null) return false;
                dbContext.tbl_CapBac.Remove(check);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    var member = new tbl_CapBac()
                    {
                        PK_sMaCapBac = StringHelper.ToUnsignString(item.TenCapBac).ToLower(),
                        sTenCapBac = item.TenCapBac
                    };
                    dbContext.tbl_CapBac.Add(member);
                    return await dbContext.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCapBac(string maCB)
        {
            try
            {
                var check = await dbContext.tbl_CapBac.FindAsync(maCB);
                if (check == null) return false;
                dbContext.tbl_CapBac.Remove(check);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ResultPaging<DiaChi> GetListDiaChi(GetListPaging paging)
        {
            var model = dbContext.tbl_DiaChi.ToList()
                .Select(x => new DiaChi()
                {
                    MaDiaChi = x.PK_sMaDiaChi,
                    TenDiaChi = x.sTenDiaChi
                });
            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.MaDiaChi.Contains(paging.keyWord.Trim()) || x.TenDiaChi.Contains(paging.keyWord.Trim()));
            }

            int total = model.Count();

            var items = model.OrderBy(x => x.MaDiaChi)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<DiaChi>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public async Task<int> CreateDiaChi(DiaChi item)
        {
            try
            {
                item.MaDiaChi = StringHelper.ToUnsignString(item.TenDiaChi);
                var check = await dbContext.tbl_DiaChi.FindAsync(item.MaDiaChi);
                if (check != null) return 0;
                var member = new tbl_DiaChi()
                {
                    PK_sMaDiaChi = item.MaDiaChi.ToLower(),
                    sTenDiaChi = item.TenDiaChi
                };
                dbContext.tbl_DiaChi.Add(member);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> EditDiaChi(DiaChi item)
        {
            try
            {
                var check = await dbContext.tbl_DiaChi.FindAsync(item.MaDiaChi);
                if (check == null) return false;
                dbContext.tbl_DiaChi.Remove(check);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    var member = new tbl_DiaChi()
                    {
                        PK_sMaDiaChi = StringHelper.ToUnsignString(item.TenDiaChi).ToLower(),
                        sTenDiaChi = item.TenDiaChi
                    };
                    dbContext.tbl_DiaChi.Add(member);
                    return await dbContext.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDiaChi(string maDC)
        {
            try
            {
                var check = await dbContext.tbl_DiaChi.FindAsync(maDC);
                if (check == null) return false;
                dbContext.tbl_DiaChi.Remove(check);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ResultPaging<LoaiCongViec> GetListLoaiCV(GetListPaging paging)
        {
            var model = dbContext.tbl_LoaiCongViec.ToList()
                .Select(x => new LoaiCongViec()
                {
                    MaLoaiCV = x.PK_sMaLoaiCV,
                    TenLoaiCV = x.sTenLoaiCV
                });
            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.MaLoaiCV.Contains(paging.keyWord.Trim()) || x.TenLoaiCV.Contains(paging.keyWord.Trim()));
            }

            int total = model.Count();

            var items = model.OrderBy(x => x.MaLoaiCV)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<LoaiCongViec>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public async Task<int> CreateLoaiCV(LoaiCongViec item)
        {
            try
            {
                item.MaLoaiCV = StringHelper.ToUnsignString(item.TenLoaiCV);
                var check = await dbContext.tbl_LoaiCongViec.FindAsync(item.MaLoaiCV);
                if (check != null) return 0;
                var member = new tbl_LoaiCongViec()
                {
                    PK_sMaLoaiCV = item.MaLoaiCV.ToLower(),
                    sTenLoaiCV = item.TenLoaiCV
                };
                dbContext.tbl_LoaiCongViec.Add(member);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> EditLoaiCV(LoaiCongViec item)
        {
            try
            {
                var check = await dbContext.tbl_LoaiCongViec.FindAsync(item.MaLoaiCV);
                if (check == null) return false;
                dbContext.tbl_LoaiCongViec.Remove(check);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    var member = new tbl_LoaiCongViec()
                    {
                        PK_sMaLoaiCV = StringHelper.ToUnsignString(item.TenLoaiCV).ToLower(),
                        sTenLoaiCV = item.TenLoaiCV
                    };
                    dbContext.tbl_LoaiCongViec.Add(member);
                    return await dbContext.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteLoaiCV(string maLoaiCV)
        {
            try
            {
                var check = await dbContext.tbl_LoaiCongViec.FindAsync(maLoaiCV);
                if (check == null) return false;
                dbContext.tbl_LoaiCongViec.Remove(check);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ResultPaging<MucLuong> GetListMucLuong(GetListPaging paging)
        {
            var model = dbContext.tbl_MucLuong.ToList()
                .Select(x => new MucLuong()
                {
                    MaMucLuong = x.PK_sMaMucLuong,
                    TenMucLuong = x.sTenMucLuong
                });
            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.MaMucLuong.Contains(paging.keyWord.Trim()) || x.TenMucLuong.Contains(paging.keyWord.Trim()));
            }

            int total = model.Count();

            var items = model.OrderBy(x => x.MaMucLuong)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<MucLuong>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public async Task<int> CreateMucLuong(MucLuong item)
        {
            try
            {
                item.MaMucLuong = StringHelper.ToUnsignString(item.TenMucLuong);
                var check = await dbContext.tbl_MucLuong.FindAsync(item.MaMucLuong);
                if (check != null) return 0;
                var member = new tbl_MucLuong()
                {
                    PK_sMaMucLuong = item.MaMucLuong.ToLower(),
                    sTenMucLuong = item.TenMucLuong
                };
                dbContext.tbl_MucLuong.Add(member);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> EditMucLuong(MucLuong item)
        {
            try
            {
                var check = await dbContext.tbl_MucLuong.FindAsync(item.MaMucLuong);
                if (check == null) return false;
                dbContext.tbl_MucLuong.Remove(check);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    var member = new tbl_MucLuong()
                    {
                        PK_sMaMucLuong = StringHelper.ToUnsignString(item.TenMucLuong).ToLower(),
                        sTenMucLuong = item.TenMucLuong
                    };
                    dbContext.tbl_MucLuong.Add(member);
                    return await dbContext.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMucLuong(string maML)
        {
            try
            {
                var check = await dbContext.tbl_MucLuong.FindAsync(maML);
                if (check == null) return false;
                dbContext.tbl_MucLuong.Remove(check);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}