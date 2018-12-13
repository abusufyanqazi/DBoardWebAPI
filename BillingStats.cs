using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using util;

namespace DashBoardAPI
{

    public class BillStatsContainer
    {
        public  List<BillingStatsBatch> collBillStats = new List<BillingStatsBatch>();
        public List<BillingStatsSummary> billStatSummary=new List<BillingStatsSummary>();

        public BillStatsContainer(string code, DataTable dtDaily, DataTable dtBatch)
        {
            string month = String.Empty;
            string name = String.Empty;
            string scode = String.Empty;

            if (dtDaily != null && dtBatch != null)
            {
                foreach (DataRow dr in dtDaily.Select("CODE='" + code + "'"))
                {
                    foreach (DataRow dr2 in dtBatch.Select("CODE='" + code + "'"))
                    {
                        collBillStats.Add(new BillingStatsBatch(dr2));
                    }
                }

                collBillStats.Add(BillingStatsSummary.GetBillStatBatchSummaryNE(code, dtDaily));
                foreach (DataRow dr in dtDaily.Select("CODE <> '" + code + "'"))
                {
                    
                     name = utility.GetColumnValue(dr, "NAME");
                     month = DateTime.Parse(utility.GetColumnValue(dr, "MONTH")).ToString("dd-MMM-yy");
                     scode  =utility.GetColumnValue(dr, "CODE");
                    billStatSummary.Add(new BillingStatsSummary(month, scode, name, dtDaily));
                }
                billStatSummary.Add(GetBillStatBatchSummary(month, code, code == "15" ? "MEPCO" : code));
            }
        }

        private BillingStatsSummary GetBillStatBatchSummary(string pMonth, string pCode, string pName)
        {
             string BATCH = "TOTAL";
             string SDIV_CODE = pCode;
             string SDIV_NAME = pName;
            UInt32 TNOCONSUMERS = 0;
            UInt32 NOUNBILLEDCASES = 0;
            UInt32 NOSTSREADING = 0;
            UInt32 NODISCASES = 0;
            UInt32 NORECCASES = 0;
            UInt32 NOMCOCASES = 0;
            UInt32 NODEFMETERS = 0;
            UInt32 LOCKCASES = 0;
            UInt32 NONEWCONN = 0;
            Int64 CREDBALCONSM = 0;
            UInt32 NOHEAVYBCASES = 0;
            Int64 CREDBALAMT = 0;
            
            foreach (BillingStatsSummary bs in billStatSummary)
            {
                BillingStatsBatch b= bs.total;
                if (b.BATCH == "TOTAL")
                {
                    TNOCONSUMERS += UInt32.Parse(b.TNOCONSUMERS);
                    NOUNBILLEDCASES += UInt32.Parse(b.NOUNBILLEDCASES);
                    NOSTSREADING += UInt32.Parse(b.NOSTSREADING);
                    NODISCASES += UInt32.Parse(b.NODISCASES);
                    NORECCASES += UInt32.Parse(b.NORECCASES);
                    NOMCOCASES += UInt32.Parse(b.NOMCOCASES);
                    NODEFMETERS += UInt32.Parse(b.NODEFMETERS);
                    LOCKCASES += UInt32.Parse(b.LOCKCASES);
                    NONEWCONN += UInt32.Parse(b.NONEWCONN);
                    CREDBALCONSM += Int64.Parse(b.CREDBALCONSM);
                    NOHEAVYBCASES += UInt32.Parse(b.NOHEAVYBCASES);
                    CREDBALAMT += Int64.Parse(b.CREDBALAMT);
                }
            }

            return new BillingStatsSummary(pMonth,pCode,pName, new BillingStatsBatch("TOTAL", TNOCONSUMERS.ToString(), NOUNBILLEDCASES.ToString(),
                NOSTSREADING.ToString(), NODISCASES.ToString(), NORECCASES.ToString(), NOMCOCASES.ToString(),
                NODEFMETERS.ToString(), LOCKCASES.ToString(), NONEWCONN.ToString(), CREDBALCONSM.ToString(),
                NOHEAVYBCASES.ToString(), CREDBALAMT.ToString()));
        }

    }

    public class BillingStatsBatch
    {
        public string BATCH { get; set; }
        public string TNOCONSUMERS { get; set; }
        public string NOUNBILLEDCASES { get; set; }
        public string NOSTSREADING { get; set; }
        public string NODISCASES { get; set; }
        public string NORECCASES { get; set; }
        public string NOMCOCASES { get; set; }
        public string NODEFMETERS { get; set; }
        public string LOCKCASES { get; set; }
        public string NONEWCONN { get; set; }
        public string CREDBALCONSM { get; set; }
        public string NOHEAVYBCASES { get; set; }
        public string CREDBALAMT { get; set; }

        public BillingStatsBatch(DataRow dr)
        {
            this.BATCH = utility.GetColumnValue(dr, "BATCH");
            this.TNOCONSUMERS = utility.GetColumnValue(dr, "TNOCONSUMERS");
            this.NOUNBILLEDCASES = utility.GetColumnValue(dr, "NOUNBILLEDCASES");
            this.NOSTSREADING = utility.GetColumnValue(dr, "NOSTSREADING");
            this.NODISCASES = utility.GetColumnValue(dr, "NODISCASES");
            this.NORECCASES = utility.GetColumnValue(dr, "NORECCASES");
            this.NOMCOCASES = utility.GetColumnValue(dr, "NOMCOCASES");
            this.NODEFMETERS = utility.GetColumnValue(dr, "NODEFMETERS");
            this.LOCKCASES = utility.GetColumnValue(dr, "LOCKCASES");
            this.NONEWCONN = utility.GetColumnValue(dr, "NONEWCONN");
            this.CREDBALCONSM = utility.GetColumnValue(dr, "CREDBALCONSM");
            this.NOHEAVYBCASES = utility.GetColumnValue(dr, "NOHEAVYBCASES");
            this.CREDBALAMT = utility.GetColumnValue(dr, "CREDBALAMT");
        }

        public BillingStatsBatch(string BAT, string TOTCONS, string UNBILLEDCASES, string STSREADING,
            string DISCASES, string RECCASES, string MCOCASES, string DEFMETERS, string LCKCASES, string NEWCONN,
            string CRBALCONS, string HEAVYCASES, string CRBALAMNT)
        {
            this.BATCH = BAT;
            this.TNOCONSUMERS = TOTCONS;
            this.NOUNBILLEDCASES = UNBILLEDCASES;
            this.NOSTSREADING = STSREADING;
            this.NODISCASES = DISCASES;
            this.NORECCASES = RECCASES;
            this.NOMCOCASES = MCOCASES;
            this.NODEFMETERS = DEFMETERS;
            this.LOCKCASES = LCKCASES;
            this.NONEWCONN = NEWCONN;
            this.CREDBALCONSM = CRBALCONS;
            this.NOHEAVYBCASES = HEAVYCASES;
            this.CREDBALAMT = CRBALAMNT;
        }
    }

    public class BillingStatsSummary
    {
        public string MONTH { get; set; } 
        public string CODE { get; set; }
        public string NAME { get; set; }
        public BillingStatsBatch total;

        public  BillingStatsSummary(string mONTH, string cODE, string nAME, DataTable dt) 
        {
            this.MONTH = mONTH;
            this.CODE = cODE;
            this.NAME = nAME;
            total = GetBillStatBatchSummaryE(cODE,dt);
        }

        public BillingStatsSummary(string mONTH, string cODE, string nAME, BillingStatsBatch bsBat)
        {
            this.MONTH = mONTH;
            this.CODE = cODE;
            this.NAME = nAME;
            total = bsBat;
        }

        public static BillingStatsBatch GetBillStatBatchSummaryNE(string code, DataTable dt)
        {
            string BATCH = "TOTAL";
            string SDIV_CODE = "TOTAL";
            string SDIV_NAME = "TOTAL";
            int TNOCONSUMERS = 0;
            int NOUNBILLEDCASES = 0;
            int NOSTSREADING = 0;
            int NODISCASES = 0;
            int NORECCASES = 0;
            int NOMCOCASES = 0;
            int NODEFMETERS = 0;
            int LOCKCASES = 0;
            int NONEWCONN = 0;
            Int64 CREDBALCONSM = 0;
            int NOHEAVYBCASES = 0;
            Int64 CREDBALAMT = 0;

            foreach (DataRow bs in dt.Select("CODE <> '"+ code + "'"))
            {
                TNOCONSUMERS += int.Parse(utility.GetColumnValue(bs, "TNOCONSUMERS"));
                NOUNBILLEDCASES += int.Parse(utility.GetColumnValue(bs, "NOUNBILLEDCASES"));
                NOSTSREADING += int.Parse(utility.GetColumnValue(bs, "NOSTSREADING"));
                NODISCASES += int.Parse(utility.GetColumnValue(bs, "NODISCASES"));
                NORECCASES += int.Parse(utility.GetColumnValue(bs, "NORECCASES"));
                NOMCOCASES += int.Parse(utility.GetColumnValue(bs, "NOMCOCASES"));
                NODEFMETERS += int.Parse(utility.GetColumnValue(bs, "NODEFMETERS"));
                LOCKCASES += int.Parse(utility.GetColumnValue(bs, "LOCKCASES"));
                NONEWCONN += int.Parse(utility.GetColumnValue(bs, "NONEWCONN"));
                CREDBALCONSM += Int64.Parse(utility.GetColumnValue(bs, "CREDBALCONSM"));
                NOHEAVYBCASES += int.Parse(utility.GetColumnValue(bs, "NOHEAVYBCASES"));
                CREDBALAMT += Int64.Parse(utility.GetColumnValue(bs, "CREDBALAMT"));
            }

            return new BillingStatsBatch(BATCH, TNOCONSUMERS.ToString(), NOUNBILLEDCASES.ToString(),
                NOSTSREADING.ToString(), NODISCASES.ToString(), NORECCASES.ToString(), NOMCOCASES.ToString(), NODEFMETERS.ToString(), LOCKCASES.ToString(), NONEWCONN.ToString(),
                CREDBALCONSM.ToString(), NOHEAVYBCASES.ToString(), CREDBALAMT.ToString());

        }
        public static BillingStatsBatch GetBillStatBatchSummaryE(string code, DataTable dt)
        {
            string BATCH = "TOTAL";
            string SDIV_CODE = "TOTAL";
            string SDIV_NAME = "TOTAL";
            int TNOCONSUMERS = 0;
            int NOUNBILLEDCASES = 0;
            int NOSTSREADING = 0;
            int NODISCASES = 0;
            int NORECCASES = 0;
            int NOMCOCASES = 0;
            int NODEFMETERS = 0;
            int LOCKCASES = 0;
            int NONEWCONN = 0;
            Int64 CREDBALCONSM = 0;
            int NOHEAVYBCASES = 0;
            Int64 CREDBALAMT = 0;

            foreach (DataRow bs in dt.Select("CODE = '" + code + "'"))
            {
                TNOCONSUMERS += int.Parse(utility.GetColumnValue(bs, "TNOCONSUMERS"));
                NOUNBILLEDCASES += int.Parse(utility.GetColumnValue(bs, "NOUNBILLEDCASES"));
                NOSTSREADING += int.Parse(utility.GetColumnValue(bs, "NOSTSREADING"));
                NODISCASES += int.Parse(utility.GetColumnValue(bs, "NODISCASES"));
                NORECCASES += int.Parse(utility.GetColumnValue(bs, "NORECCASES"));
                NOMCOCASES += int.Parse(utility.GetColumnValue(bs, "NOMCOCASES"));
                NODEFMETERS += int.Parse(utility.GetColumnValue(bs, "NODEFMETERS"));
                LOCKCASES += int.Parse(utility.GetColumnValue(bs, "LOCKCASES"));
                NONEWCONN += int.Parse(utility.GetColumnValue(bs, "NONEWCONN"));
                CREDBALCONSM += Int64.Parse(utility.GetColumnValue(bs, "CREDBALCONSM"));
                NOHEAVYBCASES += int.Parse(utility.GetColumnValue(bs, "NOHEAVYBCASES"));
                CREDBALAMT += Int64.Parse(utility.GetColumnValue(bs, "CREDBALAMT"));
            }

            return new BillingStatsBatch(BATCH, TNOCONSUMERS.ToString(), NOUNBILLEDCASES.ToString(),
                NOSTSREADING.ToString(), NODISCASES.ToString(), NORECCASES.ToString(), NOMCOCASES.ToString(), NODEFMETERS.ToString(), LOCKCASES.ToString(), NONEWCONN.ToString(),
                CREDBALCONSM.ToString(), NOHEAVYBCASES.ToString(), CREDBALAMT.ToString());

        }
    }

  
}