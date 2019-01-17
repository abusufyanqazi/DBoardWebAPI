﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Data.OracleClient;
using System.Text;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleCommand = System.Data.OracleClient.OracleCommand;
using OracleConnection = System.Data.OracleClient.OracleConnection;
using OracleDataAdapter = System.Data.OracleClient.OracleDataAdapter;
using OracleParameter = System.Data.OracleClient.OracleParameter;

//using Oracle.ManagedDataAccess.Client;

/// <summary>
/// Summary description for DB_Utility
/// </summary>

namespace DAL
{
    public class DB_Utility
    {
        private string _constr;

        public DB_Utility(string conStr)
        {
            //
            // TODO: Add constructor logic here
            //
            _constr = conStr;
        }

        public DataTable GetCashCollSummary(string code, DateTime billMon)
        {
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr);

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            string sortorder = "SRT_ORDER1";

            switch (code.Length)
            {
                case 2:
                {
                    sortorder = "IN('3','5')";
                    break;
                }
                case 3:
                {
                    sortorder = "IN('2','3')";
                    break;
                }
                case 4:
                {
                    sortorder = "IN('1','2')";
                    break;
                }
                default:
                {
                    sortorder = "IN('3','5')";
                    break;
                }


            }

            string sql =
                @"SELECT SRT_ORDER2, SRT_ORDER1, BILLMONTH, MAINDATE, MAINDATEC, sdiv_code CODE, sdiv_name NAME, DAILY_STUBS, ONLINE_STUBS, NORMAL_CASH_COLLECTED, ONLINE_CASH_COLLECTED, NORMAL_CASH_POSTED, ONLINE_CASH_POSTED, TOTAL_CASH_POSTED, RCO_FEE, ADV_CASH, UNIDENTIFIED_CASH, P_DISC_PAYMENT, GOVT_PAYMENT, TUBEWELL_PAYMENT
                                    FROM VW_CASH_COLL_SUMMARY";
            if (!string.IsNullOrEmpty(code))
            {
                sql += " WHERE sdiv_code LIKE '" + code + "%' AND SRT_ORDER2 " + sortorder
                       //+ " AND BILLMONTH='01-" + billMon.ToString("MMM") + "-" + billMon.ToString("yyyy") + "'" 
                       + " AND BILLMONTH=(SELECT MAX(BILLMONTH) FROM VW_CASH_COLL_SUMMARY)"
                       + " ORDER BY SRT_ORDER1";
            }

            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable GetFeederLosses(DateTime billMon)
        {
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr);

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            string sql =
                @"select ""DIV_CIR"",""DIVNAME"",""0 or below 0"" ZERO_BELOW,""0-10"" ZERO_TEN,""10 20"" TEN_TWENTY,""20 30"" TWENTY_THIRTY,
                            ""30 40"" THIRTY_FOURTY,""40 50"" FOURTY_FIFTY,""Above 50"" ABOVE_FIFTY, ""TOTAL"",""BPERIOD"",""CC_CODE"" 
                            from VW_FEEDER_LINE_LOSS";
            //sql += " WHERE BPERIOD='01-" + billMon.ToString("MMM") + "-" + billMon.ToString("yyyy") + "'" 
            sql += " WHERE BPERIOD=(SELECT MAX(BPERIOD) FROM VW_FEEDER_LINE_LOSS)"
                   + " ORDER BY DIV_CIR";
            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable getBillingStatus()
        {
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr); //
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            cmd = new OracleCommand(
                @"SELECT BILLING_MONTH, DIV_NAME, BATCH_DUE, BATCH_RECEIVED, BATCH_BILLED, CC_CODE, RECEIVED_DATE, POSTED_DATE
                                FROM BILLING_STATUS_DIV
                                where BILLING_MONTH=(SELECT MAX(BILLING_MONTH) FROM BILLING_STATUS_DIV)", con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable GetCollVsCompAssmnt(string code)
        {
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr); //
            string sortorder = "";

            switch (code.Length)
            {
                case 2:
                {
                    sortorder = "IN('3','5')";
                    break;
                }
                case 3:
                {
                    sortorder = "IN('2','3')";
                    break;
                }
                case 4:
                {
                    sortorder = "IN('1','2')";
                    break;
                }
                default:
                {
                    sortorder = "IN('3','5')";
                    break;
                }


            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            string sql =
                @"SELECT SRT_ORDER2, SRT_ORDER1, SDIVCODE, SDIVNAME, PVT_COMP_ASSES, GVT_COMP_ASSES, COMP_ASSES, PVT_COLL, GVT_COLL, TOT_COLL, B_PERIOD, CC_CODE, PVT_PERCENT, GVT_PERCENT, TOT_PERCENT
                                      FROM  VW_COLL_VS_COMP_ASS
                                      WHERE B_PERIOD=(SELECT MAX(B_PERIOD) FROM VW_COLL_VS_COMP_ASS)";
            if (!string.IsNullOrEmpty(code))
            {
                sql += " AND SDIVCODE LIKE '" + code + "%' AND SRT_ORDER2 " + sortorder;

            }

            sql += " ORDER BY SRT_ORDER1";
            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        //COLLECTION VS BILLING 
        public DataTable GetCollVsBilling(string code)
        {
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr); //
            string sortorder = "";

            switch (code.Length)
            {
                case 2:
                {
                    sortorder = "IN('3','5')";
                    break;
                }
                case 3:
                {
                    sortorder = "IN('2','3')";
                    break;
                }
                case 4:
                {
                    sortorder = "IN('1','2')";
                    break;
                }
                default:
                {
                    sortorder = "IN('3','5')";
                    break;
                }


            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            string sql =
                @"SELECT SRT_ORDER2, SRT_ORDER1, SDIVCODE, SDIVNAME, PVT_ASSESS, GVT_ASSESS, TOT_ASSESS, PVT_COLL, GVT_COLL, TOT_COLL, B_PERIOD, CC_CODE, PVT_PERCENT, GVT_PERCENT, TOT_PERCENT
                                      FROM VW_COLL_VS_BILL
                                       WHERE B_PERIOD=(SELECT MAX(B_PERIOD) FROM VW_COLL_VS_BILL)";
            if (!string.IsNullOrEmpty(code))
            {
                sql += " AND SDIVCODE LIKE '" + code + "%' AND SRT_ORDER2 " + sortorder;
            }

            sql += " ORDER BY SRT_ORDER1";

            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable getReceiveables(string code, DateTime billMon)
        {
            string sql =
                @"SELECT SRT_ORDER2, SRT_ORDER1, sdiv_code CODE, sdiv_name NAME, PVT_REC, GVT_REC, TOT_REC, PVT_SPILL, 
                                      GVT_SPILL, TOT_SPILL, PVT_ARREAR, GVT_ARREAR, TOT_ARREAR, CC_CODE, B_PERIOD
                                      FROM VW_RECIEVABLES";
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr);

            string sortorder = "";

            switch (code.Length)
            {
                case 2:
                {
                    sortorder = "IN('3','5')";
                    break;
                }
                case 3:
                {
                    sortorder = "IN('2','3')";
                    break;
                }
                case 4:
                {
                    sortorder = "IN('1','2')";
                    break;
                }
                default:
                {
                    sortorder = "IN('3','5')";
                    break;
                }


            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            sql += " WHERE SRT_ORDER2 " + sortorder;
            if (!string.IsNullOrEmpty(code))
            {
                sql += " AND sdiv_code LIKE '" + code + "%'";
            }

            //sql += " AND B_PERIOD='01-" + billMon.ToString("MMM") + "-" + billMon.ToString("yyyy") + "'";
            sql += " AND B_PERIOD=(SELECT MAX(B_PERIOD) FROM VW_RECIEVABLES)";
            sql += " ORDER BY SRT_ORDER1";

            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable getMonLosses(string code, DateTime billMon)
        {
            string sql =
                @"SELECT SRT_ORDER2, SRT_ORDER1, SDIV, SDIVNAME, B_PERIOD, RCV, BIL, LOS, PCT, PRV_PERIOD, PRV_RCV, PRV_BIL, PRV_LOS, PRV_PCT, VAR_INCDEC
                           FROM VW_MONTHLY_LINE_LOSS";
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr);

            string sortorder = "";

            switch (code.Length)
            {
                case 2:
                {
                    sortorder = "IN('3','5')";
                    break;
                }
                case 3:
                {
                    sortorder = "IN('2','3')";
                    break;
                }
                case 4:
                {
                    sortorder = "IN('1','2')";
                    break;
                }
                default:
                {
                    sortorder = "IN('3','5')";
                    break;
                }


            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            sql += " WHERE SRT_ORDER2 " + sortorder;
            if (!string.IsNullOrEmpty(code))
            {
                sql += " AND SDIV LIKE '" + code + "%'";
            }

            sql += " AND B_PERIOD=(SELECT MAX(B_PERIOD) FROM VW_MONTHLY_LINE_LOSS)";
            //sql += " AND B_PERIOD='01-" + billMon.ToString("MMM") + "-" + billMon.ToString("yyyy") + "'";
            sql += " ORDER BY SRT_ORDER1";
            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable getPrgsLosses(string code, DateTime billMon)
        {
            string sql =
                @"SELECT SRT_ORDER2, SRT_ORDER1, SDIV, SDIVNAME, B_PERIOD, RCV, BIL, LOS, PCT, PRV_PERIOD, PRV_RCV, PRV_BIL, PRV_LOS, PRV_PCT, VAR_INCDEC
                            FROM VW_PROG_LINE_LOSSES";
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr);

            string sortorder = "";

            switch (code.Length)
            {
                case 2:
                {
                    sortorder = "IN('3','5')";
                    break;
                }
                case 3:
                {
                    sortorder = "IN('2','3')";
                    break;
                }
                case 4:
                {
                    sortorder = "IN('1','2')";
                    break;
                }
                default:
                {
                    sortorder = "IN('3','5')";
                    break;
                }


            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            sql += " WHERE SRT_ORDER2 " + sortorder;
            if (!string.IsNullOrEmpty(code))
            {
                sql += " AND SDIV LIKE '" + code + "%'";
            }

            //sql += " AND B_PERIOD='01-" + billMon.ToString("MMM") + "-" + billMon.ToString("yyyy") + "'";
            sql += " ORDER BY SRT_ORDER1";
            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable getBillingStatsBatchWise(string code, DateTime billMon)
        {
            string sql =
                @"SELECT SRT_ORDER2, SRT_ORDER1, MONTH, BATCH, SDIV_CODE CODE, SDIV_NAME NAME, TNOCONSUMERS, NOUNBILLEDCASES, NOSTSREADING, NODISCASES, NORECCASES, NOMCOCASES, NODEFMETERS, LOCKCASES, NONEWCONN, CREDBALCONSM, NOHEAVYBCASES, CREDBALAMT "
                + " from VW_BILLING_STATS_BATCHWISE "
                + " where MONTH = (select max(MONTH) from VW_BILLING_STATS_BATCHWISE)";
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr);

            string sortorder = "";

            switch (code.Length)
            {
                case 2:
                {
                    sortorder = "IN('3','5')";
                    break;
                }
                case 3:
                {
                    sortorder = "IN('2','3')";
                    break;
                }
                case 4:
                {
                    sortorder = "IN('1','2')";
                    break;
                }
                default:
                {
                    sortorder = "IN('3','5')";
                    break;
                }


            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            sql += " AND SRT_ORDER2 " + sortorder;
            if (!string.IsNullOrEmpty(code))
            {
                sql += " AND SDIV_CODE LIKE '" + code + "%'";
            }

            //sql += " AND B_PERIOD='01-" + billMon.ToString("MMM") + "-" + billMon.ToString("yyyy") + "'";
            sql += " ORDER BY SRT_ORDER1";
            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable getAssesmentBatchWise(string code, DateTime billMon)
        {
            string sql =
                @"SELECT SRT_ORDER2, SRT_ORDER1, MONTH, BATCH, SDIV_CODE, SDIV_NAME, NOBILLSISSUED, OPB, CURASSESS, GOVTASSESS, NET, UNITBILLED, RURALUNITBILLED, URBANUNITBILLED, NOADJUSTM, UNITADJ, AMTADJ, NODETADJ, DETADJUNITS, DETADJAMT"
                + " from VW_ASSESMENT_BATCHWISE "
                + " where MONTH = (select max(MONTH) from VW_ASSESMENT_BATCHWISE)";
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr);

            string sortorder = "";

            switch (code.Length)
            {
                case 2:
                {
                    sortorder = "IN('3','5')";
                    break;
                }
                case 3:
                {
                    sortorder = "IN('2','3')";
                    break;
                }
                case 4:
                {
                    sortorder = "IN('1','2')";
                    break;
                }
                default:
                {
                    sortorder = "IN('3','5')";
                    break;
                }


            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            sql += " AND SRT_ORDER2 " + sortorder;
            if (!string.IsNullOrEmpty(code))
            {
                sql += " AND SDIV_CODE LIKE '" + code + "%'";
            }

            //sql += " AND B_PERIOD='01-" + billMon.ToString("MMM") + "-" + billMon.ToString("yyyy") + "'";
            sql += " ORDER BY SRT_ORDER1";
            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable getBillingStatsDaily(string code, DateTime billMon)
        {
            string sql =
                @"SELECT SRT_ORDER2, SRT_ORDER1, MONTH, SDIV_CODE CODE, SDIV_NAME NAME, TNOCONSUMERS, NOUNBILLEDCASES, NOSTSREADING, NODISCASES, NORECCASES, NOMCOCASES, NODEFMETERS, LOCKCASES, NONEWCONN, CREDBALCONSM, NOHEAVYBCASES, CREDBALAMT "
                + " from VW_BILLING_STATS_DAILY "
                + " where MONTH = (select max(MONTH) from VW_BILLING_STATS_DAILY)";
            OracleConnection con = null;
            OracleCommand cmd;
            con = new OracleConnection(_constr);

            string sortorder = "";

            switch (code.Length)
            {
                case 2:
                {
                    sortorder = "IN('3','5')";
                    break;
                }
                case 3:
                {
                    sortorder = "IN('2','3')";
                    break;
                }
                case 4:
                {
                    sortorder = "IN('1','2')";
                    break;
                }
                default:
                {
                    sortorder = "IN('3','5')";
                    break;
                }


            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            sql += " AND SRT_ORDER2 " + sortorder;
            if (!string.IsNullOrEmpty(code))
            {
                sql += " AND SDIV_CODE LIKE '" + code + "%'";
            }

            //sql += " AND B_PERIOD='01-" + billMon.ToString("MMM") + "-" + billMon.ToString("yyyy") + "'";
            sql += " ORDER BY SRT_ORDER1";
            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable getTheftData(string refNo)
        {
            string sql = @"select BILL_MNTH, NOTE_NO, ADJ_DT, UNITS, AMOUNT,PAY_AGAINTS_DET"
                         + " from vw_theft_dt_portal "
                         //+ " where BATCH||SDIV|| CONS_NO ='" + refNo + "'";
                         + " WHERE  APPLICATION_NO = (SELECT GET_APP_BY_REF_CUR_MONTH('" + refNo + "') FROM DUAL)";
            OracleConnection con = null;
            OracleCommand cmd;
            string mndConStr = System.Configuration.ConfigurationManager.ConnectionStrings["MND_CONSTR"].ToString();
            con = new OracleConnection(mndConStr);


            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            cmd = new OracleCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return null;
        }

        public DataTable getBillData(string ts, string kwh, DateTime strtPeriod, DateTime endPeriod, string trf)
        {
            string startMonth = strtPeriod.ToString("dd") + "-" + strtPeriod.ToString("MMM") + "-" +
                                strtPeriod.ToString("yyyy");
            string endMonth = endPeriod.ToString("dd") + "-" + endPeriod.ToString("MMM") + "-" +
                              endPeriod.ToString("yyyy");
            string sql = "PROC_BILL4API";
            OracleConnection con = null;
            string cisConStr = System.Configuration.ConfigurationManager.ConnectionStrings["MEPCO2.6"].ToString();
            con = new OracleConnection(cisConStr);
            DataTable dt = new DataTable();

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                OracleCommand cmd =
                    new OracleCommand(sql, con);
                
                OracleParameter opTS = new OracleParameter("TS", OracleDbType.Varchar2);
                opTS.Value = ts;
                cmd.Parameters.Add(opTS);

                OracleParameter opKWH = new OracleParameter("KWH", OracleType.Number);
                opKWH.Value = Int32.Parse(kwh);
                cmd.Parameters.Add(opKWH);
                OracleParameter opSperiod = new OracleParameter("S_PERIOD", OracleDbType.Date);
                opSperiod.Value = startMonth.ToUpper();
                cmd.Parameters.Add(opSperiod);

                OracleParameter opEperiod = new OracleParameter("E_PERIOD", OracleDbType.Date);
                opEperiod.Value = endMonth.ToUpper();
                cmd.Parameters.Add(opEperiod);

                OracleParameter opTRF = new OracleParameter("TRF", OracleType.VarChar);
                opTRF.Value = trf;
                cmd.Parameters.Add(opTRF);
                OracleParameter opResult=new OracleParameter("RESULT", OracleType.Cursor);
                opResult.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(opResult);
                
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                
                da.Fill(dt);  
            }
            catch (Exception ex)
            {
                DataTable dtErr = new DataTable();
                dtErr.Columns.Add("Desc");
                DataRow drErr = dtErr.NewRow();
                drErr["Desc"] = ex.ToString();
                dtErr.Rows.Add(drErr);
                return dtErr;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return dt;
        }
    }
}