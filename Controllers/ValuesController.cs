using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using util;

namespace DashBoardAPI.Controllers
{
    public class ValuesController : ApiController
    {
        
        // GET api/<controller>
       // [Route("api/{controller}")]
        //[HttpGet()]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "String 1", "String 2" };
        //}

        
        //public IEnumerable<string> Get(string token)
        
        //[Route("api/{controller}/{code}")]
        //[HttpGet()]

        //[HttpGet()]
        //public string Get(string controller, string code)
        //{
        //string resp=String.Empty;
        //switch (controller)
        //    {
        //        case "COLLVSCOMPASSMNT":
        //            resp=DBoardBridge.GetCollVsCompAssmnt(String.Empty, "code");
        //        break;
        //    case "COLLVSBILLING":
        //        resp = DBoardBridge.GetCollVsBilling(String.Empty, "code");
        //            break;
        //    case "RECEIVABLE":
        //            resp = DBoardBridge.GetReceivable(String.Empty, "code");
        //            break;
        //    case "BILLINGSTATUS":
        //            resp = DBoardBridge.GetBillingStatus(String.Empty);
        //            break;
        //        default:
        //        resp = "Unknown API";
        //        break;
        //    }

        //    return resp;
        //}

        //[Route("COLLVSBILLING/{token}/{code}")]
        ////public IEnumerable<string> Get(string token)
        //public string Get(string token, string code)
        //{
        //    return DBoardBridge.GetCollVsBilling(token, code);
        //}

        //[Route("RECEIVABLE/{token}/{code}")]
        ////public IEnumerable<string> Get(string token)
        //public string Get(string token, string code)
        //{
        //    return DBoardBridge.GetReceivable(token, code);
        //}

        //[Route("BILLINGSTATUS/{token}")]
        ////public IEnumerable<string> Get(string token)
        //public string Get(string token)
        //{
        //    return DBoardBridge.GetBillingStatus(token);
        //}
        
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}