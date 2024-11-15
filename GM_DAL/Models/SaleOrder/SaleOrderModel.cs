using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.SaleOrder
{
    public class SaleOrderModel:ModelCommonField
    {
        public long id { get; set; }
        public string code { get; set; }
        public DateTime? orderDate { get; set; }
        public int customerId { get; set; }
        public string customerCode { get; set; }
        public string customerName { get; set; }
        public string taxAddress { get; set; }
        public string saleUserCode { get; set; }
        public string saleUserFullName { get; set; }     
        public decimal totalNoVAT { get; set; }
        public decimal vat { get; set; }
        public decimal totalWithVAT { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }
        public string? InvoiceNum { get; set; }
        public string? TransactionId { get; set; }
        public string? UUID { get; set; }
        public string? InvSeri { get; set; }


    }
}
