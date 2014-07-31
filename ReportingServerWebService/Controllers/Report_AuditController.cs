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
    public class Report_AuditController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get()
        {
            List<ReportRow> listReport;
            Report_Audit reportObj = Report_Audit.getAccessReportObj();
            listReport = reportObj.getData("null", "null", "null", "null", "null", "null", "null", "null", "null");

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

    
      public HttpResponseMessage GetById(String badgeId, String personId, String companyId, String divisionId, String empId,String categoryId, String wcType, String wcData)
        {
            List<ReportRow> listReport;
            Report_Audit reportObj = Report_Audit.getAccessReportObj();
            listReport = reportObj.getData(badgeId, personId, companyId, divisionId, empId, categoryId, "null", wcType, wcData);

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

      public HttpResponseMessage GetById(String badgeId, String personId, String companyId, String divisionId, String empId, String categoryId, String badgeStatus, String wcType, String wcData)
      {
          List<ReportRow> listReport;
          Report_Audit reportObj = Report_Audit.getAccessReportObj();
          listReport = reportObj.getData(badgeId, personId, companyId, divisionId, empId, categoryId, badgeStatus,wcType, wcData);

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
