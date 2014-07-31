using ReportingServerWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ReportingServerWebService.Models
{
    public class Report_AlarmDoor
    {
        public static Report_AlarmDoor alarmDoorReportObj;
        private static Boolean isConditionSelected = false;
        String inputGroup;
        String inputDesc;
        String alarm;
        String facility;
        String happenedDateTime;
        String day;

        public String InputGroup { get { return this.inputGroup; } set { this.inputGroup = value; } }
        public String InputDesc { get { return this.inputDesc; } set { this.inputDesc = value; } }
        public String Alarm { get { return this.alarm; } set { this.alarm = value; } }
        public String Facility { get { return this.facility; } set { this.facility = value; } }
        public String HappenedDateTime { get { return this.happenedDateTime; } set { this.happenedDateTime = value; } }
        public String Day { get { return this.day; } set { this.day = value; } }


        public static Report_AlarmDoor getAlarmDoorReportObj()
        {
            if (alarmDoorReportObj == null)
            {
                alarmDoorReportObj = new Report_AlarmDoor();
            }
            return alarmDoorReportObj;
        }


        public List<ReportRow> getData(String facilityId, String alarmDesc, String inputDesc, String stDate, String endDate, String stTime, String endTime, String daysStr, String monthsStr)
        {
            Report_AlarmDoor reportObj = Report_AlarmDoor.getAlarmDoorReportObj();

            SqlConnection conn = null;
            SqlDataReader sqlreader = null;
            bool isLatestData = false;

            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "";
                String ppQuery = "";

                SqlCommand command = new SqlCommand();
                List<ReportRow> rowList = new List<ReportRow>();

                //This flag is used for dynamic query formation.
                //It checks if AND or WHERE needs to be appended after the SELECT clause
                Report_AlarmDoor.isConditionSelected = false;

                int[] days = null;
                int[] months = null;

                //Convert the days string to an integer array
                if (!daysStr.Equals("null"))
                {
                    string[] d = daysStr.Split(',');
                    days = new int[d.Length];
                    for (int i = 0; i < d.Length; i++)
                    {
                        days[i] = Convert.ToInt32(d[i]);
                    }
                }

                //Convert the months string to an integer array
                if (!monthsStr.Equals("null"))
                {
                    string[] m = monthsStr.Split(',');
                    months = new int[m.Length];
                    for (int i = 0; i < m.Length; i++)
                    {
                        months[i] = Convert.ToInt32(m[i]);
                    }
                }

                //Check if the data needs to be pulled from PP
                if (!stDate.Equals("null"))
                {
                    DateTime startDateTime;
                    startDateTime = new DateTime(Convert.ToInt32(stDate.Substring(0, 4)), Convert.ToInt32(stDate.Substring(4, 2)), Convert.ToInt32(stDate.Substring(6, 2)), 0, 0, 0);

                    //Check if the date is today's date to pull today's data from PP
                    if (startDateTime.CompareTo(DateTime.Today) >= 0)
                    {
                        isLatestData = true;
                    }

                }

                if (!endDate.Equals("null"))
                {
                    DateTime endDateTime;
                    endDateTime = new DateTime(Convert.ToInt32(endDate.Substring(0, 4)), Convert.ToInt32(endDate.Substring(4, 2)), Convert.ToInt32(endDate.Substring(6, 2)), 23, 59, 59);

                    if (endDateTime.CompareTo(DateTime.Today) >= 0)
                    {
                        isLatestData = true;
                    }
                }



                //Form the base query string
                query = "SELECT INPUT_GROUP,INPUT_DESCRIPTION,ALARM_DESC,FACILITY,[HAPPENED_DATETIME],DATENAME(dw, [HAPPENED_DATETIME]) AS DAY FROM  [view_rs_top_alarm_by_door]";

                if (!facilityId.Equals("null"))
                {
                    if (Report_AlarmDoor.isConditionSelected)
                        query = query + " AND   [view_rs_top_alarm_by_door].FACILITY_ID = " + facilityId;
                    else
                        query = query + " WHERE   [view_rs_top_alarm_by_door].FACILITY_ID = " + facilityId;
                    Report_AlarmDoor.isConditionSelected = true;
                }

                if (!alarmDesc.Equals("null"))
                {
                    if (Report_AlarmDoor.isConditionSelected)
                        query = query + " AND   [view_rs_top_alarm_by_door].ALARM_ID =" + alarmDesc;
                    else
                        query = query + " WHERE   [view_rs_top_alarm_by_door].ALARM_ID =" + alarmDesc;
                    Report_AlarmDoor.isConditionSelected = true;
                }

                if (!inputDesc.Equals("null"))
                {
                    if (Report_AlarmDoor.isConditionSelected)
                        query = query + " AND   [view_rs_top_alarm_by_door].INPUT_ID =" + inputDesc;
                    else
                        query = query + " WHERE   [view_rs_top_alarm_by_door].INPUT_ID =" + inputDesc;
                    Report_AlarmDoor.isConditionSelected = true;
                }

                if (!stTime.Equals("null"))
                {
                    if (Report_AlarmDoor.isConditionSelected)
                        query = query + " AND @startTime <=  [view_rs_top_alarm_by_door].HAPPENED_TIME ";
                    else
                        query = query + " WHERE  @startTime <=  [view_rs_top_alarm_by_door].HAPPENED_TIME ";
                    Report_AlarmDoor.isConditionSelected = true;
                }

                if (!endTime.Equals("null"))
                {
                    if (Report_AlarmDoor.isConditionSelected)
                        query = query + " AND @endTime >=  [view_rs_top_alarm_by_door].HAPPENED_TIME ";
                    else
                        query = query + " WHERE  @endTime >=  [view_rs_top_alarm_by_door].HAPPENED_TIME ";
                    Report_AlarmDoor.isConditionSelected = true;
                }

                if (days != null && days.Count() > 0)
                {
                    //Forms the query string based on the days array
                    query = Report_AlarmDoor.getDaysQueryString(days, query);
                }

                if (months != null && months.Count() > 0)
                {
                    //Forms the query string based on the months array
                    query = Report_AlarmDoor.getMonthQueryString(months, query);
                }

                ppQuery = query;

                //Dynamic query creation
                if (!stDate.Equals("null"))
                {
                    if (Report_AlarmDoor.isConditionSelected)
                        query = query + " AND @startDate <=  [view_rs_top_alarm_by_door].HAPPENED_DATETIME ";
                    else
                        query = query + " WHERE @startDate <=  [view_rs_top_alarm_by_door].HAPPENED_DATETIME ";

                    Report_AlarmDoor.isConditionSelected = true;
                }

                if (!endDate.Equals("null"))
                {
                    
                    if (Report_AlarmDoor.isConditionSelected)
                        query = query + " AND  @endDate >=  [view_rs_top_alarm_by_door].HAPPENED_DATETIME ";
                    else
                        query = query + " WHERE   @endDate >=  [view_rs_top_alarm_by_door].HAPPENED_DATETIME ";
                    Report_AlarmDoor.isConditionSelected = true;
                }

                //Append the query date parameters
                if (!stDate.Equals("null"))
                {
                    DateTime startDateTime;
                    startDateTime = new DateTime(Convert.ToInt32(stDate.Substring(0, 4)), Convert.ToInt32(stDate.Substring(4, 2)), Convert.ToInt32(stDate.Substring(6, 2)), 0, 0, 0);
                    
                    command.Parameters.AddWithValue("@startDate", startDateTime);
                }

                if (!endDate.Equals("null"))
                {
                    DateTime endDateTime;
                    endDateTime = new DateTime(Convert.ToInt32(endDate.Substring(0, 4)), Convert.ToInt32(endDate.Substring(4, 2)), Convert.ToInt32(endDate.Substring(6, 2)), 23, 59, 59);
                    if(isLatestData)
                    {
                        //If the end time is later than yesterday, pull the data till yesterday from Reporting Server and the rest from PP
                        endDateTime = DateTime.Today.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                    }

                    command.Parameters.AddWithValue("@endDate", endDateTime);
                }

                if (!stTime.Equals("null"))
                {
                    TimeSpan startQueryTime = new TimeSpan(Convert.ToInt32(stTime.Substring(0, 2)), Convert.ToInt32(stTime.Substring(2, 2)), Convert.ToInt32(stTime.Substring(4, 2)));
                    command.Parameters.AddWithValue("@startTime", startQueryTime);
                }

                if (!endTime.Equals("null"))
                {
                    TimeSpan endQueryTime = new TimeSpan(Convert.ToInt32(endTime.Substring(0, 2)), Convert.ToInt32(endTime.Substring(2, 2)), Convert.ToInt32(endTime.Substring(4, 2)));
                    command.Parameters.AddWithValue("@endTime", endQueryTime);
                }

               
                //Set the sqlcommand 
                command.Connection = conn;
                command.CommandText = query;
                command.CommandTimeout = 0;

                //Execute and fill the object array
                int count = 0;
                using (sqlreader = command.ExecuteReader())
                {
                    //Read from the reader
                    while (sqlreader.Read())
                    {
                        //Fill in the object
                        //INPUT_GROUP,DOOR,ALARM_DESC,FACILITY,[HAPPENED_DATETIME],DATENAME(dw, [HAPPENED_DATETIME]) AS DAY
                        Report_AlarmDoor report = new Report_AlarmDoor();
                        report.InputGroup = sqlreader.GetSqlValue(0).ToString().Trim();
                        report.InputDesc = sqlreader.GetSqlValue(1).ToString().Trim();
                        report.Alarm = sqlreader.GetSqlValue(2).ToString().Trim();
                        report.Facility = sqlreader.GetSqlValue(3).ToString().Trim();
                        report.HappenedDateTime = sqlreader.GetSqlValue(4).ToString().Trim();
                        report.Day = sqlreader.GetSqlValue(5).ToString().Trim();

                        //Fill the report row object for response
                        ReportRow repRow = new ReportRow();
                        repRow.id = (++count).ToString();
                        repRow.datarow = report;
                        rowList.Add(repRow);
                    }
                }

                if (conn != null)
                {
                    conn.Close();
                }
                if (sqlreader != null)
                {
                    sqlreader.Close();
                }

             

                /*List<ReportRow> ppDataRow = new List<ReportRow>();
                if (isLatestData)
                {
                    ppDataRow = getPPQueryData(ppQuery, stTime, endTime);
                }

                rowList.AddRange(ppDataRow);*/

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


        public static string getDaysQueryString(int[] days, String queryStr)
        {
            Boolean isDaySet = false;
            Boolean isAllSet = false;
            String query = "";

            for (int dayCount = 0; dayCount < days.Length; dayCount++)
            {
                if (days[dayCount] == 0)
                {
                    isAllSet = true;
                    break;
                }
                if (days[dayCount] == 1)
                {
                    if (isDaySet)
                    {
                        query = query + " OR DATENAME(dw, [HAPPENED_DATETIME]) Like 'Monday'";

                    }
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Monday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Monday'";
                    Report_AlarmDoor.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 2)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [HAPPENED_DATETIME]) Like 'Tuesday'";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Tuesday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Tuesday'";
                    Report_AlarmDoor.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 3)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [HAPPENED_DATETIME]) Like 'Wednesday'";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Wednesday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Wednesday'";
                    Report_AlarmDoor.isConditionSelected = true;
                    isDaySet = true;

                }
                if (days[dayCount] == 4)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [HAPPENED_DATETIME]) Like 'Thursday'";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Thursday'";
                        else
                            query = query + " Where ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Thursday'";
                    Report_AlarmDoor.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 5)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [HAPPENED_DATETIME]) Like 'Friday'";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Friday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Friday'";
                    Report_AlarmDoor.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 6)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [HAPPENED_DATETIME]) Like 'Saturday'";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Saturday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Saturday'";
                    Report_AlarmDoor.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 7)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [HAPPENED_DATETIME]) Like 'Sunday'";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Sunday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [HAPPENED_DATETIME]) Like 'Sunday'";
                    Report_AlarmDoor.isConditionSelected = true;
                    isDaySet = true;
                }
            }

            if (!isAllSet)
            {
                queryStr = queryStr + query;
                queryStr = queryStr + " )";
            }
            return queryStr;
        }


        public static string getMonthQueryString(int[] month, String queryStr)
        {
            Boolean isMonthSet = false;
            Boolean isAllSet = false;
            String query = "";

            for (int monthCount = 0; monthCount < month.Count(); monthCount++)
            {
                if (month[monthCount] == 0)
                {
                    isAllSet = true;
                    break;
                }
                if (month[monthCount] == 1)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 1";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 1";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 1";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 2)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 2";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 2";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 2";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 3)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 3";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 3";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 3";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;

                }
                if (month[monthCount] == 4)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 4";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 4";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 4";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;

                }
                if (month[monthCount] == 5)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 5";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 5";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 5";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 6)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 6";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 6";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 6";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 7)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 7";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 7";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 7";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 8)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 8";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 8";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 8";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 9)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 9";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 9";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 9";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 10)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 10";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 10";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 10";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 11)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 11";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 11";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 11";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 12)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([HAPPENED_DATETIME]) = 12";
                    else
                        if (Report_AlarmDoor.isConditionSelected)
                            query = query + " AND ( MONTH([HAPPENED_DATETIME]) = 12";
                        else
                            query = query + " WHERE ( MONTH([HAPPENED_DATETIME]) = 12";
                    Report_AlarmDoor.isConditionSelected = true;
                    isMonthSet = true;
                }

            }

            if (!isAllSet)
            {
                queryStr = queryStr + query;
                queryStr = queryStr + " )";
            }
            return queryStr;
        }


        public static List<ReportRow> getPPQueryData(String ppQuery,String stTime,String endTime)
        {
            List<ReportRow> rowList = new List<ReportRow>();
            SqlConnection connection = null;
            SqlDataReader sqlreader = null;
            try
            {
                ppQuery = ppQuery.Replace("[view_rs_top_alarm_by_door]", "[view_rs_top_alarm_by_door_PP]");

                // create and open a connection object
                connection = ConnectionManager.getConnection();
                connection.Open();

                SqlCommand command = new SqlCommand();


                if (!stTime.Equals("null"))
                {
                    TimeSpan startQueryTime = new TimeSpan(Convert.ToInt32(stTime.Substring(0, 2)), Convert.ToInt32(stTime.Substring(2, 2)), Convert.ToInt32(stTime.Substring(4, 2)));
                    command.Parameters.AddWithValue("@startTime", startQueryTime);
                }

                if (!endTime.Equals("null"))
                {
                    TimeSpan endQueryTime = new TimeSpan(Convert.ToInt32(endTime.Substring(0, 2)), Convert.ToInt32(endTime.Substring(2, 2)), Convert.ToInt32(endTime.Substring(4, 2)));
                    command.Parameters.AddWithValue("@endTime", endQueryTime);
                }

                //Set the query string for the reporting server connection
                command.Connection = connection;
                command.CommandText = ppQuery;

                int count = 0;
                using (sqlreader = command.ExecuteReader())
                {
                    //Read from the reader
                    while (sqlreader.Read())
                    {
                        Report_AlarmDoor report = new Report_AlarmDoor();
                        report.InputGroup = sqlreader.GetSqlValue(0).ToString().Trim();
                        report.InputDesc = sqlreader.GetSqlValue(1).ToString().Trim();
                        report.Alarm = sqlreader.GetSqlValue(2).ToString().Trim();
                        report.Facility = sqlreader.GetSqlValue(3).ToString().Trim();
                        report.HappenedDateTime = sqlreader.GetSqlValue(4).ToString().Trim();
                        report.Day = sqlreader.GetSqlValue(5).ToString().Trim();

                        //Fill the report row object for response
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
                if (connection != null)
                {
                    connection.Close();
                }
                if (sqlreader != null)
                {
                    sqlreader.Close();
                }
            }
        }
    }
}