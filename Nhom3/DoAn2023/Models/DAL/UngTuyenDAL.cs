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
using DoAn2023.Models.ViewModels.HoSoXinViec;
using DoAn2023.Models.ViewModels.UngTuyen;

namespace DoAn2023.Models.DAL
{
    public class UngTuyenDAL
    {
        private readonly TuyenDungDbContext dbContext;

        public UngTuyenDAL()
        {
            dbContext = new TuyenDungDbContext();
        }

        public List<UngTuyen> GetListByUser(int idNguoiDung)
        {
            try
            {
                var list = (from a in dbContext.tbl_TinTuyenDung
                            join b in dbContext.tbl_UngTuyen
                            on a.PK_iMaTTD equals b.FK_iMaTTD
                            where b.PK_iMaUngVien == idNguoiDung && b.sLinkHoSo != null
                            orderby b.dNgayUngTuyen descending
                            select new
                            {
                                tintuyendung = a,
                                ungtuyen = b
                            }).ToList();
                var data = list.Select(x => new UngTuyen()
                {
                    MaUngVien = x.ungtuyen.PK_iMaUngVien,
                    MaTTD = x.tintuyendung.PK_iMaTTD,
                    MaNTD = x.tintuyendung.FK_iMaNTD,
                    TenCongViec = x.tintuyendung.sTenCongViec,
                    AnhDaiDien = new TinTuyenDungDAL().GetAnhDDByIdTinTD(x.tintuyendung.PK_iMaTTD),
                    LinkHoSo = x.ungtuyen.sLinkHoSo,
                    TenNTD = new TinTuyenDungDAL().GetAuthorByIdTinTD(x.tintuyendung.PK_iMaTTD),
                    NgayUngTuyen = ((DateTime)x.ungtuyen.dNgayUngTuyen).ToString("dd/MM/yyyy HH:mm:ss"),
                    TrangThai = x.ungtuyen.sTrangThai
                });
                return data.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResultPaging<UngTuyenVm> GetListByNTD(bool status, int month, GetListPaging paging, int idNguoiDung)
        {
            var model = from a in dbContext.tbl_UngTuyen
                        join b in dbContext.tbl_TinTuyenDung
                        on a.FK_iMaTTD equals b.PK_iMaTTD
                        where b.FK_iMaNTD == idNguoiDung 
                        select new 
                        {
                            UngTuyen = a,
                            TinTuyenDung = b
                        };
            if(!status)
            {
                model = model.Where(x => x.UngTuyen.sTrangThai == TrangThaiUngTuyen.CHUAXEM);
            }
            else
            {
                model = model.Where(x => x.UngTuyen.sTrangThai != TrangThaiUngTuyen.CHUAXEM && x.UngTuyen.sTrangThai != TrangThaiUngTuyen.DALUU);
            }

            if(month > 0)
            {
                model = model.Where(x => x.TinTuyenDung.dNgayDang.Value.Month == month);
            }

            var listItem = model.ToList().Select(x => new UngTuyenVm()
            {
                MaTTD = x.UngTuyen.FK_iMaTTD,
                MaUngVien = x.UngTuyen.PK_iMaUngVien,
                LinkHoSo = x.UngTuyen.sLinkHoSo,
                NgayUngTuyen = ((DateTime)(x.UngTuyen.dNgayUngTuyen)).ToString("dd/MM/yyyy HH:mm:ss"),
                TenCongViec = x.TinTuyenDung.sTenCongViec,
                TenUngVien = GetNameByIdUngVien(x.UngTuyen.PK_iMaUngVien),
                TrangThai = x.UngTuyen.sTrangThai
            }).ToList();

            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                listItem = listItem.Where(x => x.TenCongViec.ToUpper().Contains(paging.keyWord.ToUpper().Trim()) 
                || x.TrangThai.ToUpper().Contains(paging.keyWord.ToUpper().Trim())).ToList();
            }

            int total = model.Count();

            var items = listItem.OrderBy(x => x.MaTTD)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<UngTuyenVm>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public UngTuyenVm GetItem(int maUV, int maTTD)
        {
            var model = (from a in dbContext.tbl_UngTuyen
                        join b in dbContext.tbl_TinTuyenDung
                        on a.FK_iMaTTD equals b.PK_iMaTTD
                        where a.PK_iMaUngVien == maUV && a.FK_iMaTTD == maTTD
                        select new 
                        {
                            UngTuyen = a,
                            TinTuyenDung = b
                        }).FirstOrDefault();

            var item = new UngTuyenVm()
            {
                MaTTD = model.UngTuyen.FK_iMaTTD,
                MaUngVien = model.UngTuyen.PK_iMaUngVien,
                LinkHoSo = model.UngTuyen.sLinkHoSo,
                NgayUngTuyen = ((DateTime)(model.UngTuyen.dNgayUngTuyen)).ToString("dd/MM/yyyy HH:mm:ss"),
                TenCongViec = model.TinTuyenDung.sTenCongViec,
                TenUngVien = GetNameByIdUngVien(model.UngTuyen.PK_iMaUngVien),
                TrangThai = model.UngTuyen.sTrangThai
            };
            return item;
        }

        public string GetNameByIdUngVien(int id)
        {
            return dbContext.tbl_UngVien.Find(id).sTenUngVien;
        }

        public int UngTuyenOnline(int maUV, int maTTD, string link)
        {
            try
            {
                var check = dbContext.tbl_UngTuyen.Where(x => x.PK_iMaUngVien == maUV && x.FK_iMaTTD == maTTD).FirstOrDefault();
                if (check != null && check.sLinkHoSo != null) return 0;
                if(check == null)
                {
                    var item = new tbl_UngTuyen()
                    {
                        PK_iMaUngVien = maUV,
                        FK_iMaTTD = maTTD,
                        dNgayUngTuyen = DateTime.Now,
                        sLinkHoSo = link,
                        sTrangThai = TrangThaiUngTuyen.CHUAXEM
                    };
                    dbContext.tbl_UngTuyen.Add(item);
                }
                else
                {
                    check.dNgayUngTuyen = DateTime.Now;
                    check.sLinkHoSo = link;
                    check.sTrangThai = TrangThaiUngTuyen.CHUAXEM;
                }
                return dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "");
                return -1;
            }
        }

        public int UngTuyenFile(int maUV, int maTTD, HttpPostedFileBase file, HttpServerUtilityBase httpServer)
        {
            try
            {
                var check = dbContext.tbl_UngTuyen.Where(x => x.PK_iMaUngVien == maUV && x.FK_iMaTTD == maTTD).FirstOrDefault();
                if (check != null && check.sLinkHoSo != null) return 0;

                string link = "";
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    link = "/Files/CV/" + fileName;
                    fileName = Path.Combine(httpServer.MapPath("~/Files/CV/"), fileName);
                    file.SaveAs(fileName);
                }

                if(check == null)
                {
                    var item = new tbl_UngTuyen()
                    {
                        PK_iMaUngVien = maUV,
                        FK_iMaTTD = maTTD,
                        dNgayUngTuyen = DateTime.Now,
                        sLinkHoSo = link,
                        sTrangThai = TrangThaiUngTuyen.CHUAXEM
                    };
                    dbContext.tbl_UngTuyen.Add(item);
                }else
                {
                    check.sLinkHoSo = link;
                    check.dNgayUngTuyen = DateTime.Now;
                    check.sTrangThai = TrangThaiUngTuyen.CHUAXEM;
                }
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int Save(int maUV, int maTTD)
        {
            var result = dbContext.tbl_UngTuyen.Where(x => x.PK_iMaUngVien == maUV && x.FK_iMaTTD == maTTD).FirstOrDefault();
            if (result != null)
            {
                if(result.sTrangThai == TrangThaiUngTuyen.DALUU)
                {
                    dbContext.tbl_UngTuyen.Remove(result);
                    dbContext.SaveChanges();
                    return 0;
                }else
                {
                    return -1;
                }
            }
            var item = new tbl_UngTuyen()
            {
                PK_iMaUngVien = maUV,
                FK_iMaTTD = maTTD,
                dNgayUngTuyen = DateTime.Now,
                sTrangThai = TrangThaiUngTuyen.DALUU
            };
            dbContext.tbl_UngTuyen.Add(item);
            return dbContext.SaveChanges();
        }

        public int GetStatus(int maUV, int maTTD)
        {
            var result = dbContext.tbl_UngTuyen.Where(x => x.PK_iMaUngVien == maUV && x.FK_iMaTTD == maTTD).FirstOrDefault();
            if (result == null) return 0;
            if(result.sTrangThai == TrangThaiUngTuyen.DALUU)
            {
                return 1;
            }
            return -1;
        }

        public async Task<string> UpdateStatus(int maUV, int maTTD)
        {
            try
            {
                var item = await dbContext.tbl_UngTuyen.Where(x => x.PK_iMaUngVien == maUV && x.FK_iMaTTD == maTTD).SingleOrDefaultAsync();
                if(item.sTrangThai == TrangThaiUngTuyen.CHUAXEM)
                {
                    item.sTrangThai = TrangThaiUngTuyen.DAXEM;
                    await dbContext.SaveChangesAsync();
                }
                return item.sLinkHoSo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateStatus(int maUV, int maTTD, string trangThai)
        {
            try
            {
                var item = await dbContext.tbl_UngTuyen.Where(x => x.PK_iMaUngVien == maUV && x.FK_iMaTTD == maTTD).SingleOrDefaultAsync();
                item.sTrangThai = trangThai;
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int SlUngTuyen(int maNTD)
        {
            try
            {
                var query = from a in dbContext.tbl_UngTuyen
                            join b in dbContext.tbl_TinTuyenDung
                            on a.FK_iMaTTD equals b.PK_iMaTTD
                            where b.FK_iMaNTD == maNTD && a.sTrangThai != TrangThaiUngTuyen.DALUU
                            select a.PK_iMaUngVien;
                return query.Count();

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int SlUngTuyen(int maNTD, int thang)
        {
            try
            {
                var query = from a in dbContext.tbl_UngTuyen
                            join b in dbContext.tbl_TinTuyenDung
                            on a.FK_iMaTTD equals b.PK_iMaTTD
                            where b.FK_iMaNTD == maNTD && a.sTrangThai != TrangThaiUngTuyen.DALUU && a.dNgayUngTuyen.Value.Month == thang
                            select a.PK_iMaUngVien;
                return query.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int SlNhanViec(int thang)
        {
            try
            {
                var number = dbContext.tbl_UngTuyen.Where(x => x.sTrangThai == TrangThaiUngTuyen.CHAPNHAN 
                            && x.dNgayUngTuyen.Value.Month == thang).Count();
                return number;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}