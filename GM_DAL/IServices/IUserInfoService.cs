using GM_DAL.Models.User;
using GM_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.IServices
{
    public interface IUserInfoService
    {
        APIResultObject<AuthenSuccessModel> Login(string userName, string pass);
    }
}
