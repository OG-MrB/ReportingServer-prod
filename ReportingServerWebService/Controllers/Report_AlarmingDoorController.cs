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
    public class Report_AlarmingDoorController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get(String facilityId, String alarmDesc, String inputDesc, String stDate, String endDate, String stTime, String endTime, String days, String months)
        {
            List<ReportRow> listReport;
            Report_AlarmDoor reportObj = Report_AlarmDoor.getAlarmDoorReportObj();

            listReport = reportObj.getData(facilityId,alarmDesc,inputDesc,stDate,endDate,stTime,endTime,days,months);


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
