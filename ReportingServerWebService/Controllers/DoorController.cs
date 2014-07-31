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
    public class DoorController : System.Web.Http.ApiController
    {
         
        /*public String Get()
        {
            //Get all door
            Dictionary<String, String> listEmpId = Door.getAllDoors();
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listEmpId);
            return json;
          
        }

        public String GetDoorByChar(String keyStroke)
        {
            //Get all door
            Dictionary<String, String> listEmpId = Door.getAllDoors(keyStroke);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(listEmpId);
            return json;
          
        }*/

        public HttpResponseMessage Get()
        {
            List<Door> listEmpId = Door.getAllDoorsList();
            var jsonNew = new
            {
                result = listEmpId
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 
          
        }

        public HttpResponseMessage GetDoorByChar(String keyStroke)
        {
            List<Door> listEmpId = Door.getAllDoorsList(keyStroke);
            var jsonNew = new
            {
                result = listEmpId
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 
          
        }
    }
}
