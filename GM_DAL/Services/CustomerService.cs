using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.CaptionTeam;
using GM_DAL.Models.Customer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Services
{
    public class CustomerService:BaseService, ICustomerService
    {
        private GMDbContext db;
        public CustomerService(GMDbContext db)
        {
            this.db = db;
        }


        public async Task<APIResultObject<CustomerModel>> GetCustomerById(int id)
        {
            var res = new APIResultObject<CustomerModel>();
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter("@CustomerId",id),
                };
                ValidNullValue(param);
                var dataExutte = await db.Database.SqlQueryRaw<CustomerModel>($"EXEC sp_GetCustomerById @CustomerId", param).ToListAsync();
                if (dataExutte != null && dataExutte.Any())
                {
                    res.data = dataExutte.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }
            return res;
        }



    }
}
