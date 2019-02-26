using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DashBoardAPI.Controllers
{
    public class DefaultSumAgeController : ApiController
    {
        public DefaulterSummary Get(string token, string code = "15", string age = "PRIVATE", string phase = "RUNNING DEFAULTERS", string trf = "DOMESTIC")
        {
            //                                               token, code, type, status, tariff
            return new DBoardBridge().GetDefaulterSummaryAge(token, code, age, phase,trf);
        }
    }
}

