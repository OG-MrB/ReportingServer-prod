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
    public class Report_Activity
    {
        public static Report_Activity accessReportObj;
        private static Boolean isConditionSelected = false;
        String badge;
        String company;
        String reader;
        String firstName;
        String lastName;
        String status;
        String name;
        String dateTime;
        String day;
        String accessTime;
        String empId;
        String personId;

        public String Badge { get { return this.badge; } set { this.badge = value; } }
        public String Company { get { return this.company; } set { this.company = value; } }
        public String Reader { get { return this.reader; } set { this.reader = value; } }
        public String FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public String LastName { get { return this.lastName; } set { this.lastName = value; } }
        public String Status { get { return this.status; } set { this.status = value; } }
        public String Name { get { return this.name; } set { this.name = value; } }
        public String DateHistory { get { return this.dateTime; } set { this.dateTime = value; } }
        public String Day { get { return this.day; } set { this.day = value; } }
        public String AccessTime { get { return this.accessTime; } set { this.accessTime = value; } }

        public String EmpId { get { return this.empId; } set { this.empId = value; } }
        public String PersonId { get { return this.personId; } set { this.personId = value; } }

        public static Report_Activity getAccessReportObj()
        {
            if (accessReportObj == null)
            {
                accessReportObj = new Report_Activity();
            }
            return accessReportObj;
        }

/*

        private static string getAreaquery(string areaString, string query)
        {
            Boolean isAreaCondition = false;
            String mainQuery = query;
            areaString = areaString.Trim(';');
            String[] areaArray = areaString.Split(';');

            if (areaArray.Count() > 0)
            {
                String[] areaRow = areaArray[0].Split(',');
                String facility = areaRow[0].Trim();
                String area = areaRow[1].Trim();
                String category = areaRow[2].Trim();
                String reader = areaRow[3].Trim();
                if (Report_Activity.isConditionSelected)
                {
                    isAreaCondition = true;
                }
                else
                {
                    isAreaCondition = false;
                }

                if (!reader.Equals("null"))
                {
                    if (Report_Activity.isConditionSelected)
                        query = query + " AND  badge_history.reader_desc like '" + reader + "'";
                    else
                        query = query + " WHERE  badge_history.reader_desc like '" + reader + "'";
                    Report_Activity.isConditionSelected = true;

                }

                if (!facility.Equals("null"))
                {
                    if (Report_Activity.isConditionSelected)
                        query = query + " AND  facility.id = " + facility;
                    else
                        query = query + " WHERE  facility.id = " + facility;
                    Report_Activity.isConditionSelected = true;

                }

                if (!area.Equals("null"))
                {
                    if (Report_Activity.isConditionSelected)
                        query = query + " AND area.id = " + area;
                    else
                        query = query + " WHERE area.id = " + area;
                    Report_Activity.isConditionSelected = true;

                }

                if (!category.Equals("null"))
                {
                    if (Report_Activity.isConditionSelected)
                        query = query + " AND  category.id = " + category;
                    else
                        query = query + " WHERE  category.id = " + category;
                    Report_Activity.isConditionSelected = true;
                }


            }

            for (int count = 1; count < areaArray.Length; count++)
            {
                String[] areaRow = areaArray[count].Split(',');
                String facility = areaRow[0].Trim();
                String area = areaRow[1].Trim();
                String category = areaRow[2].Trim();
                String reader = areaRow[3].Trim();

                query = query + " UNION " + mainQuery;

                if (!reader.Equals("null"))
                {
                    if (Report_Activity.isConditionSelected && isAreaCondition)
                        query = query + " AND  badge_history.reader_desc like '" + reader + "'";
                    else
                        query = query + " WHERE  badge_history.reader_desc like '" + reader + "'";
                    Report_Activity.isConditionSelected = true;
                    isAreaCondition = true;
                }

                if (!facility.Equals("null"))
                {
                    if (Report_Activity.isConditionSelected && isAreaCondition)
                        query = query + " AND  facility.id = " + facility;
                    else
                        query = query + " WHERE facility.id = " + facility;
                    Report_Activity.isConditionSelected = true;
                    isAreaCondition = true;
                }

                if (!area.Equals("null"))
                {
                    if (Report_Activity.isConditionSelected && isAreaCondition)
                        query = query + " AND area.id = " + area;
                    else
                        query = query + " WHERE area.id = " + area;
                    Report_Activity.isConditionSelected = true;
                    isAreaCondition = true;
                }

                if (!category.Equals("null"))
                {
                    if (Report_Activity.isConditionSelected && isAreaCondition)
                        query = query + " AND category.id = " + category;
                    else
                        query = query + " WHERE category.id = " + category;
                    Report_Activity.isConditionSelected = true;
                    isAreaCondition = true;
                }
            }

            return query;
        }

        public static string getDaysquery(int[] days, String queryStr)
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
                        query = query + " OR DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Monday'";

                    }
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Monday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Monday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 2)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Tuesday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Tuesday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Tuesday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 3)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Wednesday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Wednesday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Wednesday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;

                }
                if (days[dayCount] == 4)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Thursday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Thursday'";
                        else
                            query = query + " Where ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Thursday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 5)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Friday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Friday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Friday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 6)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Saturday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Saturday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Saturday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 7)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Sunday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Sunday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) Like 'Sunday'";
                    Report_Activity.isConditionSelected = true;
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

        public static string getMonthquery(int[] month, String queryStr)
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
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 1";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 1";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 1";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 2)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 2";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 2";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 2";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 3)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 3";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 3";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 3";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;

                }
                if (month[monthCount] == 4)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 4";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 4";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 4";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;

                }
                if (month[monthCount] == 5)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 5";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 5";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 5";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 6)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 6";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 6";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 6";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 7)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 7";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 7";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 7";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 8)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 8";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 8";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 8";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 9)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 9";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 9";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 9";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 10)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 10";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 10";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 10";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 11)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 11";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 11";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 11";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 12)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 12";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 12";
                        else
                            query = query + " WHERE ( MONTH(CAST(CAST(dbo.badge_history.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(dbo.badge_history.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) = 12";
                    Report_Activity.isConditionSelected = true;
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
        */
        private static string getWildCardquery(String wildCardType, String wildCardData, string query)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, wildCardType + "::" + wildCardData + "::" + query, Logger.logLevel.Debug);


            if (!wildCardData.Equals("null"))
            {
                if (wildCardType == "1")
                {

                    if (Report_Activity.isConditionSelected)
                        query = query + " AND  dbo.person.employee = " + wildCardData;
                    else
                        query = query + " WHERE  dbo.person.employee = " + wildCardData;
                    Report_Activity.isConditionSelected = true;
                }
                else if (wildCardType == "2")
                {
                    /* if (Report_Activity.isConditionSelected)
                         query = query + " AND   [view_rs_access_report_1].SSN like '" + wildCardData+"'";
                     else
                         query = query + " WHERE   [view_rs_access_report_1].SSN like '" + wildCardData + "'";
                     Report_Activity.isConditionSelected = true;*/
                }
                else if (wildCardType == "3")
                {
                    if (Report_Activity.isConditionSelected)
                        query = query + " AND dbo.person.adress5 like '%" + wildCardData + "%'";
                    else
                        query = query + " WHERE dbo.person.address5 like '%" + wildCardData + "%'";
                    Report_Activity.isConditionSelected = true;
                }
                else if (wildCardType == "4")
                {
                    if (Report_Activity.isConditionSelected)
                        query = query + " AND dbo.person.address3  like '" + wildCardData + "'";
                    else
                        query = query + " WHERE dbo.person.address3  like '" + wildCardData + "'";
                    Report_Activity.isConditionSelected = true;
                }
                else if (wildCardType == "5")
                {
                    if (Report_Activity.isConditionSelected)
                        query = query + " AND dbo.person.id  = " + wildCardData;
                    else
                        query = query + " WHERE dbo.person.id  = " + wildCardData;
                    Report_Activity.isConditionSelected = true;
                }

            }
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Info);
            return query;
        }

        public List<ReportRow> getDataByFilter(String badgeId, String personId, String companyId, String divisionId, String empId, String status, String areaString, String stDate, String stTime, String endDate, String endTime, String daysStr, String monthsStr, String wildCardType, String wildCardData)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, badgeId + "::" + personId + "::" + companyId + "::" + divisionId + "::" + empId + "::" + status + "::" + areaString + "::" + stDate + "::" + stTime + "::" + endDate + "::" + endTime + "::" + daysStr + "::" + monthsStr + "::" + wildCardType + "::" + wildCardData, Logger.logLevel.Debug);


            Report_Activity reportObj = Report_Activity.getAccessReportObj();
            SqlConnection conn = null;
            List<ReportRow> rowList = new List<ReportRow>();

            int[] days = null;
            int[] months = null;

            //Form the days array
            if (!daysStr.Equals("null"))
            {
                string[] d = daysStr.Split(',');
                days = new int[d.Length];
                for (int i = 0; i < d.Length; i++)
                {
                    days[i] = Convert.ToInt32(d[i]);
                }
            }

            //Form the months array
            if (!monthsStr.Equals("null"))
            {
                string[] m = monthsStr.Split(',');
                months = new int[m.Length];
                for (int i = 0; i < m.Length; i++)
                {
                    months[i] = Convert.ToInt32(m[i]);
                }
            }

            SqlDataReader sqlreader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();
                //Checks if condition is selected
                Report_Activity.isConditionSelected = false;
                //This string forms the query parameters
                String query = "";
                //This string forms the query parameters for PP query (latest data)
                String ppQuery = "";
                //This string forms the SELECT and FROM queryString for Reporting server query
                String mainSelectStatement = "";
                //This string forms the SELECT and FROM queryString for PP query
                String mainPPSelectStatement = "";
                //This sets if we need to query PP
                bool isLatestData = false;

                //Form the select query based on Area/facility selection
                //Criteria 1: facility
                //Criteria 2: area
                //Criteria 3: Nothing
              
                /* if (isFacilitySelected(areaString))
                    mainSelectStatement = getMainQueryString(1,stDate,endDate,companyId,divisionId);
                   else if (isAreaSelected(areaString))
                    mainSelectStatement = getMainQueryString(2, stDate, endDate, companyId, divisionId);
               else*/
                    mainSelectStatement = getMainQueryString(0, stDate, endDate, companyId, divisionId);



                Report_Activity.isConditionSelected = true;

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

                    if (endDateTime.Date.CompareTo(DateTime.Today.Date) >= 0)
                    {
                        isLatestData = true;
                    }
                }


                //Time query
                if (!stTime.Equals("null"))
                {
                    //Pasadena
                    query = query + " AND @startTime <= CAST([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime AS TIME) ";
                   // query = query + " AND @startTime <= (CAST(STR(FLOOR(dbo.badge_history.dev_xact_time / 10000), 2, 0)+ ':' + RIGHT(STR(FLOOR(dbo.badge_history.dev_xact_time / 100), 6, 0), 2)+ ':' + RIGHT(STR(dbo.badge_history.dev_xact_time), 2) AS TIME))";
                }

                //Time query
                if (!endTime.Equals("null"))
                {
                    //Pasadena
                    query = query + " AND  @endTime >= CAST([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime AS TIME)";
                    //query = query + " AND @endTime >= (CAST(STR(FLOOR(dbo.badge_history.dev_xact_time / 10000), 2, 0)+ ':' + RIGHT(STR(FLOOR(dbo.badge_history.dev_xact_time / 100), 6, 0), 2)+ ':' + RIGHT(STR(dbo.badge_history.dev_xact_time), 2) AS TIME))";
                }

                //Company condition
                if (!companyId.Equals("null"))
                {
                    query = query + " AND   dbo.rs_company.companyId =" + companyId;
                }

                //Name condition
                if (!personId.Equals("null"))
                {
                    query = query + " AND  [ACAMS_Import_Production_History].[dbo].[badge_history].employee = " + personId;
                }

                //Status condition
                if (!status.Equals("null"))
                { 
                    //Pasadena
                    //query = query + " AND badge_history.xact_type = " + status;
                    query = query + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].xact_type = " + status;
                }

                //Division condition
                if (!divisionId.Equals("null"))
                {
                    query = query + " AND  dbo.rs_division.divisionId = " + divisionId;

                }

                //Append all the wild card query conditions
                query = getWildCardquery(wildCardType, wildCardData, query);

                //badge condition
                //support multiple badges
                if (!badgeId.Equals("null"))
                {
                    String[] badgeArray = badgeId.Split(',');
                    query = query + " AND ( [ACAMS_Import_Production_History].[dbo].[badge_history].badgeno = " + badgeArray[0];
                    for (int badgeCount = 1; badgeCount < badgeArray.Length; badgeCount++)
                    {
                        if (!badgeArray[badgeCount].Trim().Equals(""))
                            query = query + " OR [ACAMS_Import_Production_History].[dbo].[badge_history].badgeno = " + badgeArray[badgeCount];
                    }

                    query = query + " ) ";
                }
              

                //Copy all the conditions of reporting server query to PP query
                ppQuery = query;


                if (days != null && days.Count() > 0)
                {
                    query = Report_Activity.getDaysqueryPasadena(days, query);
                }

                if (months != null && months.Count() > 0)
                {
                    query = Report_Activity.getMonthqueryPasadena(months, query);
                }


                if (!areaString.Equals("null"))
                {
                    mainSelectStatement = Report_Activity.getFacilityAreaQuery(areaString, query,stDate,endDate,companyId,divisionId);
                }
                else
                {
                    mainSelectStatement = mainSelectStatement + query;
                }




                SqlCommand command = new SqlCommand();
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
                    if (isLatestData)
                    {
                        //If the end time is later than yesterday, pull the data till yesterday from Reporting Server and the rest from PP
                        endDateTime = DateTime.Today.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                    }
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

                //Set the query string for the reporting server connection
                command.Connection = conn;
                command.CommandText = mainSelectStatement;
                command.CommandTimeout = 0;
                sqlreader = command.ExecuteReader();

                DataTable myTable = new DataTable();
                myTable.Load(sqlreader);



                int count = 0;
                foreach (DataRow row in myTable.Rows)
                {
                    Report_Activity report = new Report_Activity();
                    //if (row["BADGE_ID"].ToString().Trim().Length > 6)
                        report.Badge = row["BADGE_ID"].ToString().Trim();
                   // else
                        //report.Badge = "N/A";
                    report.Company = row["COMPANY"].ToString().Trim();
                    report.FirstName = row["FIRST_NAME"].ToString().Trim();
                    report.LastName = row["LAST_NAME"].ToString().Trim();
                    report.Reader = row["READER"].ToString().Trim();
                    report.Status = row["STATUS"].ToString().Trim();
                    report.Name = row["FIRST_NAME"].ToString().Trim() + " " + row["LAST_NAME"].ToString().Trim();
                    report.DateHistory = row["ACCESS_DATETIME"].ToString().Trim();
                    report.Day = row["DAYS"].ToString().Trim();
                    report.AccessTime = row["ACCESS_TIME"].ToString().Trim();
                    report.EmpId = row["EMP_ID"].ToString().Trim();
                    report.PersonId = row["PERSON_ID"].ToString().Trim();

                    ReportRow repRow = new ReportRow();
                    repRow.id = (++count).ToString();
                    repRow.datarow = report;
                    rowList.Add(repRow);
                }

                if (conn != null)
                {
                    conn.Close();
                }
                if (sqlreader != null)
                {
                    sqlreader.Close();
                }
                
                //LAXCHANGES
                 List<ReportRow> ppDataRows = new List<ReportRow>();

                 //Get the latest data from PP
                 //**********************************************************************
                
                 if (isLatestData)
                 {
                     /*if (isFacilitySelected(areaString))
                         mainPPSelectStatement = getPPData(1,companyId,divisionId);
                     else if (isAreaSelected(areaString))
                         mainPPSelectStatement = getPPData(2);
                     else*/
                         mainPPSelectStatement = getPPData(0,companyId,divisionId);

                     ppQuery = getPPConditionString(badgeId, personId, companyId, divisionId, "", status, stTime, endTime, wildCardType, wildCardData);
                     ppQuery = getWildCardquery(wildCardType, wildCardData, ppQuery);

                     if (!areaString.Equals("null"))
                     {
                         mainPPSelectStatement = Report_Activity.getPPFacilityAreaQuery(areaString, ppQuery,companyId,divisionId);
                     }
                     else
                     {
                         mainPPSelectStatement = mainPPSelectStatement + ppQuery;
                     }

                     ppDataRows = getPPDataFromView(mainPPSelectStatement, stTime, endTime);
                 }

                 rowList.AddRange(ppDataRows);
                 //**********************************************************************
                  

                Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Info);


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
                if (sqlreader != null)
                {
                    sqlreader.Close();
                }
            }
            return rowList;
        }

        private static string getMainQueryString(int criteria,string stDate,string endDate,string companyId,string divisionId)
        {
          

            String query = "";
            bool isDateSelected = false;

            query = "SELECT distinct dbo.xact_desc.description AS STATUS ";
            query = query + ", [ACAMS_Import_Production_History].[dbo].[badge_history].reader_desc AS READER ";
            query = query + ", dbo.person.first_name AS FIRST_NAME, dbo.person.last_name AS LAST_NAME ";
            query = query + ", dbo.person.employee AS EMP_ID ";
            query = query + ", dbo.person.id AS PERSON_ID ";
            query = query + ", [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime AS ACCESS_DATETIME ";
            query = query + ", CAST([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime as time) AS ACCESS_TIME ";
            query = query + ", dbo.department.user1 AS COMPANY ";
            query = query + ", [ACAMS_Import_Production_History].[dbo].[badge_history].badgeno AS BADGE_ID ";
            query = query + ", DATENAME(dw,[ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) AS DAYS ";
            //query = query + ", dbo.person.address3 AS ADDRESS ";
            //query = query + ", dbo.person.address5 as ZIP_CODE ";
            //query = query + ", dbo.department.user2 AS DIV_NAME ";
            //query = query + ", dbo.rs_company.companyId AS COMPANY_ID ";
            //query = query + ", dbo.rs_division.divisionId AS DIVISION_ID ";

           /* if (criteria == 1)
                query = query + " , dbo.facility.id as FACILITY_ID ,dbo.facility.description AS FACILITY ";
            else if (criteria == 2)
                query = query + ", dbo.area.id AS AREA_ID,dbo.area.description AS AREA ";*/

            query = query + " FROM ";
            query = query + " [ACAMS_Import_Production_History].[dbo].[badge_history], dbo.department , dbo.person , dbo.xact_desc ";
            //query = query + " ,dbo.badge ";

            if(!divisionId.Equals("null"))
            {
                query = query + "  , dbo.rs_division ";
            }

            if(!companyId.Equals("null"))
            {
                query = query + "  , dbo.rs_company ";
            }

            /*if (criteria == 1)
                query = query + ", dbo.facility ";
            else if (criteria == 2)
                query = query + ", dbo.area ";*/

            query = query + " WHERE ";

            if (!stDate.Equals("null"))
            {
                query = query + " @startDate <= [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime ";
                isDateSelected = true;
            }

            if (!endDate.Equals("null"))
            {
                if(isDateSelected)
                query = query + " AND @endDate >= [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime ";
                else
                query = query + "  @endDate >= [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime ";
                isDateSelected = true;
            }

            if(!isDateSelected)
            query = query + " [ACAMS_Import_Production_History].[dbo].[badge_history].bid is NOT NULL AND (LEN([ACAMS_Import_Production_History].[dbo].[badge_history].bid) = 11 OR LEN([ACAMS_Import_Production_History].[dbo].[badge_history].bid) = 12) ";
            else
            query = query + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].bid is NOT NULL AND (LEN([ACAMS_Import_Production_History].[dbo].[badge_history].bid) = 11 OR LEN([ACAMS_Import_Production_History].[dbo].[badge_history].bid) = 12) ";

            query = query + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].employee is NOT NULL ";
            query = query + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime is NOT NULL ";
            query = query + " AND dbo.department.id = [ACAMS_Import_Production_History].[dbo].[badge_history].dept ";
            query = query + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].xact_type = dbo.xact_desc.id ";
            query = query + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].employee = dbo.person.employee ";

            if (!divisionId.Equals("null"))
            {
                query = query + " AND dbo.department.user2 Like dbo.rs_division.divisionName ";
            }

            if (!companyId.Equals("null"))
            {
                query = query + " AND dbo.department.user1 Like dbo.rs_company.companyName ";
            }

            /*if (criteria == 1)
                query = query + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].facility = dbo.facility.id ";
            else if (criteria == 2)
                query = query + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].area = dbo.area.id ";*/

            return query;
        }

        private static string getFacilityAreaQuery(string areaString, string query,string stDate,string endDate,string companyId,string divisionId)
        {

            String mainQuery = "";
            String tempString = "";
            areaString = areaString.Trim(';');
            String[] areaArray = areaString.Split(';');

            if (areaArray.Count() > 0)
            {
                String[] areaRow = areaArray[0].Split(',');
                String facility = areaRow[0].Trim();
                String area = areaRow[1].Trim();
                String category = areaRow[2].Trim();
                String reader = areaRow[3].Trim();

                if (!facility.Equals("null"))
                {
                    mainQuery = getMainQueryString(0,stDate,endDate,companyId,divisionId);
                    mainQuery = mainQuery + query;
                    mainQuery = mainQuery + " AND  [ACAMS_Import_Production_History].[dbo].[badge_history].facility = " + facility;
                }

                else if (!area.Equals("null"))
                {
                    mainQuery = getMainQueryString(0,stDate,endDate,companyId,divisionId);
                    mainQuery = mainQuery + query;
                    mainQuery = mainQuery + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].area = " + area;
                }
                else
                {
                    mainQuery = getMainQueryString(0,stDate,endDate,companyId,divisionId);
                    mainQuery = mainQuery + query;
                }
                if (!reader.Equals("null"))
                {
                    //Pasadena
                    //mainQuery = mainQuery + " AND  badge_history.reader_desc like '" + reader + "'";
                    mainQuery = mainQuery + " AND  [ACAMS_Import_Production_History].[dbo].[badge_history].reader_desc like '" + reader + "'";
                }

            }

            for (int count = 1; count < areaArray.Length; count++)
            {
                String[] areaRow = areaArray[count].Split(',');
                String facility = areaRow[0].Trim();
                String area = areaRow[1].Trim();
                String category = areaRow[2].Trim();
                String reader = areaRow[3].Trim();

                tempString = "";

                if (!facility.Equals("null"))
                {
                    tempString = getMainQueryString(0,stDate,endDate,companyId,divisionId);
                    tempString = tempString + query;
                    tempString = tempString + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].facility = " + facility;
                }

                else if (!area.Equals("null"))
                {
                    tempString = getMainQueryString(0, stDate, endDate, companyId, divisionId);
                    tempString = tempString + query;
                    tempString = tempString + " AND [ACAMS_Import_Production_History].[dbo].[badge_history].area = " + area;
                }
                else
                {
                    tempString = getMainQueryString(0, stDate, endDate, companyId, divisionId);
                    tempString = tempString + query;
                    if (!reader.Equals("null"))
                    {
                        tempString = tempString + " AND  [ACAMS_Import_Production_History].[dbo].[badge_history].reader_desc like '" + reader + "'";
                    }

                }

                mainQuery = mainQuery + " UNION " + tempString;

            }

            return mainQuery;
        }

        private static bool isFacilitySelected(String areaString)
        {
            if (areaString.Equals("null"))
                return false;

            areaString = areaString.Trim(';');
            String[] areaArray = areaString.Split(';');

            if (areaArray.Count() > 0)
            {
                String[] areaRow = areaArray[0].Split(',');
                String facility = areaRow[0].Trim();
                String area = areaRow[1].Trim();
                String category = areaRow[2].Trim();
                String reader = areaRow[3].Trim();

                if (facility.Equals("null"))
                    return false;
                else
                    return true;
            }
            return false;
        }

        private static bool isAreaSelected(String areaString)
        {

            if (areaString.Equals("null"))
                return false;

            areaString = areaString.Trim(';');
            String[] areaArray = areaString.Split(';');

            if (areaArray.Count() > 0)
            {
                String[] areaRow = areaArray[0].Split(',');
                String facility = areaRow[0].Trim();
                String area = areaRow[1].Trim();
                String category = areaRow[2].Trim();
                String reader = areaRow[3].Trim();

                if (area.Equals("null"))
                    return false;
                else
                    return true;
            }
            return false;
        }

        
        private static string getPPData(int criteria,string companyId,string divisionId)
        {
            //Method for retriving latest data from picture perfect
            //Replace PP with the linked server DB name

         
            String query = "";
            query = "SELECT distinct pp_xact_desc.description AS STATUS, pp_b_hist.reader_desc AS READER, pp_b_hist.first_name AS FIRST_NAME, pp_b_hist.last_name AS LAST_NAME ";
            query = query + " ,pp_dept.user1 AS COMPANY ";
            query = query + " , pp_person.employee AS EMP_ID, pp_person.id AS PERSON_ID ";
            query = query + " , CAST(CAST(pp_b_hist.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(pp_b_hist.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(pp_b_hist.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime) AS ACCESS_DATETIME ";
            query = query + " , CAST(STR(FLOOR(pp_b_hist.dev_xact_time / 10000), 2, 0)+ ':' + RIGHT(STR(FLOOR(pp_b_hist.dev_xact_time / 100), 6, 0), 2)+ ':' + RIGHT(STR(pp_b_hist.dev_xact_time), 2) AS TIME) AS ACCESS_TIME ";
            query = query + " , pp_badge.unique_id AS BADGE_ID";
            query = query + " , DATENAME(dw,CAST(CAST(pp_b_hist.dev_xact_date AS VARCHAR)+' '+STUFF(STUFF(STUFF(SUBSTRING(CAST(pp_b_hist.dev_xact_time AS VARCHAR),1,6), 1, 0, REPLICATE('0', 6 - LEN(SUBSTRING(CAST(pp_b_hist.dev_xact_time AS VARCHAR),1,6)))),3,0,':'),6,0,':')+'.000' AS datetime)) AS DAYS ";
           
            //query = query + " ,pp_dept.user2 AS DIV_NAME ";
            //query = query + " , pp_person.address3 AS ADDRESS,pp_person.address5 as ZIP_CODE , ";

            /*if (criteria == 1)
                query = query + " , PP.proteus.informix.facility.id as FACILITY_ID ,PP.proteus.informix.facility.description AS FACILITY ";
            else if (criteria == 2)
                query = query + ", PP.proteus.informix.area.id AS AREA_ID,PP.proteus.informix.area.description AS AREA";*/

            query = query + " FROM ";
            query = query + " PP.proteus.informix.badge_history pp_b_hist, PP.proteus.informix.badge pp_badge, PP.proteus.informix.department pp_dept, PP.proteus.informix.person pp_person, PP.proteus.informix.xact_desc pp_xact_desc ";

            if(!companyId.Equals("null"))
            {
                 query = query + " , dbo.rs_company ";
            }

            if (!divisionId.Equals("null"))
            {
                query = query + " , dbo.rs_division ";
            }

            /*if (criteria == 1)
                query = query + ", PP.proteus.informix.facility ";
            else if (criteria == 2)
                query = query + ", PP.proteus.informix.area ";*/

            query = query + " WHERE ";
            query = query + " pp_b_hist.dev_xact_date = CAST(convert(varchar,getdate(),112) as int) ";
            query = query + " AND pp_b_hist.bid is NOT NULL AND (LEN(pp_b_hist.bid) = 11 OR LEN(pp_b_hist.bid) = 12) ";
            query = query + " AND pp_b_hist.employee is NOT NULL ";
            query = query + " AND pp_b_hist.dev_xact_date != 0 ";
            query = query + " AND pp_dept.id =  pp_b_hist.dept ";
            query = query + " AND pp_b_hist.employee = pp_person.employee ";
            query = query + " AND pp_b_hist.xact_type = pp_xact_desc.id ";
            query = query + " AND pp_badge.bid = pp_b_hist.bid AND pp_badge.person_id = pp_person.id ";

            if (!companyId.Equals("null"))
            {
                query = query + " AND pp_dept.user1 Like dbo.rs_company.companyName ";
            }

            if (!divisionId.Equals("null"))
            {
                query = query + " AND pp_dept.user2 Like dbo.rs_division.divisionName ";
            }

            /*if (criteria == 1)
                query = query + " AND pp_b_hist.facility = PP.proteus.informix.facility.id ";
            else if (criteria == 2)
                query = query + " AND pp_b_hist.area = PP.proteus.informix.area.id ";*/

            return query;

        }

        private static List<ReportRow> getPPDataFromView(String mainPPSelectStatement, String stTime, String endTime)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, mainPPSelectStatement + "::" + stTime + "::" + endTime, Logger.logLevel.Debug);

            List<ReportRow> rowList = new List<ReportRow>();
            SqlConnection connection = null;
            SqlDataReader sqlreader = null;
            try
            {
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
                command.CommandText = mainPPSelectStatement;
                command.CommandTimeout = 0;

                int count = 0;
                using (sqlreader = command.ExecuteReader())
                {

                    //Read from the reader
                    while (sqlreader.Read())
                    {
                        Report_Activity report = new Report_Activity();

                       // if (sqlreader.GetSqlValue(9).ToString().Trim().Length > 6)
                            report.Badge = sqlreader.GetSqlValue(9).ToString().Trim();
                       // else
                            //report.Badge = "N/A";

                        report.Company = sqlreader.GetSqlValue(4).ToString().Trim();
                        report.FirstName = sqlreader.GetSqlValue(2).ToString().Trim();
                        report.LastName = sqlreader.GetSqlValue(3).ToString().Trim();
                        report.Reader = sqlreader.GetSqlValue(1).ToString().Trim();
                        report.Status = sqlreader.GetSqlValue(0).ToString().Trim(); ;
                        report.Name = report.FirstName + " " + report.LastName;
                        report.DateHistory = sqlreader.GetSqlValue(7).ToString().Trim();
                        report.Day = sqlreader.GetSqlValue(10).ToString().Trim();
                        report.AccessTime = sqlreader.GetSqlValue(8).ToString().Trim();
                        report.EmpId = sqlreader.GetSqlValue(5).ToString().Trim();
                        report.PersonId = sqlreader.GetSqlValue(6).ToString().Trim();

                        ReportRow repRow = new ReportRow();
                        repRow.id = (++count).ToString();
                        repRow.datarow = report;
                        rowList.Add(repRow);
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
                if (connection != null)
                {
                    connection.Close();
                }
                if (sqlreader != null)
                {
                    sqlreader.Close();
                }
            }

            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            return rowList;
        }

        private static string getPPFacilityAreaQuery(string areaString, string query,string companyId,string divisionId)
        {

            String mainQuery = "";
            String tempString = "";
            areaString = areaString.Trim(';');
            String[] areaArray = areaString.Split(';');

            if (areaArray.Count() > 0)
            {
                String[] areaRow = areaArray[0].Split(',');
                String facility = areaRow[0].Trim();
                String area = areaRow[1].Trim();
                String category = areaRow[2].Trim();
                String reader = areaRow[3].Trim();

                if (!facility.Equals("null"))
                {
                    mainQuery = getPPData(0, companyId,divisionId);
                    mainQuery = mainQuery + query;
                    mainQuery = mainQuery + " AND  pp_b_hist.facility = " + facility;
                }

                else if (!area.Equals("null"))
                {
                    mainQuery = getPPData(0,companyId,divisionId);
                    mainQuery = mainQuery + query;
                    mainQuery = mainQuery + " AND pp_b_hist.area = " + area;
                }
                else
                {
                    mainQuery = getPPData(0,companyId,divisionId);
                    mainQuery = mainQuery + query;
                }
                if (!reader.Equals("null"))
                {
                    mainQuery = mainQuery + " AND  pp_b_hist.reader_desc like '" + reader + "'";
                }

            }

            for (int count = 1; count < areaArray.Length; count++)
            {
                String[] areaRow = areaArray[count].Split(',');
                String facility = areaRow[0].Trim();
                String area = areaRow[1].Trim();
                String category = areaRow[2].Trim();
                String reader = areaRow[3].Trim();

                tempString = "";

                if (!facility.Equals("null"))
                {
                    tempString = getPPData(0,companyId,divisionId);
                    tempString = tempString + query;
                    tempString = tempString + " AND  pp_b_hist.facility = " + facility;
                }

                else if (!area.Equals("null"))
                {
                    tempString = getPPData(0,companyId,divisionId);
                    tempString = tempString + query;
                    tempString = tempString + " AND pp_b_hist.area = " + area;
                }
                else
                {
                    tempString = getPPData(0,companyId,divisionId);
                    tempString = tempString + query;
                    if (!reader.Equals("null"))
                    {
                        tempString = tempString + " AND  pp_b_hist.reader_desc like '" + reader + "'";
                    }

                }

                mainQuery = mainQuery + " UNION " + tempString;

            }

            return mainQuery;
        }


        private static string getPPConditionString(String badgeId, String personId, String companyId, String divisionId, String query, String status, String stTime, String endTime,  String wildCardType, String wildCardData)
        {
            //Time query
            if (!stTime.Equals("null"))
            {
                //Pasadena
                query = query + " AND @startTime <= (CAST(STR(FLOOR(pp_b_hist.dev_xact_time / 10000), 2, 0)+ ':' + RIGHT(STR(FLOOR(pp_b_hist.dev_xact_time / 100), 6, 0), 2)+ ':' + RIGHT(STR(pp_b_hist.dev_xact_time), 2) AS TIME))";
                // query = query + " AND @startTime <= (CAST(STR(FLOOR(dbo.badge_history.dev_xact_time / 10000), 2, 0)+ ':' + RIGHT(STR(FLOOR(dbo.badge_history.dev_xact_time / 100), 6, 0), 2)+ ':' + RIGHT(STR(dbo.badge_history.dev_xact_time), 2) AS TIME))";
            }

            //Time query
            if (!endTime.Equals("null"))
            {
                //Pasadena
                query = query + " AND  @endTime >= (CAST(STR(FLOOR(pp_b_hist.dev_xact_time / 10000), 2, 0)+ ':' + RIGHT(STR(FLOOR(pp_b_hist.dev_xact_time / 100), 6, 0), 2)+ ':' + RIGHT(STR(pp_b_hist.dev_xact_time), 2) AS TIME))";
                //query = query + " AND @endTime >= (CAST(STR(FLOOR(dbo.badge_history.dev_xact_time / 10000), 2, 0)+ ':' + RIGHT(STR(FLOOR(dbo.badge_history.dev_xact_time / 100), 6, 0), 2)+ ':' + RIGHT(STR(dbo.badge_history.dev_xact_time), 2) AS TIME))";
            }

            //Company condition
            if (!companyId.Equals("null"))
            {
                query = query + " AND   dbo.rs_company.companyId =" + companyId;
            }

            //Name condition
            if (!personId.Equals("null"))
            {
                query = query + " AND  pp_b_hist.employee = " + personId;
            }

            //Status condition
            if (!status.Equals("null"))
            {
                //Pasadena
                //query = query + " AND badge_history.xact_type = " + status;
                query = query + " AND pp_b_hist.xact_type = " + status;
            }

            //Division condition
            if (!divisionId.Equals("null"))
            {
                query = query + " AND  dbo.rs_division.divisionId = " + divisionId;

            }

            //Append all the wild card query conditions
            query = getPPWildCardquery(wildCardType, wildCardData, query);

            //badge condition
            //support multiple badges
            if (!badgeId.Equals("null"))
            {
                String[] badgeArray = badgeId.Split(',');
                query = query + " AND ( pp_badge.unique_id = " + badgeArray[0];
                for (int badgeCount = 1; badgeCount < badgeArray.Length; badgeCount++)
                {
                    if (!badgeArray[badgeCount].Trim().Equals(""))
                        query = query + " OR pp_badge.unique_id = " + badgeArray[badgeCount];
                }

                query = query + " ) ";
            }
           

            return query;
        }

        private static string getPPWildCardquery(string wildCardType, string wildCardData, string query)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, wildCardType + "::" + wildCardData + "::" + query, Logger.logLevel.Debug);


            if (!wildCardData.Equals("null"))
            {
                if (wildCardType == "1")
                {

                    if (Report_Activity.isConditionSelected)
                        query = query + " AND  pp_person.employee = " + wildCardData;
                    else
                        query = query + " WHERE  pp_person.employee = " + wildCardData;
                    Report_Activity.isConditionSelected = true;
                }
                else if (wildCardType == "2")
                {
                    /* if (Report_Activity.isConditionSelected)
                         query = query + " AND   [view_rs_access_report_1].SSN like '" + wildCardData+"'";
                     else
                         query = query + " WHERE   [view_rs_access_report_1].SSN like '" + wildCardData + "'";
                     Report_Activity.isConditionSelected = true;*/
                }
                else if (wildCardType == "3")
                {
                    if (Report_Activity.isConditionSelected)
                        query = query + " AND pp_person.adress5 like '%" + wildCardData + "%'";
                    else
                        query = query + " WHERE pp_person.address5 like '%" + wildCardData + "%'";
                    Report_Activity.isConditionSelected = true;
                }
                else if (wildCardType == "4")
                {
                    if (Report_Activity.isConditionSelected)
                        query = query + " AND pp_person.address3  like '" + wildCardData + "'";
                    else
                        query = query + " WHERE pp_person.address3  like '" + wildCardData + "'";
                    Report_Activity.isConditionSelected = true;
                }
                else if (wildCardType == "5")
                {
                    if (Report_Activity.isConditionSelected)
                        query = query + " AND pp_person.id  = " + wildCardData;
                    else
                        query = query + " WHERE pp_person.id  = " + wildCardData;
                    Report_Activity.isConditionSelected = true;
                }

            }
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Info);
            return query; 
        }

        public static string getDaysqueryPasadena(int[] days, String queryStr)
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
                        query = query + " OR DATENAME(dw,[ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Monday'";

                    }
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Monday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Monday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 2)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Tuesday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Tuesday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Tuesday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 3)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Wednesday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Wednesday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Wednesday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;

                }
                if (days[dayCount] == 4)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Thursday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Thursday'";
                        else
                            query = query + " Where ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Thursday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 5)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Friday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Friday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Friday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 6)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Saturday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Saturday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Saturday'";
                    Report_Activity.isConditionSelected = true;
                    isDaySet = true;
                }
                if (days[dayCount] == 7)
                {
                    if (isDaySet)
                        query = query + " OR DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Sunday'";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Sunday'";
                        else
                            query = query + " WHERE ( DATENAME(dw, [ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) Like 'Sunday'";
                    Report_Activity.isConditionSelected = true;
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

        public static string getMonthqueryPasadena(int[] month, String queryStr)
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
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 1";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 1";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 1";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 2)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 2";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 2";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 2";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 3)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 3";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 3";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 3";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;

                }
                if (month[monthCount] == 4)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 4";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 4";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 4";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;

                }
                if (month[monthCount] == 5)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 5";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 5";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 5";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 6)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 6";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 6";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 6";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 7)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 7";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 7";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 7";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 8)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 8";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 8";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 8";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 9)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 9";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 9";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 9";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 10)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 10";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 10";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 10";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 11)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 11";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 11";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 11";
                    Report_Activity.isConditionSelected = true;
                    isMonthSet = true;
                }
                if (month[monthCount] == 12)
                {
                    if (isMonthSet)
                        query = query + " OR MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 12";
                    else
                        if (Report_Activity.isConditionSelected)
                            query = query + " AND ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 12";
                        else
                            query = query + " WHERE ( MONTH([ACAMS_Import_Production_History].[dbo].[badge_history].xact_datetime) = 12";
                    Report_Activity.isConditionSelected = true;
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

