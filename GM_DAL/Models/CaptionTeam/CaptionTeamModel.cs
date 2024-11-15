using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.CaptionTeam
{
    public class CaptionTeamModel:ModelCommonField
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string saleUserName { get; set; }
    }
}
