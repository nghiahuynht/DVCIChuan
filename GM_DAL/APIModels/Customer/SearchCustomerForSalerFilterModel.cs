using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.APIModels.Customer
{
    public class SearchCustomerForSalerFilterModel
    {
        public string captionTeamCode { get; set; }
        public string routeCode { get; set; }
        public string saleUserName { get; set; }
        public string custGroupCode { get; set; }
        public string keyword { get; set; }
        public int page { get; set; } = 1;
        /// <summary>
        /// Số lượng item trên 1 Page, mặc định = 10
        /// </summary>
        public int pageSize { get; set; } = 10;
    }
}
