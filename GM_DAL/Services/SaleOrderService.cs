using GM_DAL.APIModels.Customer;
using GM_DAL.APIModels;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.Customer;
using GM_DAL.Models.SaleOrder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM_DAL.APIModels.SaleOrder;
using Dapper;
using System.Data;

namespace GM_DAL.Services
{
    public class SaleOrderService:BaseService, ISaleOrderService
    {
        private SQLAdoContext adoContext;
        public SaleOrderService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }



        public async Task<APIResultObject<ResCommon>> SaveSaleOrderHeader(SaleOrderModel model)
        {
            var res = new APIResultObject<ResCommon>();
            try
            {
               
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", CommonHelper.CheckLongNull(model.id));
                parameters.Add("@CustomerId", CommonHelper.CheckIntNull(model.customerId));
                parameters.Add("@SaleUserName", CommonHelper.CheckStringNull(model.saleUserCode));
                parameters.Add("@VAT", CommonHelper.CheckDecimalNull(model.vat));
                parameters.Add("@PaymentType", CommonHelper.CheckStringNull(model.PaymentType));

                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<ResCommon>("sp_SaveOrderHeader", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }
            return res;
        }


        public async Task<ResCommon> SaveOrderLineItem(long orderId,List<SaleOrderLineItemModel> items)
        {
            var res = new ResCommon();
            try
            {

                if (items.Any())
                {
                    foreach (var item in items)
                    {

                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@Id", CommonHelper.CheckLongNull(item.id));
                        parameters.Add("@OrderId", CommonHelper.CheckLongNull(item.orderId));
                        parameters.Add("@ItemCode", CommonHelper.CheckStringNull(item.itemCode));
                        parameters.Add("@Price", CommonHelper.CheckDecimalNull(item.priceWithVAT));
                        parameters.Add("@Unit", CommonHelper.CheckStringNull(item.unit));
                        parameters.Add("@Quantity", CommonHelper.CheckStringNull(item.quantity));
                        using (var connection = adoContext.CreateConnection())
                        {
                            var resultExcute = await connection.QueryAsync<ResCommon>("sp_SaveOrderLineItem", parameters, commandType: CommandType.StoredProcedure);
                            res = resultExcute.FirstOrDefault();
                        }


                    }
                }

               
            }
            catch
            {
               
            }
            return res;
        }




        public async Task<PageSizeParamModel<SaleOrderModel>> SearchCustomerForMobileSaler(SearchOrderMobileFitlerModel filter)
        {
            var res = new PageSizeParamModel<SaleOrderModel>();
            try
            {
               

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TeamCode", CommonHelper.CheckStringNull(filter.captionTeamCode));
                parameters.Add("@RouteCode", CommonHelper.CheckStringNull(filter.routeCode));
                parameters.Add("@SaleUserName", CommonHelper.CheckStringNull(filter.saleUserName));
                parameters.Add("@FromDate", CommonHelper.CheckStringNull(filter.fromDate));
                parameters.Add("@ToDate", CommonHelper.CheckStringNull(filter.toDate));
                parameters.Add("@Keyword", CommonHelper.CheckStringNull(filter.keyword));
                parameters.Add("@Page", CommonHelper.CheckStringNull(filter.page));
                parameters.Add("@PageSize", CommonHelper.CheckStringNull(filter.pageSize));
                parameters.Add(name: "@TotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

                using (var connection = adoContext.CreateConnection())
                {

                    res.page = filter.page;
                    res.pageSize = filter.pageSize;
                    var resultExcute = await connection.QueryAsync<SaleOrderModel>("sp_SearchOrderBySalerMobile", parameters, commandType: CommandType.StoredProcedure);
                    res.totalRow = parameters.Get<long>("TotalRow");
                    res.results = resultExcute.ToList();

                }




            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
                res.results = new List<SaleOrderModel>();
            }
            return res;
        }

        public async Task<PageSizeParamModel<SaleOrderModel>> SearchOrderForMobileCustomer(SearchIOrderByCustomerFilterModel filter)
        {
            var res = new PageSizeParamModel<SaleOrderModel>();
            try
            {
               

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerCode", CommonHelper.CheckStringNull(filter.customerCode));
                parameters.Add("@Year", CommonHelper.CheckIntNull(filter.year));
                parameters.Add("@Page", CommonHelper.CheckIntNull(filter.page));
                parameters.Add("@PageSize", CommonHelper.CheckIntNull(filter.pageSize));
                parameters.Add(name: "@TotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);


                using (var connection = adoContext.CreateConnection())
                {

                    res.page = filter.page;
                    res.pageSize = filter.pageSize;
                    var resultExcute = await connection.QueryAsync<SaleOrderModel>("sp_SaveOrderLineItem", parameters, commandType: CommandType.StoredProcedure);
                    res.totalRow = parameters.Get<long>("TotalRow");
                    res.results = resultExcute.ToList();
                }





            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
                res.results = new List<SaleOrderModel>();
            }
            return res;
        }


    }
}
