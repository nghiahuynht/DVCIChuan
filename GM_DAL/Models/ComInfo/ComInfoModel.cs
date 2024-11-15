using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.ComInfo
{
    public class ComInfoModel
    {
        public string taxCode { get; set; }
        public string ComName { get; set; }
        public string Address { get; set; }

        public string userName { get; set; }
        public string password { get; set; }
        public string appId { get; set; }
        public string apiToken { get; set; }
        public string apiInvoice { get; set; }
        public string hsmUser { get; set; }
        public string hsmPass { get; set; }

    }
}
