using ReportingServerWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ReportingServerWebService.Models
{
    public class Badge
    {
        // String companyId;
        public String key;
        public String value;

        // public String CompanyId { get { return this.companyId; } set { this.companyId = value; } }
        public String BadgeId { get { return this.key; } set { this.key = value; } }
        public String BadgeValue { get { return this.value; } set { this.value = value; } }

        public static Dictionary<String, String> getAllBadges()
        {

            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            Dictionary<String, String> dictBadge = new Dictionary<string, string>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "select TOP 500 bid from badge";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String bid = reader.GetSqlValue(0).ToString().Trim();
                        if (bid.Length == 11 || bid.Length == 12)
                        {
                            if (!dictBadge.ContainsKey(bid))
                            {                             
                                    dictBadge.Add(bid, bid.Substring(5));                               
                            }
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

            dictBadge = Utilities.sortData(dictBadge);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictBadge;
        }

        public static Dictionary<String, String> GetBadgeByCompanyAndDivision(string companyId, string divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId+divisionId, Logger.logLevel.Debug);

            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllBadges();
            }

            Dictionary<String, String> dictBadge = new Dictionary<string, string>();
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
                   // query = "select TOP 500  B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company and D.user2 like @division";
                    query = "select TOP 500 B.bid FROM badge B,person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    //query = "select B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user2 like @division";
                    query = "select TOP 500 B.bid FROM badge B,person P, department D, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    //query = "select B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company";
                    query = "select TOP 500 B.bid FROM badge B,person P, department D,rs_company Comp";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName";
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
                            if (!dictBadge.ContainsKey(bid))
                            {
                                if (!dictBadge.ContainsKey(bid))
                                {
                                    dictBadge.Add(bid, bid.Substring(5));
                                }

                            }
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

            dictBadge = Utilities.sortData(dictBadge);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictBadge;
        }

        public static Dictionary<String, String> GetBadgeByCompanyAndDivision(string companyId, string divisionId,string keyStroke)
        {

            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId+keyStroke, Logger.logLevel.Debug);
            Dictionary<String, String> dictBadge = new Dictionary<string, string>();
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
                    query = "select TOP 50  bid from badge where unique_id like '%" + keyStroke + "%'";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {
                    // query = "select TOP 500  B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company and D.user2 like @division";
                    query = "select TOP 50  B.bid FROM badge B,person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND B.unique_id like '%" + keyStroke + "%'";
                }
                else if (companyId.Equals("null"))
                {
                    //query = "select B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user2 like @division";
                    query = "select TOP 50  B.bid FROM badge B,person P, department D, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND B.unique_id like '%" + keyStroke + "%'";
                }
                else
                {
                    //query = "select B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company";
                    query = "select TOP 50  B.bi FROM badge B,person P, department D,rs_company Comp";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND B.unique_id like '%" + keyStroke + "%'";
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
                            if (!dictBadge.ContainsKey(bid))
                            {
                                if (!dictBadge.ContainsKey(bid))
                                {
                                    dictBadge.Add(bid, bid.Substring(5));
                                }
                            }
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
            dictBadge = Utilities.sortData(dictBadge);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictBadge;
        }


        public static List<Badge> getAllBadgesList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);
            List<Badge> lstBadge = new List<Badge>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "select TOP 50 bid from badge";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String bid = reader.GetSqlValue(0).ToString().Trim();
                        if (bid.Length == 11 || bid.Length == 12)
                        {
                            Badge objBadge = new Badge();
                            objBadge.BadgeId = reader.GetSqlValue(0).ToString().Trim();
                            objBadge.BadgeValue = reader.GetSqlValue(0).ToString().Trim().Substring(5);
                            lstBadge.Add(objBadge);
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

            lstBadge = lstBadge.OrderBy(x => x.BadgeValue).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstBadge;
        }

        public static List<Badge> GetBadgeByCompanyAndDivisionList(string companyId, string divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId , Logger.logLevel.Debug);

            List<Badge> lstBadge = new List<Badge>();
            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllBadgesList();
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
                    // query = "select TOP 500  B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company and D.user2 like @division";
                    query = "select TOP 50 B.bid FROM badge B,person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    //query = "select B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user2 like @division";
                    query = "select TOP 50 B.bid FROM badge B,person P, department D, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    //query = "select B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company";
                    query = "select TOP 50 B.bid FROM badge B,person P, department D,rs_company Comp";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName";
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
                            Badge objBadge = new Badge();
                            objBadge.BadgeId = reader.GetSqlValue(0).ToString().Trim();
                            objBadge.BadgeValue = reader.GetSqlValue(0).ToString().Trim().Substring(5);
                            lstBadge.Add(objBadge);
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

            lstBadge = lstBadge.OrderBy(x => x.BadgeValue).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstBadge;
        }

        public static List<Badge> GetBadgeByCompanyAndDivisionList(string companyId, string divisionId, string keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId + keyStroke, Logger.logLevel.Debug);

            List<Badge> lstBadge = new List<Badge>();
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
                    query = "select TOP 50  bid from badge where unique_id like '%" + keyStroke + "%'";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {
                    // query = "select TOP 500  B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company and D.user2 like @division";
                    query = "select TOP 50  B.bid FROM badge B,person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND B.unique_id like '%" + keyStroke + "%'";
                }
                else if (companyId.Equals("null"))
                {
                    //query = "select B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user2 like @division";
                    query = "select TOP 50  B.bid FROM badge B,person P, department D, rs_division Div";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND B.unique_id like '%" + keyStroke + "%'";
                }
                else
                {
                    //query = "select B.bid, B.unique_id from badge B,person P,department D where B.person_id = P.id and P.department = D.id and D.user1 like @company";
                    query = "select TOP 50  B.bid FROM badge B,person P, department D,rs_company Comp";
                    query = query + " WHERE B.person_id = P.id AND P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND B.unique_id like '%" + keyStroke + "%'";
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
                            Badge objBadge = new Badge();
                            objBadge.BadgeId = reader.GetSqlValue(0).ToString().Trim();
                            objBadge.BadgeValue = reader.GetSqlValue(0).ToString().Trim().Substring(5);
                            lstBadge.Add(objBadge);
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

            lstBadge = lstBadge.OrderBy(x => x.BadgeValue).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstBadge;
        }

    }
}