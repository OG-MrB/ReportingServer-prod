using ReportingServerWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ReportingServerWebService.Models
{
    public class Report_Audit
    {
        public static Report_Audit auditReportObj;
        private static Boolean isConditionSelected = false;
        String companyId;
        String companyName;
        String division;
        String empId;
        String firstName;
        String lastName;
        String category;
        String badge;
        String name;
        String status;

        public String CompanyId { get { return this.companyId; } set { this.companyId = value; } }
        public String CompanyName { get { return this.companyName; } set { this.companyName = value; } }
        public String Division { get { return this.division; } set { this.division = value; } }
        public String FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public String LastName { get { return this.lastName; } set { this.lastName = value; } }
        public String Category { get { return this.category; } set { this.category = value; } }
        public String EmpId { get { return this.empId; } set { this.empId = value; } }
        public String Badge { get { return this.badge; } set { this.badge = value; } }
        public String Name { get { return this.name; } set { this.name = value; } }

        public String Status { get { return this.status; } set { this.status = value; } }

        public static Report_Audit getAccessReportObj()
        {
            if (auditReportObj == null)
            {
                auditReportObj = new Report_Audit();
            }
            return auditReportObj;
        }

       /* private static string getWildCardQueryString(String wildCardType, String wildCardData, string query)
        {
            if (!wildCardData.Equals("null"))
            {
                if (wildCardType == "1")
                {

                    if (Report_Audit.isConditionSelected)
                        query = query + " AND   [view_rs_audit_report_1].EMPLOYEE_ID = " + wildCardData;
                    else
                        query = query + " WHERE   [view_rs_audit_report_1].EMPLOYEE_ID = " + wildCardData;
                    Report_Audit.isConditionSelected = true;
                }
                else if (wildCardType == "2")
                {
                   // if (Report_Audit.isConditionSelected)
                   //     query = query + " AND   [view_rs_access_report_1].SSN = " + wildCardData;
                   // else
                   //     query = query + " WHERE   [view_rs_access_report_1].SSN = " + wildCardData;
                  //  Report_Audit.isConditionSelected = true;
                }
                else if (wildCardType == "3")
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND   [view_rs_access_report_1].ZIP_CODE Like '%" + wildCardData +"%'";
                    else
                        query = query + " WHERE   [view_rs_access_report_1].ZIP_CODE like '%" + wildCardData + "%'";
                    Report_Audit.isConditionSelected = true;
                }
                else if (wildCardType == "4")
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND   [view_rs_access_report_1].ADDRESS like '" + wildCardData +"'";
                    else
                        query = query + " WHERE   [view_rs_access_report_1].ADDRESS like '" + wildCardData+"'";
                    Report_Audit.isConditionSelected = true;
                }

            }
            return query;
        }*/


        private static string getWildCardquery(String wildCardType, String wildCardData, string query)
        {
            if (!wildCardData.Equals("null"))
            {
                if (wildCardType == "1")
                {

                    if (Report_Audit.isConditionSelected)
                        query = query + " AND  p.employee = " + wildCardData;
                    else
                        query = query + " WHERE  p.employee = " + wildCardData;
                    Report_Audit.isConditionSelected = true;
                }
                else if (wildCardType == "2")
                {
                    /* if (Report_Audit.isConditionSelected)
                         query = query + " AND   [view_rs_access_report_1].SSN like '" + wildCardData+"'";
                     else
                         query = query + " WHERE   [view_rs_access_report_1].SSN like '" + wildCardData + "'";
                     Report_Audit.isConditionSelected = true;*/
                }
                else if (wildCardType == "3")
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND p.adress5 like '%" + wildCardData + "%'";
                    else
                        query = query + " WHERE p.address5 like '%" + wildCardData + "%'";
                    Report_Audit.isConditionSelected = true;
                }
                else if (wildCardType == "4")
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND p.address3  like '" + wildCardData + "'";
                    else
                        query = query + " WHERE p.address3  like '" + wildCardData + "'";
                    Report_Audit.isConditionSelected = true;
                }
                else if (wildCardType == "5")
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND p.id  like '" + wildCardData + "'";
                    else
                        query = query + " WHERE p.id  like '" + wildCardData + "'";
                    Report_Audit.isConditionSelected = true;
                }

            }
            return query;
        }


       /* public List<ReportRow> getData(String badgeId, String personId, String companyId, String divisionId, String empId, String categoryId, String wildCardType, String wildCardData)
        {
            Report_Audit reportObj = Report_Audit.getAccessReportObj();
            SqlConnection conn = null;
         
             SqlDataReader sqlreader = null;
             try
             {
                 // create and open a connection object
                 conn = ConnectionManager.getConnection();
                 conn.Open();

                 String query = "";
                 query = "SELECT TOP 1000 [FIRST_NAME],[LAST_NAME],[COMPANY_NAME],[DIVISION],[EMPLOYEE_ID],[CATEGORY_NAME],[BADGE_ID] FROM  [view_rs_audit_report_1]";
                

                 Report_Audit.isConditionSelected = false;

                 if (!badgeId.Equals("null"))
                 {
                     if (Report_Audit.isConditionSelected)
                         query = query + " AND   [view_rs_audit_report_1].BADGE_ID like '" + badgeId+"'";
                     else
                         query = query + " WHERE   [view_rs_audit_report_1].BADGE_ID like '" + badgeId+"'";
                     Report_Audit.isConditionSelected = true;
                 }

                 if (!companyId.Equals("null"))
                 {
                     if (Report_Audit.isConditionSelected)
                         query = query + " AND   [view_rs_audit_report_1].COMPANY_ID = " + companyId;
                     else
                         query = query + " WHERE   [view_rs_audit_report_1].COMPANY_ID = " + companyId;
                     Report_Audit.isConditionSelected = true;
                 }


                 if (!personId.Equals("null"))
                 {
                     if (Report_Audit.isConditionSelected)
                         query = query + " AND   [view_rs_audit_report_1].PERSON_ID = " + personId;
                     else
                         query = query + " WHERE   [view_rs_audit_report_1].PERSON_ID = " + personId;
                     Report_Audit.isConditionSelected = true;
                 }

                 if (!empId.Equals("null"))
                 {
                     if (Report_Audit.isConditionSelected)
                         query = query + " AND   [view_rs_audit_report_1].EMPLOYEE_ID = " + empId;
                     else
                         query = query + " WHERE   [view_rs_audit_report_1].EMPLOYEE_ID = " + empId;
                     Report_Audit.isConditionSelected = true;
                 }

                 if (!categoryId.Equals("null"))
                 {
                     if (Report_Audit.isConditionSelected)
                         query = query + " AND   [view_rs_audit_report_1].CATEGORY_ID = " + categoryId;
                     else
                         query = query + " WHERE   [view_rs_audit_report_1].CATEGORY_ID = " + categoryId;
                     Report_Audit.isConditionSelected = true;
                 }

                 if (!divisionId.Equals("null"))
                 {
                     if (Report_Audit.isConditionSelected)
                         query = query + " AND   [view_rs_audit_report_1].DIVISION_ID = " + divisionId;
                     else
                         query = query + " WHERE   [view_rs_audit_report_1].DIVISION_ID = " + divisionId;
                     Report_Audit.isConditionSelected = true;
                 }

               
                 query = getWildCardQueryString(wildCardType, wildCardData, query);

                 SqlCommand command = new SqlCommand(query, conn);
                 List<ReportRow> rowList = new List<ReportRow>();
                 int count = 0;
                 using (sqlreader = command.ExecuteReader())
                 {
                     while (sqlreader.Read())
                     {
                         Report_Audit report = new Report_Audit();
                         report.CompanyName = sqlreader.GetSqlValue(2).ToString().Trim();
                         report.FirstName = sqlreader.GetSqlValue(0).ToString().Trim();
                         report.LastName = sqlreader.GetSqlValue(1).ToString().Trim();
                         report.Division = sqlreader.GetSqlValue(3).ToString().Trim();
                         report.EmpId = sqlreader.GetSqlValue(4).ToString().Trim();
                         report.Category = sqlreader.GetSqlValue(5).ToString().Trim();
                         report.Badge = sqlreader.GetSqlValue(6).ToString().Trim();
                         report.Name = report.FirstName + " " + report.LastName;

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

             
        }*/


        public List<ReportRow> getData(String badgeId, String personId, String companyId, String divisionId, String empId, String categoryId, String badgeStatus,String wildCardType, String wildCardData)
        {
            Report_Audit reportObj = Report_Audit.getAccessReportObj();
            SqlConnection conn = null;

            SqlDataReader sqlreader = null;
            try
            {
                // create and open a connection object
                conn = ConnectionManager.getConnection();
                conn.Open();

                String query = "";

                query = "SELECT distinct comp.companyId AS COMPANY_ID, comp.companyName AS COMPANY_NAME, div.divisionId AS DIVISION_ID, div.divisionName AS DIVISION, p.employee AS EMPLOYEE_ID,p.id AS PERSON_ID, p.first_name AS FIRST_NAME, p.last_name AS LAST_NAME, ";
                query = query + " c.description AS CATEGORY_NAME,c.id AS CATEGORY_ID, b.unique_id AS BADGE_ID, p.address3 AS ADDRESS,p.address5 AS ZIP_CODE,bsts.cond_desc as BADGE_STATUS";
             
                query = query + " FROM person p, department d, category c, person_category pc,badge b, rs_company comp, rs_division div, badgests bsts ";
                query = query + " WHERE  ";
                query = query + " b.bid is NOT NULL AND (LEN(b.bid) = 11 OR LEN(b.bid) = 12) ";
                query = query + " AND b.person_id is NOT NULL ";
                query = query + " AND c.id = pc.category_id AND  p.id = pc.person_id ";
                query = query + " AND d.id = p.department AND b.person_id = p.id AND  d.user1 Like comp.companyName AND d.user2 like div.divisionName AND bsts.id = b.status AND b.unique_id IS NOT NULL AND b.unique_id <> ''";

                Report_Audit.isConditionSelected = true;

                if (!badgeId.Equals("null"))
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND b.bid like '" + badgeId + "'";
                    else
                        query = query + " WHERE b.bid like '" + badgeId + "'";
                    Report_Audit.isConditionSelected = true;
                }

                if (!companyId.Equals("null"))
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND comp.companyId = " + companyId;
                    else
                        query = query + " WHERE comp.companyId = " + companyId;
                    Report_Audit.isConditionSelected = true;
                }


                if (!personId.Equals("null"))
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND p.employee = " + personId;
                    else
                        query = query + " WHERE p.employee = " + personId;
                    Report_Audit.isConditionSelected = true;
                }

                if (!empId.Equals("null"))
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND p.employee = " + empId;
                    else
                        query = query + " WHERE p.employee = " + empId;
                    Report_Audit.isConditionSelected = true;
                }

                if (!categoryId.Equals("null"))
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND c.id = " + categoryId;
                    else
                        query = query + " WHERE c.id = " + categoryId;
                    Report_Audit.isConditionSelected = true;
                }

                if (!divisionId.Equals("null"))
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND div.divisionId = " + divisionId;
                    else
                        query = query + " WHERE div.divisionId = " + divisionId;
                    Report_Audit.isConditionSelected = true;
                }

                if (!badgeStatus.Equals("null"))
                {
                    if (Report_Audit.isConditionSelected)
                        query = query + " AND b.status = " + badgeStatus;
                    else
                        query = query + " WHERE b.status = " + badgeStatus;
                    Report_Audit.isConditionSelected = true;
                }

                query = getWildCardquery(wildCardType, wildCardData, query);

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandTimeout = 0;
                List<ReportRow> rowList = new List<ReportRow>();
                int count = 0;
                using (sqlreader = command.ExecuteReader())
                {
                    while (sqlreader.Read())
                    {
                        Report_Audit report = new Report_Audit();
                        report.CompanyName = sqlreader.GetSqlValue(1).ToString().Trim();
                        report.FirstName = sqlreader.GetSqlValue(6).ToString().Trim();
                        report.LastName = sqlreader.GetSqlValue(7).ToString().Trim();
                        report.Division = sqlreader.GetSqlValue(3).ToString().Trim();
                        report.EmpId = sqlreader.GetSqlValue(4).ToString().Trim();
                        report.Category = sqlreader.GetSqlValue(8).ToString().Trim();
                        report.Badge = sqlreader.GetSqlValue(10).ToString().Trim();
                        report.Name = report.FirstName + " " + report.LastName;
                        report.Status = sqlreader.GetSqlValue(13).ToString().Trim();

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