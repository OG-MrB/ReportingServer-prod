using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ReportingServerWebService.Models;
using System.Net.Http;
using System.Net;

namespace ReportingServerWebService.Controllers
{
    public class WildCardDataController : System.Web.Http.ApiController
    {

        public HttpResponseMessage Get(String wildCardType)
        {
            /*Dictionary<string, string> listType = WildCard.getAllEntires(Convert.ToInt32(wildCardType));
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listType);
            return json;*/

            var jsonNew = new
            {
                result = WildCard.getAllEntiresList(Convert.ToInt32(wildCardType))
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 

        }

        public HttpResponseMessage GetByCompany(String wildCardType, String companyId, String divisionId)
        {
           /* Dictionary<string, string> listType = WildCard.getAllEntiresByCompany(Convert.ToInt32(wildCardType),companyId,divisionId);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listType);
            return json;*/
            var jsonNew = new
            {
                result = WildCard.getAllEntiresByCompanyList(Convert.ToInt32(wildCardType),companyId,divisionId)
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 

        }

        public HttpResponseMessage GetByChar(String wildCardType, String companyId, String divisionId, String keyStrokes)
        {
          /*  Dictionary<string, string> listType = WildCard.getAllEntires(Convert.ToInt32(wildCardType),companyId,divisionId,keyStrokes);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listType);
            return json;*/
            var jsonNew = new
            {
                result = WildCard.getAllEntiresList(Convert.ToInt32(wildCardType),companyId,divisionId,keyStrokes)
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew); 
        }
    }
}
