using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Net;

namespace ReportingServerWebService.Models
{
    public class Report_ActivityController : System.Web.Http.ApiController
    {
       
        public HttpResponseMessage Get()
        {
            List<ReportRow> listReport;
            Report_Activity reportObj = Report_Activity.getAccessReportObj();
            listReport = reportObj.getDataByFilter("null", "null", "null", "null", "null", "null", "null", "null", "null", "null", "null", "null", "null", "null", "null");

            ReportDataTable tableObj = new ReportDataTable();
            tableObj.Page = "1";
            tableObj.Records = listReport.Count().ToString();
            tableObj.Total = "2";
            tableObj.repdata = listReport;
            var jsonNew = new
            {
                report = tableObj
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 
        }

        public HttpResponseMessage Get(String badgeId, String personId, String companyId, String divisionId, String empId, String status, String areaString, String stDate, String stTime,String endDate, String endTime,String days, String months,String wcType,String wcData)
        {
            List<ReportRow> listReport;
            Report_Activity reportObj = Report_Activity.getAccessReportObj();

            listReport = reportObj.getDataByFilter(badgeId, personId, companyId, divisionId, empId, status, areaString, stDate, stTime, endDate, endTime, days, months, wcType, wcData);


            ReportDataTable tableObj = new ReportDataTable();
            tableObj.Page = "1";
            tableObj.Records = listReport.Count().ToString();
            tableObj.Total = "2";
            tableObj.repdata = listReport;
            var jsonNew = new
            {
                report = tableObj
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 

        }
    }
}
