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

namespace GM_DAL.Services
{
    public class SaleOrderService:BaseService, ISaleOrderService
    {
        private GMDbContext db;
        public SaleOrderService(GMDbContext db)
        {
            this.db = db;
        }



        public async Task<APIResultObject<ResCommon>> SaveSaleOrderHeader(SaleOrderModel model)
        {
            var res = new APIResultObject<ResCommon>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter("@Id",model.id),
                        new SqlParameter("@CustomerId",model.customerId),
                        new SqlParameter("@SaleUserName",model.saleUserCode),
                        new SqlParameter("@VAT",model.vat),
                        new SqlParameter("@PaymentType",model.PaymentType)
                };
                ValidNullValue(param);
                var dataExutte = await db.Database.SqlQueryRaw<ResCommon>($"EXEC sp_SaveOrderHeader @Id,@CustomerId,@SaleUserName,@VAT,@PaymentType", param).ToListAsync();
                if (dataExutte != null && dataExutte.Any())
                {
                    res.data = dataExutte.FirstOrDefault();
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
                        var param = new SqlParameter[] {
                                new SqlParameter("@Id",item.id),
                                new SqlParameter("@OrderId",item.orderId),
                                new SqlParameter("@ItemCode",item.itemCode),
                                new SqlParameter("@Price",item.priceWithVAT),
                                new SqlParameter("@Unit",item.unit),
                                new SqlParameter("@Quantity",item.quantity),
                        };
                        ValidNullValue(param);
                        var dataExutte = await db.Database.SqlQueryRaw<ResCommon>($"EXEC sp_SaveOrderLineItem @Id,@OrderId,@ItemCode,@Price,@Unit,@Quantity", param).ToListAsync();
                        if (dataExutte != null && dataExutte.Any())
                        {
                            res = dataExutte.FirstOrDefault();
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
                var param = new SqlParameter[] {
                        new SqlParameter("@TeamCode",filter.captionTeamCode),
                        new SqlParameter("@RouteCode",filter.routeCode),
                        new SqlParameter("@SaleUserName",filter.saleUserName),
                        new SqlParameter("@FromDate",filter.fromDate),
                        new SqlParameter("@ToDate",filter.toDate),
                        new SqlParameter("@Keyword",filter.keyword),
                        new SqlParameter("@Page",filter.page),
                        new SqlParameter("@PageSize",filter.pageSize),
                        new SqlParameter { ParameterName = "@TotalRow", DbType = System.Data.DbType.Int64, Direction = System.Data.ParameterDirection.Output }
                };
                ValidNullValue(param);
                res.results = await db.Database.SqlQueryRaw<SaleOrderModel>($"EXEC sp_SearchOrderBySalerMobile @TeamCode,@RouteCode" +
                    $",@SaleUserName,@FromDate,@ToDate,@Keyword,@Page,@PageSize,@TotalRow OUT", param).ToListAsync();
                res.page = filter.page;
                res.pageSize = filter.pageSize;
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
                var param = new SqlParameter[] {
                        new SqlParameter("@CustomerCode",filter.customerCode),
                        new SqlParameter("@Year",filter.year),
                        new SqlParameter("@Page",filter.page),
                        new SqlParameter("@PageSize",filter.pageSize),
                        new SqlParameter { ParameterName = "@TotalRow", DbType = System.Data.DbType.Int64, Direction = System.Data.ParameterDirection.Output }
                };
                ValidNullValue(param);
                res.results = await db.Database.SqlQueryRaw<SaleOrderModel>($"EXEC sp_SearchOrderByCustomerMobile @CustomerCode,@Year,@Page,@PageSize,@TotalRow OUT", param).ToListAsync();
                res.page = filter.page;
                res.pageSize = filter.pageSize;
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
