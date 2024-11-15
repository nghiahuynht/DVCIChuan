using GM_DAL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Services
{
    public class ReportService:BaseService, IReportService
    {
        private GMDbContext db;
        public ReportService(GMDbContext db)
        {
            this.db = db;
        }






    }
}
