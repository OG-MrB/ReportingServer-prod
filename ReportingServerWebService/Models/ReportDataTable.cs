using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ReportingServerWebService.Models
{
   
    public class ReportDataTable
    {
  
        String total;

        String page;
   
        String records;
  
        List<ReportRow> row;

        public String Total { get { return this.total; } set { this.total = value; } }
        public String Page { get { return this.page; } set { this.page = value; } }
        public String Records { get { return this.records; } set { this.records = value; } }
        public List<ReportRow> repdata { get { return this.row; } set { this.row = value; } }



    }
}