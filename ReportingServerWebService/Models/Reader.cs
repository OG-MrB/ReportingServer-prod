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
    public class Reader
    {
        public String key;
        public String value;

        String ReaderId { get { return this.key; } set { this.key = value; } }
        String ReaderDesc { get { return this.value; } set { this.value = value; } }

        public static Dictionary<String, String> getAllReaders()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            Dictionary<String, String> dictReader = new Dictionary<string, string>();

            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT  TOP 50 id,description,area,facility FROM reader";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                   
                    while (reader.Read())
                    {
                         String strReader =  reader.GetSqlValue(1).ToString();
                         if (!(strReader.Equals("null") || strReader.Equals("NULL") || strReader.Equals("Null") || strReader.Equals("")))
                         {
                             if (!dictReader.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                 dictReader.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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

            dictReader = Utilities.sortData(dictReader);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictReader;
        }

        public static Dictionary<String, String> getReadersByCategory(String facility, String area, String categoryId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facility + area + categoryId, Logger.logLevel.Debug);

            if (facility.Equals("null") && area.Equals("null"))
            {
                return getAllReaders();
            }

            Dictionary<String, String> dictReader = new Dictionary<string, string>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();


                String query = "";
                if (!facility.Equals("null") && !area.Equals("null"))
                {
                    query = "SELECT  TOP 500 id,description,area,facility FROM reader where facility = @facility_id and area = @area_id";
                }
                else if (!facility.Equals("null"))
                {
                    query = "SELECT  TOP 500 id,description,area,facility FROM reader where facility = @facility_id and area = @area_id";
                }
                else
                {
                    query = "SELECT  TOP 500 id,description,area,facility FROM reader where facility = @facility_id and area = @area_id";
                }


          
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@facility_id", facility.Trim());
                command.Parameters.AddWithValue("@area_id", area.Trim());

              

                DataTable myTable = new DataTable();
                myTable.Load(reader);


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strReader = reader.GetSqlValue(1).ToString();
                        if (!(strReader.Equals("null") || strReader.Equals("NULL") || strReader.Equals("Null") || strReader.Equals("")))
                        {
                            if (!dictReader.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictReader.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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

            dictReader = Utilities.sortData(dictReader);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictReader;
        }

        public static Dictionary<String, String> getReadersByCategory(String facility, String area, String categoryId,String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facility + area + categoryId + keyStroke, Logger.logLevel.Debug);

            Dictionary<String, String> dictReader = new Dictionary<string, string>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();


                String query = "";
               
                if (facility.Equals("null") && area.Equals("null"))
                {
                    query = "SELECT TOP 100  id,description,area,facility FROM reader where description like '%"+keyStroke+"%'";
                }

                else if (!facility.Equals("null") && !area.Equals("null"))
                {
                    query = "SELECT TOP 100  id,description,area,facility FROM reader where facility = @facility_id and area = @area_id AND description like '%" + keyStroke + "%'";
                }
                else if (!facility.Equals("null"))
                {
                    query = "SELECT TOP 100  id,description,area,facility FROM reader where facility = @facility_id AND description like '%" + keyStroke + "%'";
                }
                else
                {
                    query = "SELECT TOP 100  id,description,area,facility FROM reader where area = @area_id AND description like '%" + keyStroke + "%'";
                }



                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@facility_id", facility.Trim());
                command.Parameters.AddWithValue("@area_id", area.Trim());

             

                DataTable myTable = new DataTable();
                myTable.Load(reader);


                using (reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String strReader = reader.GetSqlValue(1).ToString();
                        if (!(strReader.Equals("null") || strReader.Equals("NULL") || strReader.Equals("Null") || strReader.Equals("")))
                        {
                            if (!dictReader.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictReader.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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

            dictReader = Utilities.sortData(dictReader);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictReader;
        }



        public static List<Reader> getAllReadersList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<Reader> lstReader = new List<Reader>();
             
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT  TOP 50 id,description,area,facility FROM reader";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strReader = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strReader.Equals("") || strReader.Equals("null") || strReader.Equals("NULL") || strReader.Equals("Null")))
                        {
                            Reader readerObj = new Reader();
                            readerObj.key = reader.GetSqlValue(0).ToString().Trim();
                            readerObj.value = reader.GetSqlValue(1).ToString().Trim();
                            lstReader.Add(readerObj);
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

            lstReader = lstReader.OrderBy(x => x.ReaderDesc).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstReader;
        }

        public static List<Reader> getReadersByFacilityList(String facility, String area, String categoryId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facility + area + categoryId , Logger.logLevel.Debug);


            List<Reader> lstReader = new List<Reader>();
            if (facility.Equals("null") && area.Equals("null"))
            {
                return getAllReadersList();
            }

           
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();


                String query = "";
                if (!facility.Equals("null") && !area.Equals("null"))
                {
                    query = "SELECT  TOP 50 id,description,area,facility FROM reader where facility = @facility_id and area = @area_id";
                }
                else if (!facility.Equals("null"))
                {
                    query = "SELECT  TOP 50 id,description,area,facility FROM reader where facility = @facility_id";
                }
                else
                {
                    query = "SELECT  TOP 50 id,description,area,facility FROM reader where area = @area_id";
                }



                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@facility_id", facility.Trim());
                command.Parameters.AddWithValue("@area_id", area.Trim());

               
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strReader = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strReader.Equals("") || strReader.Equals("null") || strReader.Equals("NULL") || strReader.Equals("Null")))
                        {
                            Reader readerObj = new Reader();
                            readerObj.key = reader.GetSqlValue(0).ToString().Trim();
                            readerObj.value = reader.GetSqlValue(1).ToString().Trim();
                            lstReader.Add(readerObj);
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

            lstReader = lstReader.OrderBy(x => x.ReaderDesc).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstReader;
        }

        public static List<Reader> getReadersByFacilityList(String facility, String area, String categoryId, String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facility + area + categoryId + keyStroke, Logger.logLevel.Debug);

            List<Reader> lstReader = new List<Reader>();

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();


                String query = "";

                if (facility.Equals("null") && area.Equals("null"))
                {
                    query = "SELECT TOP 50  id,description,area,facility FROM reader where description like '%" + keyStroke + "%'";
                }

                else if (!facility.Equals("null") && !area.Equals("null"))
                {
                    query = "SELECT TOP 50  id,description,area,facility FROM reader where facility = @facility_id and area = @area_id AND description like '%" + keyStroke + "%'";
                }
                else if (!facility.Equals("null"))
                {
                    query = "SELECT TOP 50  id,description,area,facility FROM reader where facility = @facility_id AND description like '%" + keyStroke + "%'";
                }
                else
                {
                    query = "SELECT TOP 50  id,description,area,facility FROM reader where area = @area_id AND description like '%" + keyStroke + "%'";
                }



                SqlCommand command = new SqlCommand(query, conn);
                if (!facility.Equals("null"))
                {
                    command.Parameters.AddWithValue("@facility_id", facility.Trim());
                    
                }

                if (!area.Equals("null"))
                {
                    command.Parameters.AddWithValue("@area_id", area.Trim());
                }
                //DataTable myTable = new DataTable();
               // myTable.Load(reader);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strReader = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strReader.Equals("") || strReader.Equals("null") || strReader.Equals("NULL") || strReader.Equals("Null")))
                        {
                            Reader readerObj = new Reader();
                            readerObj.key = reader.GetSqlValue(0).ToString().Trim();
                            readerObj.value = reader.GetSqlValue(1).ToString().Trim();
                            lstReader.Add(readerObj);
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

            lstReader = lstReader.OrderBy(x => x.ReaderDesc).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstReader;
        }
       
    }
}