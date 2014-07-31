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
    public class ReportController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get()
        {
            //Get all area
            List<Report> lstReport = Report.getReportOptions();
            var jsonNew = new
            {
                result = lstReport
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }

        public HttpResponseMessage Get(string keyStroke)
        {
            //Get all area
            List<Report> lstReport = Report.getReportOptions();
            List<Report> lstFilteredReport = new List<Report>();

            for (int i = 0; i < lstReport.Count; i++)
            {
                if (lstReport[i].value.ToLower().Contains(keyStroke.ToLower())) // (you use the word "contains". either equals or indexof might be appropriate)
                {
                    lstFilteredReport.Add(lstReport[i]);
                }
            }

            var jsonNew = new
            {
                result = lstFilteredReport
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }

    }
}
