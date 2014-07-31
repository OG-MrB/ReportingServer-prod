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
    public class Company
    {

        public String key;
        public String value;

        String CompanyId { get { return this.key; } set { this.key = value; } }
        String CompanyName { get { return this.value; } set { this.value = value; } }

        public static Dictionary<String, String> getAllCompany()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            Dictionary<String, String> dictCompany = new Dictionary<string, string>();
            

            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT  TOP 50  companyId,companyName from rs_company";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        String strCompany = reader.GetSqlValue(1).ToString();
                        if (!(strCompany.Equals("null") || strCompany.Equals("NULL") || strCompany.Equals("Null") || strCompany.Equals("")))
                        {
                            dictCompany.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString());
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

            dictCompany = Utilities.sortData(dictCompany);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictCompany;
        }

        public static Dictionary<String, String> getAllCompany(String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, keyStroke, Logger.logLevel.Debug);

            Dictionary<String, String> dictCompany = new Dictionary<string, string>();


            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT TOP 50 companyId,companyName from rs_company where companyName like '%" + keyStroke + "%'";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                        while (reader.Read())
                        {
                            String strCompany = reader.GetSqlValue(1).ToString();
                            if (!(strCompany.Equals("null") || strCompany.Equals("NULL") || strCompany.Equals("Null") || strCompany.Equals("")))
                            {
                                dictCompany.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString());
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

            dictCompany = Utilities.sortData(dictCompany);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictCompany;
        }


        public static List<Company> getAllCompanyList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);
            List<Company> lstCompany = new List<Company>();

            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT TOP 50 companyId,companyName from rs_company";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strCompany = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strCompany.Equals("null") || strCompany.Equals("NULL") || strCompany.Equals("Null") || strCompany.Equals("")))
                        {
                            Company companyObj = new Company();
                            companyObj.CompanyId = reader.GetSqlValue(0).ToString().Trim();
                            companyObj.CompanyName = reader.GetSqlValue(1).ToString().Trim();
                            lstCompany.Add(companyObj);
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

            lstCompany = lstCompany.OrderBy(x => x.CompanyName).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstCompany;
        }

        public static List<Company> getAllCompanyList(String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, keyStroke, Logger.logLevel.Debug);

            List<Company> lstCompany = new List<Company>();

            SqlConnection conn = null;

            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT TOP 50 companyId,companyName from rs_company where companyName like '%" + keyStroke + "%'";
                SqlCommand command = new SqlCommand(query, conn);

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strCompany = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strCompany.Equals("null") || strCompany.Equals("NULL") || strCompany.Equals("Null") || strCompany.Equals("")))
                        {
                            Company companyObj = new Company();
                            companyObj.CompanyId = reader.GetSqlValue(0).ToString().Trim();
                            companyObj.CompanyName = reader.GetSqlValue(1).ToString().Trim();
                            lstCompany.Add(companyObj);
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


            lstCompany = lstCompany.OrderBy(x => x.CompanyName).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstCompany;
        }
    
    }
}