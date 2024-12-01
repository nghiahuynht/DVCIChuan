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
                        new SqlParameter("@FullName",model.fullName),
                        new SqlParameter("@Phone",model.phone),
                        new SqlParameter("@Email",model.email),
                        new SqlParameter("@Title",model.title),
                        new SqlParameter("@RoleCode",model.roleCode),
                        new SqlParameter("@IsActive",model.isActive),
                        new SqlParameter("@UserName",userName),
                       
                };
                ValidNullValue(param);
                var resExcute = db.Database.SqlQueryRaw<ResCommon>($"EXEC sp_SaveUserInfo @Id,@Code,@LoginName,@FullName,@Phone,@Email,@Title,@RoleCode,@IsActive,@UserName", param).ToList();
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
                        new SqlParameter("@UserId",id),
                };
                ValidNullValue(param);
                var resExcute = db.Database.SqlQueryRaw<UserInfoModel>($"EXEC sp_GetUserInfoById @UserId", param).ToList();
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

        public async Task<DataTableResultModel<UserInfoModel>> SearchUserAccount(SearchUserFilterModel filter)
        {
            var res = new DataTableResultModel<UserInfoModel>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter("@RoleCode",filter.roleCode),
                        new SqlParameter("@Status",filter.status),
                        new SqlParameter("@Keyword", filter.keyword),
                        new SqlParameter("@Start", filter.start),
                        new SqlParameter("@Length", filter.length),
                        new SqlParameter { ParameterName = "@TotalRow", DbType = System.Data.DbType.Int16, Direction = System.Data.ParameterDirection.Output }
                };
                ValidNullValue(param);
                res.data = await db.Database.SqlQueryRaw<UserInfoModel>($"EXEC sp_SearchUserInfo @RoleCode,@Status,@Keyword,@Start,@Length,@TotalRow OUT", param).ToListAsync();
                res.recordsTotal = Convert.ToInt16(param[param.Length - 1].Value);
                res.recordsFiltered = res.recordsTotal;

            }
            catch (Exception ex)
            {
                res.data = new List<UserInfoModel>();
            }


            return res;
        }

    }
}
