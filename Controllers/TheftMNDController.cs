using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DashBoardAPI.Controllers
{
    public class TheftMNDController : ApiController
    {
        public List<TheftMND> Get(string token, string code)
        {
            return new DBoardBridge().GetTheftFromMND(token, code);
        }
    }
}
