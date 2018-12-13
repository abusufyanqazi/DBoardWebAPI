using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;

using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace util
{
    public enum ApiType
    {
        COLLVSCOMPASSMNT,
        COLLVSBILLING,
        BILLINGSTATUS,
        RECEIVABLE
    }

    public class utility
    {
        Regex regNum;
        Regex regMobNum;

        public utility()
        {
            regNum = new Regex(@"^[0-9]{14}$");
            regMobNum = new Regex(@"(?:\+\s*\d{2}[\s-]*)?(?:\d[-\s]*){11}");
        }

        public static string GetColumnValue(DataTable dt, int rowNum, string colName)
        {
            string colVal = String.Empty;
            if (dt != null && dt.Rows.Count >= rowNum && !String.IsNullOrEmpty(colName))
            {
                colVal = dt.Rows[rowNum][colName].ToString();
            }

            return colVal;
        }
        public static string GetColumnValue(DataRow dr, string colName)
        {
            string colVal = String.Empty;
            if (dr != null && !String.IsNullOrEmpty(colName))
            {
                colVal = dr[colName] != DBNull.Value ? dr[colName].ToString() : "";
            }

            return colVal;
        }

        public static string GetColumnValue(DataRowView dr, string colName)
        {
            string colVal = String.Empty;
            if (dr != null && !String.IsNullOrEmpty(colName))
            {
                colVal = dr[colName] != DBNull.Value ? dr[colName].ToString() : "";
            }

            return colVal;
        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            JSONString.Append("{");
            JSONString.Append(@"""TABLE"":[");

            if (table != null && table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }

            }
            else
            {
                JSONString.Append("No data found.");
            }
            JSONString.Append("]");
            JSONString.Append("}");
            return JSONString.ToString();
        }

        DataRow[] GetFilteredRows(DataTable dt, string filterExp)
        {
            return dt.Select(filterExp);
        }

        public string GetCollVsCompAssmntJSON(DataTable dt, ArrayList keys)
        {
            StringBuilder JSONString = new StringBuilder();
            JSONString.Append("{");
            JSONString.Append(@"""CollVsCompAssmnt"":{");
            if (dt.Rows.Count > 0)
            {
                string ColumnName = "SDIVCODE";
                //DataRow[] drCirArr = dt.Select("LEN([" + ColumnName + "]) = 3");
                DataRow[] drCirArr = GetFilteredRows(dt, "LEN([" + ColumnName + "]) = 3");
                //foreach (DataRow drCir in drCirArr)
                //for (int j = 0; j < drCirArr.Length; j++)
                foreach (object key in keys)
                {

                    string cirCode = key.ToString();
                    //string cirCode = drCirArr[j][ColumnName].ToString();
                    JSONString.Append("\"" + cirCode + "\":{");
                    //DataRow[] drCirDataArr = table.Select(ColumnName + "=" + cirCode);
                    DataRow[] drCirDataArr = GetFilteredRows(dt, ColumnName + "=" + cirCode);
                    for (int i = 0; i < drCirDataArr.Length; i++)
                    {
                        JSONString.Append("\"" + "CompAssmnt\":{");
                        JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_COMP_ASSES"].ToString() + "\",");
                        JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_COMP_ASSES"].ToString() + "\",");
                        JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["COMP_ASSES"].ToString() + "\"");
                        JSONString.Append("},");

                        JSONString.Append("\"" + "Collection\":{");
                        JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_COLL"].ToString() + "\",");
                        JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_COLL"].ToString() + "\",");
                        JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_COLL"].ToString() + "\"");
                        JSONString.Append("},");

                        JSONString.Append("\"" + "Percentage\":{");
                        JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_PERCENT"].ToString() + "\",");
                        JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_PERCENT"].ToString() + "\",");
                        JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_PERCENT"].ToString() + "\"");
                        JSONString.Append("}");
                        if (i == drCirDataArr.Length - 1)
                        {
                            JSONString.Append("}");
                        }
                        else
                        {
                            JSONString.Append("},");
                        }
                    }
                    //                    break;
                }

            }
            else
            {
                JSONString.Append("No data found.");
            }
            JSONString.Append("}");
            JSONString.Append("}");
            return JSONString.ToString();
        }

        public void GetCollVsCompAssmntPJSON(DataTable dt, StringBuilder JSONString, string cirCode)
        {

            if (dt.Rows.Count > 0)
            {
                string ColumnName = "SDIVCODE";
                JSONString.Append("{");
                JSONString.Append("\"" + cirCode + "\":[{");
                DataRow[] drCirDataArr = GetFilteredRows(dt, ColumnName + "=" + cirCode);
                for (int i = 0; i < drCirDataArr.Length; i++)
                {
                    JSONString.Append("\"" + "CompAssmnt\":{");
                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_COMP_ASSES"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_COMP_ASSES"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["COMP_ASSES"].ToString() + "\",");
                    JSONString.Append("\"" + "SDIVNAME" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() + "\"");
                    JSONString.Append("},");

                    JSONString.Append("\"" + "Collection\":{");
                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_COLL"].ToString() + "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_COLL"].ToString() + "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_COLL"].ToString() + "\",");
                    JSONString.Append("\"" + "SDIVNAME" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() + "\"");
                    JSONString.Append("},");

                    JSONString.Append("\"" + "Percentage\":{");
                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_PERCENT"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_PERCENT"].ToString() + "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_PERCENT"].ToString() + "\",");
                    JSONString.Append("\"" + "SDIVNAME" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() + "\"");
                    JSONString.Append("}");
                    if (i == drCirDataArr.Length - 1)
                    {
                        JSONString.Append("}]");
                    }
                    else
                    {
                        JSONString.Append("}],");
                    }
                }

            }
        }

        public void GetCollVsCompAssmntPJSON1(DataTable dt, StringBuilder JSONString, string cirCode)
        {

            if (dt.Rows.Count > 0)
            {
                string ColumnName = "SDIVCODE";

                JSONString.Append("{" + "\"" + "Code" + "\"" + ":" + "\"" + cirCode + "\",");
                //JSONString.Append("\"" + "Name" + "\":" + "\"" + "{0}" + "\",");

                DataRow[] drCirDataArr = GetFilteredRows(dt, ColumnName + "=" + cirCode);
                for (int i = 0; i < drCirDataArr.Length; i++)
                {
                    JSONString.Append("\"" + "Name" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() + "\",");
                    JSONString.Append("\"" + "CompAssmnt\":{");

                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_COMP_ASSES"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_COMP_ASSES"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["COMP_ASSES"].ToString() + "\"");

                    JSONString.Append("},");

                    JSONString.Append("\"" + "Collection\":{");
                  //  JSONString.Append("\"" + "Name" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() +
                  //      "\",");
                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_COLL"].ToString() + "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_COLL"].ToString() + "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_COLL"].ToString() + "\"");

                    JSONString.Append("},");

                    JSONString.Append("\"" + "Percentage\":{");
                   // JSONString.Append("\"" + "Name" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() +
                   //     "\",");
                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_PERCENT"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_PERCENT"].ToString() + "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_PERCENT"].ToString() + "\"");

                    JSONString.Append("}");
                    if (i == drCirDataArr.Length - 1)
                    {
                        JSONString.Append("");
                    }
                    else
                    {
                        JSONString.Append(",");
                    }
                }

            }
        }


        //COLLECTON VS BILLING FUNCTION
        public void GetCollVsBillingPJSON(DataTable dt, StringBuilder JSONString, string cirCode)
        {

            if (dt.Rows.Count > 0)
            {
                string ColumnName = "SDIVCODE";

                JSONString.Append("{" + "\"" + "Code" + "\"" + ":" + "\"" + cirCode + "\",");
                //JSONString.Append("\"" + "Name" + "\":" + "\"" + "{0}" + "\",");

                DataRow[] drCirDataArr = GetFilteredRows(dt, ColumnName + "=" + cirCode);
                for (int i = 0; i < drCirDataArr.Length; i++)
                {
                    JSONString.Append("\"" + "Name" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() + "\",");
                    JSONString.Append("\"" + "MonthlyBilling\":{");

                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_ASSESS"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_ASSESS"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_ASSESS"].ToString() + "\"");

                    JSONString.Append("},");

                    JSONString.Append("\"" + "Collection\":{");
                    //  JSONString.Append("\"" + "Name" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() +
                    //      "\",");
                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_COLL"].ToString() + "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_COLL"].ToString() + "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_COLL"].ToString() + "\"");

                    JSONString.Append("},");

                    JSONString.Append("\"" + "Percentage\":{");
                    // JSONString.Append("\"" + "Name" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() +
                    //     "\",");
                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_PERCENT"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_PERCENT"].ToString() + "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_PERCENT"].ToString() + "\"");

                    JSONString.Append("}");
                    if (i == drCirDataArr.Length - 1)
                    {
                        JSONString.Append("");
                    }
                    else
                    {
                        JSONString.Append(",");
                    }
                }

            }
        }


        // GET-RECEIABLE
        public void GetReceievablePJSON(DataTable dt, StringBuilder JSONString, string cirCode)
        {
            //DataTable dt1 = dt;
            if (dt.Rows.Count > 0)
            {
                string ColumnName = "SDIVCODE";

                JSONString.Append("{" + "\"" + "Code" + "\"" + ":" + "\"" + cirCode + "\",");
                //JSONString.Append("\"" + "Name" + "\":" + "\"" + "{0}" + "\",");
                
                DataRow[] drCirDataArr = GetFilteredRows(dt, ColumnName + "='" + cirCode + "'");
                //DataRow[] drCirDataArr = dt.Select(ColumnName + "='" + cirCode + "'");
                for (int i = 0; i < drCirDataArr.Length; i++)
                {
                    JSONString.Append("\"" + "Name" + "\":" + "\"" + drCirDataArr[i]["NAME"].ToString() + "\",");
                    JSONString.Append("\"" + "Receivable\":{");

                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_REC"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_REC"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_REC"].ToString() + "\"");

                    JSONString.Append("},");

                    JSONString.Append("\"" + "Spill_Over\":{");
                    //  JSONString.Append("\"" + "Name" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() +
                    //      "\",");
                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_SPILL"].ToString() + "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_SPILL"].ToString() + "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_SPILL"].ToString() + "\"");

                    JSONString.Append("},");

                    JSONString.Append("\"" + "Arear\":{");
                    // JSONString.Append("\"" + "Name" + "\":" + "\"" + drCirDataArr[i]["SDIVNAME"].ToString() +
                    //     "\",");
                    JSONString.Append("\"" + "Private" + "\":" + "\"" + drCirDataArr[i]["PVT_ARREAR"].ToString() +
                                      "\",");
                    JSONString.Append("\"" + "Govt" + "\":" + "\"" + drCirDataArr[i]["GVT_ARREAR"].ToString() + "\",");
                    JSONString.Append("\"" + "Total" + "\":" + "\"" + drCirDataArr[i]["TOT_ARREAR"].ToString() + "\"");

                    JSONString.Append("}");
                    if (i == drCirDataArr.Length - 1)
                    {
                        JSONString.Append("");
                    }
                    else
                    {
                        JSONString.Append(",");
                    }
                }

            }
        }

        public string DataTableToJson(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder JsonStr = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                JsonStr.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    JsonStr.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count - 1)
                        {
                            //JsonStr.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                            JsonStr.Append(ds.Tables[0].Columns[j].ColumnName.ToString() + ":" + ds.Tables[0].Rows[i][j].ToString() + ",");
                        }
                        else if (j == ds.Tables[0].Columns.Count - 1)
                        {
                            //JsonStr.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                            JsonStr.Append(ds.Tables[0].Columns[j].ColumnName.ToString() + ":" + ds.Tables[0].Rows[i][j].ToString());
                        }
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        JsonStr.Append("}");
                    }
                    else
                    {
                        JsonStr.Append("},");
                    }
                }
                JsonStr.Append("]");
                return JsonStr.Replace("\\", "").ToString();
            }
            else
            {
                return null;
            }
        }

        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }

        public static string GetFormatedDate(string unfDate)
        {
            DateTime fDate = new DateTime(1900, 01, 01);
            DateTime.TryParse(unfDate, out fDate);
            return fDate.ToString("dd-MMM-yy");
        }

        public string FormatDecimal(decimal value)
        {
            return value.ToString("00000000000.00");

        }
        public bool IsNumber(string text)
        {

            return regNum.IsMatch(text);
        }

        public bool IsMobNumber(string text)
        {

            return regMobNum.IsMatch(text);
        }
    }
}