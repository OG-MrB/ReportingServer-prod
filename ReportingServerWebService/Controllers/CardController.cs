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
    public class CardController : System.Web.Http.ApiController
    {

        public HttpResponseMessage Get()
        {
            List<CardNo> lstCard = new List<CardNo>();
            lstCard = CardNo.getAllCardNosList();
            var jsonNew = new
            {
                result = lstCard
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetCardByCompanyAndDivision(String company, String division)
        {
            List<CardNo> lstCard = new List<CardNo>();
            lstCard = CardNo.GetCardNoByCompanyAndDivisionList(company, division);
            var jsonNew = new
            {
                result = lstCard
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetCardByCompanyAndDivision(String company, String division, String keyStroke)
        {
            //Get all Card
            List<CardNo> lstCard = new List<CardNo>();
            lstCard = CardNo.GetCardNoByCompanyAndDivisionList(company, division, keyStroke);
            var jsonNew = new
            {
                result = lstCard
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }



    }
}
