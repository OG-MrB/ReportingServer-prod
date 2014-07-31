using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ReportingServerWebService.Controllers
{
    public class ConnectionManager
    {
               SqlConnection connection;

               public static SqlConnection getConnection()
               {
                   SqlConnection connection = new SqlConnection();
                   connection.ConnectionString = ConfigurationManager.ConnectionStrings["PP_LAX"].ConnectionString;
                   //connection.ConnectionString = "Server=BIRDI-SRV01\\BIRDI2008;Database=PP_LAX;User Id=APAC;Password=Password1";
                   return connection;
               }

               
    }
}