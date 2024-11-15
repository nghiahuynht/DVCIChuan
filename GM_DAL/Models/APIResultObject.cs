using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM_DAL.Enum;

namespace GM_DAL.Models
{
    public class APIResultObject<T>
    {
        private ResultCode code;
        public APIResultObject()
        {
            this.code = ResultCode.Ok;
            this.message = new APIMessageResponse(this.code);
            
        }

       
        public T data { get; set; }
        public APIMessageResponse message { get; set; }

    }
}
