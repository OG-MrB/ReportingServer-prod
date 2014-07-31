using ReportingServerWebService.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ReportingServerWebService.Models
{
    public class WildCard
    {
        public string key;
        public string value;

        public enum wildCardType
        {
            Employee_Id = 1,
            SSN,
            Zip_code,
            Work_Location
        };

        public static List<WildCard> getWCOptions()
        {
            List<WildCard> lstWildCard = new List<WildCard>();
            for (int i = 1; i <= 5; i++)
            {
                WildCard wc = new WildCard();
                wc.key = i.ToString();
                if (i == 1)
                    wc.value = "Employee Id";
                else if (i == 2) continue;
                //wc.value = "SSN";
                else if (i == 3)
                    wc.value = "Zip code";
                else if (i == 4)
                    wc.value = "City";
                else if (i == 5)
                    wc.value = "Person Id";
                lstWildCard.Add(wc);
            }
            return lstWildCard;
        }

        public static Dictionary<String, String> getAllEntiresByCompany(int type, String companyId, String divisionId)
        {
            if (type == 1)
            {        
                    return EmpId.getEmpByCompany(companyId, divisionId);
            }
            else if (type == 2)
            {
                     return SSN.getSSNByCompany(companyId, divisionId);
            }
            else if (type == 3)
            {
                return ZipCode.getZipByCompany(companyId, divisionId);
            }
            else if (type == 4)
            {
                return Address.getCityByCompany(companyId, divisionId);
            }
           
            return new Dictionary<string, string>();


        }

        public static Dictionary<String, String> getAllEntires(int type, String companyId, String divisionId, String keyStroke)
        {
            if(type == 1)
            {
                return EmpId.getEmpByCompany(companyId, divisionId, keyStroke);
            }  
            else if(type == 2)
            {
                return SSN.getSSNByCompany(companyId, divisionId, keyStroke);
            }
            else if (type == 3)
            {
                return ZipCode.getZipByCompany(companyId, divisionId, keyStroke);
            }
            else if (type == 4)
            {
                return Address.getCityByCompany(companyId,divisionId,keyStroke);
            }
           
                return new Dictionary<string, string>();
            
            
        }

        public static Dictionary<String, String> getAllEntires(int type)
        {
            if (type == 1)
            {
                return EmpId.getAllEmpId();
            }
            else if (type == 2)
            {
                return SSN.getAllSSN();
            }
            else if (type == 3)
            {
                return ZipCode.getAllZipCode();
            }
            else if (type == 4)
            {
                return  Address.getAllCities();
            }

            return new Dictionary<string, string>();


        }

        public static IEnumerable<object> getAllEntiresList(int type)
        {
           
            if (type == 1)
            {
                return EmpId.getAllEmpIdList().Cast<object>();
            }
            else if (type == 2)
            {
                return SSN.getAllSSNList().Cast<object>();
            }
            else if (type == 3)
            {
                return ZipCode.getAllZipCodeList().Cast<object>();
            }
            else if (type == 4)
            {
                return Address.getAllCitiesList().Cast<object>();
            }
            else if (type == 5)
            {
                return Person.getAllPersonList().Cast<object>();
            }
            return new List<object>();


        }

        public static IEnumerable<object> getAllEntiresByCompanyList(int type, String companyId, String divisionId)
        {
            if (type == 1)
            {
                return EmpId.getEmpByCompanyList(companyId, divisionId).Cast<object>(); 
            }
            else if (type == 2)
            {
                return SSN.getSSNByCompanyList(companyId, divisionId).Cast<object>();
            }
            else if (type == 3)
            {
                return ZipCode.getZipByCompanyList(companyId, divisionId).Cast<object>();
            }
            else if (type == 4)
            {
                return Address.getCityByCompanyList(companyId, divisionId).Cast<object>();
            }
            else if (type == 5)
            {
                return Person.getEmpByCompanyList(companyId, divisionId).Cast<object>();
            }
            return new List<object>();


        }

        public static IEnumerable<object> getAllEntiresList(int type, String companyId, String divisionId, String keyStroke)
        {
            if (type == 1)
            {
                return EmpId.getEmpByCompanyList(companyId, divisionId, keyStroke).Cast<object>(); ;
            }
            else if (type == 2)
            {
                return SSN.getSSNByCompanyList(companyId, divisionId, keyStroke).Cast<object>(); ;
            }
            else if (type == 3)
            {
                return ZipCode.getZipByCompanyList(companyId, divisionId, keyStroke).Cast<object>(); ;
            }
            else if (type == 4)
            {
                return Address.getCityByCompanyList(companyId, divisionId, keyStroke).Cast<object>(); ;
            }
            else if (type == 5)
            {
                return Person.getEmpByCompanyList(companyId, divisionId,keyStroke).Cast<object>();
            }
            return new List<object>();


        }

    }
}