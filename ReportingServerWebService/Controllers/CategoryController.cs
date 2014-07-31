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
    public class CategoryController : System.Web.Http.ApiController
    {
      /*  public String Get()
        {
            //Get all Category
            Dictionary<String, String> listCategory = Category.getAllCategories();
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listCategory);
            return json;
        }

        public String GetCategoryById(String facilityId,String areaID)
        {
            //Get all Category by area
            Dictionary<String, String> listCategory = Category.getCategoriesByArea(facilityId, areaID);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listCategory);
            return json;
        }

        public String GetCategoryById(String facilityId, String areaID,String keyStroke)
        {
            //Get all Category by area
            Dictionary<String, String> listCategory = Category.getCategoriesByArea(facilityId, areaID,keyStroke);
            var jsonSerialiser = new JavaScriptSerializer();
            jsonSerialiser.MaxJsonLength = int.MaxValue;
            var json = jsonSerialiser.Serialize(listCategory);
            return json;
        } */

        public HttpResponseMessage Get()
        {
            //Get all Category
            //Get all area
            List<Category> lstCategory = new List<Category>();
            lstCategory = Category.getAllCategoriesList();
            var jsonNew = new
            {
                result = lstCategory
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }

        public HttpResponseMessage GetCategoryById(String facilityId, String areaId)
        {
            //Get all Category by Category
            List<Category> lstCategory = new List<Category>();
            lstCategory = Category.getCategoriesByAreaList(facilityId, areaId);
            var jsonNew = new
            {
                result = lstCategory
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }

        public HttpResponseMessage GetCategoryById(String facilityId, String areaId, String keyStroke)
        {
            //Get all Category by Category
            List<Category> lstCategory = new List<Category>();
            lstCategory = Category.getCategoriesByAreaList(facilityId, areaId, keyStroke);
            var jsonNew = new
            {
                result = lstCategory
            };
            return Request.CreateResponse(HttpStatusCode.OK, jsonNew);
        }
    }
}
