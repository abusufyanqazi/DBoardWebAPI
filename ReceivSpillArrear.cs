using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using util;

namespace DashBoardAPI
{
    public class ReceivSpillArrear
    {
        public string SDivCode { get; set; }
        public string SDivName { get; set; }
        public string BPeriod { get; set; }
        public string CcCode { get; set; }
        public Receivable Receive { get; set; }
        public SpillOver Spill { get; set; }
        public Arrear Arr { get; set; }

        public ReceivSpillArrear(DataRowView dr)
        {
            this.SDivCode = utility.GetColumnValue(dr, "CODE");
            this.SDivName = utility.GetColumnValue(dr, "NAME");
            this.BPeriod = utility.GetColumnValue(dr, "B_PERIOD");
            this.CcCode = utility.GetColumnValue(dr, "CC_CODE");

            this.Receive = new Receivable(utility.GetColumnValue(dr, "PVT_REC"),
                utility.GetColumnValue(dr, "GVT_REC"),
                utility.GetColumnValue(dr, "TOT_REC"));
            this.Spill = new SpillOver(utility.GetColumnValue(dr, "PVT_SPILL"),
                utility.GetColumnValue(dr, "GVT_SPILL"),
                utility.GetColumnValue(dr, "TOT_SPILL"));
            this.Arr= new Arrear(utility.GetColumnValue(dr, "PVT_ARREAR"),
                utility.GetColumnValue(dr, "GVT_ARREAR"),
                utility.GetColumnValue(dr, "TOT_ARREAR"));
        }
    }

    public class Receivable
    {
        public string PvtReceivable { get; set; }
        public string GvtReceivable { get; set; }
        public string TotReceivable { get; set; }

        public Receivable(string pvtReceive, string gvtAReceive, string totReveive)
        {
            this.PvtReceivable = pvtReceive;
            this.GvtReceivable = gvtAReceive;
            this.TotReceivable = totReveive;
        }
    }

    public class SpillOver
    {
        public string PvtSpill { get; set; }
        public string GvtSpill { get; set; }
        public string TotSpill { get; set; }

        public SpillOver(string pvtSpill, string gvtSpill, string totSpill)
        {
            this.PvtSpill = pvtSpill;
            this.GvtSpill = gvtSpill;
            this.TotSpill = totSpill;
        }
    }

    public class Arrear
    {
        public string PvtArrear { get; set; }
        public string GvtArrear { get; set; }
        public string TotArrear { get; set; }

        public Arrear(string pvtArrear, string gvtArrear, string totArrear)
        {
            this.PvtArrear = pvtArrear;
            this.GvtArrear = gvtArrear;
            this.TotArrear = totArrear;
        }
    }

}