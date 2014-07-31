using ReportingServerWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ReportingServerWebService.Controllers
{
    public class StatusController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get(String statusType)
        {
            List<Status> listEmpId = Status.getAllStatusList(statusType);
            var jsonNew = new
            {
                result = listEmpId
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 
        }

        public HttpResponseMessage Get(String statusType,String keyStroke)
        {
            List<Status> listEmpId = Status.getAllStatusListAutoComplete(statusType,keyStroke);
            var jsonNew = new
            {
                result = listEmpId
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }
    }
}
