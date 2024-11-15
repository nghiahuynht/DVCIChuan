using Microsoft.AspNetCore.Mvc;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.User;
using System.Text;
using System.Security.Cryptography;

namespace PlatFormAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private IUserInfoService _userInfoService;
        public AccountController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(APIResultObject<AuthenSuccessModel>), StatusCodes.Status200OK)]
        public APIResultObject<AuthenSuccessModel> Login(string userName,string pass)
        {
            string passEncr = EncrypMD5(pass);
            var res = _userInfoService.Login(userName, passEncr);
            return res;
        }


        private string EncrypMD5(string pass)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();
        }
    }
}

