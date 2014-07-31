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
    public class BadgeController : System.Web.Http.ApiController
    {

     /*   public String Get()
        {
            //Get all area
            Dictionary<String, String> listBadge = Badge.getAllBadges();
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listBadge);
             return json;

        }

        public String GetBadgeByCompanyAndDivision(String company,String division)
        {
            //Get all area
            Dictionary<String, String> listBadge = Badge.GetBadgeByCompanyAndDivision(company, division);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listBadge);
            return json;

        }

        public String GetBadgeByCompanyAndDivision(String company, String division,String keyStroke)
        {
            //Get all area
            Dictionary<String, String> listBadge = Badge.GetBadgeByCompanyAndDivision(company, division,keyStroke);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listBadge);
            return json;

        } */

        public HttpResponseMessage Get()
        {
            List<Badge> lstBadge = new List<Badge>();
            lstBadge = Badge.getAllBadgesList();
            var jsonNew = new
            {
                result = lstBadge
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetBadgeByCompanyAndDivision(String company, String division)
        {
            List<Badge> lstBadge = new List<Badge>();
            lstBadge = Badge.GetBadgeByCompanyAndDivisionList(company, division);
            var jsonNew = new
            {
                result = lstBadge
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetBadgeByCompanyAndDivision(String company, String division, String keyStroke)
        {
            //Get all Badge
            List<Badge> lstBadge = new List<Badge>();
            lstBadge = Badge.GetBadgeByCompanyAndDivisionList(company, division, keyStroke);
            var jsonNew = new
            {
                result = lstBadge
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

       

    }
}
