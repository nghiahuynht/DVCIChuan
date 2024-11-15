using GM_DAL.Models.RouteSale;
using GM_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM_DAL.APIModels.Customer;
using GM_DAL.APIModels;

namespace GM_DAL.IServices
{
    public interface IRouteSaleService
    {
        Task<APIResultObject<List<RouteSaleModel>>> GetRouteByTeamCode(string teamCode);
        Task<PageSizeParamModel<SearchCustomerForSalerGridModel>> SearchCustomerForMobileSaler(SearchCustomerForSalerFilterModel filter);
    }
}
