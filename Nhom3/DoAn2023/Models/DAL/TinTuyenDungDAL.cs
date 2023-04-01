using DoAn2023.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DoAn2023.Areas.Admin.Models;
using DoAn2023.Models.EF;
using DoAn2023.Models.ViewModels.Common;
using DoAn2023.Models.ViewModels.TinTuyenDung;

namespace DoAn2023.Models.DAL
{
    public class TinTuyenDungDAL
    {
        private readonly TuyenDungDbContext dbContext;

        public TinTuyenDungDAL()
        {
            dbContext = new TuyenDungDbContext();
        }

        public async Task<TinTuyenDungEdit> GetById(int id)
        {
            try
            {
                var item = await dbContext.tbl_TinTuyenDung.Where(x => x.PK_iMaTTD.Equals(id)).SingleOrDefaultAsync();
                if (item == null) return null;
                return new TinTuyenDungEdit()
                {
                    MaTTD = item.PK_iMaTTD,
                    TenCongViec = item.sTenCongViec,
                    MaCN = item.FK_sMaCN,
                    MaLoaiCV = item.FK_sMaLoaiCV,
                    MaMucLuong = item.FK_sMaMucLuong,
                    MaCapBac = item.FK_sMaCapBac,
                    MaDiaChi = item.FK_sMaDiaChi,
                    DiaChiLamViec = item.sDiaChiLamViec,
                    SoLuong = item.iSoLuong,
                    GioiTinhYC = item.sGioiTinhYC,
                    MoTaCongViec = item.sMoTaCongViec,
                    YeuCauUngVien = item.sYeuCauUngVien,
                    KyNangLienQuan = item.sKyNangLienQuan,
                    QuyenLoi = item.sQuyenLoi,
                    GhiChu = item.sGhiChu,
                    HanNop = item.dHanNop.GetValueOrDefault().ToString("yyyy-MM-ddThh:mm"),
                    TrangThai = (bool)item.bTrangThai
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TinTuyenDungVmDetail> GetViewById(int id)
        {
            try
            {
                var item = await dbContext.tbl_TinTuyenDung.FindAsync(id);
                if (item == null) return null;
                var ntd = await dbContext.tbl_NhaTuyenDung.FindAsync(item.FK_iMaNTD);
                if (ntd == null) return null;
                return new TinTuyenDungVmDetail()
                {
                    MaTTD = item.PK_iMaTTD,
                    MaNTD = ntd.PK_iMaNTD,
                    TenCongViec = item.sTenCongViec,
                    MoTa = ntd.sMoTa,
                    DiaChiNTD = ntd.sDiaChi,
                    MucLuong = GetMucLuongByMa(item.FK_sMaMucLuong),
                    TenNTD = GetAuthorByIdTinTD(item.PK_iMaTTD),
                    AnhDaiDien = GetAnhDDByIdTinTD(item.PK_iMaTTD),
                    ChuyenNganh = GetChuyenNganhByMa(item.FK_sMaCN),
                    LoaiCV = GetLoaiCVByMa(item.FK_sMaLoaiCV),
                    CapBac = GetCapBacByMa(item.FK_sMaCapBac),
                    DiaChi = GetDiaChiByMa(item.FK_sMaDiaChi),
                    GioiTinhYC = item.sGioiTinhYC,
                    KyNangLienQuan = item.sKyNangLienQuan,
                    MoTaCongViec = item.sMoTaCongViec,
                    QuyenLoi = item.sQuyenLoi,
                    SoLuong = (int)item.iSoLuong,
                    YeuCauUngVien = item.sYeuCauUngVien,
                    QuyMo = ntd.sQuyMo,
                    DiaChiLamViec = item.sDiaChiLamViec,
                    HanNop = item.dHanNop.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm:ss")
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetAuthorByIdTinTD(int id)
        {
            try
            {
                var item = dbContext.tbl_TinTuyenDung.Find(id);
                if (item == null) return null;
                var authorId = item.FK_iMaNTD;
                var user = dbContext.tbl_TaiKhoan.Find(authorId);
                if (user.FK_iMaQuyen == CommonConstants.NHA_TUYEN_DUNG)
                    return (dbContext.tbl_NhaTuyenDung.Find(authorId)).sTenNTD;
                else if (user.FK_iMaQuyen == CommonConstants.QUAN_TRI)
                    return "Admin";
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetAnhDDByIdTinTD(int id)
        {
            try
            {
                var item = dbContext.tbl_TinTuyenDung.Find(id);
                if (item == null) return null;
                var authorId = item.FK_iMaNTD;
                var user = dbContext.tbl_NhaTuyenDung.Find(authorId);
                return user.sAnhDaiDien;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResultPaging<TinTuyenDungVm> GetList(bool? hetHan, GetListPaging paging, bool trangThai, int idNguoiDung)
        {
            IQueryable<tbl_TinTuyenDung> model = dbContext.tbl_TinTuyenDung;
            var role = dbContext.tbl_TaiKhoan.Find(idNguoiDung).FK_iMaQuyen;
            if (role == CommonConstants.QUAN_TRI)
            {
                model = model.Where(x => x.bTrangThai == trangThai);
            }
            else
            {
                model = model.Where(x => x.bTrangThai == trangThai && x.FK_iMaNTD == idNguoiDung);
                if (hetHan == true)
                {
                    model = model.Where(x => x.dHanNop < DateTime.Now);
                }
            }

            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.sTenCongViec.Contains(paging.keyWord.ToLower()));
            }

            var listItem = model.ToList().Select(x => new TinTuyenDungVm()
            {
                MaTTD = x.PK_iMaTTD,
                TenNTD = GetAuthorByIdTinTD(x.PK_iMaTTD),
                TenCongViec = x.sTenCongViec,
                NgayDang = ((DateTime)(x.dNgayDang)).ToString("dd/MM/yyyy HH:mm:ss"),
                HanNop = ((DateTime)(x.dHanNop)).ToString("dd/MM/yyyy HH:mm:ss"),
                LuotXem = x.iLuotXem,
                SoLuong = x.iSoLuong,
                GhiChu = x.sGhiChu,
                TrangThai = (bool)x.bTrangThai ? "Đã phê duyệt" : "Chờ phê duyệt"
            }).ToList();

            int total = model.Count();

            var items = listItem.OrderByDescending(x => x.MaTTD)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<TinTuyenDungVm>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public ResultPaging<TinTuyenDungVmHome> LayDanhSachTheoNhaTuyenDung(GetListPaging paging, int idNtd)
        {
            var model = dbContext.tbl_TinTuyenDung.Where(x=>x.FK_iMaNTD == idNtd && x.bTrangThai == true);

            var listItem = model.ToList().Select(x => new TinTuyenDungVmHome()
            {
                MaTTD = x.PK_iMaTTD,
                MaNTD = x.FK_iMaNTD,
                TenNTD = GetAuthorByIdTinTD(x.PK_iMaTTD),
                AnhDaiDien = GetAnhDDByIdTinTD(x.PK_iMaTTD),
                TenCongViec = x.sTenCongViec,
                DiaChi = GetDiaChiByMa(x.FK_sMaDiaChi),
                LoaiCV = GetLoaiCVByMa(x.FK_sMaLoaiCV),
                MucLuong = GetMucLuongByMa(x.FK_sMaMucLuong),
                NgayDang = ((DateTime)(x.dNgayDang)).ToString("dd/MM/yyyy"),
                HanNop = ((DateTime)(x.dHanNop)).ToString("dd/MM/yyyy HH:mm:ss")
            }).ToList();

            int total = model.Count();

            var items = listItem.OrderBy(x => x.MaTTD)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<TinTuyenDungVmHome>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public ResultPaging<TinTuyenDungVmHome> GetListByIdUser(GetListPaging paging, int idUser)
        {
            var user = dbContext.tbl_HoSoXinViec.Where(x => x.FK_iMaUngVien == idUser).OrderByDescending(x => x.PK_iMaHoSo).FirstOrDefault();
            if (user == null)
                return new ResultPaging<TinTuyenDungVmHome>()
                {
                    Items = null,
                    TotalRecord = 0
                };

            var model = dbContext.tbl_TinTuyenDung.Where(x=> x.bTrangThai == true && (x.FK_sMaCapBac == user.FK_sMaCapBac || x.FK_sMaCN == user.FK_sMaCN || x.FK_sMaLoaiCV == user.FK_sMaLoaiCV));

            var listItem = model.ToList().Select(x => new TinTuyenDungVmHome()
            {
                MaTTD = x.PK_iMaTTD,
                MaNTD = x.FK_iMaNTD,
                TenNTD = GetAuthorByIdTinTD(x.PK_iMaTTD),
                AnhDaiDien = GetAnhDDByIdTinTD(x.PK_iMaTTD),
                TenCongViec = x.sTenCongViec,
                DiaChi = GetDiaChiByMa(x.FK_sMaDiaChi),
                LoaiCV = GetLoaiCVByMa(x.FK_sMaLoaiCV),
                MucLuong = GetMucLuongByMa(x.FK_sMaMucLuong),
                NgayDang = ((DateTime)(x.dNgayDang)).ToString("dd/MM/yyyy"),
                HanNop = ((DateTime)(x.dHanNop)).ToString("dd/MM/yyyy HH:mm:ss")
            }).ToList();

            int total = model.Count();

            var items = listItem.OrderBy(x => x.MaTTD)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<TinTuyenDungVmHome>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public ResultPaging<TinTuyenDungVmHome> GetListSearch(GetListPaging paging, TinTuyenDungSearch search)
        {
            var model = dbContext.tbl_TinTuyenDung.Where(x => x.bTrangThai == true);

            if(!string.IsNullOrEmpty(search.CapBac))
            {
                model = model.Where(x => x.FK_sMaCapBac.Equals(search.CapBac));
            }

            if (!string.IsNullOrEmpty(search.ChuyenNganh))
            {
                model = model.Where(x => x.FK_sMaCN.Equals(search.ChuyenNganh));
            }

            if (!string.IsNullOrEmpty(search.DiaChi))
            {
                model = model.Where(x => x.FK_sMaDiaChi.Equals(search.DiaChi));
            }

            if (!string.IsNullOrEmpty(search.MucLuong))
            {
                model = model.Where(x => x.FK_sMaMucLuong.Equals(search.MucLuong));
            }
            if (!string.IsNullOrEmpty(search.LoaiCV))
            {
                model = model.Where(x => x.FK_sMaLoaiCV.Equals(search.LoaiCV));
            }

            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.sTenCongViec.ToLower().Contains(paging.keyWord.Trim()));
            }

            var listItem = model.ToList().Select(x => new TinTuyenDungVmHome()
            {
                MaTTD = x.PK_iMaTTD,
                MaNTD = x.FK_iMaNTD,
                TenNTD = GetAuthorByIdTinTD(x.PK_iMaTTD),
                AnhDaiDien = GetAnhDDByIdTinTD(x.PK_iMaTTD),
                TenCongViec = x.sTenCongViec,
                DiaChi = GetDiaChiByMa(x.FK_sMaDiaChi),
                LoaiCV = GetLoaiCVByMa(x.FK_sMaLoaiCV),
                MucLuong = GetMucLuongByMa(x.FK_sMaMucLuong),
                NgayDang = ((DateTime)(x.dNgayDang)).ToString("dd/MM/yyyy"),
                HanNop = ((DateTime)(x.dHanNop)).ToString("dd/MM/yyyy HH:mm:ss")
            }).ToList();

            int total = model.Count();

            var items = listItem.OrderBy(x => x.MaTTD)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<TinTuyenDungVmHome>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public List<TinTuyenDungVmHome> GetListItemHot(int top)
        {
            var list = dbContext.tbl_TinTuyenDung.Where(x => x.bTrangThai == true)
                .OrderByDescending(x => x.dNgayDang).Take(top).ToList()
                .Select(x=> new TinTuyenDungVmHome() 
                { 
                    MaTTD = x.PK_iMaTTD,
                    MaNTD = x.FK_iMaNTD,
                    TenNTD = GetAuthorByIdTinTD(x.PK_iMaTTD),
                    AnhDaiDien = GetAnhDDByIdTinTD(x.PK_iMaTTD),
                    TenCongViec = x.sTenCongViec,
                    DiaChi = GetDiaChiByMa(x.FK_sMaDiaChi),
                    LoaiCV = GetLoaiCVByMa(x.FK_sMaLoaiCV),
                    MucLuong = GetMucLuongByMa(x.FK_sMaMucLuong),
                    HanNop = ((DateTime)x.dHanNop).ToString("dd/MM/yyyy")
                }).ToList();
            return list;
        }

        public List<TinTuyenDungLuu> GetListSaveByIdUser(int id, string subject)
        {
            try
            {
                var list = (from a in dbContext.tbl_TinTuyenDung
                           join b in dbContext.tbl_UngTuyen
                           on a.PK_iMaTTD equals b.FK_iMaTTD
                           where b.PK_iMaUngVien == id && b.sTrangThai.Equals(subject)
                           select new 
                           { 
                                tintuyendung = a,
                                ungtuyen = b
                           }).ToList();
                var data = list.Select(x => new TinTuyenDungLuu()
                {
                    MaTTD = x.tintuyendung.PK_iMaTTD,
                    MaNTD = x.tintuyendung.FK_iMaNTD,
                    TenNTD = GetAuthorByIdTinTD(x.tintuyendung.PK_iMaTTD),
                    AnhDaiDien = GetAnhDDByIdTinTD(x.tintuyendung.PK_iMaTTD),
                    TenCongViec = x.tintuyendung.sTenCongViec,
                    DiaChi = GetDiaChiByMa(x.tintuyendung.FK_sMaDiaChi),
                    LoaiCV = GetLoaiCVByMa(x.tintuyendung.FK_sMaLoaiCV),
                    MucLuong = GetMucLuongByMa(x.tintuyendung.FK_sMaMucLuong),
                    HanNop = ((DateTime)x.tintuyendung.dHanNop).ToString("dd/MM/yyyy"),
                    NgayDang = ((DateTime)x.tintuyendung.dNgayDang).ToString("dd/MM/yyyy"),
                    NgayUngTuyen = ((DateTime)x.ungtuyen.dNgayUngTuyen).ToString("dd/MM/yyyy"),
                    TrangThai = x.ungtuyen.sTrangThai
                });
                return data.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<TinTuyenDungVm> GetListTopView(int top, int? idNguoiDung)
        {
            IEnumerable<tbl_TinTuyenDung> model = dbContext.tbl_TinTuyenDung.Where(x => x.bTrangThai == true);
            
            if (idNguoiDung != null)
            {
                model = model.Where(x => x.FK_iMaNTD == idNguoiDung);
            }
            var list = model.OrderByDescending(x => x.iLuotXem).Take(top).ToList()
                .Select(x => new TinTuyenDungVm()
                {
                    MaTTD = x.PK_iMaTTD,
                    TenNTD = GetAuthorByIdTinTD(x.PK_iMaTTD),
                    TenCongViec = x.sTenCongViec,
                    LuotXem = x.iLuotXem,
                    NgayDang = ((DateTime)x.dNgayDang).ToString("dd/MM/yyyy")
                }).ToList();
            return list;
        }

        public string GetDiaChiByMa(string maDC)
        {
            try
            {
                var item = dbContext.tbl_DiaChi.Find(maDC);
                if (item == null) return null;
                return item.sTenDiaChi;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetChuyenNganhByMa(string maCN)
        {
            try
            {
                var item = dbContext.tbl_ChuyenNganh.Find(maCN);
                if (item == null) return null;
                return item.sTenCN;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetMucLuongByMa(string maML)
        {
            try
            {
                var item = dbContext.tbl_MucLuong.Find(maML);
                if (item == null) return null;
                return item.sTenMucLuong;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetLoaiCVByMa(string maLoaiCV)
        {
            try
            {
                var item = dbContext.tbl_LoaiCongViec.Find(maLoaiCV);
                if (item == null) return null;
                return item.sTenLoaiCV;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetCapBacByMa(string maCB)
        {
            try
            {
                var item = dbContext.tbl_CapBac.Find(maCB);
                if (item == null) return null;
                return item.sTenCapBac;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> Create(TinTuyenDungCreate item, int idNguoiDung)
        {
            try
            {
                var check = await dbContext.tbl_NhaTuyenDung.FindAsync(idNguoiDung);
                if (check == null) return 0;
                var member = new tbl_TinTuyenDung()
                {
                    sTenCongViec = item.TenCongViec,
                    FK_iMaNTD = idNguoiDung,
                    FK_sMaCapBac = item.MaCapBac,
                    FK_sMaCN = item.MaCN,
                    FK_sMaDiaChi = item.MaDiaChi,
                    FK_sMaLoaiCV = item.MaLoaiCV,
                    FK_sMaMucLuong = item.MaMucLuong,
                    dNgayDang = DateTime.Now,
                    dHanNop = item.HanNop,
                    bTrangThai = false,
                    iLuotXem = 0,
                    iSoLuong = item.SoLuong,
                    sDiaChiLamViec = item.DiaChiLamViec,
                    sGioiTinhYC = item.GioiTinhYC,
                    sKyNangLienQuan = item.KyNangLienQuan,
                    sMoTaCongViec = item.MoTaCongViec,
                    sQuyenLoi = item.QuyenLoi,
                    sYeuCauUngVien = item.YeuCauUngVien
                };
                dbContext.tbl_TinTuyenDung.Add(member);
                var result = await dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "");
                return -1;
            }

            // Add Watch to fix error: ((System.Data.Entity.Validation.DbEntityValidationException)$exception).EntityValidationErrors
        }

        public async Task<bool> Delete(int maTTD)
        {
            try
            {
                var member = await dbContext.tbl_TinTuyenDung.FindAsync(maTTD);
                if (member == null) return false;
                dbContext.tbl_TinTuyenDung.Remove(member);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditTTD(TinTuyenDungEdit item, int idNguoiDung)
        {
            try
            {
                var role = dbContext.tbl_TaiKhoan.Find(idNguoiDung).FK_iMaQuyen;
                var member = await dbContext.tbl_TinTuyenDung.Where(x=> x.PK_iMaTTD == item.MaTTD).SingleOrDefaultAsync();
                if (member == null) return false;
                if(role == CommonConstants.QUAN_TRI)
                {
                    member.sGhiChu = item.GhiChu;
                    member.bTrangThai = item.TrangThai;
                }else
                {
                    member.sTenCongViec = item.TenCongViec;
                    member.FK_sMaCapBac = item.MaCapBac;
                    member.FK_sMaCN = item.MaCN;
                    member.FK_sMaDiaChi = item.MaDiaChi;
                    member.FK_sMaLoaiCV = item.MaLoaiCV;
                    member.FK_sMaMucLuong = item.MaMucLuong;
                    member.dHanNop = DateTime.Parse(item.HanNop);
                    member.iSoLuong = item.SoLuong;
                    member.sDiaChiLamViec = item.DiaChiLamViec;
                    member.sGioiTinhYC = item.GioiTinhYC;
                    member.sKyNangLienQuan = item.KyNangLienQuan;
                    member.sMoTaCongViec = item.MoTaCongViec;
                    member.sQuyenLoi = item.QuyenLoi;
                    member.sYeuCauUngVien = item.YeuCauUngVien;
                    member.bTrangThai = false;
                }

                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Duyet(int maTTD)
        {
            try
            {
                var member = await dbContext.tbl_TinTuyenDung.FindAsync(maTTD);
                if (member == null) return false;
                member.bTrangThai = true;
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateLuotXemTTD(int maTTD)
        {
            try
            {
                var member = await dbContext.tbl_TinTuyenDung.FindAsync(maTTD);
                if (member == null) return false;
                member.iLuotXem++;
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<tbl_CapBac> ListCapBac()
        {
            var list = dbContext.tbl_CapBac.ToList();
            return list;
        }

        public List<tbl_ChuyenNganh> ListChuyenNganh()
        {
            var list = dbContext.tbl_ChuyenNganh.ToList();
            return list;
        }

        public List<tbl_DiaChi> ListDiaChi()
        {
            var list = dbContext.tbl_DiaChi.ToList();
            return list;
        }

        public List<tbl_MucLuong> ListMucLuong()
        {
            var list = dbContext.tbl_MucLuong.ToList();
            return list;
        }

        public List<tbl_LoaiCongViec> ListLoaiCV()
        {
            var list = dbContext.tbl_LoaiCongViec.ToList();
            return list;
        }

        public void UpdateCount(int id)
        {
            var item = dbContext.tbl_TinTuyenDung.Find(id);
            if (item == null) return;
            item.iLuotXem = item.iLuotXem + 1;
            dbContext.SaveChanges();
        }

        public int SlTinTuyenDung(int? idNguoiDung, bool trangThai)
        {
            try
            {
                var number = dbContext.tbl_TinTuyenDung.Where(x => x.bTrangThai == trangThai);
                if (idNguoiDung != null)
                {
                    return number.Where(x => x.FK_iMaNTD == idNguoiDung).Count();
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