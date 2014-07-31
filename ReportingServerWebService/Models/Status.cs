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
    public class Status
    {
        public String value;
        public String key;

        String StatusDesc { get { return this.value; } set { this.value = value; } }
        String StatusId { get { return this.key; } set { this.key = value; } }

        public static Dictionary<String, String> getAllStatus(String statusType)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, statusType, Logger.logLevel.Debug);

            Dictionary<String, String> dictStatus= new Dictionary<string, string>();
            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "";
                if(statusType.Equals("1"))
                    query = "SELECT TOP 500 id,description FROM xact_desc";
                else
                    query = "SELECT TOP 500 id,cond_desc FROM badgests";

                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (!dictStatus.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                            dictStatus.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictStatus;
        }

        public static List<Status> getAllStatusList(String statusType)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, statusType, Logger.logLevel.Debug);

            List<Status> lstStatus = new List<Status>();
            
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "";
                if (statusType.Equals("1"))
                    query = "SELECT TOP 50 id,description FROM xact_desc";
                else
                    query = "SELECT TOP 50 id,cond_desc FROM badgests";

                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Status objStatus = new Status();
                        objStatus.key = reader.GetSqlValue(0).ToString().Trim();
                        objStatus.value = reader.GetSqlValue(1).ToString().Trim();
                        lstStatus.Add(objStatus);
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

            lstStatus = lstStatus.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstStatus;
        }

        public static List<Status> getAllStatusListAutoComplete(String statusType,String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, statusType, Logger.logLevel.Debug);

            List<Status> lstStatus = new List<Status>();

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "";
                if (statusType.Equals("1"))
                    query = "SELECT TOP 50 id,description FROM xact_desc where description like '%" + keyStroke + "%'";
                else
                    query = "SELECT TOP 50 id,cond_desc FROM badgests where cond_desc like '%" + keyStroke + "%'";

                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Status objStatus = new Status();
                        objStatus.key = reader.GetSqlValue(0).ToString().Trim();
                        objStatus.value = reader.GetSqlValue(1).ToString().Trim();
                        lstStatus.Add(objStatus);
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

            lstStatus = lstStatus.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstStatus;
        }
    
    }
}