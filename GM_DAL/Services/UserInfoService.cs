using GM_DAL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM_DAL.Models;
using GM_DAL.Models.User;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GM_DAL.Services
{
    public class UserInfoService: BaseService, IUserInfoService
    {
        private GMDbContext db;
        public UserInfoService(GMDbContext db)
        {
            this.db = db;
        }


        public APIResultObject<AuthenSuccessModel> Login(string userName,string pass)
        {
            var res = new APIResultObject<AuthenSuccessModel>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter("@UserName",userName),
                        new SqlParameter("@Pass",pass)
                };
                ValidNullValue(param);
                var resExcute = db.Database.SqlQueryRaw<AuthenSuccessModel>($"EXEC sp_Login @UserName,@Pass", param).ToList();
                if (resExcute != null && resExcute.Any())
                {
                    res.data = resExcute.FirstOrDefault();
                }
                
            }
            catch(Exception ex)
            {
                res.message.exMessage = ex.Message;
            }

            return res;
        }



        public async Task<APIResultObject<List<MenuModel>>> GetMenuByRole(string role)
        {
            var res = new APIResultObject<List<MenuModel>>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter("@RoleCode",role),
 
                };
                ValidNullValue(param);
                res.data = await db.Database.SqlQueryRaw<MenuModel>($"EXEC sp_GetMenuByRole @RoleCode", param).ToListAsync();
                

            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }

            return res;
        }



        public APIResultObject<ResCommon> SaveUserInfo(UserInfoModel model,string userName)
        {
            var res = new APIResultObject<ResCommon>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter("@Id",model.id),
                        new SqlParameter("@Code",model.code),
                        new SqlParameter("@LoginName",model.loginName),
                        new SqlParameter("@Phone",model.phone),
                        new SqlParameter("@Email",model.email),
                        new SqlParameter("@Title",model.title),
                        new SqlParameter("@RoleCode",model.roleCode),
                        new SqlParameter("@IsActive",model.isActive),
                };
                ValidNullValue(param);
                var resExcute = db.Database.SqlQueryRaw<ResCommon>($"EXEC sp_SaveUserInfo @Id,@Code,@LoginName,@Phone,@Email,@Title,@RoleCode,@IsActive", param).ToList();
                if (resExcute != null && resExcute.Any())
                {
                    res.data = resExcute.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }


            return res;
        }



        public APIResultObject<UserInfoModel> GetUserById(int id)
        {
            var res = new APIResultObject<UserInfoModel>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter("@Id",id),
                };
                ValidNullValue(param);
                var resExcute = db.Database.SqlQueryRaw<UserInfoModel>($"EXEC sp_GetUserInfoByUd @Id", param).ToList();
                if (resExcute != null && resExcute.Any())
                {
                    res.data = resExcute.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }


            return res;
        }



    }
}
