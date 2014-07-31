using ReportingServerWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Reflection;
using System.Diagnostics;

namespace ReportingServerWebService.Models
{
    [DataContract]
    public class Facility
    {
        [DataMember]
        public String key;
        [DataMember]
        public String value;

        String FacilityId { get { return this.key; } set { this.key = value; } }
        String FacilityDesc { get { return this.value; } set { this.value = value; } }

        
        public static List<Facility> getAllFacilityList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<Facility> lstFacility = new List<Facility>();
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();
               
                String query = "SELECT TOP 30 id,description FROM facility";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        String strFacility = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strFacility.Equals("") || strFacility.Equals("null") || strFacility.Equals("NULL") || strFacility.Equals("Null")))
                        {
                            Facility facility = new Facility();
                            facility.FacilityId = reader.GetSqlValue(0).ToString().Trim();
                            facility.FacilityDesc = reader.GetSqlValue(1).ToString().Trim();
                            lstFacility.Add(facility);

                        }

                    }
                }
                
                

            }
             catch(Exception ex)
            {
                var stackTrace = new StackTrace(ex, true);
                var line = stackTrace.GetFrame(0).GetFileLineNumber();
                Logger.LogExceptions(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message,line.ToString(), Logger.logLevel.Exception);
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
            lstFacility = lstFacility.OrderBy(x => x.FacilityDesc).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstFacility;
        }

        public static List<Facility> getAllFacilityListKeyStroke(String facilityChar)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facilityChar, Logger.logLevel.Debug);

            List<Facility> lstFacility = new List<Facility>();

            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT  TOP 50  id,description FROM facility where description like '%" + facilityChar + "%'";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        String strFacility = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strFacility.Equals("") || strFacility.Equals("null") || strFacility.Equals("NULL") || strFacility.Equals("Null")))
                        {
                            Facility facility = new Facility();
                            facility.FacilityId = reader.GetSqlValue(0).ToString().Trim();
                            facility.FacilityDesc = reader.GetSqlValue(1).ToString().Trim();
                            lstFacility.Add(facility);

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

            lstFacility = lstFacility.OrderBy(x => x.FacilityDesc).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstFacility;
        }


        
        public static Dictionary<String, String> getAllFacility()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            Dictionary<String, String> dictFacility = new Dictionary<string, string>();

            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();
                //String query = "select TOP 1000 id,address5 from person";
                String query = "SELECT  TOP 500  id,description FROM facility";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        String strFacility = reader.GetSqlValue(1).ToString();
                        if (!(strFacility.Equals("") || strFacility.Equals("null") || strFacility.Equals("NULL") || strFacility.Equals("Null")))
                        {
                            if (!dictFacility.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictFacility.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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
            dictFacility = Utilities.sortData(dictFacility);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictFacility;
        }


        public static Dictionary<String, String> getAllFacilityKeyStroke(String facilityChar)
        {

            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facilityChar, Logger.logLevel.Debug);


            Dictionary<String, String> dictFacility = new Dictionary<string, string>();

            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT  TOP 500  id,description FROM facility where description like '%" + facilityChar + "%'";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        String strFacility = reader.GetSqlValue(1).ToString();
                        if (!(strFacility.Equals("") || strFacility.Equals("null") || strFacility.Equals("NULL") || strFacility.Equals("Null")))
                        {
                            if (!dictFacility.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictFacility.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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

            dictFacility = Utilities.sortData(dictFacility);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictFacility;
        }

    }
}