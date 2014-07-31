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
    public class Report_DoorCategoryController : System.Web.Http.ApiController
    {

        public HttpResponseMessage Get(String areaId,String categoryId,String doorId)
        {
            List<ReportRow> listReport;
            Report_DoorCategory reportObj = Report_DoorCategory.getAccessReportObj();
            listReport = reportObj.getData(areaId,categoryId,doorId);

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
