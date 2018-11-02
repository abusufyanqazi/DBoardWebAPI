using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using util;
using DashBoardAPI;

namespace DashBoardAPI
{
    public class CollectMonBilling
    {
        public string SDivCode { get; set; }
        public string SDivName { get; set; }
        public string BPeriod { get; set; }
        public string CcCode { get; set; }
        public CompAssMnt ComputedAssMnt { get; set; }
        public Collection Collect { get; set; }
        public Percent Percnt { get; set; }

        public CollectMonBilling(DataRowView dr)
        {
            this.SDivCode = utility.GetColumnValue(dr, "SDIVCODE");
            this.SDivName = utility.GetColumnValue(dr, "SDIVNAME");
            this.BPeriod = utility.GetColumnValue(dr, "B_PERIOD");
            this.CcCode = utility.GetColumnValue(dr, "CC_CODE");
            this.ComputedAssMnt = new CompAssMnt(utility.GetColumnValue(dr, "PVT_ASSESS"),
                utility.GetColumnValue(dr, "GVT_ASSESS"),
                utility.GetColumnValue(dr, "TOT_ASSESS"));
            this.Collect = new Collection(utility.GetColumnValue(dr, "PVT_COLL"),
                utility.GetColumnValue(dr, "GVT_COLL"),
                utility.GetColumnValue(dr, "TOT_COLL"));
            this.Percnt = new Percent(utility.GetColumnValue(dr, "PVT_PERCENT"),
                utility.GetColumnValue(dr, "GVT_PERCENT"),
                utility.GetColumnValue(dr, "TOT_PERCENT"));
        }

    }
}