using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ReportingServerWebService.Models
{
    public static class Utilities
    {
        public static Dictionary<String, String> sortData(Dictionary<String, String> data)
        {
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Entry Point", Logger.logLevel.Info); 
            List<KeyValuePair<string, string>> myList = data.ToList();

            myList.Sort((firstPair, nextPair) =>
            {
                return firstPair.Value.CompareTo(nextPair.Value);
            }
            );
            
            var dictionary = myList.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            Logger.LogDebug(MethodBase.GetCurrentMethod().DeclaringType.ToString(), MethodBase.GetCurrentMethod().Name, "Exit Point", Logger.logLevel.Info);
            return dictionary;

        }


        public static string getIdentityName(string key)
        {
            if (key.Equals("1")) //Name
            {
                return " AND P.employee = ";
            }
            else if (key.Equals("2")) //Badge
            {
                return " AND B.bid = ";
            }
            else if (key.Equals("3")) // zipcode
            {
                return " AND P.address5 = ";
            }
            else if (key.Equals("4")) // Employee
            {
                return " AND P.employee = ";
            }
            else if (key.Equals("5")) // person id
            {
                return " AND P.id = ";
            }
             else  // city
            {
                return " AND P.address3 = ";
            }
           
        }
    }
}