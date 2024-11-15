using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using GM_DAL.Models.User;
using GM_DAL.IServices;
using GM_DAL.Services;
using System.Reflection.Emit;

namespace GM_DAL
{
    public class GMDbContext:DbContext
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _configtion;
        public GMDbContext(DbContextOptions<GMDbContext> options
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configtion) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _configtion = configtion;
        }

       // public DbSet<AuthenSuccessModel> AuthenSuccessModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<AuthenSuccessModel>().HasKey(o => o.userName);
            base.OnModelCreating(builder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

           

            string connectionName = _httpContextAccessor?.HttpContext?.Request.Headers["ComCode"];

            connectionName = "RacDonDuong";

            string connectionString = _configtion["ConnectionStrings:"+ connectionName];
            optionsBuilder.UseSqlServer(connectionString);

        }

        


    }
}
