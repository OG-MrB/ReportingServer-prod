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
    public class Area
    {
        public String key;
        public String value;

       
         String Key { get { return this.key; } set { this.key = value; } }
         String Value { get { return this.value; } set { this.value = value; } }

        public static Dictionary<String,String> getAllAreas()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            Dictionary<String, String> dictArea = new Dictionary<string, string>();

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT TOP 1000 id,description,facility FROM area";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       String strArea =  reader.GetSqlValue(1).ToString();
                       if (!(strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            if (!dictArea.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictArea.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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

            dictArea = Utilities.sortData(dictArea);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictArea;
        }

        public static Dictionary<String, String> getAreaByID(String[] facilityId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facilityId[0], Logger.logLevel.Debug);

            if (facilityId == null || facilityId[0].Equals("null"))
            {
                return getAllAreas();
            }

            Dictionary<String, String> dictArea = new Dictionary<string, string>();
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT TOP 1000 id,description,facility FROM area WHERE facility = " + facilityId[0].Trim();

                for (int i = 1; i < facilityId.Length; i++)
                {
                    query = query + " OR facility = " + facilityId[i].Trim();
                }

                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strArea = reader.GetSqlValue(1).ToString();
                        if (!(strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            if (!dictArea.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictArea.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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

            dictArea = Utilities.sortData(dictArea);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictArea;
        }

        public static Dictionary<String, String> getAreaByID(String[] facilityId,String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facilityId[0]+keyStroke, Logger.logLevel.Debug);

            Dictionary<String, String> dictArea = new Dictionary<string, string>();
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();
                String query = "";
                if(facilityId[0].Equals("null"))
                {
                    query = "SELECT TOP 1000 id,description,facility FROM area WHERE description like '%" + keyStroke + "%'";
                }
                else 
                {
                    query = "SELECT TOP 1000 id,description,facility FROM area WHERE facility = " + facilityId[0].Trim() + " AND description like '%" + keyStroke + "%'";
                }

                for (int i = 1; i < facilityId.Length; i++)
                {
                    query = query + " OR facility = " + facilityId[i].Trim();
                }

                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strArea = reader.GetSqlValue(1).ToString();
                        if (!(strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            if (!dictArea.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictArea.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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

            dictArea = Utilities.sortData(dictArea);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictArea;
        }



        public static List<Area> getAllAreasList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<Area> lstArea = new List<Area>();

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT TOP 50 id,description,facility FROM area";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strArea = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strArea.Equals("") || strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            Area areaObj = new Area();
                            areaObj.key = reader.GetSqlValue(0).ToString().Trim();
                            areaObj.value = reader.GetSqlValue(1).ToString().Trim();
                            lstArea.Add(areaObj);
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

            lstArea = lstArea.OrderBy(x => x.Value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstArea;
        }

        public static List<Area> getAreaByIDList(String[] facilityId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<Area> lstArea = new List<Area>();
            if (facilityId == null || facilityId[0].Equals("null"))
            {
                return getAllAreasList();
            }

          
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT TOP 50 id,description,facility FROM area WHERE facility = " + facilityId[0].Trim();

                for (int i = 1; i < facilityId.Length; i++)
                {
                    query = query + " OR facility = " + facilityId[i].Trim();
                }

                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strArea = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strArea.Equals("") || strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            Area areaObj = new Area();
                            areaObj.key = reader.GetSqlValue(0).ToString().Trim();
                            areaObj.value = reader.GetSqlValue(1).ToString().Trim();
                            lstArea.Add(areaObj);
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

            lstArea = lstArea.OrderBy(x => x.Value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstArea;
        }

        public static List<Area> getAreaByIDList(String[] facilityId, String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facilityId[0] + keyStroke, Logger.logLevel.Debug);
            List<Area> lstArea = new List<Area>();
            /*if (facilityId == null || facilityId[0].Equals("null"))
            {
                return getAllAreasList();
            }*/

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();
                String query = "";
               // if (facilityId[0].Equals("null"))
               // {
                    query = "SELECT TOP 1000 id,description,facility FROM area WHERE description like '%" + keyStroke + "%'";
               // }
               // else
               // {
               //     query = "SELECT TOP 1000 id,description,facility FROM area WHERE facility = " + facilityId[0].Trim() + " AND description like '%" + keyStroke + "%'";
               // }

               // for (int i = 1; i < facilityId.Length; i++)
               // {
               //     query = query + " OR facility = " + facilityId[i].Trim();
              //  }

                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strArea = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strArea.Equals("") || strArea.Equals("null") || strArea.Equals("NULL") || strArea.Equals("Null")))
                        {
                            Area areaObj = new Area();
                            areaObj.key = reader.GetSqlValue(0).ToString().Trim();
                            areaObj.value = reader.GetSqlValue(1).ToString().Trim();
                            lstArea.Add(areaObj);
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

            lstArea = lstArea.OrderBy(x => x.Value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstArea;
        }
       
    }
}