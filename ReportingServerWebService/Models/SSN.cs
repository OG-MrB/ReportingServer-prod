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
    public class SSN
    {
        public string key;
        public string value;

        public static Dictionary<String, String> getAllSSN()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            Dictionary<String, String> dictEmp = new Dictionary<string, string>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "select distinct TOP 1000 description from person_user where slot_number = 13";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strArea = reader.GetSqlValue(0).ToString();
                        if (!(strArea.Equals("") || strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            if (!dictEmp.ContainsKey(reader.GetSqlValue(0).ToString()))
                                dictEmp.Add(reader.GetSqlValue(0).ToString(), reader.GetSqlValue(0).ToString().Trim());
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
            dictEmp = Utilities.sortData(dictEmp);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictEmp;
        }


        public static Dictionary<String, String> getSSNByCompany(String companyId, String divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId , Logger.logLevel.Debug);

            Dictionary<String, String> dictEmp = new Dictionary<string, string>();

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
                    //query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division group by P.id";
                    query = "select distinct TOP 1000 PU.description FROM person P, department D,rs_company Comp, rs_division Div, person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND  P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select distinct TOP 1000  PU.description FROM person P, department D, rs_division Div, person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";
                    query = "select distinct TOP 1000  PU.description FROM person P, department D,rs_company Comp,person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND P.department = D.id AND Comp.companyId = @company AND  D.user1 LIKE Comp.companyName";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strArea = reader.GetSqlValue(0).ToString();
                        if (!(strArea.Equals("") || strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            if (!dictEmp.ContainsKey(reader.GetSqlValue(0).ToString()))
                                dictEmp.Add(reader.GetSqlValue(0).ToString(), reader.GetSqlValue(0).ToString().Trim());
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
            dictEmp = Utilities.sortData(dictEmp);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictEmp;
        }


        public static Dictionary<String, String> getSSNByCompany(String companyId, String divisionId, String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId + keyStroke, Logger.logLevel.Debug);

            Dictionary<String, String> dictEmp = new Dictionary<string, string>();

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
                    query = "select distinct TOP 1000  PU.description FROM person_user PU WHERE PU.description LIKE '%" + keyStroke + "%'";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {
                    //query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division group by P.id";

                    query = "select distinct TOP 1000  PU.description FROM person P, department D,rs_company Comp, rs_division Div, person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND  P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND PU.description LIKE '%" + keyStroke + "%'";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select distinct TOP 1000 PU.description FROM person P, department D, rs_division Div, person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND  P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND PU.description LIKE '%" + keyStroke + "%'";

              
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";
                    query = "select distinct TOP 1000  PU.description FROM person P, department D,rs_company Comp, person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND  P.department = D.id AND Comp.companyId = @company AND  D.user1 LIKE Comp.companyName AND PU.description LIKE '%" + keyStroke + "%'";

                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strArea = reader.GetSqlValue(0).ToString();
                        if (!(strArea.Equals("") || strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            if (!dictEmp.ContainsKey(reader.GetSqlValue(0).ToString()))
                                dictEmp.Add(reader.GetSqlValue(0).ToString(), reader.GetSqlValue(0).ToString().Trim());
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
            dictEmp = Utilities.sortData(dictEmp);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictEmp;
        }


        public static List<SSN> getAllSSNList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<SSN> lstSsn = new List<SSN>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "select distinct TOP 50 description from person_user where slot_number = 13";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        SSN objSSN = new SSN();
                        String strArea = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strArea.Equals("") || strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            objSSN.key = reader.GetSqlValue(0).ToString().Trim();
                            objSSN.value = reader.GetSqlValue(0).ToString().Trim();
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

            lstSsn = lstSsn.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstSsn;
        }


        public static List<SSN> getSSNByCompanyList(String companyId, String divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId , Logger.logLevel.Debug);

            List<SSN> lstSsn = new List<SSN>();

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
                    //query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division group by P.id";
                    query = "select distinct TOP 50 PU.description FROM person P, department D,rs_company Comp, rs_division Div, person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND  P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select distinct TOP 50  PU.description FROM person P, department D, rs_division Div, person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";
                    query = "select distinct TOP 50  PU.description FROM person P, department D,rs_company Comp,person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND P.department = D.id AND Comp.companyId = @company AND  D.user1 LIKE Comp.companyName";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());

                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        SSN objSSN = new SSN();
                        String strArea = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strArea.Equals("") || strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            objSSN.key = reader.GetSqlValue(0).ToString().Trim();
                            objSSN.value = reader.GetSqlValue(0).ToString().Trim();
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

            lstSsn = lstSsn.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstSsn;
        }


        public static List<SSN> getSSNByCompanyList(String companyId, String divisionId, String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId + keyStroke, Logger.logLevel.Debug);

            List<SSN> lstSsn = new List<SSN>();
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
                    query = "select distinct TOP 50  PU.description FROM person_user PU WHERE PU.description LIKE '%" + keyStroke + "%'";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {
                    //query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division group by P.id";

                    query = "select distinct TOP 50  PU.description FROM person P, department D,rs_company Comp, rs_division Div, person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND  P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND PU.description LIKE '%" + keyStroke + "%'";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select distinct TOP 50 PU.description FROM person P, department D, rs_division Div, person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND  P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND PU.description LIKE '%" + keyStroke + "%'";


                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";
                    query = "select distinct TOP 50  PU.description FROM person P, department D,rs_company Comp, person_user PU";
                    query = query + " WHERE P.id = PU.person_id AND  P.department = D.id AND Comp.companyId = @company AND  D.user1 LIKE Comp.companyName AND PU.description LIKE '%" + keyStroke + "%'";

                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        SSN objSSN = new SSN();
                        String strArea = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strArea.Equals("") || strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            objSSN.key = reader.GetSqlValue(0).ToString().Trim();
                            objSSN.value = reader.GetSqlValue(0).ToString().Trim();
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

            lstSsn = lstSsn.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstSsn;
        }
    
    }
}