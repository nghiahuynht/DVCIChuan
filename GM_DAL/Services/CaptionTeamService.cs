using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.CaptionTeam;
using GM_DAL.Models.User;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Services
{
    public class CaptionTeamService:BaseService,ICaptionTeamService
    {

        private GMDbContext db;
        public CaptionTeamService(GMDbContext db)
        {
            this.db = db;
        }


        public async Task<APIResultObject<List<CaptionTeamModel>>> GetTeamBySaleUser(string saleUserName)
        {
            var res = new APIResultObject<List<CaptionTeamModel>>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter("@SaleUserName",saleUserName),
                };
                ValidNullValue(param);
                res.data = await db.Database.SqlQueryRaw<CaptionTeamModel>($"EXEC sp_GetTeamBySaleUser @SaleUserName", param).ToListAsync();

            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
                res.data = new List<CaptionTeamModel>();
            }

            return res;
        }



    }
}
