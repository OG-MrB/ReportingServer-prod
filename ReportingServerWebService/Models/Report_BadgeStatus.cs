using ReportingServerWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ReportingServerWebService.Models
{
    public class Report_BadgeStatus
    {
        public static Report_BadgeStatus badgeStatusReportReportObj;
        private static Boolean isConditionSelected = false;
        String badge;
        String cardNo;
        String company;
        String category;
        String firstName;
        String lastName;
        String badgeStatus;
        String division;
        String invalidDateTime;
        String issueDateTime;
        String expiredDateTime;
        String returnDateTime;
        String invalidDay;
        String employeeID;
        String name;

        public String CardNo { get { return this.cardNo; } set { this.cardNo = value; } }
        public String Badge { get { return this.badge; } set { this.badge = value; } }
        public String Company { get { return this.company; } set { this.company = value; } }
        public String Category { get { return this.category; } set { this.category = value; } }
        public String FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public String LastName { get { return this.lastName; } set { this.lastName = value; } }
        public String BadgeStatus { get { return this.badgeStatus; } set { this.badgeStatus = value; } }
        public String Division { get { return this.division; } set { this.division = value; } }
        public String InvalidDateTime { get { return this.invalidDateTime; } set { this.invalidDateTime = value; } }
        public String IssueDateTime { get { return this.issueDateTime; } set { this.issueDateTime = value; } }
        public String ExpiredDateTime { get { return this.expiredDateTime; } set { this.expiredDateTime = value; } }
        public String ReturnDateTime { get { return this.returnDateTime; } set { this.returnDateTime = value; } }
        public String InvalidDay { get { return this.invalidDay; } set { this.invalidDay = value; } }
        public String EmployeeID { get { return this.employeeID; } set { this.employeeID = value; } }
        public String Name { get { return this.name; } set { this.name = value; } }

        //Gets the current object
        public static Report_BadgeStatus getBadgeStatusReportReportObj()
        {
            //First time create an object
            if (badgeStatusReportReportObj == null)
            {
                badgeStatusReportReportObj = new Report_BadgeStatus();
            }
            return badgeStatusReportReportObj;
        }


        private static string getWildCardquery(String wildCardType, String wildCardData, string query)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, wildCardType + "::" + wildCardData + "::" + query, Logger.logLevel.Debug);


            if (!wildCardData.Equals("null"))
            {
                if (wildCardType == "1")
                {

                    if (Report_BadgeStatus.isConditionSelected)
                        query = query + " AND  dbo.[view_rs_badge_status_report].employee = " + wildCardData;
                    else
                        query = query + " WHERE  dbo.[view_rs_badge_status_report].employee = " + wildCardData;
                    Report_BadgeStatus.isConditionSelected = true;
                }
                else if (wildCardType == "2")
                {
                    /* if (Report_BadgeStatus.isConditionSelected)
                         query = query + " AND   [view_rs_access_report_1].SSN like '" + wildCardData+"'";
                     else
                         query = query + " WHERE   [view_rs_access_report_1].SSN like '" + wildCardData + "'";
                     Report_BadgeStatus.isConditionSelected = true;*/
                }
                else if (wildCardType == "3")
                {
                    if (Report_BadgeStatus.isConditionSelected)
                        query = query + " AND dbo.[view_rs_badge_status_report].ZIP_CODE like '%" + wildCardData + "%'";
                    else
                        query = query + " WHERE dbo.[view_rs_badge_status_report].ZIP_CODE like '%" + wildCardData + "%'";
                    Report_BadgeStatus.isConditionSelected = true;
                }
                else if (wildCardType == "4")
                {
                    if (Report_BadgeStatus.isConditionSelected)
                        query = query + " AND dbo.[view_rs_badge_status_report].ADDRESS  like '" + wildCardData + "'";
                    else
                        query = query + " WHERE dbo.[view_rs_badge_status_report].ADDRESS  like '" + wildCardData + "'";
                    Report_BadgeStatus.isConditionSelected = true;
                }
                else if (wildCardType == "5")
                {
                    if (Report_BadgeStatus.isConditionSelected)
                        query = query + " AND dbo.[view_rs_badge_status_report].PERSON_ID  like '" + wildCardData + "'";
                    else
                        query = query + " WHERE dbo.[view_rs_badge_status_report].PERSON_ID  like '" + wildCardData + "'";
                    Report_BadgeStatus.isConditionSelected = true;
                }

            }
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Info);
            return query;
        }


        //Gets the report based on the parameters
        //Here {days} and {months} are the integer arrays. A value of 0 represents ALL.
        //Date Format : yyyymmdd
        //Time Format : hhmmss
        public List<ReportRow> getData(String badgeId, String personId, String companyId, String divisionId, String empId, String status, String stDate, String endDate, String daysStr, String monthsStr,String wildCardType,String wildCardText)
        {
            Report_BadgeStatus reportObj = Report_BadgeStatus.getBadgeStatusReportReportObj();

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

                //This flag is used for dynamic query formation.
                //It checks if AND or WHERE needs to be appended after the SELECT clause
                Report_BadgeStatus.isConditionSelected = false;

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

             
                //Form the base query string
                query = "SELECT [employee],[first_name],[last_name],[user1],[user2],[BADGE_ID],[cond_desc],[ISSUE_DATETIME],[EXPIRED_DATETIME],[RETURN_DATETIME],DATENAME(dw, [EXPIRED_DATETIME]) AS DAYS,[PHY_BADGE_ID] FROM  [view_rs_badge_status_report]";
                
                //Dynamic query creation
                if (!stDate.Equals("null"))
                {
                    Report_BadgeStatus.isConditionSelected = true;
                    query = query + " WHERE @startDate <=  [view_rs_badge_status_report].EXPIRED_DATETIME ";
                }

                if (!endDate.Equals("null"))
                {
                    if (Report_BadgeStatus.isConditionSelected)
                        query = query + "  AND @endDate >= [view_rs_badge_status_report].EXPIRED_DATETIME ";
                    else
                        query = query + " WHERE  @endDate >= [view_rs_badge_status_report].EXPIRED_DATETIME ";
                    Report_BadgeStatus.isConditionSelected = true;
                }

                if (!badgeId.Equals("null"))
                {
                    if (Report_BadgeStatus.isConditionSelected)
                        query = query + " AND   [view_rs_badge_status_report].PHY_BADGE_ID like '" + badgeId + "'";
                    else
                        query = query + " WHERE   [view_rs_badge_status_report].PHY_BADGE_ID like '" + badgeId + "'";
                    Report_BadgeStatus.isConditionSelected = true;
                }


                if (!companyId.Equals("null"))
                {
                    if (Report_BadgeStatus.isConditionSelected)
                        query = query + " AND   [view_rs_badge_status_report].COMPANY_ID =" + companyId;
                    else
                        query = query + " WHERE   [view_rs_badge_status_report].COMPANY_ID =" + companyId;
                    Report_BadgeStatus.isConditionSelected = true;
                }


                if (!personId.Equals("null"))
                {
                    if (Report_BadgeStatus.isConditionSelected)
                        query = query + " AND   [view_rs_badge_status_report].employee =" + personId ;
                    else
                        query = query + " WHERE   [view_rs_badge_status_report].employee =" + personId;
                    Report_BadgeStatus.isConditionSelected = true;
                }


                if (!status.Equals("null"))
                {
                    if (Report_BadgeStatus.isConditionSelected)
                        query = query + " AND   [view_rs_badge_status_report].STATUS_ID = " + status;
                    else
                        query = query + " WHERE   [view_rs_badge_status_report].STATUS_ID = " + status;
                    Report_BadgeStatus.isConditionSelected = true;
                }

                if (!divisionId.Equals("null"))
                {
                    if (Report_BadgeStatus.isConditionSelected)
                        query = query + " AND   [view_rs_badge_status_report].DIVISION_ID = " + divisionId;
                    else
                        query = query + " WHERE   [view_rs_badge_status_report].DIVISION_ID = " + divisionId;
                    Report_BadgeStatus.isConditionSelected = true;
                }

                //Append all the wild card query conditions
                query = getWildCardquery(wildCardType, wildCardText, query);

                if (days != null && days.Count() > 0)
                {
                    //Forms the query string based on the days array
                    query = Report_BadgeStatus.getDaysQueryString(days, query);
                }

                if (months != null && months.Count() > 0)
                {
                    //Forms the query string based on the months array
                    query = Report_BadgeStatus.getMonthQueryString(months, query);
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
                    command.Parameters.AddWithValue("@endDate", endDateTime);
                }


                //Set the sqlcommand 
                command.Connection = conn;
                command.CommandText = query;
                command.CommandTimeout = 300;
                //Execute and fill the object array
                int count = 0;
                using (sqlreader = command.ExecuteReader())
                {
                    
                    //Read from the reader
                    while (sqlreader.Read())
                    {
                        //Fill in the object
                         Report_BadgeStatus report = new Report_BadgeStatus();
                         report.EmployeeID = sqlreader.GetSqlValue(0).ToString().Trim();
                         report.FirstName = sqlreader.GetSqlValue(1).ToString().Trim();
                         report.LastName = sqlreader.GetSqlValue(2).ToString().Trim();
                         report.Company = sqlreader.GetSqlValue(3).ToString().Trim();
                         report.Division = sqlreader.GetSqlValue(4).ToString().Trim();
                         report.Badge = sqlreader.GetSqlValue(5).ToString().Trim();
                         report.BadgeStatus = sqlreader.GetSqlValue(6).ToString().Trim();
                         report.IssueDateTime = sqlreader.GetSqlValue(7).ToString().Trim();
                         report.ExpiredDateTime = sqlreader.GetSqlValue(8).ToString().Trim();
                         report.ReturnDateTime = sqlreader.GetSqlValue(9).ToString().Trim();
                         if (report.ReturnDateTime.Equals("Null") || report.ReturnDateTime.Equals("null") || report.ReturnDateTime.Equals("NULL"))
                             report.ReturnDateTime = "N/A";

                         report.invalidDay = sqlreader.GetSqlValue(10).ToString().Trim();
                         report.CardNo = sqlreader.GetSqlValue(11).ToString().Trim().Substring(4);
                         report.Name = report.FirstName + " " + report.LastName;

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

        //Forms the query string based on the days array
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
                        query = query + " OR DATENAME(dw,[EXPIRED_DATETIME]) Like 'Monday'";

                    }
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Monday'";
                        else
                            query = query + " WHERE ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Monday'";
                    Report_BadgeStatus.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 2)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw,[EXPIRED_DATETIME]) Like 'Tuesday'";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Tuesday'";
                        else
                            query = query + " WHERE ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Tuesday'";
                    Report_BadgeStatus.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 3)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw,[EXPIRED_DATETIME]) Like 'Wednesday'";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Wednesday'";
                        else
                            query = query + " WHERE ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Wednesday'";
                    Report_BadgeStatus.isConditionSelected = true;
                    isDaySet = true;

                }
                if (days[dayCount] == 4)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw,[EXPIRED_DATETIME]) Like 'Thursday'";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Thursday'";
                        else
                            query = query + " Where ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Thursday'";
                    Report_BadgeStatus.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 5)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw,[EXPIRED_DATETIME]) Like 'Friday'";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Friday'";
                        else
                            query = query + " WHERE ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Friday'";
                    Report_BadgeStatus.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 6)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw,[EXPIRED_DATETIME]) Like 'Saturday'";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Saturday'";
                        else
                            query = query + " WHERE ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Saturday'";
                    Report_BadgeStatus.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 7)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw,[EXPIRED_DATETIME]) Like 'Sunday'";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Sunday'";
                        else
                            query = query + " WHERE ( DATENAME(dw,[EXPIRED_DATETIME]) Like 'Sunday'";
                    Report_BadgeStatus.isConditionSelected = true;
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

        //Forms the query string based on the months array
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
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 1";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 1";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 1";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 2)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 2";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 2";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 2";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 3)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 3";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 3";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 3";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;

                }
                if (month[monthCount] == 4)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 4";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 4";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 4";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;

                }
                if (month[monthCount] == 5)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 5";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 5";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 5";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 6)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 6";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 6";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 6";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 7)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 7";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 7";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 7";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 8)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 8";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 8";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 8";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 9)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 9";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 9";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 9";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 10)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 10";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 10";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 10";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 11)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 11";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 11";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 11";
                    Report_BadgeStatus.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 12)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([EXPIRED_DATETIME]) = 12";
                    else
                        if (Report_BadgeStatus.isConditionSelected)
                            query = query + " AND ( MONTH([EXPIRED_DATETIME]) = 12";
                        else
                            query = query + " WHERE ( MONTH([EXPIRED_DATETIME]) = 12";
                    Report_BadgeStatus.isConditionSelected = true;
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


    }
}

        