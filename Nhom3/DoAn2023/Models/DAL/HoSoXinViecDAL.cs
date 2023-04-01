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
using DoAn2023.Models.ViewModels.HoSoXinViec;

namespace DoAn2023.Models.DAL
{
    public class HoSoXinViecDAL
    {
        private readonly TuyenDungDbContext dbContext;

        public HoSoXinViecDAL()
        {
            dbContext = new TuyenDungDbContext();
        }

        public HoSoXinViecFull GetById(int id, int idNguoiDung)
        {
            try
            {
                var hoSo = dbContext.tbl_HoSoXinViec.Where(x=>x.PK_iMaHoSo == id && x.FK_iMaUngVien == idNguoiDung).SingleOrDefault();
                var ungVien = dbContext.tbl_UngVien.Find(idNguoiDung);
                if (hoSo == null || ungVien == null) return null;
                var tinTuyenDung = new TinTuyenDungDAL();
                return new HoSoXinViecFull()
                {
                    MaHoSo = hoSo.PK_iMaHoSo,
                    MaUngVien = ungVien.PK_iMaUngVien,
                    Email = dbContext.tbl_TaiKhoan.Find(idNguoiDung).sEmail,
                    TenUngVien = ungVien.sTenUngVien,
                    SoDienThoai = ungVien.sSoDienThoai,
                    GioiTinh = ungVien.sGioiTinh,
                    NgaySinh = ungVien.dNgaySinh.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    AnhDaiDien = ungVien.sAnhDaiDien,
                    DiaChi = ungVien.sDiaChi,
                    TenHoSo = hoSo.sTenHoSo,
                    ChuyenNganh = tinTuyenDung.GetChuyenNganhByMa(hoSo.FK_sMaCN),
                    CapBac  = tinTuyenDung.GetCapBacByMa(hoSo.FK_sMaCapBac),
                    LoaiCV = tinTuyenDung.GetLoaiCVByMa(hoSo.FK_sMaLoaiCV),
                    GiaiThuong = hoSo.sGiaiThuong,
                    HocVan = hoSo.sHocVan,
                    KinhNghiem = hoSo.sKinhNghiem,
                    KyNangMem = hoSo.sKyNangMem,
                    KyNang = hoSo.sKyNang,
                    MucTieuNgheNghiep = hoSo.sMucTieuNgheNghiep
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public HoSoXinViecEdit GetByIdEdit(int id, int idNguoiDung)
        {
            try
            {
                var hoSo = dbContext.tbl_HoSoXinViec.Where(x=>x.PK_iMaHoSo == id && x.FK_iMaUngVien == idNguoiDung).SingleOrDefault();
                if (hoSo == null) return null;
                var tinTuyenDung = new TinTuyenDungDAL();
                return new HoSoXinViecEdit()
                {
                    MaHoSo = hoSo.PK_iMaHoSo,
                    TenHoSo = hoSo.sTenHoSo,
                    MaCN = hoSo.FK_sMaCN,
                    MaCapBac = hoSo.FK_sMaCapBac,
                    MaLoaiCV = hoSo.FK_sMaLoaiCV,
                    GiaiThuong = hoSo.sGiaiThuong,
                    HocVan = hoSo.sHocVan,
                    KinhNghiem = hoSo.sKinhNghiem,
                    KyNangMem = hoSo.sKyNangMem,
                    KyNang = hoSo.sKyNang,
                    MucTieuNgheNghiep = hoSo.sMucTieuNgheNghiep
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<HoSoXinViecVm> GetListByIdNguoiDung(int id)
        {
            try
            {
                var list = dbContext.tbl_HoSoXinViec.Where(x => x.FK_iMaUngVien == id).ToList()
                    .Select(x => new HoSoXinViecVm()
                    {
                        MaHoSo = x.PK_iMaHoSo,
                        TenHoSo = x.sTenHoSo,
                        GiaiThuong = x.sGiaiThuong,
                        HocVan = x.sHocVan,
                        KinhNghiem = x.sKinhNghiem,
                        KyNang = x.sKyNang,
                        KyNangMem = x.sKyNangMem,
                        MaCapBac = new TinTuyenDungDAL().GetCapBacByMa(x.FK_sMaCapBac),
                        MaCN = x.FK_sMaCN,
                        MaLoaiCV = x.FK_sMaLoaiCV,
                        MaUngVien = x.FK_iMaUngVien,
                        MucTieuNgheNghiep = x.sMucTieuNgheNghiep
                    }).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResultPaging<HoSoXinViecFull> GetListSearch(GetListPaging paging, HoSoXinViecSearch search)
        {
            IQueryable<tbl_HoSoXinViec> model = dbContext.tbl_HoSoXinViec;

            if (!string.IsNullOrEmpty(search.CapBac))
            {
                model = model.Where(x => x.FK_sMaCapBac.Equals(search.CapBac));
            }

            if (!string.IsNullOrEmpty(search.ChuyenNganh))
            {
                model = model.Where(x => x.FK_sMaCN.Equals(search.ChuyenNganh));
            }

            if (!string.IsNullOrEmpty(search.LoaiCV))
            {
                model = model.Where(x => x.FK_sMaLoaiCV.Equals(search.LoaiCV));
            }

            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.sTenHoSo.ToLower().Contains(paging.keyWord.ToLower()));
            }

            var listItem = model.ToList().Select(x => new HoSoXinViecFull()
            {
                MaHoSo = x.PK_iMaHoSo,
                MaUngVien = x.FK_iMaUngVien,
                TenUngVien = dbContext.tbl_UngVien.Find(x.FK_iMaUngVien).sTenUngVien,
                GioiTinh = dbContext.tbl_UngVien.Find(x.FK_iMaUngVien).sGioiTinh,
                NgaySinh = dbContext.tbl_UngVien.Find(x.FK_iMaUngVien).dNgaySinh.GetValueOrDefault().ToString("dd/MM/yyyy"),
                AnhDaiDien = dbContext.tbl_UngVien.Find(x.FK_iMaUngVien).sAnhDaiDien,
                Email = dbContext.tbl_TaiKhoan.Find(x.FK_iMaUngVien).sEmail,
                ChuyenNganh = new TinTuyenDungDAL().GetChuyenNganhByMa(x.FK_sMaCN),
                CapBac = new TinTuyenDungDAL().GetCapBacByMa(x.FK_sMaCapBac),
                LoaiCV = new TinTuyenDungDAL().GetLoaiCVByMa(x.FK_sMaLoaiCV)
            }).ToList();


            int total = model.Count();

            var items = listItem.OrderBy(x => x.MaHoSo)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToList();
            return new ResultPaging<HoSoXinViecFull>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public async Task<int> Create(HoSoXinViecCreate item, int idNguoiDung)
        {
            try
            {
                var check = await dbContext.tbl_HoSoXinViec.Where(x => x.FK_iMaUngVien == idNguoiDung && x.sTenHoSo.Equals(item.TenHoSo)).FirstOrDefaultAsync();
                if (check != null) return 0;
                var member = new tbl_HoSoXinViec()
                {
                    FK_iMaUngVien = idNguoiDung,
                    FK_sMaCapBac = item.MaCapBac,
                    FK_sMaCN = item.MaCN,
                    FK_sMaLoaiCV = item.MaLoaiCV,
                    sTenHoSo = item.TenHoSo,
                    sGiaiThuong = item.GiaiThuong,
                    sHocVan = item.HocVan,
                    sKinhNghiem = item.KinhNghiem,
                    sKyNang = item.KyNang,
                    sKyNangMem = item.KyNangMem,
                    sMucTieuNgheNghiep = item.MucTieuNgheNghiep
                };
                dbContext.tbl_HoSoXinViec.Add(member);
                var result = await dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> Delete(int maHS, int idNguoiDung)
        {
            try
            {
                var member = await dbContext.tbl_HoSoXinViec.Where(x=> x.FK_iMaUngVien == idNguoiDung && x.PK_iMaHoSo == maHS).SingleOrDefaultAsync();
                if (member == null) return false;
                dbContext.tbl_HoSoXinViec.Remove(member);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> Update(HoSoXinViecEdit item, int idNguoiDung)
        {
            try
            {
                var member = await dbContext.tbl_HoSoXinViec.Where(x => x.PK_iMaHoSo == item.MaHoSo && x.FK_iMaUngVien == idNguoiDung).SingleOrDefaultAsync();
                if (member == null) return -1;
                if (!member.sTenHoSo.Equals(item.TenHoSo))
                {
                    var check = await dbContext.tbl_HoSoXinViec.Where(x => x.FK_iMaUngVien == idNguoiDung && x.sTenHoSo.Equals(item.TenHoSo) && x.PK_iMaHoSo != item.MaHoSo).FirstOrDefaultAsync();
                    if(check != null)
                    {
                        return 0;
                    }
                }
                member.sTenHoSo = item.TenHoSo;
                member.FK_sMaCN = item.MaCN;
                member.FK_sMaCN = item.MaCN;
                member.FK_sMaLoaiCV = item.MaLoaiCV;
                member.FK_sMaCapBac = item.MaCapBac;
                member.sMucTieuNgheNghiep = item.MucTieuNgheNghiep;
                member.sKinhNghiem = item.KinhNghiem;
                member.sKyNang = item.KyNang;
                member.sKyNangMem = item.KyNangMem;
                member.sHocVan = item.HocVan;
                member.sGiaiThuong = item.GiaiThuong;
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public HoSoXinViecSearch DeXuatUngVien(int idNTD)
        {
            var newTTD = dbContext.tbl_TinTuyenDung.Where(x => x.FK_iMaNTD == idNTD).OrderByDescending(x => x.PK_iMaTTD).FirstOrDefault();
            if (newTTD == null) return null;
            var model = new HoSoXinViecSearch()
            {
                CapBac = newTTD.FK_sMaCapBac,
                ChuyenNganh = newTTD.FK_sMaCN,
                LoaiCV = newTTD.FK_sMaLoaiCV
            };
            return model;
        }

    }
}