using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.Customer
{
    public class CustomerModel
    {
        public long id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string phone { get; set; }
        public decimal? Price { get; set; }
        public string taxCode { get; set; }
        public string taxAddress { get; set; }
        public string agencyName { get; set; }
        public string payerName { get; set; }
        public string teamCode { get; set; }
        public string routeCode { get; set; }
        public string saleUserCode { get; set; }
    }
}
