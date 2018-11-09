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
            DataTable dt = objDbuTil.GetCashCollSummary(code,DateTime.Now.AddMonths(-1));
            StringBuilder filterExp = new StringBuilder();
            List<CashCollection> coll = new List<CashCollection>();
            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                if (!string.IsNullOrEmpty(code))
                {
                    filterExp.AppendFormat("LEN(CODE) >= {0} and LEN(CODE) <= {1}", (code.Length).ToString(), (code.Length + 1).ToString());
                }
                dv.RowFilter = filterExp.ToString();
                dv.Sort = "SRT_ORDER2 ASC";
                foreach (DataRowView dr in dv)
                {
                    string cd = utility.GetColumnValue(dr, "CODE");
                    string name = utility.GetColumnValue(dr, "NAME");
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
            DataTable dt = objDbuTil.GetFeederLosses(DateTime.Now.AddMonths(-1));
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

        public List<ReceivSpillArrear> GetReceivable(string token, string code)
        {
            DataTable dt;
            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            List<ReceivSpillArrear> receivSpillArrears = new List<ReceivSpillArrear>();
            StringBuilder filterExp = new StringBuilder();

            if (token != secKey)
                return null;

            if (!string.IsNullOrEmpty(code))
            {
                filterExp.AppendFormat("LEN(CODE) >= {0} and LEN(CODE) <= {1}", (code.Length).ToString(), (code.Length + 1).ToString());
            }

            dt = objDbuTil.getReceiveables(code, DateTime.Now.AddMonths(-1));

            if (dt != null)
            {
                //int i = 0;
                DataView dv = dt.DefaultView;
                dv.RowFilter = filterExp.ToString();
                dv.Sort = "SRT_ORDER2 ASC";
                foreach (DataRowView dr in dv)
                {
                    receivSpillArrears.Add(new ReceivSpillArrear(dr));

                }
            }
            return receivSpillArrears;
        }

        public List<MonLosses> GetMonLosses(string token, string code)
        {
            DataTable dt;
            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            List<MonLosses> monLosseses = new List<MonLosses>();
            StringBuilder filterExp = new StringBuilder();

            if (token != secKey)
                return null;

            if (!string.IsNullOrEmpty(code))
            {
                filterExp.AppendFormat("LEN(SDIV) >= {0} and LEN(SDIV) <= {1}", (code.Length).ToString(), (code.Length + 1).ToString());
            }

            dt = objDbuTil.getMonLosses(code, DateTime.Now.AddMonths(-1));

            if (dt != null)
            {
                //int i = 0;
                DataView dv = dt.DefaultView;
                dv.RowFilter = filterExp.ToString();
                dv.Sort = "SRT_ORDER2 ASC";
                foreach (DataRowView dr in dv)
                {
                    monLosseses.Add(new MonLosses(dr));

                }
            }
            return monLosseses;
        }

        public List<MonLosses> GetPrgsLosses(string token, string code)
        {
            DataTable dt;
            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            List<MonLosses> prgsLosseses = new List<MonLosses>();
            StringBuilder filterExp = new StringBuilder();

            if (token != secKey)
                return null;

            if (!string.IsNullOrEmpty(code))
            {
                filterExp.AppendFormat("LEN(SDIV) >= {0} and LEN(SDIV) <= {1}", (code.Length).ToString(), (code.Length + 1).ToString());
            }

            dt = objDbuTil.getPrgsLosses(code, DateTime.Now.AddMonths(-1));

            if (dt != null)
            {
                //int i = 0;
                DataView dv = dt.DefaultView;
                dv.RowFilter = filterExp.ToString();
                dv.Sort = "SRT_ORDER2 ASC";
                foreach (DataRowView dr in dv)
                {
                    prgsLosseses.Add(new MonLosses(dr));

                }
            }
            return prgsLosseses;
        }
        public BillStatsContainer GetBillingStatsBatchWise(string token, string code)
        {
            DataTable dt;
            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            StringBuilder filterExp = new StringBuilder();

            if (token != secKey)
                return null;

            if (!string.IsNullOrEmpty(code))
            {
                filterExp.AppendFormat("LEN(CODE) >= {0} and LEN(CODE) <= {1}", (code.Length).ToString(), (code.Length + 1).ToString());
            }

            dt = objDbuTil.getBillingStatsBatchWise(code, DateTime.Now.AddMonths(-1));

            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = filterExp.ToString();
                dv.Sort = "SRT_ORDER2 ASC";
                BillStatsContainer billStContainer = new BillStatsContainer(code, dv.ToTable());
                return billStContainer;


                //foreach (DataRowView dr in dv)
                //{
                //    string cd = utility.GetColumnValue(dr, "CODE");
                //    string name = utility.GetColumnValue(dr, "NAME");
                //    string month = utility.GetColumnValue(dr, "MONTH");
                //    bStats.Add(new BillingStats(month,cd,name,dv.ToTable()));

                //}

            }
            return null;
        }
        

        public static string GetBillingStatus(string token)
        {
            string ret = "Error";

            if (token != secKey)
                return "Ivalid Token.";
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