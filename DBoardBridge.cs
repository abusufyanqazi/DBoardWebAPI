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
                DataView distinctview = new DataView(dt);
                DataTable distinctValues = distinctview.ToTable(true, "CODE", "NAME");
                foreach (DataRow dr in distinctValues.Rows)
                {
                    string cd = utility.GetColumnValue(dr, "CODE");
                    string name = utility.GetColumnValue(dr, "NAME");
                    dv.RowFilter = string.Format("CODE = '{0}'",cd);
                    DataTable dt1 = dv.ToTable();
                    coll.Add(new CashCollection(cd, name, dt1));
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
            DataTable dtD,dtB;
            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            StringBuilder filterExp = new StringBuilder();

            if (token != secKey)
                return null;

            if (!string.IsNullOrEmpty(code))
            {
                filterExp.AppendFormat("LEN(CODE) >= {0} and LEN(CODE) <= {1}", (code.Length).ToString(), (code.Length + 1).ToString());
            }

            dtD = objDbuTil.getBillingStatsDaily(code, DateTime.Now.AddMonths(-1));
            dtB = objDbuTil.getBillingStatsBatchWise(code, DateTime.Now.AddMonths(-1));

            if (dtD != null)
            {
                DataView dvD = dtD.DefaultView;
                dvD.RowFilter = filterExp.ToString();
                dvD.Sort = "SRT_ORDER2 ASC";

                DataView dvB = dtB.DefaultView;
                dvB.RowFilter = filterExp.ToString();
                dvB.Sort = "SRT_ORDER2 ASC";


                BillStatsContainer billStContainer = new BillStatsContainer(code, dvD.ToTable(), dvB.ToTable());
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
        //
        public BillStatsDailyContainer GetBillingStatsDaily(string token, string code)
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

            dt = objDbuTil.getBillingStatsDaily(code, DateTime.Now.AddMonths(-1));

            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = filterExp.ToString();
                dv.Sort = "SRT_ORDER2 ASC";
                BillStatsDailyContainer billStContainer = new BillStatsDailyContainer(code, dv.ToTable());
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

        public List<TheftMND> GetTheftFromMND(string token, string refNo)
        {
            DataTable dt;
            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            StringBuilder filterExp = new StringBuilder();
            List<TheftMND> theftData = new List<TheftMND>();
            if (token != secKey)
                return null;

            dt = objDbuTil.getTheftData(refNo);

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    theftData.Add(new TheftMND(dr["BILL_MNTH"].ToString(), dr["NOTE_NO"].ToString(),
                        dr["ADJ_DT"].ToString(), dr["UNITS"].ToString(), dr["AMOUNT"].ToString(),
                        dr["PAY_AGAINTS_DET"].ToString()));
                }
            }

            return theftData;
        }

        public List<AssmntBatchWiseObject> GetAssesmentBatchWise(string token, string code)
        {
            if (token != secKey)
                return null;

            utility util = new utility();
            DB_Utility objDbuTil = new DB_Utility(conStr);
            DataTable dt = objDbuTil.getAssesmentBatchWise(code, DateTime.Now.AddMonths(-1));
            StringBuilder filterExp = new StringBuilder();
            List<AssmntBatchWiseObject> coll = new List<AssmntBatchWiseObject>();
            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                if (!string.IsNullOrEmpty(code))
                {
                    filterExp.AppendFormat("LEN(SDIV_CODE) >= {0} and LEN(SDIV_CODE) <= {1}", (code.Length).ToString(), (code.Length + 1).ToString());
                }
                dv.RowFilter = filterExp.ToString();
                dv.Sort = "SRT_ORDER2 ASC";
                DataView distinctview = new DataView(dt);
                DataTable distinctValues = distinctview.ToTable(true, "SDIV_CODE", "SDIV_NAME","MONTH");
                foreach (DataRow dr in distinctValues.Rows)
                {
                    string cd = utility.GetColumnValue(dr, "SDIV_CODE");
                    string name = utility.GetColumnValue(dr, "SDIV_NAME");
                    string month = utility.GetFormatedDate(utility.GetColumnValue(dr, "MONTH"));
                    dv.RowFilter = string.Format("SDIV_CODE = '{0}'", cd);
                    DataTable dt1 = dv.ToTable();
                    coll.Add(new AssmntBatchWiseObject(cd, name, month, dt1));
                }
            }
            return coll;
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