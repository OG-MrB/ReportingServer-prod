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
    public class NameController : System.Web.Http.ApiController
    {
     /*   public String Get()
        {
            //Get all area
            Dictionary<String, String> listName = Name.getAllNames();
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listName);
            return json;

        }

        public String GetBadgeByCompanyAndDivision(String company, String division)
        {
            //Get all area
            Dictionary<String, String> listName = Name.getNamesByCompany(company, division);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listName);
            return json;

        }

        public String GetBadgeByCompanyAndDivision(String company, String division,String keyStroke)
        {
            //Get all area
            Dictionary<String, String> listName = Name.getNamesByCompany(company, division,keyStroke);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listName);
            return json;

        }*/

        public HttpResponseMessage Get()
        {
            List<Name> lstName = new List<Name>();
            lstName = Name.getAllNamesList();
            var jsonNew = new
            {
                result = lstName
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetBadgeByCompanyAndDivision(String company, String division)
        {
            List<Name> lstName = new List<Name>();
            lstName = Name.getNamesByCompanyList(company, division);
            var jsonNew = new
            {
                result = lstName
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);


        }

        public HttpResponseMessage GetBadgeByCompanyAndDivision(String company, String division, String keyStroke)
        {
            List<Name> lstName = new List<Name>();
            lstName = Name.getNamesByCompanyList(company, division,keyStroke);
            var jsonNew = new
            {
                result = lstName
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

    }
}
