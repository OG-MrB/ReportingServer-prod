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
    public class Report_TopSoundingAlarmController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get(String criteria, String top, String stDate, String stTime, String endDate, String endTime)
        {
            List<ReportRow> listReport;
            Report_TopSoundingAlarm reportObj = Report_TopSoundingAlarm.getTopSoundingAlarmReportObj();
            listReport = reportObj.getData(criteria,top,stDate,stTime,endDate,endTime);

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
