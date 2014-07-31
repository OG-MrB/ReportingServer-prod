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
    public class PersonController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get()
        {
            //Get all area
            List<Person> lstPerson = new List<Person>();
            lstPerson = Person.getAllPersonList();
            var jsonNew = new
            {
                result = lstPerson
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetEmpByCompanyAndDivision(String company, String division)
        {
            //Get all area
            List<Person> lstPerson = new List<Person>();
            lstPerson = Person.getEmpByCompanyList(company, division);
            var jsonNew = new
            {
                result = lstPerson
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }

        public HttpResponseMessage GetEmpByCompanyAndDivision(String company, String division, String keyStroke)
        {
            //Get all area
            List<Person> lstPerson = new List<Person>();
            lstPerson = Person.getEmpByCompanyList(company, division,keyStroke);
            var jsonNew = new
            {
                result = lstPerson
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }
    }
}
