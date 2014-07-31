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
    public class CompanyController : System.Web.Http.ApiController
    {
       /* public String Get()
        {
            //Get all area
            Dictionary<String, String> listCompany = Company.getAllCompany();
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listCompany);
            return json;

        }

        public String Get(String keyStroke)
        {
            //Get all area
            Dictionary<String, String> listCompany = Company.getAllCompany(keyStroke);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listCompany);
            return json;

        }*/


        public HttpResponseMessage Get()
        {
            //Get all Companys
            List<Company> lstCompany = new List<Company>();
            lstCompany = Company.getAllCompanyList();
            var jsonNew = new
            {
                result = lstCompany
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage Get(String keyStroke)
        {
            //Get all Companys
            List<Company> lstCompany = new List<Company>();
            lstCompany = Company.getAllCompanyList(keyStroke);
            var jsonNew = new
            {
                result = lstCompany
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

    }
}
