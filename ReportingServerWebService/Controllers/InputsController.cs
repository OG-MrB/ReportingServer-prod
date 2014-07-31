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
    public class InputsController : System.Web.Http.ApiController
    {

       
        public HttpResponseMessage Get()
        {
            List<Inputs> listEmpId = Inputs.getAllInputssList();
            var jsonNew = new
            {
                result = listEmpId
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetInputsByChar(String keyStroke)
        {
            List<Inputs> listEmpId = Inputs.getAllInputssList(keyStroke);
            var jsonNew = new
            {
                result = listEmpId
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }
    }
}
