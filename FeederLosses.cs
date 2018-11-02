using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using util;

namespace DashBoardAPI
{
    public class FeederLosses
    {
        public string Code { get; set; }
        public string OfficeName { get; set; }
        public string ZeroBelow { get; set; }
        public string ZeroTen { get; set; }
        public string TenTwenty { get; set; }
        public string TwentyThirty { get; set; }
        public string ThirtyForty { get; set; }
        public string FortyFifty { get; set; }
        public string AboveFifty { get; set; }
        public string Total { get; set; }

        public FeederLosses(DataRow dr)
        {
            this.Code = utility.GetColumnValue(dr, "DIV_CIR");
            this.OfficeName = utility.GetColumnValue(dr, "DIVNAME");
            this.ZeroBelow = utility.GetColumnValue(dr, "ZERO_BELOW");
            this.ZeroTen = utility.GetColumnValue(dr, "ZERO_TEN");
            this.TenTwenty = utility.GetColumnValue(dr, "TEN_TWENTY");
            this.TwentyThirty = utility.GetColumnValue(dr, "TEN_TWENTY");
            this.ThirtyForty = utility.GetColumnValue(dr, "THIRTY_FOURTY");
            this.FortyFifty = utility.GetColumnValue(dr, "FOURTY_FIFTY");
            this.AboveFifty = utility.GetColumnValue(dr, "ABOVE_FIFTY");
            this.Total = utility.GetColumnValue(dr, "TOTAL");
        }
    }
}