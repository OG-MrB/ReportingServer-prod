using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportingServerWebService.Models;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net;


namespace ReportingServerWebService.Controllers
{
    public class ProductController : System.Web.Http.ApiController
    {


     /*   public HttpResponseMessage Get()
        {//,String fname,String lname,String company,String stDate,String endDate,String empid,String reader,String status,String days
            List<ReportRow> listReport;
            Report_Activity reportObj = Report_Activity.getAccessReportObj();

            listReport = reportObj.getData("null","null", "null", "null", "null", "null", "null", "null","null", "null", "null", "null", "null","null","null");

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

        }*/

    }
}
