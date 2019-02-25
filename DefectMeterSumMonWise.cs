using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using util;

namespace DashBoardAPI
{
    public class DefectMeterSumMonWise
    {
        public string BillingMonth { get; set; }
        public string CenterCode { get; set; }
        
        //public string CenterName { get; set; }

        public List<DefectMonWise> defectMtrSumMonWise = new List<DefectMonWise>();

        public DefectMeterSumMonWise()
        {
            this.BillingMonth = string.Empty;
            this.CenterCode = string.Empty;
        }

        public DefectMeterSumMonWise(string pBillMon, string pCode, DataTable pDT)
        {
            this.BillingMonth = utility.GetBillMonth(pBillMon);
            this.CenterCode = pCode;

            foreach (DataRow dr in pDT.Rows)
            {
                defectMtrSumMonWise.Add(new DefectMonWise(dr));
            }
        }
    }

    public class DefectMonWise
    {
        public string SrNo { get; set; }
        public string OfficeName { get; set; }
        public string OneMonth { get; set; }
        public string Two_3_Months { get; set; }
        public string Four_6_Months { get; set; }
        public string MoreThan6Months { get; set; }

        public DefectMonWise(DataRow dr)
        {
            this.SrNo = utility.GetColumnValue(dr, "SRNO");
            this.OfficeName = utility.GetColumnValue(dr, "NAME");
            this.OneMonth = utility.GetColumnValue(dr, "ONE_MONTH");
            this.Two_3_Months = utility.GetColumnValue(dr, "TWO_TO_3_MONTH");
            this.Four_6_Months = utility.GetColumnValue(dr, "FOUR_TO_6_MONTH");
            this.MoreThan6Months = utility.GetColumnValue(dr, "MORE_THEN_6_MONTH");
        }
    }
}