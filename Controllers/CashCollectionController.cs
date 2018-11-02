using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel;

namespace DashBoardAPI.Controllers
{
    public class CashCollectionController : ApiController
    {
        public List<CashCollection> Get(string token, string code = "15")
        {
            return new DBoardBridge().GetCashCollSummary(token, code);
        }
    }
}
