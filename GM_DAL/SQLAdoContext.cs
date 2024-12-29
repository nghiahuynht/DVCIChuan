using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL
{
    public class SQLAdoContext
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private string? _connectionString;
        public SQLAdoContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _connectionString = "";


        }


        public IDbConnection CreateConnection()
        {
            string? connectionName = _httpContextAccessor?.HttpContext?.Request.Headers["ComCode"];

            if (string.IsNullOrEmpty(connectionName))
            {
                connectionName = "ComInfo";
            }

            _connectionString = _configuration["ConnectionStrings:" + connectionName];
            return new SqlConnection(_connectionString);
        }
    }
}
