using ExportToPDF;
using System;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Export
{
    public class pdfPage : iTextSharp.text.pdf.PdfPageEventHelper
    {
       /* PdfTemplate footerTemplate;
        PdfContentByte cb;
        BaseFont bf;*/
        protected Font footer
        {
            get
            {
                // create a basecolor to use for the footer font, if needed.

                Font font = FontFactory.GetFont("Arial", 7, Font.NORMAL, iTextSharp.text.Color.BLACK);
                return font;
            }
        }



       /*  public override void OnOpenDocument(PdfWriter writer, Document doc)
        {
            cb = writer.DirectContent;
            footerTemplate = cb.CreateTemplate(50, 50);
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb = writer.DirectContent;
        } */


        //override the OnStartPage event handler to add our header
        public override void OnStartPage(PdfWriter writer, Document doc)
        {
              PdfPTable headerTbl = new PdfPTable(1);
            headerTbl.TotalWidth = doc.PageSize.Width;
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("http://10.65.216.71:5666/resources/report_images/header_logo.PNG");
            PdfPCell cell = new PdfPCell(logo);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;

            headerTbl.AddCell(cell);



            headerTbl.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height - 10), writer.DirectContent);
        }

        //override the OnPageEnd event handler to add our footer
        public override void OnEndPage(PdfWriter writer, Document doc)
        {






            //I use a PdfPtable with 2 columns to position my footer where I want it
            PdfPTable footerTbl = new PdfPTable(3);

            //set the width of the table to be the same as the document
            footerTbl.TotalWidth = doc.PageSize.Width;

            //Center the table on the page
            footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

            //Create a paragraph that contains the footer text
            Paragraph SSI = new Paragraph("SSI - HIGHLY RESTRICTED INFORMATION", footer);




            PdfPCell cell = new PdfPCell(SSI);
            cell.Border = 0;
            //cell.PaddingLeft = 10;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingLeft = 25f;
            footerTbl.AddCell(cell);


            String text = "Page " + writer.PageNumber;
            Paragraph pageN = new Paragraph(text, footer);
            
            
            cell = new PdfPCell(pageN);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            footerTbl.AddCell(cell);

           /*
            //Add paging to footer
            {
                
                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.ShowText(text);
                cb.SetTextMatrix(doc.PageSize.GetRight(180), doc.PageSize.GetBottom(30));
                cb.EndText();
                float len = bf.GetWidthPoint(text, 8);
                cb.AddTemplate(footerTemplate, doc.PageSize.GetRight(180) + len, doc.PageSize.GetBottom(30));
            }
            */


            DateTime now = DateTime.Now;

            Paragraph date_time = new Paragraph("Run at : " + now, footer);
            cell = new PdfPCell(date_time);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            cell.PaddingRight = 25f;
            footerTbl.AddCell(cell);

            //write the rows out to the PDF output stream.
            footerTbl.WriteSelectedRows(0, -1, 0, (doc.BottomMargin - 5), writer.DirectContent);

          /*  cb.MoveTo(40, doc.PageSize.GetBottom(50)); */


        }


      /*  public void onCloseDocument(PdfWriter writer, Document doc)
   {
       base.OnCloseDocument(writer, doc);
       footerTemplate.BeginText();
       footerTemplate.SetTextMatrix(0, 0);

       footerTemplate.SetFontAndSize(bf, 8);
       footerTemplate.ShowText((writer.PageNumber - 1).ToString());
       footerTemplate.EndText();



   }*/

    }

}