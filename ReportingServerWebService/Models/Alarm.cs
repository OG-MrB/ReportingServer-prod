using ReportingServerWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ReportingServerWebService.Models
{
    public class Alarm
    {
        public String key;
        public String value;

        public String AlarmId { get { return this.key; } set { this.key = value; } }
        public String AlarmDesc { get { return this.value; } set { this.value = value; } }

        public static Dictionary<String, String> getAllAlarms()
        {
            Dictionary<String, String> dictArea = new Dictionary<string, string>();

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT id,description FROM alarm";
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
            return dictArea;
        }

        public static Dictionary<String, String> getAllAlarms(String keyStroke)
        {
            Dictionary<String, String> dictArea = new Dictionary<string, string>();

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT id,description FROM alarm where description like '%"+keyStroke+"%'";
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
            return dictArea;
        }

        public static List<Alarm> getAllAlarmsList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);
            List<Alarm> lstAlarm = new List<Alarm>();
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT id,description FROM alarm";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strAlarm = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strAlarm.Equals("") || strAlarm.Equals("null") || strAlarm.Equals("NULL") || strAlarm.Equals("Null")))
                        {
                            Alarm alarmObj = new Alarm();
                            alarmObj.key = reader.GetSqlValue(0).ToString().Trim();
                            alarmObj.value = reader.GetSqlValue(1).ToString().Trim();
                            lstAlarm.Add(alarmObj);
                        }
                    }
                }
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
            lstAlarm = lstAlarm.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstAlarm;
        }

        public static List<Alarm> getAllAlarmsListByKeystroke(String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<Alarm> lstAlarm = new List<Alarm>();

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT id,description FROM alarm where description like '%" + keyStroke + "%'";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        String strAlarm = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strAlarm.Equals("") || strAlarm.Equals("null") || strAlarm.Equals("NULL") || strAlarm.Equals("Null")))
                        {
                            Alarm alarmObj = new Alarm();
                            alarmObj.key = reader.GetSqlValue(0).ToString().Trim();
                            alarmObj.value = reader.GetSqlValue(1).ToString().Trim();
                            lstAlarm.Add(alarmObj);
                        }
                    }
                }
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
            lstAlarm = lstAlarm.OrderBy(x => x.value).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstAlarm;
        }
    }
}