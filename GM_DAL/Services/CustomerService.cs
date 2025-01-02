using Dapper;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.CaptionTeam;
using GM_DAL.Models.Customer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Services
{
    public class CustomerService:BaseService, ICustomerService
    {
        private SQLAdoContext adoContext;
        public CustomerService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }


        public async Task<APIResultObject<CustomerModel>> GetCustomerById(int id)
        {
            var res = new APIResultObject<CustomerModel>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", CommonHelper.CheckIntNull(id));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<CustomerModel>("sp_GetCustomerById", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.FirstOrDefault();
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
