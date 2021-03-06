﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DashBoardAPI.Controllers
{
    public class BillingStatsDailyController : ApiController
    {
        //GetBillingStatsDaily
        public BillStatsDailyContainer Get(string token, string code = "15")
        {
            return new DBoardBridge().GetBillingStatsDaily(token, code);
        }
    }
}
