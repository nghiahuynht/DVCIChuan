using GM_DAL.APIModels;
using GM_DAL.APIModels.SaleOrder;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.CaptionTeam;
using GM_DAL.Models.SaleOrder;
using Microsoft.AspNetCore.Mvc;

namespace PlatFormAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SaleOrderController : ControllerBase
    {
        private ISaleOrderService _saleOrderService;
        public SaleOrderController(ISaleOrderService _saleOrderService)
        {
            this._saleOrderService = _saleOrderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(APIResultObject<List<ResCommon>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<ResCommon>> SaveOrderHeader(SaleOrderModel model)
        {
            var res = await _saleOrderService.SaveSaleOrderHeader(model);
            return res;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PageSizeParamModel<SaleOrderModel>), StatusCodes.Status200OK)]
        public async Task<PageSizeParamModel<SaleOrderModel>> SearchOrderBySaler(SearchOrderMobileFitlerModel filter)
        {
            var res = await _saleOrderService.SearchCustomerForMobileSaler(filter);
            return res;
        }



        [HttpPost]
        [ProducesResponseType(typeof(PageSizeParamModel<SaleOrderModel>), StatusCodes.Status200OK)]
        public async Task<PageSizeParamModel<SaleOrderModel>> SearchOrderByCustomer(SearchIOrderByCustomerFilterModel filter)
        {
            var res = await _saleOrderService.SearchOrderForMobileCustomer(filter);
            return res;
        }


    }
}
