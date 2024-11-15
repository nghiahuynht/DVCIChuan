using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.APIModels.SaleOrder
{
    public class SearchIOrderByCustomerFilterModel
    {
        public string customerCode { get; set; }
        public string year { get; set; }
        public int page { get; set; } = 1;
        /// <summary>
        /// Số lượng item trên 1 Page, mặc định = 10
        /// </summary>
        public int pageSize { get; set; } = 10;

    }
}
