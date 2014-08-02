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
    public class WildCardController : System.Web.Http.ApiController
    {
        

        public HttpResponseMessage Get(string report)
        {
            //Get all area
            List<WildCard> lstWildCard = WildCard.getWCOptions(report);
             var jsonNew = new
            {
                result = lstWildCard
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }

        public HttpResponseMessage Get(string report,string keyStroke)
        {
            //Get all area
            List<WildCard> lstWildCard = WildCard.getWCOptions(report);
            List<WildCard> lstFilteredWildCard = new List<WildCard>();

            for (int i = 0; i < lstWildCard.Count; i++)
            {
                if (lstWildCard[i].value.ToLower().Contains(keyStroke.ToLower())) // (you use the word "contains". either equals or indexof might be appropriate)
                {
                    lstFilteredWildCard.Add(lstWildCard[i]);
                }
            }

            var jsonNew = new
            {
                result = lstFilteredWildCard
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }

      /*  public HttpResponseMessage Get(string keyStroke)
        {
            //Get all area
            List<WildCard> lstWildCard = WildCard.getWCOptions(report);
            List<WildCard> lstFilteredWildCard = new List<WildCard>();

            for (int i = 0; i < lstWildCard.Count; i++)
            {
                if (lstWildCard[i].value.ToLower().Contains(keyStroke.ToLower())) // (you use the word "contains". either equals or indexof might be appropriate)
                {
                    lstFilteredWildCard.Add(lstWildCard[i]);
                }
            }

            var jsonNew = new
            {
                result = lstFilteredWildCard
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }*/
    }
}
