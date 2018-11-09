using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DashBoardAPI.Controllers
{
    public class PrgsLossesController : ApiController
    {
        public List<MonLosses> Get(string token, string code = "15")
        {
            return new DBoardBridge().GetPrgsLosses(token, code);
        }
    }
}
