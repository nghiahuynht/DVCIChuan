using GM_DAL.IServices;
using GM_DAL.Models.CaptionTeam;
using GM_DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM_DAL.Models.RouteSale;
using GM_DAL.APIModels;
using GM_DAL.APIModels.Customer;
using System.Data;

namespace GM_DAL.Services
{
    public class RouteSaleService:BaseService, IRouteSaleService
    {
        private GMDbContext db;
        public RouteSaleService(GMDbContext db)
        {
            this.db = db;
        }


        public async Task<APIResultObject<List<RouteSaleModel>>> GetRouteByTeamCode(string teamCode)
        {
            var res = new APIResultObject<List<RouteSaleModel>>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter("@TeamCode",teamCode),
                };
                ValidNullValue(param);
                res.data = await db.Database.SqlQueryRaw<RouteSaleModel>($"EXEC sp_GetRouteByTeamCode @TeamCode", param).ToListAsync();

            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
                res.data = new List<RouteSaleModel>();
            }

            return res;
        }


        public async Task<PageSizeParamModel<SearchCustomerForSalerGridModel>> SearchCustomerForMobileSaler(SearchCustomerForSalerFilterModel filter)
        {
            var res = new PageSizeParamModel<SearchCustomerForSalerGridModel>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter("@TeamCode",filter.captionTeamCode),
                        new SqlParameter("@RouteCode",filter.routeCode),
                        new SqlParameter("@SaleUserName",filter.saleUserName),
                        new SqlParameter("@CustGroupCode",filter.saleUserName),
                        new SqlParameter("@Keyword",filter.keyword),
                        new SqlParameter("@Page",filter.page),
                        new SqlParameter("@PageSize",filter.pageSize),
                        new SqlParameter { ParameterName = "@TotalRow", DbType = System.Data.DbType.Int64, Direction = System.Data.ParameterDirection.Output }
                };
                ValidNullValue(param);
                res.results = await db.Database.SqlQueryRaw<SearchCustomerForSalerGridModel>($"EXEC sp_SearchCustomerBySaler @TeamCode,@RouteCode" +
                    $",@SaleUserName,@CustGroupCode,@Keyword,@Page,@PageSize,@TotalRow OUT", param).ToListAsync();
                res.page = filter.page;
                res.pageSize = filter.pageSize;
            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
                res.results = new List<SearchCustomerForSalerGridModel>();
            }
            return res;
        }


    }
}
