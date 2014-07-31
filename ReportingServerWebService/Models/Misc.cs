using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiscingServerWebService.Models
{
    public class Misc
    {
        public string key;
        public string value;

        public static List<Misc> getMiscOptions()
        {
            List<Misc> lstMisc = new List<Misc>();
            for (int i = 1; i <= 3; i++)
            {
                Misc MiscObj = new Misc();

                if (i == 1)
                {
                    MiscObj.key = "input_desc";
                    MiscObj.value = "Input Description";
                }
                else if (i == 2)
                {
                    MiscObj.key = "alarm_desc";
                    MiscObj.value = "Alarm Description";
                }
                else if (i == 3)
                {
                    MiscObj.key = "aCriteria_facility";
                    MiscObj.value = "Facility";
                }
              
                lstMisc.Add(MiscObj);
            }
            return lstMisc;
        }
    }
}

 