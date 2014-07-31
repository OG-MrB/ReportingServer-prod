using MiscingServerWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ReportingServerWebService.Controllers
{
    public class MiscController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get()
        {
            List<Misc> listMiscId = Misc.getMiscOptions();
            var jsonNew = new
            {
                result = listMiscId
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }

        public HttpResponseMessage Get(String keyStroke)
        {
            List<Misc> listMiscId = Misc.getMiscOptions();
            List<Misc> lstFilteredMisc = new List<Misc>();

            for (int i = 0; i < listMiscId.Count; i++)
            {
                if (listMiscId[i].value.ToLower().Contains(keyStroke.ToLower())) // (you use the word "contains". either equals or indexof might be appropriate)
                {
                    lstFilteredMisc.Add(listMiscId[i]);
                }
            }

            var jsonNew = new
            {
                result = lstFilteredMisc
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }
    }
}
