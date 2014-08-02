using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ReportingServerWebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {


            config.Routes.MapHttpRoute(
             name: "Product",
             routeTemplate: "api/Product/{id}",
             defaults: new { controller = "Product", id = RouteParameter.Optional }

         );

            config.Routes.MapHttpRoute(
             name: "Facility",
             routeTemplate: "api/Facility/{facilityChar}",
             defaults: new { controller = "Facility", facilityChar = RouteParameter.Optional }

         );
            config.Routes.MapHttpRoute(
             name: "Alarm",
             routeTemplate: "api/Alarm/{alarmChar}",
             defaults: new { controller = "Alarm", alarmChar = RouteParameter.Optional }

         );

            config.Routes.MapHttpRoute(
            name: "Door",
            routeTemplate: "api/Door/{keyStroke}",
            defaults: new { controller = "Door", keyStroke = RouteParameter.Optional }

        );

            config.Routes.MapHttpRoute(
           name: "Inputs",
           routeTemplate: "api/Inputs/{keyStroke}",
           defaults: new { controller = "Inputs", keyStroke = RouteParameter.Optional }

       );

            config.Routes.MapHttpRoute(
           name: "Report",
           routeTemplate: "api/Report/{keyStroke}",
           defaults: new { controller = "Report", keyStroke = RouteParameter.Optional }

       );

            config.Routes.MapHttpRoute(
             name: "WildCard",
             routeTemplate: "api/WildCard/{report}/{keyStroke}",
             defaults: new { controller = "WildCard", keyStroke = RouteParameter.Optional, report = RouteParameter.Optional }

         );

            config.Routes.MapHttpRoute(
             name: "Misc",
             routeTemplate: "api/Misc/{keyStroke}",
             defaults: new { controller = "Misc", keyStroke = RouteParameter.Optional }

         );

            config.Routes.MapHttpRoute(
             name: "Area",
             routeTemplate: "api/Area/{id}/{keyStroke}",
             defaults: new { controller = "Area", id = RouteParameter.Optional, keyStroke = RouteParameter.Optional }

         );

            config.Routes.MapHttpRoute(
             name: "Category",
             routeTemplate: "api/Category/{facilityId}/{areaId}/{keyStroke}",
             defaults: new { controller = "Category", areaId = RouteParameter.Optional, facilityId = RouteParameter.Optional, keyStroke = RouteParameter.Optional }

         );
            config.Routes.MapHttpRoute(
            name: "Reader",
            routeTemplate: "api/Reader/{facility}/{area}/{category}/{keyStroke}",
            defaults: new { controller = "Reader", category = RouteParameter.Optional, facility = RouteParameter.Optional, area = RouteParameter.Optional, keyStroke = RouteParameter.Optional }

         );
            config.Routes.MapHttpRoute(
             name: "Report_Activity",
                routeTemplate: "api/Report_Activity/{badgeId}/{personId}/{companyId}/{divisionId}/{empId}/{status}/{areaString}/{stDate}/{stTime}/{endDate}/{endTime}/{days}/{months}/{wcType}/{wcData}",
             defaults: new
             {
                 controller = "Report_Activity",
                 badgeId = RouteParameter.Optional,
                 personId = RouteParameter.Optional,
                 companyId = RouteParameter.Optional,
                 empId = RouteParameter.Optional,
                 status = RouteParameter.Optional,
                 divisionId = RouteParameter.Optional,
                 areaString = RouteParameter.Optional,
                 stDate = RouteParameter.Optional,
                 endDate = RouteParameter.Optional,
                 stTime = RouteParameter.Optional,
                 endTime = RouteParameter.Optional,
                 days = RouteParameter.Optional,
                 months = RouteParameter.Optional,
                 wcType = RouteParameter.Optional,
                 wcData = RouteParameter.Optional
             });

            config.Routes.MapHttpRoute(
            name: "Company",
            routeTemplate: "api/Company/{keyStroke}",
            defaults: new
            {
                controller = "Company",
                keyStroke = RouteParameter.Optional
            });

            config.Routes.MapHttpRoute(
            name: "Division",
            routeTemplate: "api/Division/{company}/{keyStroke}",
            defaults: new { controller = "Division", company = RouteParameter.Optional, keyStroke = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
            name: "Badge",
            routeTemplate: "api/Badge/{company}/{division}/{keyStroke}",
            defaults: new { controller = "Badge", company = RouteParameter.Optional, division = RouteParameter.Optional, keyStroke = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
            name: "Card",
            routeTemplate: "api/Card/{company}/{division}/{keyStroke}",
            defaults: new { controller = "Card", company = RouteParameter.Optional, division = RouteParameter.Optional, keyStroke = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
            name: "EmpId",
            routeTemplate: "api/EmpId/{company}/{division}/{keyStroke}",
            defaults: new { controller = "EmpId", company = RouteParameter.Optional, division = RouteParameter.Optional, keyStroke = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
           name: "Person",
           routeTemplate: "api/Person/{company}/{division}/{keyStroke}",
           defaults: new { controller = "Person", company = RouteParameter.Optional, division = RouteParameter.Optional, keyStroke = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
            name: "Name",
            routeTemplate: "api/Name/{company}/{division}/{keyStroke}",
            defaults: new { controller = "Name", company = RouteParameter.Optional, division = RouteParameter.Optional, keyStroke = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
           name: "WildCardData",
           routeTemplate: "api/WildCardData/{wildCardType}/{companyId}/{divisionID}/{keyStrokes}",
           defaults: new { controller = "WildCardData", wildCardType = RouteParameter.Optional, companyId = RouteParameter.Optional, divisionId = RouteParameter.Optional, keyStrokes = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
           name: "Status",
           routeTemplate: "api/Status/{statusType}/{keyStroke}",
           defaults: new { controller = "Status", statusType = RouteParameter.Optional, keyStroke = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
            name: "Report_Audit",
            routeTemplate: "api/Report_Audit/{badgeId}/{personId}/{companyId}/{divisionId}/{empId}/{categoryId}/{badgeStatus}/{wcType}/{wcData}",
            defaults: new
            {
                controller = "Report_Audit",
                badgeId = RouteParameter.Optional,
                personId = RouteParameter.Optional,
                companyId = RouteParameter.Optional,
                divisionId = RouteParameter.Optional,
                empId = RouteParameter.Optional,
                categoryId = RouteParameter.Optional,
                badgeStatus = RouteParameter.Optional,
                wcType = RouteParameter.Optional,
                wcData = RouteParameter.Optional
            });

            config.Routes.MapHttpRoute(
           name: "Report_DoorCategory",
           routeTemplate: "api/Report_DoorCategory/{areaId}/{categoryId}/{doorId}",
           defaults: new
           {
               controller = "Report_DoorCategory",
               areaId = RouteParameter.Optional,
               doorId = RouteParameter.Optional,
               categoryId = RouteParameter.Optional
           });

            config.Routes.MapHttpRoute(
            name: "Report_TopSoundingAlarm",
            routeTemplate: "api/Report_TopSoundingAlarm/{criteria}/{top}/{stDate}/{stTime}/{endDate}/{endTime}",
            defaults: new
            {
                controller = "Report_TopSoundingAlarm",
                criteria = RouteParameter.Optional,
                top = RouteParameter.Optional,
                stDate = RouteParameter.Optional,
                stTime = RouteParameter.Optional,
                endDate = RouteParameter.Optional,
                endTime = RouteParameter.Optional
            });


            config.Routes.MapHttpRoute(
           name: "Report_BadgeStatus",
           routeTemplate: "api/Report_BadgeStatus/{badgeId}/{personId}/{companyId}/{divisionId}/{empId}/{status}/{stDate}/{endDate}/{days}/{months}/{wcType}/{wcData}",
           defaults: new
           {
               controller = "Report_BadgeStatus",
               badgeId = RouteParameter.Optional,
               personId = RouteParameter.Optional,
               companyId = RouteParameter.Optional,
               empId = RouteParameter.Optional,
               status = RouteParameter.Optional,
               divisionId = RouteParameter.Optional,              
               stDate = RouteParameter.Optional,
               endDate = RouteParameter.Optional,
               days = RouteParameter.Optional,
               months = RouteParameter.Optional,
               wcType = RouteParameter.Optional,
               wcData = RouteParameter.Optional
           });
            
                config.Routes.MapHttpRoute(
           name: "Report_AlarmingDoor",
           routeTemplate: "api/Report_AlarmingDoor/{facilityId}/{alarmDesc}/{inputDesc}/{stDate}/{endDate}/{stTime}/{endTime}/{days}/{months}",
           defaults: new
           {
               controller = "Report_AlarmingDoor",
               facilityId = RouteParameter.Optional,
               alarmDesc = RouteParameter.Optional,
               inputDesc = RouteParameter.Optional,
               stDate = RouteParameter.Optional,
               endDate = RouteParameter.Optional,
               stTime = RouteParameter.Optional,
               endTime = RouteParameter.Optional,
               days = RouteParameter.Optional,
               months = RouteParameter.Optional
           });

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
