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
using DoAn2023.Models.ViewModels.BaiViet;
using DoAn2023.Models.ViewModels.Common;

namespace DoAn2023.Models.DAL
{
    public class BaiVietDAL
    {
        private readonly TuyenDungDbContext dbContext;

        public BaiVietDAL()
        {
            dbContext = new TuyenDungDbContext();
        }

        public async Task<BaiVietEdit> GetById(int id)
        {
            try
            {
                var item = await dbContext.tbl_BaiViet.Where(x => x.PK_iMaBaiViet.Equals(id)).SingleOrDefaultAsync();
                if (item == null) return null;
                return new BaiVietEdit()
                {
                    MaBaiViet = item.PK_iMaBaiViet,
                    AnhChinh = item.sAnhChinh,
                    TenBaiViet = item.sTenBaiViet,
                    NoiDung = item.sNoiDung,
                    TenTacGia = GetAuthorByIdBaiViet(id),
                    TrangThai = (bool)item.bTrangThai
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BaiVietVm> GetByIdView(int id)
        {
            try
            {
                var item = await dbContext.tbl_BaiViet.Where(x => x.PK_iMaBaiViet.Equals(id)).SingleOrDefaultAsync();
                if (item == null) return null;
                return new BaiVietVm()
                {
                    MaBaiViet = item.PK_iMaBaiViet,
                    AnhChinh = item.sAnhChinh,
                    TenBaiViet = item.sTenBaiViet,
                    NoiDung = item.sNoiDung,
                    TenTacGia = GetAuthorByIdBaiViet((int)item.PK_iMaBaiViet),
                    ThoiGian = ((DateTime)(item.dThoiGian)).ToString("dd/MM/yyyy HH:mm:ss"),
                    LuotXem = item.iLuotXem
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetAuthorByIdBaiViet(int id)
        {
            try
            {
                var item = dbContext.tbl_BaiViet.Find(id);
                if (item == null) return null;
                var authorId = item.FK_iMaTaiKhoan;
                var user = dbContext.tbl_TaiKhoan.Find(authorId);
                if (user.FK_iMaQuyen == CommonConstants.NHA_TUYEN_DUNG)
                    return (dbContext.tbl_NhaTuyenDung.Find(authorId)).sTenNTD;
                else if(user.FK_iMaQuyen == CommonConstants.QUAN_TRI)
                    return "Admin";
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResultPaging<BaiVietVm> GetList(GetListPaging paging, bool trangThai, int idNguoiDung)
        {
            var model = dbContext.tbl_BaiViet.Where(x => x.bTrangThai == trangThai);
            var role = dbContext.tbl_TaiKhoan.Find(idNguoiDung).FK_iMaQuyen;
            if(role == CommonConstants.NHA_TUYEN_DUNG)
            {
                model = model.Where(x => x.FK_iMaTaiKhoan == idNguoiDung);
            }
            var listItem = model.ToList().Select(x => new BaiVietVm()
                    {
                        MaBaiViet = x.PK_iMaBaiViet,
                        AnhChinh = x.sAnhChinh,
                        TenBaiViet = x.sTenBaiViet,
                        TenTacGia = GetAuthorByIdBaiViet((int)x.PK_iMaBaiViet),
                        ThoiGian = ((DateTime)(x.dThoiGian)).ToString("dd/MM/yyyy HH:mm:ss"),
                        LuotXem = x.iLuotXem,
                        TrangThai = (bool)x.bTrangThai ? "Đã duyệt" : "Chưa được duyệt"
                    }).ToList();
            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                listItem = listItem.Where(x => x.TenBaiViet.Contains(paging.keyWord.Trim())).ToList();
            }

            int total = model.Count();

            var items = listItem.OrderBy(x => x.MaBaiViet)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<BaiVietVm>()
            {
                Items = items,
                TotalRecord = total
            };
        }
        public List<BaiVietVm> GetListItemHot(int top)
        {
            var list = dbContext.tbl_BaiViet.Where(x => x.bTrangThai == true)
                .OrderByDescending(x => x.dThoiGian).Take(top).ToList()
                .Select(x => new BaiVietVm()
                {
                    MaBaiViet = x.PK_iMaBaiViet,
                    AnhChinh = x.sAnhChinh,
                    TenBaiViet = x.sTenBaiViet,
                    TenTacGia = GetAuthorByIdBaiViet((int)x.PK_iMaBaiViet),
                    ThoiGian = ((DateTime)(x.dThoiGian)).ToString("dd/MM/yyyy HH:mm:ss"),
                    LuotXem = x.iLuotXem
                }).ToList();
            return list;
        }

        public async Task<int> Create(BaiVietCreate item, HttpServerUtilityBase httpServer, int idNguoiDung)
        {
            try
            {
                var tieude = StringHelper.ToUnsignString(item.TenBaiViet);
                var check = await dbContext.tbl_BaiViet.Where(x=>x.sTieuDe.Equals(tieude)).FirstOrDefaultAsync();
                if (check != null) return 0;

                if(item.Image != null && item.Image.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item.Image.FileName);
                    string extension = Path.GetExtension(item.Image.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    item.AnhChinh = "/Images/Blogs/" + fileName;
                    fileName = Path.Combine(httpServer.MapPath("~/Images/Blogs/"), fileName);
                    item.Image.SaveAs(fileName);
                }

                var role = (await dbContext.tbl_TaiKhoan.FindAsync(idNguoiDung)).FK_iMaQuyen;

                var member = new tbl_BaiViet()
                {
                    sTenBaiViet = item.TenBaiViet,
                    sAnhChinh = item.AnhChinh,
                    FK_iMaTaiKhoan = idNguoiDung,
                    sNoiDung = item.NoiDung,
                    sTieuDe = StringHelper.ToUnsignString(item.TenBaiViet),
                    iLuotXem = 0,
                    dThoiGian = DateTime.Now,
                    bTrangThai = role == CommonConstants.QUAN_TRI ? item.TrangThai : false
                };
                dbContext.tbl_BaiViet.Add(member);
                var result = await dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> Delete(int maBaiViet)
        {
            try
            {
                var member = await dbContext.tbl_BaiViet.FindAsync(maBaiViet);
                if (member == null) return false;
                dbContext.tbl_BaiViet.Remove(member);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> Duyet(int maBaiViet)
        {
            try
            {
                var member = await dbContext.tbl_BaiViet.FindAsync(maBaiViet);
                if (member == null) return false;
                member.bTrangThai = true;
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateBaiViet(BaiVietEdit item, HttpServerUtilityBase httpServer, int idNguoiDung)
        {
            try
            {
                var member = await dbContext.tbl_BaiViet.FindAsync(item.MaBaiViet);
                if (member == null) return false;

                if (item.Image != null && item.Image.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item.Image.FileName);
                    string extension = Path.GetExtension(item.Image.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    item.AnhChinh = "/Images/Blogs/" + fileName;
                    fileName = Path.Combine(httpServer.MapPath("~/Images/Blogs/"), fileName);
                    item.Image.SaveAs(fileName);
                    member.sAnhChinh = item.AnhChinh;
                }
                member.sTenBaiViet = item.TenBaiViet;
                member.sTieuDe = StringHelper.ToUnsignString(item.TenBaiViet);
                member.sNoiDung = item.NoiDung;

                var role = (await dbContext.tbl_TaiKhoan.FindAsync(idNguoiDung)).FK_iMaQuyen;
                if(role == CommonConstants.QUAN_TRI)
                {
                    member.bTrangThai = item.TrangThai;
                }
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateCount(int id)
        {
            var item = dbContext.tbl_BaiViet.Find(id);
            if (item == null) return;
            item.iLuotXem = item.iLuotXem + 1;
            dbContext.SaveChanges();
        }

        public int SlBaiViet(int ?idNguoiDung, bool trangThai)
        {
            try
            {
                var number = dbContext.tbl_BaiViet.Where(x=> x.bTrangThai == trangThai);
                if(idNguoiDung != null)
                {
                    return number.Where(x => x.FK_iMaTaiKhoan == idNguoiDung).Count();
                }
                return number.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}