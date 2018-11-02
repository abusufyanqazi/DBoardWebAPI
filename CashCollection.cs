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

        public CollectionMainDt[] CollMD = new CollectionMainDt[31];
        public CashCollection(string pCode, string pName, DataTable dt)
        {
            this.Code = pCode;
            this.Name = pName;

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                CollMD[i++] = new CollectionMainDt(dr);
            }

        }

    }
    public class CollectionMainDt
    {
        public string MainDate { get; set; }
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

        public CollectionMainDt()
        {
            this.MainDate = "0";
            this.DailyStub = "0";
            this.OnlineStubs = "0";
            this.DailyAmountCollected = "0";
            this.OnlineAmountCollected = "0";
            this.DailyAmountPosted = "0";
            this.OnlineAmountPosted = "0";
            this.TotalAmountPosted = "0";
            this.RcoFee = "0";
            this.AdvancePayment = "0";
            this.UnidentifiedCash = "0";
            this.PDiscPayment = "0";
            this.GovtPayment = "0";
            this.TubeWellPayment = "0";
        }
    }
}