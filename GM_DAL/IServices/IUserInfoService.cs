﻿using GM_DAL.Models.User;
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
        Task<APIResultObject<AuthenSuccessModel>> Login(string userName, string pass);
        Task<APIResultObject<List<MenuModel>>> GetMenuByRole(string role);
        Task<APIResultObject<ResCommon>> SaveUserInfo(UserInfoModel model, string userName);
        Task<APIResultObject<UserInfoModel>> GetUserById(int id);
        Task<DataTableResultModel<UserInfoModel>> SearchUserAccount(SearchUserFilterModel filter);
    }
}
