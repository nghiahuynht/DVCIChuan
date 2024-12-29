using Dapper;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.CaptionTeam;
using GM_DAL.Models.ComInfo;
using GM_DAL.Models.User;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using GM_DAL;

namespace GM_DAL.Services
{
    public class CaptionTeamService:BaseService,ICaptionTeamService
    {

        private GMDbContext db;
        private SQLAdoContext adoContext;
        public CaptionTeamService(GMDbContext db, SQLAdoContext adoContext)
        {
            this.db = db;
            this.adoContext = adoContext;
        }


        public async Task<APIResultObject<List<CaptionTeamModel>>> GetTeamBySaleUser(string saleUserName)
        {
            var res = new APIResultObject<List<CaptionTeamModel>>();
            try
            {
                

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SaleUserName", CommonHelper.CheckStringNull(saleUserName));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<CaptionTeamModel>("sp_GetConnectInfoByComCode", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }
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
