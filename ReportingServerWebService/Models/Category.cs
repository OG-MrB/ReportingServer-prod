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
    public class Category
    {
        public String key;
        public String value;
        String areaId;
        String facilityId;

        String CategoryId { get { return this.key; } set { this.key = value; } }
        String CategoryDesc { get { return this.value; } set { this.value = value; } }
        String FacilityId { get { return this.areaId; } set { this.areaId = value; } }
        String AreaId { get { return this.facilityId; } set { this.facilityId = value; } }

        public static Dictionary<String, String> getAllCategories()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            Dictionary<String, String> dictCategory = new Dictionary<string, string>();
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT TOP 500 id,description FROM category";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         String strCategory =  reader.GetSqlValue(1).ToString();
                         if (!(strCategory.Equals("null") || strCategory.Equals("NULL") || strCategory.Equals("Null")))
                         {
                             if (!dictCategory.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                 dictCategory.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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
            dictCategory = Utilities.sortData(dictCategory);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictCategory;
        }


        public static Dictionary<String, String> getCategoriesByArea(String facilityId, String areaId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facilityId+areaId, Logger.logLevel.Debug);

            if(facilityId.Equals("null") && areaId.Equals("null"))
            {
                return getAllCategories();
            }

            Dictionary<String, String> dictCategory = new Dictionary<string, string>();
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "";
                if (!facilityId.Equals("null") && !areaId.Equals("null"))
                {
                    query = "Select TOP 500  C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.area_id = @area_id AND CA.facility = @facility_Id";
                }
                else if (!facilityId.Equals("null"))
                {
                    query = "Select TOP 500  C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.facility = @facility_Id";
                }
                else
                {
                    query = "Select  TOP 500 C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.area_id = @area_id";
                }
                
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@area_id", areaId.Trim());
                command.Parameters.AddWithValue("@facility_Id", facilityId.Trim());

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String strCategory = reader.GetSqlValue(1).ToString();
                        if (!(strCategory.Equals("null") || strCategory.Equals("NULL") || strCategory.Equals("Null")))
                        {
                            if (!dictCategory.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictCategory.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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
            dictCategory = Utilities.sortData(dictCategory);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictCategory;
        }


        public static Dictionary<String, String> getCategoriesByArea(String facilityId, String areaId,String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facilityId + areaId + keyStroke, Logger.logLevel.Debug);

            Dictionary<String, String> dictCategory = new Dictionary<string, string>();
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "";

                if (facilityId.Equals("null") && areaId.Equals("null"))
                {
                    query = "Select TOP 100  C.id,C.description from category C where description like '%"+keyStroke+"%'"; 
                }
                else if (!facilityId.Equals("null") && !areaId.Equals("null"))
                {
                    query = "Select TOP 100  C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.area_id = @area_id AND CA.facility = @facility_Id AND C.description like '%" + keyStroke + "%'";
                }
                else if (!facilityId.Equals("null"))
                {
                    query = "Select TOP 100  C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.facility = @facility_Id AND C.description like '%" + keyStroke + "%'";
                }
                else
                {
                    query = "Select TOP 100  C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.area_id = @area_id AND C.description like '%" + keyStroke + "%'";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@area_id", areaId.Trim());
                command.Parameters.AddWithValue("@facility_Id", facilityId.Trim());

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        String strCategory = reader.GetSqlValue(1).ToString();
                        if (!(strCategory.Equals("null") || strCategory.Equals("NULL") || strCategory.Equals("Null")))
                        {
                            if (!dictCategory.ContainsKey(reader.GetSqlValue(0).ToString().Trim()))
                                dictCategory.Add(reader.GetSqlValue(0).ToString().Trim(), reader.GetSqlValue(1).ToString().Trim());
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
            dictCategory = Utilities.sortData(dictCategory);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return dictCategory;
        }



        public static List<Category> getAllCategoriesList()
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "", Logger.logLevel.Debug);

            List<Category> lstCategory = new List<Category>();
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "SELECT TOP 50 id,description FROM category";
                SqlCommand command = new SqlCommand(query, conn);


                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        String strCategory = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strCategory.Equals("") || strCategory.Equals("null") || strCategory.Equals("NULL") || strCategory.Equals("Null")))
                        {
                            Category categoryObj = new Category();
                            categoryObj.CategoryId = reader.GetSqlValue(0).ToString().Trim();
                            categoryObj.CategoryDesc = strCategory;
                            lstCategory.Add(categoryObj);

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

            lstCategory = lstCategory.OrderBy(x => x.CategoryDesc).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstCategory;
        }


        public static List<Category> getCategoriesByAreaList(String facilityId, String areaId)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facilityId + areaId , Logger.logLevel.Debug);

            List<Category> lstCategory = new List<Category>();
            if (facilityId.Equals("null") && areaId.Equals("null"))
            {
                return getAllCategoriesList();
            }

           
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "";
                if (!facilityId.Equals("null") && !areaId.Equals("null"))
                {
                    query = "Select TOP 50  C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.area_id = @area_id AND CA.facility = @facility_Id";
                }
                else if (!facilityId.Equals("null"))
                {
                    query = "Select TOP 50  C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.facility = @facility_Id";
                }
                else
                {
                    query = "Select  TOP 50 C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.area_id = @area_id";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@area_id", areaId.Trim());
                command.Parameters.AddWithValue("@facility_Id", facilityId.Trim());

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        String strCategory = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strCategory.Equals("") || strCategory.Equals("null") || strCategory.Equals("NULL") || strCategory.Equals("Null")))
                        {
                            Category categoryObj = new Category();
                            categoryObj.CategoryId = reader.GetSqlValue(0).ToString().Trim();
                            categoryObj.CategoryDesc = strCategory;
                            lstCategory.Add(categoryObj);

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

            lstCategory = lstCategory.OrderBy(x => x.CategoryDesc).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstCategory;
        }


        public static List<Category> getCategoriesByAreaList(String facilityId, String areaId, String keyStroke)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, facilityId + areaId + keyStroke, Logger.logLevel.Debug);

            List<Category> lstCategory = new List<Category>();
           
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "";

                if (facilityId.Equals("null") && areaId.Equals("null"))
                {
                    query = "Select TOP 100  C.id,C.description from category C where description like '%" + keyStroke + "%'";
                }
                else if (!facilityId.Equals("null") && !areaId.Equals("null"))
                {
                    query = "Select TOP 100  C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.area_id = @area_id AND CA.facility = @facility_Id AND C.description like '%" + keyStroke + "%'";
                }
                else if (!facilityId.Equals("null"))
                {
                    query = "Select TOP 100  C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.facility = @facility_Id AND C.description like '%" + keyStroke + "%'";
                }
                else
                {
                    query = "Select TOP 100  C.id,C.description,CA.area_id,CA.facility from category C ,area_category CA where C.id = CA.category_id AND CA.area_id = @area_id AND C.description like '%" + keyStroke + "%'";
                }

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@area_id", areaId.Trim());
                command.Parameters.AddWithValue("@facility_Id", facilityId.Trim());

                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        String strCategory = reader.GetSqlValue(1).ToString().Trim();
                        if (!(strCategory.Equals("") || strCategory.Equals("null") || strCategory.Equals("NULL") || strCategory.Equals("Null")))
                        {
                            Category categoryObj = new Category();
                            categoryObj.CategoryId = reader.GetSqlValue(0).ToString().Trim();
                            categoryObj.CategoryDesc = strCategory;
                            lstCategory.Add(categoryObj);

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

            lstCategory = lstCategory.OrderBy(x => x.CategoryDesc).ToList();
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Debug);
            return lstCategory;
        }
    }
}