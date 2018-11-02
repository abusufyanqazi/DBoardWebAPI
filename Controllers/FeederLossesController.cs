using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DashBoardAPI.Controllers
{
    public class FeederLossesController : ApiController
    {
        public List<FeederLosses> Get(string token)
        {
            return new DBoardBridge().GetFeederLosses(token);
        }
    }
}
