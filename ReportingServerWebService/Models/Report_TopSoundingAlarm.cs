using ReportingServerWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ReportingServerWebService.Models
{
    public class Report_TopSoundingAlarm
    {
        public static Report_TopSoundingAlarm topSoundingAlarmReportObj;
        private static Boolean isConditionSelected = false;
        String criteria;
        int count;


        public String Criteria { get { return this.criteria; } set { this.criteria = value; } }
        public int Count { get { return this.count; } set { this.count = value; } }



        public static Report_TopSoundingAlarm getTopSoundingAlarmReportObj()
        {
            if (topSoundingAlarmReportObj == null)
            {
                topSoundingAlarmReportObj = new Report_TopSoundingAlarm();
            }
            return topSoundingAlarmReportObj;
        }

        public List<ReportRow> getData(String criteria, String top,String stDate,String stTime,String endDate,String endTime)
        {
            Report_TopSoundingAlarm reportObj = Report_TopSoundingAlarm.getTopSoundingAlarmReportObj();
            
            SqlConnection conn = null;
            SqlDataReader sqlreader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "";

                SqlCommand command = new SqlCommand();
                List<ReportRow> rowList = new List<ReportRow>();

                Report_TopSoundingAlarm.isConditionSelected = false;
                if(top.Equals("null"))
                {
                    top = "5";
                }
                else
                {
                    int n;
                    bool isNumeric = int.TryParse(top, out n);

                    if (!isNumeric)
                        top = "5";
                }

                if (criteria.Equals("input_desc"))
                {
                    query = "SELECT TOP " +top+"  [input_desc] AS INPUT_DESC,Count(*) AS COUNT FROM  [view_rs_top_sounding_alarm_report]";
                }
                else if (criteria.Equals("alarm_desc"))
                {
                    query = "SELECT TOP " + top + "  [alarm_desc] AS ALARM_DESC,Count(*) AS COUNT FROM  [view_rs_top_sounding_alarm_report]";
                }
                else
                {
                    query = "SELECT TOP " + top + "  [description] AS FACILITY,Count(*) AS COUNT FROM  [view_rs_top_sounding_alarm_report]";
                }

                if (!stDate.Equals("null"))
                {
                    if (Report_TopSoundingAlarm.isConditionSelected)
                        query = query + " AND @startDate <=  [view_rs_top_sounding_alarm_report].HAPPENED_DATETIME ";
                    else
                        query = query + " WHERE @startDate <=  [view_rs_top_sounding_alarm_report].HAPPENED_DATETIME ";
                    Report_TopSoundingAlarm.isConditionSelected = true;
                }

                if (!endDate.Equals("null"))
                {
                    if (Report_TopSoundingAlarm.isConditionSelected)
                        query = query + " AND  @endDate >=  [view_rs_top_sounding_alarm_report].HAPPENED_DATETIME ";
                    else
                        query = query + " WHERE @endDate >=  [view_rs_top_sounding_alarm_report].HAPPENED_DATETIME ";
                    Report_TopSoundingAlarm.isConditionSelected = true;
                }

                if (!stTime.Equals("null"))
                {
                    if (Report_TopSoundingAlarm.isConditionSelected)
                        query = query + " AND @startTime <=  [view_rs_top_sounding_alarm_report].HAPPENED_TIME ";
                    else
                        query = query + " WHERE @startTime <=  [view_rs_top_sounding_alarm_report].HAPPENED_TIME ";
                    Report_TopSoundingAlarm.isConditionSelected = true;
                }

                if (!endTime.Equals("null"))
                {
                    if (Report_TopSoundingAlarm.isConditionSelected)
                        query = query + " AND @endTime >=  [view_rs_top_sounding_alarm_report].HAPPENED_TIME ";
                    else
                        query = query + " WHERE  @endTime >=  [view_rs_top_sounding_alarm_report].HAPPENED_TIME ";
                    Report_TopSoundingAlarm.isConditionSelected = true;
                }


                if (!stDate.Equals("null"))
                {
                    DateTime startDateTime = new DateTime(Convert.ToInt32(stDate.Substring(0, 4)), Convert.ToInt32(stDate.Substring(4, 2)), Convert.ToInt32(stDate.Substring(6, 2)));
                    command.Parameters.AddWithValue("@startDate", startDateTime);
                }

                if (!endDate.Equals("null"))
                {
                    DateTime endDateTime = new DateTime(Convert.ToInt32(endDate.Substring(0, 4)), Convert.ToInt32(endDate.Substring(4, 2)), Convert.ToInt32(endDate.Substring(6, 2)));
                    command.Parameters.AddWithValue("@endDate", endDateTime);
                }

                if (!stTime.Equals("null"))
                {
                    TimeSpan startQueryTime = new TimeSpan(Convert.ToInt32(stTime.Substring(0, 2)), Convert.ToInt32(stTime.Substring(2, 2)), Convert.ToInt32(stTime.Substring(4, 2)));
                    command.Parameters.AddWithValue("@startTime", startQueryTime);
                }

                if (!endTime.Equals("null"))
                {
                    DateTime endDateTime = new DateTime(Convert.ToInt32(endDate.Substring(0, 4)), Convert.ToInt32(endDate.Substring(4, 2)), Convert.ToInt32(endDate.Substring(6, 2)));
                    command.Parameters.AddWithValue("@endDate", endDateTime);
                }

                if (criteria.Equals("input_desc"))
                {
                    query = query + " GROUP BY [input_desc]";
                }
                else if (criteria.Equals("alarm_desc"))
                {
                    query = query + " GROUP BY [alarm_desc]";
                }
                else
                {
                    query = query + " GROUP BY [description]";
                }


                command.Connection = conn;
                command.CommandText = query;
                command.CommandTimeout = 300;

                int count = 0;
                using (sqlreader = command.ExecuteReader())
                {
                    while (sqlreader.Read())
                    {
                        Report_TopSoundingAlarm report = new Report_TopSoundingAlarm();
                        report.Criteria = sqlreader.GetSqlValue(0).ToString().Trim();
                        report.Count = Convert.ToInt32(sqlreader.GetSqlValue(1).ToString().Trim());
                       
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