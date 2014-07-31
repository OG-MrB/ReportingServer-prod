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
    public class AlarmController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get()
        {
            //Get all area
            List<Alarm> lstAlarm = new List<Alarm>();
            lstAlarm = Alarm.getAllAlarmsList();
            var jsonNew = new
            {
                result = lstAlarm
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }

        public HttpResponseMessage GetAlarmBYCHar(String alarmChar)
        {
            //Get all area by facility
            List<Alarm> lstAlarm = new List<Alarm>();
            lstAlarm = Alarm.getAllAlarmsListByKeystroke(alarmChar);
            var jsonNew = new
            {
                result = lstAlarm
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);

        }
    }
}
