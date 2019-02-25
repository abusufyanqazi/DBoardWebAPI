using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using util;

namespace DashBoardAPI
{
    public class DefectMeterSumTrfWise
    {
        public string BillingMonth { get; set; }
        public string CenterCode { get; set; }
        
        //public string CenterName { get; set; }

        public List<DefectTrfWise> defectTrfWise = new List<DefectTrfWise>();

        public DefectMeterSumTrfWise()
        {
            this.BillingMonth = string.Empty;
            this.CenterCode = string.Empty;
        }

        public DefectMeterSumTrfWise(string pBillMon, string pCode, DataTable pDT)
        {
            this.BillingMonth = utility.GetBillMonth(pBillMon);
            this.CenterCode = pCode;

            foreach (DataRow dr in pDT.Rows)
            {
                defectTrfWise.Add(new DefectTrfWise(dr));
            }
        }
    }

    public class DefectTrfWise
    {
        public string SrNo { get; set; }
        public string OfficeName { get; set; }
        public string Domestic { get; set; }
        public string Commercial { get; set; }
        public string Industrial { get; set; }
        public string Agricultural { get; set; }
        public string Others { get; set; }
        public string Total { get; set; }

        public DefectTrfWise(DataRow dr)
        {
            this.SrNo = utility.GetColumnValue(dr, "SRNO");
            this.OfficeName = utility.GetColumnValue(dr, "NAME");
            this.Domestic = utility.GetColumnValue(dr, "DOMESTIC");
            this.Commercial = utility.GetColumnValue(dr, "COMMERCIAL");
            this.Industrial = utility.GetColumnValue(dr, "INDUSTRIAL");
            this.Agricultural = utility.GetColumnValue(dr, "AGRICULTURE");
            this.Others = utility.GetColumnValue(dr, "OTHERS");
            this.Total = utility.GetColumnValue(dr, "TOTAL");
        }
    }
}