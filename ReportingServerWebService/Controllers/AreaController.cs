using ReportingServerWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ReportingServerWebService.Controllers
{
    public class AreaController : System.Web.Http.ApiController
    {
     /*   public String Get()
        {
            //Get all area
            Dictionary<String,String> listArea = Area.getAllAreas();
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listArea);
            return json;
           
        }

        public String GetByFacilityId([FromUri]String id)
        {
            //Get all area by facility
            id = id.Trim(',');
            String[] facilities = id.Split(',');

            Dictionary<String, String> listArea = Area.getAreaByID(facilities);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listArea);
            return json;
           
        }

        public String GetByFacilityId([FromUri]String id,String keyStroke)
        {
            //Get all area by facility
            id = id.Trim(',');
            String[] facilities = id.Split(',');

            Dictionary<String, String> listArea = Area.getAreaByID(facilities,keyStroke);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listArea);
            return json;

        }*/

        public HttpResponseMessage Get()
        {
            //Get all area
            List<Area> lstArea = new List<Area>();
            lstArea = Area.getAllAreasList();
            var jsonNew = new
            {
                result = lstArea
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetByAreaId([FromUri]String id)
        {
            //Get all area by Area
            id = id.Trim(',');
            String[] facilities = id.Split(',');

            List<Area> lstArea = new List<Area>();
            lstArea = Area.getAreaByIDList(facilities);
            var jsonNew = new
            {
                result = lstArea
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetByAreaId([FromUri]String id, String keyStroke)
        {
            //Get all area by Area
            id = id.Trim(',');
            String[] facilities = id.Split(',');

            List<Area> lstArea = new List<Area>();
            lstArea = Area.getAreaByIDList(facilities,keyStroke);
            var jsonNew = new
            {
                result = lstArea
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }
    }
}
