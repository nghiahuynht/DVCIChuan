using GM_DAL.Models.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GM_DAL.IServices;
using GM_DAL.Models;

namespace WebApp.Controllers
{
    public class AccountController : AppBaseController
    {
        private IUserInfoService _userInfoService;

        public AccountController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }



        public IActionResult LoginPage()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult LoginPage(LoginModel model)
        {
            string failedAlert = string.Empty;
            ClaimsIdentity identity = new ClaimsIdentity() ;
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



        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }



        public IActionResult SearchUserAccount()
        {
            return View();
        }

        public async Task<IActionResult> UserAccountDetail(int? id)
        {
            var user =new APIResultObject<UserInfoModel>() ;
            if (id.HasValue && id.Value != 0)
            {
                user = await _userInfoService.GetUserById(id.Value);
            }

            ViewBag.RoleCodes = new List<ComboboxModel>
            {
                new ComboboxModel{value="Admin",text="Quản trị"},
                new ComboboxModel{value="Manager",text="Quản lý"},
                new ComboboxModel{value="Accountant",text="Kế toán"}
            };

            return View(user.data);
        }

        [HttpPost]
        public JsonResult SaveUserDetail([FromBody] UserInfoModel model)
        {
            var res = _userInfoService.SaveUserInfo(model, AuthInfo().userName);
            return Json(res);
        }

        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<DataTableResultModel<UserInfoModel>> SearchUserInfoAction(SearchUserFilterModel filter)
        {
            var res = await _userInfoService.SearchUserAccount(filter);
            return res;
        }





    }
}
