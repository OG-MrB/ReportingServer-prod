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
    public class EmpIdController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get()
        {
            //Get all area
            List<EmpId> listEmpId = EmpId.getAllEmpIdList();
            var jsonNew = new
            {
                result = listEmpId
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 
        }

        public HttpResponseMessage GetEmpByCompanyAndDivision(String company, String division)
        {
            //Get all area
            List<EmpId> listEmpId = EmpId.getEmpByCompanyList(company, division);
            var jsonNew = new
            {
                result = listEmpId
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 
        }

        public HttpResponseMessage GetEmpByCompanyAndDivision(String company, String division, String keyStroke)
        {
            //Get all area
            List<EmpId> listEmpId = EmpId.getEmpByCompanyList(company, division, keyStroke);
            var jsonNew = new
            {
                result = listEmpId
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 
        }

    }
}
