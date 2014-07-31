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
    public class Person
    {
        // String companyId;
        public String key;
        public String value;

        public static Dictionary<String, String> getAllPerson()
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

                String query = "select  TOP 500  id from person";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (!dictEmp.ContainsKey(reader.GetSqlValue(0).ToString()))
                            dictEmp.Add(reader.GetSqlValue(0).ToString(), reader.GetSqlValue(0).ToString().Trim());

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


        public static Dictionary<String, String> getEmpByCompany(String companyId, String divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId, Logger.logLevel.Debug);


            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllPerson();
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
                    query = "select  TOP 500 P.id FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select  TOP 500  P.id FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";
                    query = "select  TOP 500  P.id FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND  D.user1 LIKE Comp.companyName";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (!dictEmp.ContainsKey(reader.GetSqlValue(0).ToString()))
                            dictEmp.Add(reader.GetSqlValue(0).ToString(), reader.GetSqlValue(0).ToString().Trim());

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


        public static Dictionary<String, String> getEmpByCompany(String companyId, String divisionId, String keyStroke)
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
                    query = "select TOP 50 id from person group by employee where id LIKE '%" + keyStroke + "%'";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {
                    //query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division group by P.id";
                    query = "select TOP 50  P.id FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND P.id LIKE '%" + keyStroke + "%'";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select TOP 50  P.id FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND P.id LIKE '%" + keyStroke + "%'";
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";
                    query = "select TOP 50  P.id FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND  D.user1 LIKE Comp.companyName AND P.id LIKE '%" + keyStroke + "%'";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (!dictEmp.ContainsKey(reader.GetSqlValue(0).ToString()))
                            dictEmp.Add(reader.GetSqlValue(0).ToString(), reader.GetSqlValue(0).ToString().Trim());

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



        public static List<Person> getAllPersonList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);


            List<Person> lstEmp = new List<Person>();
            //Dictionary<String, String> dictEmp = new Dictionary<string, string>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "select  TOP 50  id from person";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strPerson = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strPerson.Equals("null") || strPerson.Equals("NULL") || strPerson.Equals("Null") || strPerson.Equals("")))
                        {
                            Person empObj = new Person();
                            empObj.key = strPerson;
                            empObj.value = strPerson;
                            lstEmp.Add(empObj);
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

            lstEmp = lstEmp.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstEmp;
        }


        public static List<Person> getEmpByCompanyList(String companyId, String divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId, Logger.logLevel.Debug);


            List<Person> lstEmp = new List<Person>();
            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllPersonList();
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
                    query = "select  TOP 50 P.id FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select  TOP 50  P.id FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";
                    query = "select  TOP 50  P.id FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND  D.user1 LIKE Comp.companyName";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strPerson = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strPerson.Equals("null") || strPerson.Equals("NULL") || strPerson.Equals("Null") || strPerson.Equals("")))
                        {
                            Person empObj = new Person();
                            empObj.key = strPerson;
                            empObj.value = strPerson;
                            lstEmp.Add(empObj);
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

            lstEmp = lstEmp.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstEmp;
        }


        public static List<Person> getEmpByCompanyList(String companyId, String divisionId, String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId + keyStroke, Logger.logLevel.Debug);


            List<Person> lstEmp = new List<Person>();

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
                    query = "select TOP 50 id from person where id LIKE '%" + keyStroke + "%'";
                }

                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {
                    //query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division group by P.id";
                    query = "select TOP 50  P.id FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND P.id LIKE '%" + keyStroke + "%'";
                }
                else if (companyId.Equals("null"))
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and  D.user2 like @division group by P.id";
                    query = "select TOP 50  P.id FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName AND P.id LIKE '%" + keyStroke + "%'";
                }
                else
                {
                    // query = "select TOP 500  P.id from person P,department D where P.department = D.id and D.user1 like @company group by P.id";
                    query = "select TOP 50  P.id FROM person P, department D,rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND  D.user1 LIKE Comp.companyName AND P.id LIKE '%" + keyStroke + "%'";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strPerson = reader.GetSqlValue(0).ToString().Trim();
                        if (!(strPerson.Equals("null") || strPerson.Equals("NULL") || strPerson.Equals("Null") || strPerson.Equals("")))
                        {
                            Person empObj = new Person();
                            empObj.key = strPerson;
                            empObj.value = strPerson;
                            lstEmp.Add(empObj);
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

            lstEmp = lstEmp.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstEmp;
        }
    }
}