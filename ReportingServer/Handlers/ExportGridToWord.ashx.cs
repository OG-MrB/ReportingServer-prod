using ExportToPDF;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace Export
{
    /// <summary>
    /// Summary description for ExportGridToExcel
    /// </summary>
    public class ExportGridToWord : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string tabData = context.Request["excelData"]; 

            DataTable dt = ConvertCsvData(tabData);
            if (dt == null)
            {
                //  Add some error-catching here...
                return;
            }
            
             //MS Word Code
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=DataTable.doc");
            System.Web.HttpContext.Current.Response.Charset = "";
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            System.Web.HttpContext.Current.Response.Output.Write(sw.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();


            //End of Word Code


            // ************************************************ //

/*
            
            string excelFilename = context.Request["filename"];  

            if (File.Exists(excelFilename))
                File.Delete(excelFilename);

            CreateExcelFile.CreateExcelDocument(dt, excelFilename, context.Response);*/
        }

        private DataTable ConvertCsvData(string CSVdata)
        {
            //  Convert a tab-separated set of data into a DataTable, ready for our C# CreateExcelFile libraries
            //  to turn into an Excel file.
            //
            DataTable dt = new DataTable();
            try
            {
                System.Diagnostics.Trace.WriteLine(CSVdata);

                string[] Lines = CSVdata.Split(new char[] { '\r', '\n' });
                if (Lines == null)
                    return dt;
                if (Lines.GetLength(0) == 0)
                    return dt;

                string[] HeaderText = Lines[0].Split('\t');

                int numOfColumns = HeaderText.Count();

                
                foreach (string header in HeaderText)
                    dt.Columns.Add(header, typeof(string));

                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    string[] Fields = Lines[i].Split('\t');
                    if (Fields.GetLength(0) == numOfColumns)
                    {
                        Row = dt.NewRow();
                        for (int f = 0; f < numOfColumns; f++)
                            Row[f] = Fields[f];
                        dt.Rows.Add(Row);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("An exception occurred: " + ex.Message);
                return null;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}