using ReportingServerWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ReportingServerWebService.Models
{
    public class CardNo
    {
        // String companyId;
        public String key;
        public String value;

        // public String CompanyId { get { return this.companyId; } set { this.companyId = value; } }
        public String CardNoId { get { return this.key; } set { this.key = value; } }
        public String CardNoValue { get { return this.value; } set { this.value = value; } }

        public static List<CardNo> getAllCardNosList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);
            List<CardNo> lstCardNo = new List<CardNo>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "select TOP 50 bid from badge where bid is NOT NULL and (len(bid)=11) ";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String bid = reader.GetSqlValue(0).ToString().Trim();
                        if (bid.Length == 11 || bid.Length == 12)
                        {
                            CardNo objCardNo = new CardNo();
                            objCardNo.CardNoId = reader.GetSqlValue(0).ToString().Trim();
                            objCardNo.CardNoValue = reader.GetSqlValue(0).ToString().Trim().Substring(5);
                            lstCardNo.Add(objCardNo);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace(ex, true);
                var line = stackTrace.GetFrame(0).GetFileLineNumber();
                Logger.LogExceptions(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message, line.ToString(), Logger.logLevel.Exception);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }

            lstCardNo = lstCardNo.OrderBy(x => x.CardNoValue).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstCardNo;
        }

        public static List<CardNo> GetCardNoByCompanyAndDivisionList(string companyId, string divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId , Logger.logLevel.Debug);

            List<CardNo> lstCardNo = new List<CardNo>();
            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllCardNosList();
            }

           
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();


                String query;
                if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {
                    // query = "select TOP 500  B.bid, B.unique_id from CardNo B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company and D.user2 like @division";
                    query = "select TOP 50 B.bid FROM badge B,person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName and B.bid is NOT NULL and (len(B.bid)=11 )";
                }
                else if (companyId.Equals("null"))
                {
                    //query = "select B.bid, B.unique_id from CardNo B,person P,department D where B.person_id = P.id and P.department = D.id and D.user2 like @division";
                    query = "select TOP 50 B.bid FROM badge B,person P, department D, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName and B.bid is NOT NULL and (len(B.bid)=11 )";
                }
                else
                {
                    //query = "select B.bid, B.unique_id from CardNo B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company";
                    query = "select TOP 50 B.bid FROM badge B,person P, department D,rs_company Comp";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName and B.bid is NOT NULL and (len(B.bid)=11 )";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String bid = reader.GetSqlValue(0).ToString().Trim();
                        if (bid.Length == 11 || bid.Length == 12)
                        {
                            CardNo objCardNo = new CardNo();
                            objCardNo.CardNoId = reader.GetSqlValue(0).ToString().Trim();
                            objCardNo.CardNoValue = reader.GetSqlValue(0).ToString().Trim().Substring(5);
                            lstCardNo.Add(objCardNo);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace(ex, true);
                var line = stackTrace.GetFrame(0).GetFileLineNumber();
                Logger.LogExceptions(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message, line.ToString(), Logger.logLevel.Exception);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }

            lstCardNo = lstCardNo.OrderBy(x => x.CardNoValue).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstCardNo;
        }

        public static List<CardNo> GetCardNoByCompanyAndDivisionList(string companyId, string divisionId, string keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId + keyStroke, Logger.logLevel.Debug);

            List<CardNo> lstCardNo = new List<CardNo>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();


                String query;

                if (companyId.Equals("null") && divisionId.Equals("null"))
                {
                    query = "select TOP 50 B.bid from badge B where B.bid like '%" + keyStroke + "%' and (len(B.bid)=11 )";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {
                    // query = "select TOP 500  B.bid, B.unique_id from CardNo B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company and D.user2 like @division";
                    query = "select TOP 50  B.bid FROM badge B,person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND B.bid like '%" + keyStroke + "%' AND (len(B.bid)=11 )";
                }
                else if (companyId.Equals("null"))
                {
                    //query = "select B.bid, B.unique_id from CardNo B,person P,department D where B.person_id = P.id and P.department = D.id and D.user2 like @division";
                    query = "select TOP 50  B.bid FROM badge B,person P, department D, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND B.bid like '%" + keyStroke + "%' and (len(B.bid)=11 ) ";
                }
                else
                {
                    //query = "select B.bid, B.unique_id from CardNo B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company";
                    query = "select TOP 50  B.bid FROM badge B,person P, department D,rs_company Comp";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND B.bid like '%" + keyStroke + "%' and (len(B.bid)=11 )";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String bid = reader.GetSqlValue(0).ToString().Trim();
                        if (bid.Length == 11 || bid.Length == 12)
                        {
                            CardNo objCardNo = new CardNo();
                            objCardNo.CardNoId = reader.GetSqlValue(0).ToString().Trim();
                            objCardNo.CardNoValue = reader.GetSqlValue(0).ToString().Trim().Substring(5);
                            lstCardNo.Add(objCardNo);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace(ex, true);
                var line = stackTrace.GetFrame(0).GetFileLineNumber();
                Logger.LogExceptions(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message, line.ToString(), Logger.logLevel.Exception);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }

            lstCardNo = lstCardNo.OrderBy(x => x.CardNoValue).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstCardNo;
        }

    }
    
}