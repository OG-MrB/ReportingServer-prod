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
    public class ZipCode
    {
        public String key;
        public String value;

        public static Dictionary<String, String> getAllZipCode()
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

                String query = "select distinct  TOP 500 address5 from person";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strZipCode = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strZipCode.Equals("null") || strZipCode.Equals("NULL") || strZipCode.Equals("Null") || strZipCode.Equals("")))
                        {
                            
                            strZipCode = strZipCode.Substring(0, 5).Trim();
                            int n;
                            bool isNumeric = int.TryParse(strZipCode, out n);

                            if (isNumeric && !dictEmp.ContainsKey(strZipCode))
                                dictEmp.Add(strZipCode, strZipCode);
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


        public static Dictionary<String, String> getZipByCompany(String companyId, String divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId , Logger.logLevel.Debug);

            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllZipCode();
            }

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
                    query = "select distinct  TOP 500 P.address5 FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select distinct  TOP 500 P.address5 FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";
                    query = "select distinct TOP 500  P.address5 FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    String strZipCode = reader.GetSqlValue(0).ToString().Trim();
                    if (!(strZipCode.Equals("null") || strZipCode.Equals("NULL") || strZipCode.Equals("Null") || strZipCode.Equals("")))
                    {
                        strZipCode = strZipCode.Substring(0, 5);
                        if (!dictEmp.ContainsKey(strZipCode))
                            dictEmp.Add(strZipCode, strZipCode);
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


        public static Dictionary<String, String> getZipByCompany(String companyId, String divisionId, String keyStroke)
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
                    query = "select distinct TOP 1000  P.address5 FROM person P WHERE P.address5 LIKE '%" + keyStroke + "%'";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {

                    query = "select distinct TOP 1000 P.address5 FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND P.address5 LIKE '%" + keyStroke + "%'";

                }
                else if (companyId.Equals("null"))
                {

                    query = "select distinct TOP 1000  P.address5 FROM person P, department D,rs_division Div";
                    query = query + " WHERE P.department = D.id AND  Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND P.address5 LIKE '%" + keyStroke + "%'";

                }
                else
                {
                    query = "select distinct TOP 1000  P.address5 FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND P.address5 LIKE '%" + keyStroke + "%'";

                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    String strZipCode = reader.GetSqlValue(0).ToString().Trim();
                    if (!(strZipCode.Equals("null") || strZipCode.Equals("NULL") || strZipCode.Equals("Null") || strZipCode.Equals("")))
                    {
                        strZipCode = strZipCode.Substring(0, 5);
                        if (!dictEmp.ContainsKey(strZipCode))
                            dictEmp.Add(strZipCode, strZipCode);
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


        public static List<ZipCode> getAllZipCodeList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<ZipCode> lstZipCode = new List<ZipCode>();
            
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "select distinct  TOP 50 address5 from person";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strZipCode = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strZipCode.Equals("null") || strZipCode.Equals("NULL") || strZipCode.Equals("Null") || strZipCode.Equals("")))
                        {
                            ZipCode zipObj = new ZipCode();
                            strZipCode = strZipCode.Substring(0, 5).Trim();
                            int n;
                            bool isNumeric = int.TryParse(strZipCode, out n);

                            if (isNumeric)
                            {
                                zipObj.value = strZipCode;
                                zipObj.key = strZipCode;
                                lstZipCode.Add(zipObj);
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

            lstZipCode = lstZipCode.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstZipCode;
        }


        public static List<ZipCode> getZipByCompanyList(String companyId, String divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId, Logger.logLevel.Debug);

            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllZipCodeList();
            }

            List<ZipCode> lstZipCode = new List<ZipCode>();

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
                    query = "select distinct  TOP 50 P.address5 FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select distinct  TOP 50 P.address5 FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";
                    query = "select distinct TOP 50  P.address5 FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strZipCode = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strZipCode.Equals("null") || strZipCode.Equals("NULL") || strZipCode.Equals("Null") || strZipCode.Equals("")))
                        {
                            ZipCode zipObj = new ZipCode();
                            strZipCode = strZipCode.Substring(0, 5).Trim();
                            int n;
                            bool isNumeric = int.TryParse(strZipCode, out n);

                            if (isNumeric)
                            {
                                zipObj.value = strZipCode;
                                zipObj.key = strZipCode;
                                lstZipCode.Add(zipObj);
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

            lstZipCode = lstZipCode.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstZipCode;
        }


        public static List<ZipCode> getZipByCompanyList(String companyId, String divisionId, String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId + keyStroke, Logger.logLevel.Debug);

            List<ZipCode> lstZipCode = new List<ZipCode>();

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
                    query = "select distinct TOP 50  P.address5 FROM person P WHERE P.address5 LIKE '%" + keyStroke + "%'";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {

                    query = "select distinct TOP 50 P.address5 FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND P.address5 LIKE '%" + keyStroke + "%'";

                }
                else if (companyId.Equals("null"))
                {

                    query = "select distinct TOP 50  P.address5 FROM person P, department D,rs_division Div";
                    query = query + " WHERE P.department = D.id AND  Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND P.address5 LIKE '%" + keyStroke + "%'";

                }
                else
                {
                    query = "select distinct TOP 50  P.address5 FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND P.address5 LIKE '%" + keyStroke + "%'";

                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strZipCode = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strZipCode.Equals("null") || strZipCode.Equals("NULL") || strZipCode.Equals("Null") || strZipCode.Equals("")))
                        {
                            ZipCode zipObj = new ZipCode();
                            strZipCode = strZipCode.Substring(0, 5).Trim();
                            int n;
                            bool isNumeric = int.TryParse(strZipCode, out n);

                            if (isNumeric)
                            {
                                zipObj.value = strZipCode;
                                zipObj.key = strZipCode;
                                lstZipCode.Add(zipObj);
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

            lstZipCode = lstZipCode.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstZipCode;
        }
    
    }
}