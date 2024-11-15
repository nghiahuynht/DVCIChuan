using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.User
{
    public class UserInfoModel
    {
        public int id;
        public string userName { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public bool isActive { get; set; }
        public string phone { get; set; }
        public string title { get; set; }
        public string roleCode { get; set; }
    }
}
