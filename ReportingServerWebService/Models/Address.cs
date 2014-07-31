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
    public class Address
    {
        public string key;
        public string value;

        public static Dictionary<String, String> getAllCities()
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

                String query = "select distinct TOP 1000 address3 from person";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strZipCode = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strZipCode.Equals("null") || strZipCode.Equals("NULL") || strZipCode.Equals("Null") || strZipCode.Equals("")))
                        {
                            if (!dictEmp.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictEmp.Add(reader.GetSqlValue(0).ToString().Trim(), strZipCode);
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


        public static Dictionary<String, String> getCityByCompany(String companyId, String divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId+divisionId, Logger.logLevel.Debug);

            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllCities();
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
                    query = "select distinct TOP 1000  P.address3 FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select distinct TOP 1000 P.address3 FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";

                    query = "select distinct TOP 1000 P.address3 FROM person P, department D,rs_company Comp";
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

                            if (!dictEmp.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictEmp.Add(reader.GetSqlValue(0).ToString().Trim(), strZipCode);
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


        public static Dictionary<String, String> getCityByCompany(String companyId, String divisionId, String keyStroke)
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
                    query = "select distinct TOP 1000  P.address3 FROM person P WHERE P.address3 LIKE '%" + keyStroke + "%'";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {

                    query = "select distinct TOP 1000  P.address3 FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND P.address3 LIKE '%" + keyStroke + "%'";

                }
                else if (companyId.Equals("null"))
                {

                    query = "select distinct TOP 1000  P.address3 FROM person P, department D,rs_division Div";
                    query = query + " WHERE P.department = D.id AND  Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND P.address3 LIKE '%" + keyStroke + "%'";

                }
                else
                {
                    query = "select distinct TOP 1000 P.address3 FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND P.address3 LIKE '%" + keyStroke + "%'";

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

                            if (!dictEmp.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictEmp.Add(reader.GetSqlValue(0).ToString().Trim(), strZipCode);
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


        public static List<Address> getAllCitiesList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<Address> lstAddress = new List<Address>();
            
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "select distinct TOP 50 address3 from person";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strAddress = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strAddress.Equals("null") || strAddress.Equals("NULL") || strAddress.Equals("Null") || strAddress.Equals("")))
                        {
                            Address addressObj = new Address();
                            addressObj.key = reader.GetSqlValue(0).ToString().Trim();
                            addressObj.value = reader.GetSqlValue(0).ToString().Trim();
                            lstAddress.Add(addressObj);
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

            lstAddress = lstAddress.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstAddress;
        }


        public static List<Address> getCityByCompanyList(String companyId, String divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId, Logger.logLevel.Debug);

            List<Address> lstAddress = new List<Address>();
            
            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllCitiesList();
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
                    //query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division group by P.id";
                    query = "select distinct TOP 50  P.address3 FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select distinct TOP 50 P.address3 FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";

                    query = "select distinct TOP 50 P.address3 FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strAddress = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strAddress.Equals("null") || strAddress.Equals("NULL") || strAddress.Equals("Null") || strAddress.Equals("")))
                        {
                            Address addressObj = new Address();
                            addressObj.key = reader.GetSqlValue(0).ToString().Trim();
                            addressObj.value = reader.GetSqlValue(0).ToString().Trim();
                            lstAddress.Add(addressObj);
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

            lstAddress = lstAddress.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstAddress;
        }


        public static List<Address> getCityByCompanyList(String companyId, String divisionId, String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId + keyStroke, Logger.logLevel.Debug);

            List<Address> lstAddress = new List<Address>();

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
                    query = "select distinct TOP 50  P.address3 FROM person P WHERE P.address3 LIKE '%" + keyStroke + "%'";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {

                    query = "select distinct TOP 50  P.address3 FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND P.address3 LIKE '%" + keyStroke + "%'";

                }
                else if (companyId.Equals("null"))
                {

                    query = "select distinct TOP 50  P.address3 FROM person P, department D,rs_division Div";
                    query = query + " WHERE P.department = D.id AND  Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND P.address3 LIKE '%" + keyStroke + "%'";

                }
                else
                {
                    query = "select distinct TOP 50 P.address3 FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND P.address3 LIKE '%" + keyStroke + "%'";

                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strAddress = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strAddress.Equals("null") || strAddress.Equals("NULL") || strAddress.Equals("Null") || strAddress.Equals("")))
                        {
                            Address addressObj = new Address();
                            addressObj.key = reader.GetSqlValue(0).ToString().Trim();
                            addressObj.value = reader.GetSqlValue(0).ToString().Trim();
                            lstAddress.Add(addressObj);
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
            lstAddress = lstAddress.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstAddress;
        }
    
    }
}