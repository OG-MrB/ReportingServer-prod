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
    public class DivisionController : System.Web.Http.ApiController
    {
       /* public String Get()
        {
            //Get all area
            Dictionary<String, String> listDivision = Division.getAllDivisions();
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listDivision);
            return json;

        }

        public String GetDivisionByCompany(string company)
        {
            //Get all area
            Dictionary<String, String> listDivision = Division.getDivisionInCompany(company);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listDivision);
            return json;

        }


        public String GetDivisionByCompany(string company,string keyStroke)
        {
            //Get all area
            Dictionary<String, String> listDivision = Division.getDivisionInCompany(company,keyStroke);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listDivision);
            return json;

        } */


        public HttpResponseMessage Get()
        {
            //Get all area
            List<Division> lstDivision = new List<Division>();
            lstDivision = Division.getAllDivisionsList();
            var jsonNew = new
            {
                result = lstDivision
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetDivisionByCompany(string company)
        {
            //Get all area
            List<Division> lstDivision = new List<Division>();
            lstDivision = Division.getDivisionInCompanyList(company);
            var jsonNew = new
            {
                result = lstDivision
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }


        public HttpResponseMessage GetDivisionByCompany(string company, string keyStroke)
        {
            //Get all area
            List<Division> lstDivision = new List<Division>();
            lstDivision = Division.getDivisionInCompanyList(company, keyStroke);
            var jsonNew = new
            {
                result = lstDivision
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }
    }
}
