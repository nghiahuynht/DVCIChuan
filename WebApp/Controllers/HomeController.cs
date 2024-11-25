using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    
    public class HomeController : AppBaseController
    {
       

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            var userinfo = AuthInfo();
            if (string.IsNullOrEmpty(userinfo.userName))
            {
                return RedirectToAction("LoginPage", "Account");
            }
            return View();
        }








      
    }
}
