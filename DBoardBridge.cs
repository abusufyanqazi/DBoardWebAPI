using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using  util;
using DAL;

namespace DashBoardAPI
{
    public  class DBoardBridge
    {
        static string secKey = System.Configuration.ConfigurationManager.AppSettings["SECKEY"].ToString();
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["CONSTR"].ToString();

        public List<CashCollection> GetCashCollSummary(string token, string code)
        {
            if (token != secKey)
                return null;

            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            DataTable dt = objDbuTil.GetCashCollSummary(code,DateTime.Now.AddMonths(-2));
            StringBuilder filterExp = new StringBuilder();
            List<CashCollection> coll = new List<CashCollection>();
            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                if (!string.IsNullOrEmpty(code))
                {
                    filterExp.AppendFormat("LEN(SDIV_CODE) >= {0} and LEN(SDIV_CODE) <= {1}", (code.Length).ToString(), (code.Length + 1).ToString());
                }
                dv.RowFilter = filterExp.ToString();
                dv.Sort = "SRT_ORDER2 ASC";
                foreach (DataRowView dr in dv)
                {
                    int i = 0;
                    string cd = utility.GetColumnValue(dr, "SDIV_CODE");
                    string name = utility.GetColumnValue(dr, "SDIV_NAME");
                    coll.Add(new CashCollection(cd, name, dv.ToTable()));
                }
            }
            return coll;
        }

        public List<FeederLosses> GetFeederLosses(string token)
        {
            if (token != secKey)
                return null;

            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            DataTable dt = objDbuTil.GetFeederLosses(DateTime.Now.AddMonths(-2));
            StringBuilder filterExp = new StringBuilder();
            List<FeederLosses> coll = new List<FeederLosses>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    coll.Add(new FeederLosses(dr));
                }
            }
            return coll;
        }

        public  List<CollectCompAssMnt> GetCollVsCompAssMnt(string token, string code)
        {
            DataTable dt;
            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            List<CollectCompAssMnt> collCompAssMnt = new List<CollectCompAssMnt>();
            StringBuilder filterExp = new StringBuilder();

            if (token != secKey)
                return null;

                if (!string.IsNullOrEmpty(code))
                {
                    filterExp.AppendFormat("LEN(SDIVCODE) >= {0} and LEN(SDIVCODE) <= {1}", (code.Length).ToString(), (code.Length + 1).ToString());
                }

                dt = objDbuTil.GetCollVsCompAssmnt(code);

                if (dt != null)
                {
                    //int i = 0;
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = filterExp.ToString();
                    dv.Sort = "SRT_ORDER2 ASC";
                    foreach (DataRowView dr in dv)
                    {
                        collCompAssMnt.Add(new CollectCompAssMnt(dr));

                    }
                }
            return collCompAssMnt;
        }

        public List<CollectMonBilling> GetCollVsBilling(string token, string code)
        {
            DataTable dt;
            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            List<CollectMonBilling> collVsBilling = new List<CollectMonBilling>();
            StringBuilder filterExp = new StringBuilder();

            if (token != secKey)
                return null;

            if (!string.IsNullOrEmpty(code))
            {
                filterExp.AppendFormat("LEN(SDIVCODE) >= {0} and LEN(SDIVCODE) <= {1}", (code.Length).ToString(), (code.Length + 1).ToString());
            }

            dt = objDbuTil.GetCollVsBilling(code);

            if (dt != null)
            {
                //int i = 0;
                DataView dv = dt.DefaultView;
                dv.RowFilter = filterExp.ToString();
                dv.Sort = "SRT_ORDER2 ASC";
                foreach (DataRowView dr in dv)
                {
                    collVsBilling.Add(new CollectMonBilling(dr));

                }
            }
            return collVsBilling;
        }

        public  string GetReceivable(string token, string code)
        {
            string ret = "Error";

            if (token != secKey)
                return "Ivalid Token.";
            try
            {
                DB_Utility objDBUTil = new DB_Utility(conStr);
                DataTable dt = objDBUTil.getReceiveables(code);
                utility util = new utility();

                ret = util.DataTableToJSONWithStringBuilder(dt);
            }
            catch (Exception ex)
            {
                ret = ex.ToString();
            }
            return ret;
        }
        
        public static string GetBillingStatus(string token)
        {
            string ret = "Error";

            //if (token != secKey)
            //    return "Ivalid Token.";
            try
            {
                DB_Utility objDBUTil = new DB_Utility(conStr);
                DataTable dt = objDBUTil.getBillingStatus();
                utility util = new utility();

                ret = util.DataTableToJSONWithStringBuilder(dt);
            }
            catch (Exception ex)
            {
                ret = ex.ToString();
            }
            return ret;
        }

    }
}