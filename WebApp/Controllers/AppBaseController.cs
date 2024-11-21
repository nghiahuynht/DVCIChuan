using GM_DAL.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApp.Controllers
{
    public class AppBaseController : Controller
    {
        public AuthenSuccessModel AuthInfo()
        {
            var authen = new AuthenSuccessModel();
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                authen.userName = User.Identity.Name;
                authen.fullName = User.Claims.FirstOrDefault(x => x.Type == "FullName").Value;
                authen.role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            }

            return authen;
        }
    }
}
