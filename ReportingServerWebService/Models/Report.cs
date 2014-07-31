using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportingServerWebService.Models
{

    public class Report
    {
        public string key;
        public string value;

        public static List<Report> getReportOptions()
        {
            List<Report> lstReport = new List<Report>();
            for (int i = 1; i <= 6; i++)
            {
                Report reportObj = new Report();

                if (i == 1)
                {
                    reportObj.key = "Report_Activity";
                    reportObj.value = "Activity";
                }
                else if (i == 2)
                {
                    reportObj.key = "Report_Audit";
                    reportObj.value = "Audit";
                }
                else if (i == 3)
                {
                    reportObj.key = "Report_AlarmStatus";
                    reportObj.value = "Alarm Status";
                }
                else if (i == 4)
                {
                    reportObj.key = "Report_BadgeStatus";
                    reportObj.value = "Badge Status";
                }
                else if (i == 5)
                {
                    reportObj.key = "Report_DoorCategory";
                    reportObj.value = "Door Category";
                }
                else if (i == 6)
                {
                    reportObj.key = "Report_TopSoundingAlarm";
                    reportObj.value = "Top Sounding Alarms";
                }

                lstReport.Add(reportObj);
            }
            return lstReport;
        }
    }
}
