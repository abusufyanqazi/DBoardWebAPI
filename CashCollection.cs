using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using util;

namespace DashBoardAPI
{
    public class CashCollection
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string MaxMainDT { get; set; }

        public List<CollectionMainDt> CollMD = new List<CollectionMainDt>(); 
        public CashCollection(string pCode, string pName, DataTable dt)
        {
            this.Code = pCode;
            this.Name = pName;
            foreach (DataRow dr in dt.Rows)
            {
                CollMD.Add(new CollectionMainDt(dr));
            }
            this.MaxMainDT = GetMaxMainDateC();
            AddMainDateSummary();
        }

        private string GetMaxMainDateC()
        {
            DateTime maxMainDateC = new DateTime(1900,01,01);
            foreach (CollectionMainDt mdc in CollMD)
            {
                if (DateTime.Parse(mdc.MainDateC) > maxMainDateC)
                    maxMainDateC = DateTime.Parse(mdc.MainDateC);
            }

            return maxMainDateC.ToString("dd-MMM-yy");
        }

        public void AddMainDateSummary()
        {
            string mDate = "Total"; 
            int dStub = 0; 
            int oStubes = 0; 
            Int64 dAmntCollected = 0;
            Int64 oAmountCollected = 0;
            Int64 dAmountPosted = 0;
            Int64 oAmountPosted = 0;
            Int64 tAmountPosted = 0; 
            int rcoFee = 0;
            Int64 advPayment = 0;
            Int64 utCash = 0;
            Int64 pdcPayment = 0;
            Int64 gvtPayment = 0;
            Int64 twellPayment = 0;

            foreach (CollectionMainDt md in CollMD)
            {
                dStub += int.Parse(md.DailyStub);
                oStubes += int.Parse(md.OnlineStubs);
                dAmntCollected += Int64.Parse(md.DailyAmountCollected);
                oAmountCollected += Int64.Parse(md.OnlineAmountCollected);
                dAmountPosted += Int64.Parse(md.DailyAmountPosted);
                oAmountPosted += Int64.Parse(md.OnlineAmountPosted);
                tAmountPosted += Int64.Parse(md.TotalAmountPosted);
                rcoFee += int.Parse(md.RcoFee);
                advPayment += Int64.Parse(md.AdvancePayment);
                utCash += Int64.Parse(md.UnidentifiedCash);
                pdcPayment += Int64.Parse(md.PDiscPayment);
                gvtPayment += Int64.Parse(md.GovtPayment);
                twellPayment += Int64.Parse(md.TubeWellPayment);
            }

            CollMD.Add(new CollectionMainDt(mDate.ToString(), dStub.ToString(), oStubes.ToString(), dAmntCollected.ToString(), oAmountCollected.ToString(),
                dAmountPosted.ToString(), oAmountPosted.ToString(), tAmountPosted.ToString(), rcoFee.ToString(), advPayment.ToString(), utCash.ToString(), pdcPayment.ToString(), gvtPayment.ToString(), twellPayment.ToString()));
        }

    }
    public class CollectionMainDt
    {
        public string MainDate { get; set; }
        public string MainDateC { get; set; }
        public string DailyStub{ get; set; }
        public string OnlineStubs{ get; set; }
        public string DailyAmountCollected	{ get; set; }
        public string OnlineAmountCollected	{ get; set; }
        public string DailyAmountPosted	{ get; set; }
        public string OnlineAmountPosted	{ get; set; }
        public string TotalAmountPosted	{ get; set; }
        public string RcoFee	{ get; set; }
        public string AdvancePayment	{ get; set; }
        public string UnidentifiedCash	{ get; set; }
        public string PDiscPayment	{ get; set; }
        public string GovtPayment	{ get; set; }
        public string TubeWellPayment { get; set; }

        public CollectionMainDt(DataRow dr)
        {
            this.MainDate = utility.GetColumnValue(dr, "MAINDATE");
            this.MainDateC = utility.GetColumnValue(dr, "MAINDATEC");
            this.DailyStub = utility.GetColumnValue(dr, "DAILY_STUBS");
            this.OnlineStubs = utility.GetColumnValue(dr, "ONLINE_STUBS");
            this.DailyAmountCollected = utility.GetColumnValue(dr, "NORMAL_CASH_COLLECTED");
            this.OnlineAmountCollected = utility.GetColumnValue(dr, "ONLINE_CASH_COLLECTED");
            this.DailyAmountPosted = utility.GetColumnValue(dr, "NORMAL_CASH_POSTED");
            this.OnlineAmountPosted = utility.GetColumnValue(dr, "ONLINE_CASH_POSTED");
            this.TotalAmountPosted = utility.GetColumnValue(dr, "TOTAL_CASH_POSTED");
            this.RcoFee = utility.GetColumnValue(dr, "RCO_FEE");
            this.AdvancePayment = utility.GetColumnValue(dr, "ADV_CASH");
            this.UnidentifiedCash = utility.GetColumnValue(dr, "UNIDENTIFIED_CASH");
            this.PDiscPayment = utility.GetColumnValue(dr, "P_DISC_PAYMENT");
            this.GovtPayment = utility.GetColumnValue(dr, "GOVT_PAYMENT");
            this.TubeWellPayment = utility.GetColumnValue(dr, "TUBEWELL_PAYMENT");
        }

        public CollectionMainDt(string mDate, string dStub, string oStubes, string dAmntCollected, string oAmountCollected, string dAmountPosted,string oAmountPosted, 
            string tAmountPosted, string rcoFee, string advPayment, string utCash, string pdcPayment, string gvtPayment, string twellPayment)
        {
            this.MainDate = mDate;
            this.DailyStub = dStub;
            this.OnlineStubs = oStubes;
            this.DailyAmountCollected = dAmntCollected;
            this.OnlineAmountCollected = oAmountCollected;
            this.DailyAmountPosted = dAmountPosted;
            this.OnlineAmountPosted = oAmountPosted;
            this.TotalAmountPosted = tAmountPosted;
            this.RcoFee = rcoFee;
            this.AdvancePayment = advPayment;
            this.UnidentifiedCash = utCash;
            this.PDiscPayment = pdcPayment;
            this.GovtPayment = gvtPayment;
            this.TubeWellPayment = twellPayment;
        }
    }
}