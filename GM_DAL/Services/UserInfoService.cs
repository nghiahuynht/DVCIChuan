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



    }
}
