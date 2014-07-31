using ReportingServerWebService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ReportingServerWebService.Controllers
{
    public class FacilityController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get()
        {
            //Get all facility
           /* Dictionary<String,String> listFacility = Facility.getAllFacility();

            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listFacility);
            return json;*/

            List<Facility> lstFacility = new List<Facility>();
            lstFacility = Facility.getAllFacilityList();
            var jsonNew = new 
            {
                result = lstFacility
            };
            return Request.CreateResponse(HttpStatusCode.OK,jsonNew);
        }


        public HttpResponseMessage Get(String facilityChar)
        {
            //Get all facility
            /*Dictionary<String, String> listFacility = Facility.getAllFacility(facilityChar);

            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listFacility);
            return json;*/

            List<Facility> lstFacility = new List<Facility>();
            lstFacility = Facility.getAllFacilityListKeyStroke(facilityChar);
            var jsonNew = new
            {
                result = lstFacility
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }

    }
}
