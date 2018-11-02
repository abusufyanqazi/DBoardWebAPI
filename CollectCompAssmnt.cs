using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using util;

namespace DashBoardAPI
{
    public class CollectCompAssMnt
    {
        public string SDivCode { get; set; }
        public string SDivName { get; set; }
        public string BPeriod { get; set; }
        public string CcCode { get; set; }
        public CompAssMnt ComputedAssMnt { get; set; }
        public Collection Collect { get; set; }
        public Percent Percnt { get; set; }

        public CollectCompAssMnt(DataRowView dr)
        {
            this.SDivCode = utility.GetColumnValue(dr, "SDIVCODE");
            this.SDivName = utility.GetColumnValue(dr, "SDIVNAME");
            this.BPeriod = utility.GetColumnValue(dr, "B_PERIOD");
            this.CcCode = utility.GetColumnValue(dr, "CC_CODE");

            this.ComputedAssMnt = new CompAssMnt(utility.GetColumnValue(dr, "PVT_COMP_ASSES"),
                utility.GetColumnValue(dr, "GVT_COMP_ASSES"),
                utility.GetColumnValue(dr, "COMP_ASSES"));
            this.Collect = new Collection(utility.GetColumnValue(dr, "PVT_COLL"),
                utility.GetColumnValue(dr, "GVT_COLL"),
                utility.GetColumnValue(dr, "TOT_COLL"));
            this.Percnt = new Percent(utility.GetColumnValue(dr, "PVT_PERCENT"),
                utility.GetColumnValue(dr, "GVT_PERCENT"),
                utility.GetColumnValue(dr, "TOT_PERCENT"));
        }
    }

    public class CompAssMnt
        {
            public string PvtCompAsses { get; set; }
            public string GvtCompAsses { get; set; }
            public string TotCompAsses { get; set; }

            public CompAssMnt(string pvtAssMnt, string gvtAssMnt, string totAssMnt)
            {
                this.PvtCompAsses = pvtAssMnt;
                this.GvtCompAsses = gvtAssMnt;
                this.TotCompAsses = totAssMnt;
            }
        }

        public class Collection
        {
            public string PvtColl { get; set; }
            public string GvtColl { get; set; }
            public string TotColl { get; set; }

            public Collection(string pvtColl, string gvtColl, string totColl)
            {
                this.PvtColl = pvtColl;
                this.GvtColl = gvtColl;
                this.TotColl = totColl;
            }
        }

        public class Percent
        {
            public string PvtPercent { get; set; }
            public string GvtPercent { get; set; }
            public string TotPercent { get; set; }

            public Percent(string pvtPer, string gvtPer, string totPer)
            {
                this.PvtPercent = pvtPer;
                this.GvtPercent = gvtPer;
                this.TotPercent = totPer;
            }
        }
}