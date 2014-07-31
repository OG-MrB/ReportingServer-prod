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
    public class Name
    {
        // String companyId;
        private String fName;
        private String lName;
        private String initial;
        public String value;
        public String key;

        private String FName { get { return this.fName; } set { this.fName = value; } }
        private String LName { get { return this.lName; } set { this.lName = value; } }
        private String Initial { get { return this.initial; } set { this.initial = value; } }
        private String FullName { get { return this.value; } set { this.value = value; } }
        private String PersonID { get { return this.key; } set { this.key = value; } }

        public static Dictionary<String, String> getAllNames()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            Dictionary<String, String> dictName = new Dictionary<string, string>();

            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "select  TOP 500  employee,first_name,last_name,initials from person";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Name name = new Name();
                        name.FName = reader.GetSqlValue(1).ToString().Trim();
                        name.LName = reader.GetSqlValue(2).ToString().Trim();
                        name.initial = reader.GetSqlValue(3).ToString().Trim();

                        if (name.initial.Equals("") || name.initial.Equals("null") || name.initial.Equals("NULL") || name.initial.Equals("Null"))
                        {
                            name.FullName = name.FName + " " + name.LName;
                        }
                        else
                        {
                            name.FullName = name.FName + " " + name.initial + " " + name.LName;
                        }

                        if (!dictName.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                            dictName.Add(reader.GetSqlValue(0).ToString().Trim(), name.FullName);

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
            dictName = Utilities.sortData(dictName);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictName;
        }

        public static Dictionary<String, String> getNamesByCompany(String companyId, String divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId , Logger.logLevel.Debug);

            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllNames();
            }

            Dictionary<String, String> dictName = new Dictionary<string, string>();
            

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
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division";
                    query = "select  TOP 500  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user2 like @division";
                    query = "select  TOP 500  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user1 like @company";
                    query = "select  TOP 500  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D, rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName";
                }
                
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());

                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Name name = new Name();
                        name.FName = reader.GetSqlValue(1).ToString().Trim();
                        name.LName = reader.GetSqlValue(2).ToString().Trim();
                        name.initial = reader.GetSqlValue(3).ToString().Trim();

                        if (name.initial.Equals("") || name.initial.Equals("null") || name.initial.Equals("NULL") || name.initial.Equals("Null"))
                        {
                            name.FullName = name.FName + " " + name.LName;
                        }
                        else
                        {
                            name.FullName = name.FName + " " + name.initial + " " + name.LName;
                        }

                        if (!dictName.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                            dictName.Add(reader.GetSqlValue(0).ToString().Trim(), name.FullName);

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
            dictName = Utilities.sortData(dictName);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictName;
        }

        public static Dictionary<String, String> getNamesByCompany(String companyId, String divisionId,String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId + keyStroke, Logger.logLevel.Debug);

            Dictionary<String, String> dictName = new Dictionary<string, string>();


            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();
                String query;
                if(companyId.Equals("null") && divisionId.Equals("null"))
                {
                    query = "select TOP 50  employee,first_name,last_name,initials from person where first_name+' '+initials+' '+last_name like '%" + keyStroke + "%' OR first_name+' '+last_name like '%" + keyStroke + "%' OR last_name+' '+first_name like '%" + keyStroke + "%'";
                }
                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division";
                    query = "select TOP 50  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                    query = query + " AND (first_name+' '+initials+' '+last_name like '%" + keyStroke + "%' OR first_name+' '+last_name like '%" + keyStroke + "%' OR last_name+' '+first_name like '%" + keyStroke + "%')";
                }
                else if (companyId.Equals("null"))
                {
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user2 like @division";
                    query = "select TOP 50  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                    query = query + " AND (first_name+' '+initials+' '+last_name like '%" + keyStroke + "%' OR first_name+' '+last_name like '%" + keyStroke + "%' OR last_name+' '+first_name like '%" + keyStroke + "%')";
                }
                else
                {
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user1 like @company";
                    query = "select TOP 50  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D, rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName";
                    query = query + " AND (first_name+' '+initials+' '+last_name like '%" + keyStroke + "%' OR first_name+' '+last_name like '%" + keyStroke + "%' OR last_name+' '+first_name like '%" + keyStroke + "%')";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());

                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Name name = new Name();
                        name.FName = reader.GetSqlValue(1).ToString().Trim();
                        name.LName = reader.GetSqlValue(2).ToString().Trim();
                        name.initial = reader.GetSqlValue(3).ToString().Trim();

                        if (name.initial.Equals("") || name.initial.Equals("null") || name.initial.Equals("NULL") || name.initial.Equals("Null"))
                        {
                            name.FullName = name.FName + " " + name.LName;
                        }
                        else
                        {
                            name.FullName = name.FName + " " + name.initial + " " + name.LName;
                        }

                        if (!dictName.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                            dictName.Add(reader.GetSqlValue(0).ToString().Trim(), name.FullName);

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
            dictName = Utilities.sortData(dictName);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictName;
        }

        public static List<Name> getAllNamesList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug); 
            List<Name> lstName = new List<Name>();

            Dictionary<String, String> dictName = new Dictionary<string, string>();

            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "select  TOP 50  employee,first_name,last_name,initials from person";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Name name = new Name();
                        name.FName = reader.GetSqlValue(1).ToString().Trim();
                        name.LName = reader.GetSqlValue(2).ToString().Trim();
                        name.initial = reader.GetSqlValue(3).ToString().Trim();
                        name.PersonID = reader.GetSqlValue(0).ToString().Trim();
                        if (!dictName.ContainsKey(name.PersonID))
                        {
                            dictName.Add(name.PersonID, name.PersonID);

                            if (name.initial.Equals("") || name.initial.Equals("null") || name.initial.Equals("NULL") || name.initial.Equals("Null"))
                            {
                                name.FullName = name.FName + " " + name.LName;
                            }
                            else
                            {
                                name.FullName = name.FName + " " + name.initial + " " + name.LName;
                            }

                            String strName = name.FullName;
                            if (!(strName.Equals("null") || strName.Equals("NULL") || strName.Equals("Null") || strName.Equals("")))
                            {
                                lstName.Add(name);
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

            lstName = lstName.OrderBy(x => x.FullName).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstName;
        }

        public static List<Name> getNamesByCompanyList(String companyId, String divisionId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId , Logger.logLevel.Debug);

            List<Name> lstName = new List<Name>(); 
            Dictionary<String, String> dictName = new Dictionary<string, string>();
            
            if (companyId.Equals("null") && divisionId.Equals("null"))
            {
                return getAllNamesList();
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
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division";
                    query = "select  TOP 50  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                }
                else if (companyId.Equals("null"))
                {
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user2 like @division";
                    query = "select  TOP 50  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                }
                else
                {
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user1 like @company";
                    query = "select  TOP 50  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D, rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());

                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Name name = new Name();
                        name.FName = reader.GetSqlValue(1).ToString().Trim();
                        name.LName = reader.GetSqlValue(2).ToString().Trim();
                        name.initial = reader.GetSqlValue(3).ToString().Trim();
                        name.PersonID = reader.GetSqlValue(0).ToString().Trim();
                        if (!dictName.ContainsKey(name.PersonID))
                        {
                            dictName.Add(name.PersonID, name.PersonID);
                            if (name.initial.Equals("") || name.initial.Equals("null") || name.initial.Equals("NULL") || name.initial.Equals("Null"))
                            {
                                name.FullName = name.FName + " " + name.LName;
                            }
                            else
                            {
                                name.FullName = name.FName + " " + name.initial + " " + name.LName;
                            }

                            String strName = name.FullName;
                            if (!(strName.Equals("null") || strName.Equals("NULL") || strName.Equals("Null") || strName.Equals("")))
                            {
                                lstName.Add(name);
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

            lstName = lstName.OrderBy(x => x.FullName).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstName;
        }

        public static List<Name> getNamesByCompanyList(String companyId, String divisionId, String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, companyId + divisionId + keyStroke, Logger.logLevel.Debug);

            List<Name> lstName = new List<Name>();
            Dictionary<String, String> dictName = new Dictionary<string, string>();
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
                    query = "select TOP 50  employee,first_name,last_name,initials from person where first_name+' '+initials+' '+last_name like '%" + keyStroke + "%' OR first_name+' '+last_name like '%" + keyStroke + "%' OR last_name+' '+first_name like '%" + keyStroke + "%'";
                }
                else if (!companyId.Equals("null") && !divisionId.Equals("null"))
                {
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user1 like @company and D.user2 like @division";
                    query = "select TOP 50  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D,rs_company Comp, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND Div.divisionId = @division AND D.user1 LIKE Comp.companyName AND D.user2 LIKE Div.divisionName";
                    query = query + " AND (first_name like '" + keyStroke + "%' OR last_name like '" + keyStroke + "%')";

                    //query = query + " AND (first_name+' '+initials+' '+last_name like '" + keyStroke + "%' OR first_name+' '+last_name like '" + keyStroke + "%' OR last_name+' '+first_name like '%" + keyStroke + "%')";
                }
                else if (companyId.Equals("null"))
                {
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user2 like @division";
                    query = "select TOP 50  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D, rs_division Div";
                    query = query + " WHERE P.department = D.id AND Div.divisionId = @division AND D.user2 LIKE Div.divisionName";
                    //query = query + " AND (first_name+' '+initials+' '+last_name like '" + keyStroke + "%' OR first_name+' '+last_name like '" + keyStroke + "%' OR last_name+' '+first_name like '%" + keyStroke + "%')";
                    query = query + " AND (first_name like '" + keyStroke + "%' OR last_name like '" + keyStroke + "%')";
                }
                else
                {
                    //query = "select TOP 500  P.id,P.first_name,P.last_name,P.initials from person P,department D where P.department = D.id and D.user1 like @company";
                    query = "select TOP 50  P.employee,P.first_name,P.last_name,P.initials FROM person P, department D, rs_company Comp";
                    query = query + " WHERE P.department = D.id AND Comp.companyId = @company AND D.user1 LIKE Comp.companyName";
                    //query = query + " AND (first_name+' '+initials+' '+last_name like '" + keyStroke + "%' OR first_name+' '+last_name like '" + keyStroke + "%' OR last_name+' '+first_name like '%" + keyStroke + "%')";
                    query = query + " AND (first_name like '" + keyStroke + "%' OR last_name like '" + keyStroke + "%')";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@company", companyId.Trim());
                command.Parameters.AddWithValue("@division", divisionId.Trim());
                
                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Name name = new Name();
                        name.FName = reader.GetSqlValue(1).ToString().Trim();
                        name.LName = reader.GetSqlValue(2).ToString().Trim();
                        name.initial = reader.GetSqlValue(3).ToString().Trim();
                        name.PersonID = reader.GetSqlValue(0).ToString().Trim();

                        if (!dictName.ContainsKey(name.PersonID))
                        {
                            dictName.Add(name.PersonID, name.PersonID);

                            if (name.initial.Equals("") || name.initial.Equals("null") || name.initial.Equals("NULL") || name.initial.Equals("Null"))
                            {
                                name.FullName = name.FName + " " + name.LName;
                            }
                            else
                            {
                                name.FullName = name.FName + " " + name.initial + " " + name.LName;
                            }

                            String strName = name.FullName;
                            if (!(strName.Equals("null") || strName.Equals("NULL") || strName.Equals("Null") || strName.Equals("")))
                            {
                                lstName.Add(name);
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

            lstName = lstName.OrderBy(x => x.FullName).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstName;
        }

    }
}