using ReportingServerWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ReportingServerWebService.Controllers
{
    public class Report_BadgeStatusController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get(String badgeId, String personId, String companyId, String divisionId, String empId, String status, String stDate, String endDate, String days, String months,String wcType,String wcData)
        {
            List<ReportRow> listReport;
            Report_BadgeStatus reportObj = Report_BadgeStatus.getBadgeStatusReportReportObj();

            listReport = reportObj.getData(badgeId, personId, companyId, divisionId, empId, status, stDate, endDate, days, months,wcType,wcData);


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
