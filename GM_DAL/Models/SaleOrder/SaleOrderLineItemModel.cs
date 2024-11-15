using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.SaleOrder
{
    public class SaleOrderLineItemModel
    {
        public long id { get; set; }
        public long orderId { get; set; }
        public string itemCode { get; set;}
        public string itemName { get; set; }
        public string unit { get; set; }
        public int quantity { get; set; }
        public decimal? priceNoVAT { get; set; }
        public decimal? vat { get; set; }
        public decimal? priceWithVAT { get; set; }
        public decimal? Total { get; set; }
    }
}
