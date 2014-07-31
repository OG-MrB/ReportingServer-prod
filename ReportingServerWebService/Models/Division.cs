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
    public class Division
    {
        public String value;
        public String key;

        String DivisionName { get { return this.value; } set { this.value = value; } }
        String DivisionId { get { return this.key; } set { this.key = value; } }

        public static Dictionary<String, String> getAllDivisions()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            Dictionary<String, String> dictDivision = new Dictionary<string, string>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                //String query = "SELECT TOP 500  (user2) FROM department group by user2";
                String query = "SELECT  TOP 500  divisionId,divisionName from rs_division";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                   
                    while (reader.Read())
                    {
                         String strDivision = reader.GetSqlValue(1).ToString();
                         if (!(strDivision.Equals("null") || strDivision.Equals("NULL") || strDivision.Equals("Null") || strDivision.Equals("")))
                         {
                             dictDivision.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString());
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

            dictDivision = Utilities.sortData(dictDivision);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictDivision;
        }

        public static Dictionary<String, String> getDivisionInCompany(String companyId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId , Logger.logLevel.Debug);

            if (companyId.Equals("null"))
            {
                return getAllDivisions(); 
            }

            Dictionary<String, String> dictDivision = new Dictionary<string, string>();
            
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

               // String query = "SELECT TOP 500 (user2) FROM department where user1 like @company group by user2";
                String query = "SELECT TOP 500  Div.divisionId,Div.divisionName FROM rs_division Div, department D , rs_company Comp WHERE Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId);
                using (reader = command.ExecuteReader())
                {
                   
                    while (reader.Read())
                    {
                        String strDivision = reader.GetSqlValue(1).ToString();
                        if (!(strDivision.Equals("null") || strDivision.Equals("NULL") || strDivision.Equals("Null") || strDivision.Equals("")))
                        {
                            dictDivision.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString());
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

            dictDivision = Utilities.sortData(dictDivision);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictDivision;
        }

        public static Dictionary<String, String> getDivisionInCompany(String companyId,String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + keyStroke, Logger.logLevel.Debug);

            Dictionary<String, String> dictDivision = new Dictionary<string, string>();

            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                // String query = "SELECT TOP 500 (user2) FROM department where user1 like @company group by user2";
                String query = "";
                if (companyId.Equals("null"))
                {
                    query = "SELECT TOP 1000 divisionId,divisionName FROM rs_division where divisionName like '%" + keyStroke + "%'";
                }
                else
                    query = "SELECT TOP 1000 Div.divisionId,Div.divisionName FROM rs_division Div, department D , rs_company Comp WHERE Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND Div.divisionName like '%" + keyStroke + "%'"; ;

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId);
                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strDivision = reader.GetSqlValue(1).ToString();
                        if (!(strDivision.Equals("null") || strDivision.Equals("NULL") || strDivision.Equals("Null") || strDivision.Equals("")))
                        {
                            dictDivision.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString());
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

            dictDivision = Utilities.sortData(dictDivision);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictDivision;
        }


        public static List<Division> getAllDivisionsList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<Division> lstDivision = new List<Division>();
           
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                //String query = "SELECT TOP 500  (user2) FROM department group by user2";
                String query = "SELECT  TOP 50  divisionId,divisionName from rs_division";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strDivision = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strDivision.Equals("null") || strDivision.Equals("NULL") || strDivision.Equals("Null") || strDivision.Equals("")))
                        {
                            Division div = new Division();
                            div.DivisionId = reader.GetSqlValue(0).ToString().Trim(); 
                            div.DivisionName = reader.GetSqlValue(1).ToString().Trim();
                            lstDivision.Add(div);
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

            lstDivision = lstDivision.OrderBy(x => x.DivisionName).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstDivision;
        }

        public static List<Division> getDivisionInCompanyList(String companyId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId, Logger.logLevel.Debug);

            if (companyId.Equals("null"))
            {
                return getAllDivisionsList();
            }

            List<Division> lstDivision = new List<Division>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                // String query = "SELECT TOP 500 (user2) FROM department where user1 like @company group by user2";
                String query = "SELECT TOP 50  Div.divisionId,Div.divisionName FROM rs_division Div, department D , rs_company Comp WHERE Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId);
                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strDivision = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strDivision.Equals("null") || strDivision.Equals("NULL") || strDivision.Equals("Null") || strDivision.Equals("")))
                        {
                            Division div = new Division();
                            div.DivisionId = reader.GetSqlValue(0).ToString().Trim();
                            div.DivisionName = reader.GetSqlValue(1).ToString().Trim();
                            lstDivision.Add(div);
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

            lstDivision = lstDivision.OrderBy(x => x.DivisionName).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstDivision;
        }

        public static List<Division> getDivisionInCompanyList(String companyId, String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId+keyStroke, Logger.logLevel.Debug);

            List<Division> lstDivision = new List<Division>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                // String query = "SELECT TOP 500 (user2) FROM department where user1 like @company group by user2";
                String query = "";
                if (companyId.Equals("null"))
                {
                    query = "SELECT TOP 50 divisionId,divisionName FROM rs_division where divisionName like '%" + keyStroke + "%'";
                }
                else
                    query = "SELECT TOP 50 Div.divisionId,Div.divisionName FROM rs_division Div, department D , rs_company Comp WHERE Comp.companyId = @company AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName AND Div.divisionName like '%" + keyStroke + "%'"; ;

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId);
                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strDivision = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strDivision.Equals("null") || strDivision.Equals("NULL") || strDivision.Equals("Null") || strDivision.Equals("")))
                        {
                            Division div = new Division();
                            div.DivisionId = reader.GetSqlValue(0).ToString().Trim();
                            div.DivisionName = reader.GetSqlValue(1).ToString().Trim();
                            lstDivision.Add(div);
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

            lstDivision = lstDivision.OrderBy(x => x.DivisionName).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstDivision;
        }
    
    }
}