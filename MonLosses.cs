using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using util;

namespace DashBoardAPI
{
    public class MonLosses
    {
        public string SDivCode { get; set; }
        public string SDivName { get; set; }
        public string IncDec { get; set; }
        public string ATC { get; set; }
        public Losses CurLoss{ get; set; }

        public Losses PrvLoss { get; set; }
        
        public MonLosses(DataRowView dr)
        {
            this.SDivCode = utility.GetColumnValue(dr, "SDIV");
            this.SDivName = utility.GetColumnValue(dr, "SDIVNAME");
            this.IncDec = utility.GetColumnValue(dr, "VAR_INCDEC");
            this.ATC = utility.GetColumnValue(dr, "ATC");
            this.CurLoss = new Losses(utility.GetColumnValue(dr, "RCV"),
                utility.GetColumnValue(dr, "BIL"),
                utility.GetColumnValue(dr, "LOS"),
                utility.GetColumnValue(dr, "PCT"),
                utility.GetColumnValue(dr, "B_PERIOD"));
            this.PrvLoss = new Losses(utility.GetColumnValue(dr, "PRV_RCV"),
                utility.GetColumnValue(dr, "PRV_BIL"),
                utility.GetColumnValue(dr, "PRV_LOS"),
                utility.GetColumnValue(dr, "PRV_PCT"),
                utility.GetColumnValue(dr, "PRV_PERIOD"));
        }
    }

    public class Losses
    {
        public String uReceived{ get; set; }
        public String uBilled { get; set; }
        public String uLost { get; set; }
        public String percentLost { get; set; }

        public String Period { get; set; }

        public Losses(string uReceiv, string uBill, string uLos, string prcntLost, string period)
        {
            this.uReceived = uReceiv;
            this.uBilled = uBill;
            this.uLost = uLos;
            this.percentLost = prcntLost;
            this.Period = period;
        }
    }

}