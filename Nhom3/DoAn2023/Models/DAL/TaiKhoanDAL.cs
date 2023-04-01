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
using DoAn2023.Models.ViewModels.User;

namespace DoAn2023.Models.DAL
{
    public class TaiKhoanDAL
    {
        private readonly TuyenDungDbContext dbContext;

        public TaiKhoanDAL()
        {
            dbContext = new TuyenDungDbContext();
        }

        public async Task<UserEdit> GetById(int id)
        {
            try
            {
                return await (from a in dbContext.tbl_TaiKhoan
                              join b in dbContext.tbl_Quyen
                              on a.FK_iMaQuyen equals b.PK_iMaQuyen
                              where a.PK_iMaTaiKhoan == id
                              select new UserEdit()
                              {
                                  Id = a.PK_iMaTaiKhoan,
                                  Email = a.sEmail,
                                  NgayTao = (DateTime)a.dNgayTao,
                                  idQuyen = (int)a.FK_iMaQuyen,
                                  TrangThai = (bool)a.bTrangThai
                              }).SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AdminLogin> LayTaiKhoanAdminTheoEmail(string email)
        {
            try
            {
                return await dbContext.tbl_TaiKhoan
                    .Where(a => a.sEmail == email && a.FK_iMaQuyen == CommonConstants.QUAN_TRI)
                    .Select(a => new AdminLogin()
                    {
                        Id = a.PK_iMaTaiKhoan,
                        Email = a.sEmail,
                        Role = "Quản trị viên"
                    }).SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserLogin> GetByEmail_UngVien(string email)
        {
            try
            {
                return await (from a in dbContext.tbl_TaiKhoan
                              join b in dbContext.tbl_Quyen
                              on a.FK_iMaQuyen equals b.PK_iMaQuyen
                              join c in dbContext.tbl_UngVien
                              on a.PK_iMaTaiKhoan equals c.PK_iMaUngVien
                              where a.sEmail == email
                              select new UserLogin()
                              {
                                  Id = a.PK_iMaTaiKhoan,
                                  Email = a.sEmail,
                                  Name = c.sTenUngVien,
                                  Role = b.sTenQuyen
                              }).SingleOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserLogin> GetByEmail_NhaTuyenDung(string email)
        {
            try
            {
                return await (from a in dbContext.tbl_TaiKhoan
                              join b in dbContext.tbl_Quyen
                              on a.FK_iMaQuyen equals b.PK_iMaQuyen
                              join c in dbContext.tbl_NhaTuyenDung
                              on a.PK_iMaTaiKhoan equals c.PK_iMaNTD
                              where a.sEmail == email
                              select new UserLogin()
                              {
                                  Id = a.PK_iMaTaiKhoan,
                                  Email = a.sEmail,
                                  Name = c.sTenNTD,
                                  Role = b.sTenQuyen
                              }).SingleOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> Login(string email, string passWord, int maQuyen)
        {
            try
            {
                var user = await dbContext.tbl_TaiKhoan
                    .Where(x => x.sEmail == email && x.FK_iMaQuyen == maQuyen)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return -1;
                }

                if (user.bTrangThai == true)
                {
                    if (user.sMatKhau == Encryptor.MD5Hash(passWord))
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<ResultPaging<UserVm>> GetList(GetListPaging paging)
        {
            var model = from a in dbContext.tbl_TaiKhoan
                        join b in dbContext.tbl_Quyen
                        on a.FK_iMaQuyen equals b.PK_iMaQuyen
                        select new UserVm()
                        {
                            Id = a.PK_iMaTaiKhoan,
                            Email = a.sEmail,
                            NgayTao = a.dNgayTao.ToString(),
                            Quyen = b.sTenQuyen,
                            TrangThai = (bool)a.bTrangThai
                        };
            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.Email.Contains(paging.keyWord.Trim()));
            }

            int total = await model.CountAsync();

            var items = await model.OrderBy(x => x.Id)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToListAsync();
            return new ResultPaging<UserVm>()
            {
                Items = items,
                TotalRecord = total
            };
        }

        public async Task<int> Register_UngVien(ViewModels.User.RegisterModel model)
        {
            try
            {
                var check = await dbContext.tbl_TaiKhoan.Where(x => x.sEmail.Equals(model.register_email.Trim())).SingleOrDefaultAsync();
                if (check != null) return 0;
                var account = new tbl_TaiKhoan()
                {
                    sEmail = model.register_email,
                    sMatKhau = Encryptor.MD5Hash(model.register_password),
                    FK_iMaQuyen = CommonConstants.UNG_VIEN,
                    dNgayTao = DateTime.Now,
                    bTrangThai = true
                };
                dbContext.tbl_TaiKhoan.Add(account);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    var member = new tbl_UngVien()
                    {
                        PK_iMaUngVien = account.PK_iMaTaiKhoan,
                        sTenUngVien = model.register_name
                    };
                    dbContext.tbl_UngVien.Add(member);
                    return await dbContext.SaveChangesAsync();
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> Register_NhaTuyenDung(ViewModels.Employer.RegisterModel model)
        {
            try
            {
                var check = await dbContext.tbl_TaiKhoan.Where(x => x.sEmail.Equals(model.Email.Trim())).SingleOrDefaultAsync();
                if (check != null) return 0;
                var account = new tbl_TaiKhoan()
                {
                    sEmail = model.Email,
                    sMatKhau = Encryptor.MD5Hash(model.Password),
                    FK_iMaQuyen = CommonConstants.NHA_TUYEN_DUNG,
                    dNgayTao = DateTime.Now,
                    bTrangThai = true
                };
                dbContext.tbl_TaiKhoan.Add(account);
                var result = await dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    var member = new tbl_NhaTuyenDung()
                    {
                        PK_iMaNTD = account.PK_iMaTaiKhoan,
                        sTenNDD = model.Name,
                        sTenNTD = model.Company_Name,
                        sSoDienThoai = model.Phone,
                        sDiaChi = model.Address
                    };
                    dbContext.tbl_NhaTuyenDung.Add(member);
                    return await dbContext.SaveChangesAsync();
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> Create(UserCreate user)
        {
            try
            {
                var check = await dbContext.tbl_TaiKhoan.Where(x => x.sEmail.Equals(user.Email.Trim())).SingleOrDefaultAsync();
                if (check != null) return 0;
                var account = new tbl_TaiKhoan()
                {
                    sEmail = user.Email,
                    sMatKhau = Encryptor.MD5Hash(user.Password),
                    FK_iMaQuyen = user.idQuyen,
                    dNgayTao = DateTime.Now,
                    bTrangThai = user.TrangThai
                };
                dbContext.tbl_TaiKhoan.Add(account);
                var result = await dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> Delete(int idTaiKhoan)
        {
            try
            {
                var user = await dbContext.tbl_TaiKhoan.FindAsync(idTaiKhoan);
                if (user == null) return false;
                dbContext.tbl_TaiKhoan.Remove(user);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateRole(UserRole userRole)
        {
            try
            {
                var user = await dbContext.tbl_TaiKhoan.FindAsync(userRole.Id);
                if (user == null) return false;
                user.FK_iMaQuyen = userRole.idQuyen;
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(UserEdit userEdit)
        {
            try
            {
                var user = await dbContext.tbl_TaiKhoan.FindAsync(userEdit.Id);
                if (user == null) return false;
                user.FK_iMaQuyen = userEdit.idQuyen;
                user.bTrangThai = userEdit.TrangThai;
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int UpdatePass(UserPassword item, int idUser)
        {
            try
            {
                var user = dbContext.tbl_TaiKhoan.Find(idUser);
                if (user == null) return -1;
                if (Encryptor.MD5Hash(item.MatKhauCu) == user.sMatKhau)
                {
                    user.sMatKhau = Encryptor.MD5Hash(item.MatKhauMoi);
                    return dbContext.SaveChanges();
                }
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<List<tbl_Quyen>> GetAllRole()
        {
            var roles = await dbContext.tbl_Quyen.ToListAsync();
            return roles;
        }

        public async Task<UserRole> GetRoleById(int id)
        {
            try
            {
                return await (from a in dbContext.tbl_TaiKhoan
                              join b in dbContext.tbl_Quyen
                              on a.FK_iMaQuyen equals b.PK_iMaQuyen
                              where a.PK_iMaTaiKhoan == id
                              select new UserRole()
                              {
                                  Id = a.PK_iMaTaiKhoan,
                                  Email = a.sEmail,
                                  idQuyen = (int)a.FK_iMaQuyen
                              }).SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int SlUngVien()
        {
            try
            {
                var number = dbContext.tbl_TaiKhoan.Where(x => x.FK_iMaQuyen == CommonConstants.UNG_VIEN).Count();
                return number;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int SlNhaTuyenDung()
        {
            try
            {
                var number = dbContext.tbl_TaiKhoan.Where(x => x.FK_iMaQuyen == CommonConstants.NHA_TUYEN_DUNG).Count();
                return number;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}