using GM_DAL.Models.User;
using GM_DAL.Models;
using GM_DAL.Services;
using Microsoft.AspNetCore.Mvc;
using GM_DAL.IServices;
using GM_DAL.Models.CaptionTeam;
using GM_DAL.Models.RouteSale;
using GM_DAL.APIModels.Customer;
using GM_DAL.APIModels;

namespace PlatFormAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RouteSaleController : ControllerBase
    {
        private ICaptionTeamService _captionTeamService;
        private IRouteSaleService _routeSaleService;
        public RouteSaleController(ICaptionTeamService captionTeamService, IRouteSaleService routeSaleService)
        {
            _captionTeamService = captionTeamService;
            _routeSaleService = routeSaleService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(APIResultObject<List<CaptionTeamModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<CaptionTeamModel>>> GetTeamBySaleUser(string userName)
        {
            var res = await _captionTeamService.GetTeamBySaleUser(userName);
            return res;
        }


        [HttpGet]
        [ProducesResponseType(typeof(APIResultObject<List<RouteSaleModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<RouteSaleModel>>> GetRouteByTeamCode(string? teamCode)
        {
            var res = await _routeSaleService.GetRouteByTeamCode(teamCode);
            return res;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PageSizeParamModel<SearchCustomerForSalerGridModel>), StatusCodes.Status200OK)]
        public async Task<PageSizeParamModel<SearchCustomerForSalerGridModel>> SearhCustomerForMobileSaler(SearchCustomerForSalerFilterModel filter)
        {
            var res = await _routeSaleService.SearchCustomerForMobileSaler(filter);
            return res;
        }

    }
}
