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
    public class Door
    {
        public string  key;
        public string value;

        String DoorId { get { return this.key; } set { this.key = value; } }
        String DoorDesc { get { return this.value; } set { this.value = value; } }

        public static Dictionary<String, String> getAllDoors()
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

                String query = "SELECT id,description FROM door";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (!dictArea.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                            dictArea.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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


        public static Dictionary<String, String> getAllDoors(String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, keyStroke, Logger.logLevel.Debug);

            Dictionary<String, String> dictArea = new Dictionary<string, string>();

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT id,description FROM door where description like '%" + keyStroke + "%'";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (!dictArea.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                            dictArea.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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


        public static List<Door> getAllDoorsList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<Door> lstdoor = new List<Door>();
            
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT id,description FROM door";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Door objDoor = new Door();
                        objDoor.DoorId = reader.GetSqlValue(0).ToString().Trim();
                        objDoor.DoorDesc = reader.GetSqlValue(1).ToString().Trim();
                        lstdoor.Add(objDoor);
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

            lstdoor = lstdoor.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstdoor;
        }


        public static List<Door> getAllDoorsList(String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, keyStroke, Logger.logLevel.Debug);

            List<Door> lstdoor = new List<Door>();

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT id,description FROM door where description like '%" + keyStroke + "%'";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Door objDoor = new Door();
                        objDoor.DoorId = reader.GetSqlValue(0).ToString().Trim();
                        objDoor.DoorDesc = reader.GetSqlValue(1).ToString().Trim();
                        lstdoor.Add(objDoor);
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

            lstdoor = lstdoor.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstdoor;
        }


        public static string keyStroke { get; set; }
    }
}