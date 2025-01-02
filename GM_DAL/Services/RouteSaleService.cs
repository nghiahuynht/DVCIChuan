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
using Dapper;
using GM_DAL.Models.Customer;

namespace GM_DAL.Services
{
    public class RouteSaleService:BaseService, IRouteSaleService
    {
        private SQLAdoContext adoContext;   
        public RouteSaleService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }


        public async Task<APIResultObject<List<RouteSaleModel>>> GetRouteByTeamCode(string teamCode)
        {
            var res = new APIResultObject<List<RouteSaleModel>>();
            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TeamCode", CommonHelper.CheckStringNull(teamCode));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<RouteSaleModel>("sp_GetRouteByTeamCode", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }


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
               
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TeamCode", CommonHelper.CheckStringNull(filter.captionTeamCode));
                parameters.Add("@RouteCode", CommonHelper.CheckStringNull(filter.routeCode));
                parameters.Add("@SaleUserName", CommonHelper.CheckStringNull(filter.saleUserName));
                parameters.Add("@CustGroupCode", CommonHelper.CheckStringNull(filter.custGroupCode));
                parameters.Add("@Keyword", CommonHelper.CheckStringNull(filter.keyword));
                parameters.Add("@Page", CommonHelper.CheckStringNull(filter.page));
                parameters.Add("@PageSize", CommonHelper.CheckStringNull(filter.pageSize));
                parameters.Add(name: "@TotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

                using (var connection = adoContext.CreateConnection())
                {

                    res.page = filter.page;
                    res.pageSize = filter.pageSize;
                    var resultExcute = await connection.QueryAsync<SearchCustomerForSalerGridModel>("sp_GetRouteByTeamCode", parameters, commandType: CommandType.StoredProcedure);
                    res.totalRow = parameters.Get<long>("TotalRow");
                    res.results = resultExcute.ToList();
                   
                }


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
