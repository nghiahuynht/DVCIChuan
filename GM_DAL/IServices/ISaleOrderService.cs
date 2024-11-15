using GM_DAL.Models.SaleOrder;
using GM_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM_DAL.APIModels.SaleOrder;
using GM_DAL.APIModels;

namespace GM_DAL.IServices
{
    public interface ISaleOrderService
    {
        Task<APIResultObject<ResCommon>> SaveSaleOrderHeader(SaleOrderModel model);
        Task<ResCommon> SaveOrderLineItem(long orderId, List<SaleOrderLineItemModel> items);
        Task<PageSizeParamModel<SaleOrderModel>> SearchCustomerForMobileSaler(SearchOrderMobileFitlerModel filter);
        Task<PageSizeParamModel<SaleOrderModel>> SearchOrderForMobileCustomer(SearchIOrderByCustomerFilterModel filter);
    }
}
