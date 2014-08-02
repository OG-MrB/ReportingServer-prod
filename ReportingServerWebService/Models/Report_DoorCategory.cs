using ReportingServerWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ReportingServerWebService.Models
{
    public class Report_DoorCategory
    {
        public static Report_DoorCategory doorCategoryReportObj;
        private static Boolean isConditionSelected = false;
        String area;
        String door;
        String category;

        public String Area { get { return this.area; } set { this.area = value; } }
        public String Door { get { return this.door; } set { this.door = value; } }
        public String Category { get { return this.category; } set { this.category = value; } }


         public static Report_DoorCategory getAccessReportObj()
        {
            if (doorCategoryReportObj == null)
            {
                doorCategoryReportObj = new Report_DoorCategory();
            }
            return doorCategoryReportObj;
        }

         public List<ReportRow> getData(String areaId,String categoryId,String doorId)
         {
             Report_DoorCategory reportObj = Report_DoorCategory.getAccessReportObj();
             SqlConnection conn = null;
          
             SqlDataReader sqlreader = null;
             try
             {
                 // create and open a connection object
                 conn = ConnectionManager.getConnection();
                 conn.Open();

                 String query = "";
                 query = "SELECT [AREA],[CATEGORY],[DOOR] FROM  [view_rs_door_category_report]";


                 Report_DoorCategory.isConditionSelected = false;

                 if (!areaId.Equals("null"))
                 {
                     if (Report_DoorCategory.isConditionSelected)
                         query = query + " AND   [view_rs_door_category_report].AREA_ID = " + areaId;
                     else
                         query = query + " WHERE   [view_rs_door_category_report].AREA_ID = " + areaId;
                     Report_DoorCategory.isConditionSelected = true;
                 }

                 if (!categoryId.Equals("null"))
                 {
                     if (Report_DoorCategory.isConditionSelected)
                         query = query + " AND   [view_rs_door_category_report].CATEGORY_ID = " + categoryId;
                     else
                         query = query + " WHERE   [view_rs_door_category_report].CATEGORY_ID = " + categoryId;
                     Report_DoorCategory.isConditionSelected = true;
                 }

                 if (!doorId.Equals("null"))
                 {
                     if (Report_DoorCategory.isConditionSelected)
                         query = query + " AND   [view_rs_door_category_report].DOOR_ID = " + doorId;
                     else
                         query = query + " WHERE   [view_rs_door_category_report].DOOR_ID = " + doorId;
                     Report_DoorCategory.isConditionSelected = true;
                 }

                 SqlCommand command = new SqlCommand(query, conn);
                 command.CommandTimeout = 300;
                 List<ReportRow> rowList = new List<ReportRow>();
                 int count = 0;
                 using (sqlreader = command.ExecuteReader())
                 {
                     while (sqlreader.Read())
                     {
                         Report_DoorCategory report = new Report_DoorCategory();
                         report.Area = sqlreader.GetSqlValue(0).ToString().Trim();
                         report.Category = sqlreader.GetSqlValue(1).ToString().Trim();
                         report.Door = sqlreader.GetSqlValue(2).ToString().Trim();
      
                         ReportRow repRow = new ReportRow();
                         repRow.id = (++count).ToString();
                         repRow.datarow = report;
                         rowList.Add(repRow);
                     }
                 }

                 return rowList;
             }
             finally
             {
                 if (conn != null)
                 {
                     conn.Close();
                 }
                 if (sqlreader != null)
                 {
                     sqlreader.Close();
                 }
             }

         }
    }
}