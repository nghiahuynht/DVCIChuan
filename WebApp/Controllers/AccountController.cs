﻿using GM_DAL.Models.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LoginPage()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult LoginPage(LoginModel model)
        {
            string failedAlert = string.Empty;
            ClaimsIdentity identity = null;
            bool IsAuthenticated = false;


            if (!string.IsNullOrEmpty(model.userName) && !string.IsNullOrEmpty(model.userName))
            {
                var userInfo = new UserInfoModel();

                if (userInfo != null)
                {
                    //string roleName = userService.GetRoleByUser(userInfo.UserName);
                    identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name,model.userName),
                        new Claim("FullName","Admin"),
                        new Claim(ClaimTypes.Role,"Admin")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    IsAuthenticated = true;
                }
                else
                {
                    failedAlert = "Login not successfully";
                }

            }
            else
            {
                failedAlert = "Login not successfully";
            }


            if (IsAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }




            ViewBag.LoginFail = failedAlert;




            return View(model);
        }

    }
}