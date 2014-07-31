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
    public class ReaderController : System.Web.Http.ApiController
    {
       /* public String Get()
        {
            //Get all Category
            Dictionary<String, String> listReader = Reader.getAllReaders();
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listReader);
            return json;
           
        }

        public String GetCategoryById(String facility, String area, String category)
        {
            //Get all Category by area
            Dictionary<String, String> listReader = Reader.getReadersByCategory(facility, area, category);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listReader);
            return json;
           
        }

        public String GetCategoryById(String facility, String area, String category,String keyStroke)
        {
            //Get all Category by area
            Dictionary<String, String> listReader = Reader.getReadersByCategory(facility, area, category,keyStroke);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listReader);
            return json;

        }*/



        public HttpResponseMessage Get()
        {
            //Get all Readers
            List<Reader> lstReader = new List<Reader>();
            lstReader = Reader.getAllReadersList();
            var jsonNew = new
            {
                result = lstReader
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
           
        }

        public HttpResponseMessage GetReaderById(String facility, String area, String category)
        {
            //Get all Reader by area
            List<Reader> lstReader = new List<Reader>();
            lstReader = ReportingServerWebService.Models.Reader.getReadersByFacilityList(facility, area, category);
            var jsonNew = new
            {
                result = lstReader
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
           
           
        }

        public HttpResponseMessage GetReaderById(String facility, String area, String category, String keyStroke)
        {
            //Get all Reader by area
            List<Reader> lstReader = new List<Reader>();
            lstReader = ReportingServerWebService.Models.Reader.getReadersByFacilityList(facility, area, category, keyStroke);
            var jsonNew = new
            {
                result = lstReader
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
           

        }
    }
}
