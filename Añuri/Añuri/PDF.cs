using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sico
{
    public class PDF : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            base.OnEndPage(writer, doc);
            iTextSharp.text.Font _miniFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.HELVETICA, 6,
                    iTextSharp.text.Font.NORMAL,
                    BaseColor.BLACK);
            iTextSharp.text.Font _microFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.HELVETICA, 5,
                    iTextSharp.text.Font.NORMAL,
                    BaseColor.BLACK);
            iTextSharp.text.Rectangle page = doc.PageSize;
           

            Font _standardFont = new Font(Font.FontFamily.TIMES_ROMAN, 8, Font.BOLD, BaseColor.BLACK);
            Font _standardFontFooter = new Font(Font.FontFamily.TIMES_ROMAN, 7, Font.BOLD, BaseColor.BLACK);
            PdfPCell _cell;

            try
            {
               
                #region test
               
                //Image img_LogoMty = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["LOGO_MTY_DOC"].ToString()));
                string pathImagen = @"Añuri_Imagen_PDF.png";
                Image img_LogoMty = Image.GetInstance(pathImagen);
                img_LogoMty.BorderWidth = 5;
                img_LogoMty.Alignment = Image.TEXTWRAP | Element.ALIGN_CENTER;
                float percentage = 0.0f;
                percentage = 85 / img_LogoMty.Width;
                //img_LogoMty.SpacingBefore = 15f;
                //img_LogoMty.IndentationLeft = 9f;
                img_LogoMty.ScalePercent(percentage * 100);

                #endregion
                #region HEADER
                ///// Armamos el rectangulo del encabezado.
                PdfPTable tbHeader = new PdfPTable(1);
                tbHeader.TotalWidth = page.Width - doc.LeftMargin - doc.RightMargin;
                tbHeader.DefaultCell.Border = 0;
                tbHeader.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                tbHeader.DefaultCell.BorderWidthRight = 1;
                tbHeader.DefaultCell.PaddingLeft = 2f;
              
                //tbHeader.DefaultCell.Height = 300f;

                ///// Contenido del rectangulo del encabezado.
                _cell = new PdfPCell(img_LogoMty);
                _cell.HorizontalAlignment = Element.ALIGN_LEFT;
                _cell.PaddingTop = Element.ALIGN_CENTER;
                _cell.PaddingLeft = 5f;              
                _cell.MinimumHeight = 42f;             

                //var para1 = new Paragraph("Añuri: Sistema de Stock");
                //para1.SetLeading(3f, 1f);              
                //_cell.AddElement(para1);

                _cell.BorderWidthBottom = 1f;
                _cell.BorderWidthLeft = 1f;
                _cell.BorderWidthTop = 1f;
                _cell.BorderWidthRight = 1f;
                tbHeader.AddCell(_cell);

                //////  2 columnas
                //_cell = new PdfPCell(new Paragraph("############ \n ESTADO ANALITICO DEL EJERCICIO DEL PRESUPUESTO DE EGRESOS \n CLASIFICACION ADMINISTRATIVA \n DEL " +  " AL " +  "."));
                //_cell = new PdfPCell(new Paragraph("Añuri: Sistema de Stock"));
                //_cell.HorizontalAlignment = Element.ALIGN_LEFT;                
                //_cell.Border = 0;
                //tbHeader.AddCell(_cell);

                //_cell = new PdfPCell(new Paragraph(DateTime.Now.ToString("dd/MM/yyyy"), _standardFont));
                //_cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //_cell.Border = 0;
                //tbHeader.AddCell(_cell);

                tbHeader.WriteSelectedRows(0, -1, doc.LeftMargin, writer.PageSize.GetTop(doc.TopMargin) + 30, writer.DirectContent);
                #endregion

                #region FOOTER
                //Footer
                PdfPTable footer = new PdfPTable(2);
                footer.TotalWidth = page.Width - doc.LeftMargin - doc.RightMargin;
                PdfPCell f1 = new PdfPCell(new Phrase("Generado el: " +
                        string.Format("{0:MMM dd, yyyy hh:mm tt}", DateTime.Now),
                        _miniFont));
                PdfPCell f2 = new PdfPCell(new Phrase("Creado por jliCodeSoftware@gmail.com",
                       _microFont));
                f1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                f1.VerticalAlignment = Element.ALIGN_TOP;
                f1.HorizontalAlignment = Element.ALIGN_LEFT;
                f2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                f2.VerticalAlignment = Element.ALIGN_TOP;
                f2.HorizontalAlignment = Element.ALIGN_RIGHT;
                footer.AddCell(f1);
                footer.AddCell(f2);
                footer.WriteSelectedRows(
                  0, -1,
                  doc.LeftMargin,
                  doc.BottomMargin - 10,
                  writer.DirectContent
                );
                #endregion
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public override void OnCloseDocument(PdfWriter writer, Document doc)
        {
            base.OnCloseDocument(writer, doc);
        }
    }
}
