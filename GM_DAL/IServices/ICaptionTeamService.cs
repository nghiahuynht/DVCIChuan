using GM_DAL.Models.CaptionTeam;
using GM_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.IServices
{
    public interface ICaptionTeamService
    {
        Task<APIResultObject<List<CaptionTeamModel>>> GetTeamBySaleUser(string saleUserName);
    }
}
