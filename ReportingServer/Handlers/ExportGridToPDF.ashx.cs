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
    public class ExportGridToPDF : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string tabData = context.Request["excelData"]; 

            DataTable dt = ConvertCsvData(tabData);
            DataTable cr = ConvertCsvData(context.Request["criteriaArray"]);
            if (dt == null)
            {
                //  Add some error-catching here...
                return;
            }
           



            //PDF Code
            int xy;
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();



        




            string title = context.Request["filename"];  
           
            System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=" + title + ".pdf");
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 5f, 5f, 5f, 10f);
           
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

            PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("http://10.65.216.71:5666/resources/report_images/header_logo.PNG");
           

            Paragraph para = new Paragraph(new Phrase(title + "\n", new iTextSharp.text.Font(iTextSharp.text.Font.BOLD, 16f, iTextSharp.text.Font.COURIER, iTextSharp.text.Color.DARK_GRAY)));
            para.Alignment = Element.ALIGN_CENTER;

            Paragraph criteria = new Paragraph(context.Request["criteria"] + "\n\n", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10f, iTextSharp.text.Font.TIMES_ROMAN, iTextSharp.text.Color.GRAY));
            criteria.Alignment = Element.ALIGN_CENTER;
      
            
            int x = GridView1.Columns.Count;
            PdfPTable table = new PdfPTable(dt.Columns.Count);
            table.WidthPercentage = 100;
            

            /*PdfPCell cell = new PdfPCell(new Phrase("<Title of the Report>", new iTextSharp.text.Font(iTextSharp.text.Font.BOLD, 16f, iTextSharp.text.Font.COURIER, iTextSharp.text.Color.WHITE)));
            cell.BackgroundColor = new iTextSharp.text.Color(55, 178, 255);
            cell.BorderColor = new Color(192, 192, 192);
            cell.Border = Rectangle.BOTTOM_BORDER;

            cell.Colspan = dt.Columns.Count;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right*/
           // table.AddCell(cell);
            PdfPCell cell_data;
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                cell_data = new PdfPCell(new Phrase(GridView1.HeaderRow.Cells[j].Text, new iTextSharp.text.Font(iTextSharp.text.Font.BOLD, 14f, iTextSharp.text.Font.COURIER, iTextSharp.text.Color.BLACK)));
                cell_data.BackgroundColor = new iTextSharp.text.Color(102, 178, 255);
                cell_data.PaddingLeft = cell_data.PaddingRight = 5f;
                cell_data.PaddingTop = cell_data.PaddingBottom = 7f;
                cell_data.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                cell_data.BorderColor = new Color(192, 192, 192);
                cell_data.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell_data);
            }

           // table.HeaderRows = 0; //Flag the first row as a header
         
            for (int i = 0; i < GridView1.Rows.Count; i++ )
            {
                for(int k = 0; k < dt.Columns.Count; k++)
                {
                    if(i%2 == 0)
                    {
                        if (GridView1.Rows[i].Cells[k].Text != null)
                        {

                            cell_data = new PdfPCell(new Phrase(GridView1.Rows[i].Cells[k].Text, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.HELVETICA, iTextSharp.text.Color.BLACK)));
                            cell_data.BackgroundColor = new iTextSharp.text.Color(230, 230, 230);
                            cell_data.PaddingLeft = cell_data.PaddingRight = 3f;
                            cell_data.PaddingTop = cell_data.PaddingBottom = 5f;
                            cell_data.BorderColor = new Color(192,192,192);
                            cell_data.Border = Rectangle.BOTTOM_BORDER;
                            cell_data.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            table.AddCell(cell_data);
                        }
                    }
                    else
                    {
                        if (GridView1.Rows[i].Cells[k].Text != null)
                        {

                            cell_data = new PdfPCell(new Phrase(GridView1.Rows[i].Cells[k].Text, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 9f, iTextSharp.text.Font.HELVETICA, iTextSharp.text.Color.BLACK)));
                            cell_data.BackgroundColor = new iTextSharp.text.Color(255,255,255);
                            cell_data.PaddingLeft = cell_data.PaddingRight = 1f;
                            cell_data.PaddingTop = cell_data.PaddingBottom = 5f;
                            cell_data.BorderColor = new Color(192, 192, 192);
                            cell_data.Border = Rectangle.BOTTOM_BORDER;
                            cell_data.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            table.AddCell(cell_data);
                        }
                    }
                    
                }
            }

                pdfDoc.Open();
            //pdfDoc.Add(logo);
            pdfDoc.Add(para);
            pdfDoc.Add(criteria);
            pdfDoc.Add(table);
            //htmlparser.Parse(sr);
            pdfDoc.Close();
            System.Web.HttpContext.Current.Response.Write(pdfDoc);
            System.Web.HttpContext.Current.Response.End(); 

            //End of PDF Code


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