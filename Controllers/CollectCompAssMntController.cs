﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DashBoardAPI.Controllers
{
    public class CollectCompAssMntController : ApiController
    {
        public List<CollectCompAssMnt> Get(string token, string code = "15")
        {
            return new DBoardBridge().GetCollVsCompAssMnt(token,code);
        }
    }
}
