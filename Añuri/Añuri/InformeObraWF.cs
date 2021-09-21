using Añuri.Entidades;
using Añuri.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Añuri.Clases_Maestras;
using Sico;
using System.Text.RegularExpressions;

namespace Añuri
{
    public partial class InformeObraWF : Form
    {
        public int idObraSeleccionada;
        public string Obra;
        public string Domicilio;
        public InformeObraWF(int idObraSeleccionada, string Obra, string Domicilio)
        {
            InitializeComponent();
            this.idObraSeleccionada = idObraSeleccionada;
            this.Obra = Obra;
            this.Domicilio = Domicilio;
        }
        private void InformeObraWF_Load(object sender, EventArgs e)
        {
            try
            {
                idObra.Text = Convert.ToString(idObraSeleccionada);
                lblNombreObra.Text = "Informe Movimientos Obra" + " " + Obra;
                ListarMaterialesParaLaObra();
                ////// Grafico en Pesos
                List<Stock> GraficoMaterialesEnPesos = new List<Stock>();
                GraficoMaterialesEnPesos = ObrasNeg.GraficoMaterialesEnPesos(idObraSeleccionada);
                if (GraficoMaterialesEnPesos.Count > 0)
                {
                    DiseñoGraficoMaterialesEnPesos(GraficoMaterialesEnPesos);
                }
                else
                { }
            }
            catch (Exception ex)
            { }
        }
        private void DiseñoGraficoMaterialesEnPesos(List<Stock> GraficoMaterialesEnPesos)
        {
            List<string> Nombre = new List<string>();
            List<string> NombreValor = new List<string>();
            List<string> Total = new List<string>();
            foreach (var item in GraficoMaterialesEnPesos)
            {
                Nombre.Add(item.Descripcion);
                string total = Convert.ToString(item.PrecioNeto);
                string totalFinal = "$" + " " + total;
                Total.Add(totalFinal);
            }
            chartEnPesos.Series[0].Points.DataBindXY(Nombre, Total);
        }
        public static List<Stock> ListaMaterialesStatic;
        private void ListarMaterialesParaLaObra()
        {
            List<Stock> ListaMateriales = ObrasNeg.ListaMaterialesExistentes(idObraSeleccionada);
            if (ListaMateriales.Count > 0)
            {
                Stock ultimo = new Stock();
                ultimo.Descripcion = "TOTALES";
                int Kilos = CalcularTotalKilos(ListaMateriales);
                decimal TotalPrecioNeto = CalculaPrecioNeto(ListaMateriales);
                ultimo.Cantidad = Convert.ToInt32(Kilos);
                ultimo.PrecioNeto = Convert.ToDecimal(TotalPrecioNeto);
                ultimo.ValorUnitario = 0;
                ListaMateriales.Add(ultimo);
                ListaMaterialesStatic = ListaMateriales;
                string fecha = "";
                foreach (var item in ListaMateriales)
                {
                    if (item.FechaFactura == Convert.ToDateTime("1/1/0001 00:00:00"))
                    {
                        fecha = " ";
                    }
                    else
                    {
                        fecha = item.FechaFactura.ToShortDateString();
                    }
                    dgvLista.Rows.Add(item.idProducto, item.idMovimientoEntrada, item.Descripcion, fecha, item.Cantidad, item.ValorUnitario, item.PrecioNeto, 0, 1);
                }
            }
            dgvLista.ReadOnly = true;
        }

        private int CalcularTotalKilos(List<Stock> listaMateriales)
        {
            int totalKilos = 0;
            foreach (var item in listaMateriales)
            {
                totalKilos += item.Cantidad;
            }
            return totalKilos;
        }
        private decimal CalculaPrecioNeto(List<Stock> listaMateriales)
        {
            decimal totalMonto = 0;
            decimal MontoNegativo = 0;
            foreach (var item in listaMateriales)
            {
                totalMonto += item.PrecioNeto;

            }
            decimal valor = decimal.Round(totalMonto - MontoNegativo, 2);
            return valor;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ProgressBar()
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = 100000;
            progressBar1.Step = 1;

            for (int j = 0; j < 100000; j++)
            {
                Caluculate(j);
                progressBar1.PerformStep();
            }
        }
        private void Caluculate(int i)
        {
            double pow = Math.Pow(i, i);
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            ProgressBar();
            dgvLista.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dgvLista.MultiSelect = true;
            dgvLista.SelectAll();
            DataObject dataObj = dgvLista.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
            //Open an excel instance and paste the copied data
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvLista.Rows.Clear();
                DateTime FechaDesde = Convert.ToDateTime(dtFechaDesde.Value.ToShortDateString());
                DateTime FechaHasta = Convert.ToDateTime(dtFechaHasta.Value.ToShortDateString());
                List<Stock> ListaMateriales = ObrasNeg.ListaMaterialesExistentesPorFecha(idObraSeleccionada, FechaDesde, FechaHasta);
                Stock ultimo = new Stock();
                ultimo.Descripcion = "TOTALES";
                int Kilos = CalcularTotalKilos(ListaMateriales);
                decimal TotalPrecioNeto = CalculaPrecioNeto(ListaMateriales);
                ultimo.Cantidad = Convert.ToInt32(Kilos);
                ultimo.PrecioNeto = Convert.ToDecimal(TotalPrecioNeto);
                ultimo.ValorUnitario = 0;
                ListaMateriales.Add(ultimo);
                string fecha = "";
                foreach (var item in ListaMateriales)
                {
                    if (item.FechaFactura == Convert.ToDateTime("1/1/0001 00:00:00"))
                    {
                        fecha = " ";
                    }
                    else
                    {
                        fecha = item.FechaFactura.ToShortDateString();
                    }
                    dgvLista.Rows.Add(item.idProducto, item.idMovimientoEntrada, item.Descripcion, fecha, item.Cantidad, item.ValorUnitario, item.PrecioNeto, 0, 1);
                }
                dgvLista.ReadOnly = true;
            }
            catch (Exception ex)
            { }
        }
        private void btnPdf_Click(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            Document doc = new Document(PageSize.LETTER);

            string folderPath = "C:\\Añuri-Archivos\\PDFs\\Reporte Obra\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string ruta = folderPath;
            //string Periodo = "Reporte de Obra";
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(ruta + Obra + ".pdf", FileMode.Create));
            writer.PageEvent = new PDF();

            doc.AddTitle("PDF");
            doc.AddCreator("jliCode");

            // Abrimos el archivo
            doc.Open();
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font letraContenido = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font UltimoRegistro = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
            iTextSharp.text.Font DomicilioFontMenos30 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font DomicilioFont30a40 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font DomicilioFontHasta40a50 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font DomicilioFontHasta50a60 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            // Escribimos el encabezamiento en el documento
            string TextoInicial = "Informe de obra - " + Obra;
            string DomicilioTexto = "Domicilio:" + Domicilio;
            string replaceWith = "";
            DomicilioTexto = DomicilioTexto.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);

            doc.Add(new Paragraph(" "));
            Paragraph p2 = new Paragraph(new Chunk(DomicilioTexto));
            Paragraph p1 = new Paragraph(new Chunk(TextoInicial));
            p1.Alignment = Element.ALIGN_LEFT;

            if (Domicilio.Length <= 30)
            { p2 = new Paragraph(new Chunk(DomicilioTexto, DomicilioFontMenos30)); }
            if (Domicilio.Length >= 30 && Domicilio.Length <= 40)
            { p2 = new Paragraph(new Chunk(DomicilioTexto, DomicilioFont30a40)); }
            if (Domicilio.Length >= 40 && Domicilio.Length <= 50)
            { p2 = new Paragraph(new Chunk(DomicilioTexto, DomicilioFontHasta40a50)); }
            if (Domicilio.Length >= 50 && Domicilio.Length <= 60)
            { p2 = new Paragraph(new Chunk(DomicilioTexto, DomicilioFontHasta50a60)); }
            p2.Alignment = Element.ALIGN_LEFT;

            doc.Add(new Paragraph(p1));
            doc.Add(new Paragraph(p2));
            doc.Add(new Paragraph(" "));

            //doc.Add(Chunk.NEWLINE);

            // Creamos una tabla que contendrá las cabeceras
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(5);
            tblPrueba.WidthPercentage = 110;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clMaterial = new PdfPCell(new Phrase("Material", _standardFont));
            clMaterial.BorderWidth = 0;
            clMaterial.BorderWidthBottom = 0.50f;
            clMaterial.BorderWidthLeft = 0.50f;
            clMaterial.BorderWidthRight = 0.50f;
            clMaterial.BorderWidthTop = 0.50f;

            PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
            clFecha.BorderWidth = 0;
            clFecha.BorderWidthBottom = 0.50f;
            clFecha.BorderWidthLeft = 0.50f;
            clFecha.BorderWidthRight = 0.50f;
            clFecha.BorderWidthTop = 0.50f;

            PdfPCell clKilos = new PdfPCell(new Phrase("Kilos", _standardFont));
            clKilos.BorderWidth = 0;
            clKilos.BorderWidthBottom = 0.50f;
            clKilos.BorderWidthLeft = 0.50f;
            clKilos.BorderWidthRight = 0.50f;
            clKilos.BorderWidthTop = 0.50f;


            PdfPCell clPrecioUnitario = new PdfPCell(new Phrase("Precio Unitario", _standardFont));
            clPrecioUnitario.BorderWidth = 0;
            clPrecioUnitario.BorderWidthBottom = 0.50f;
            clPrecioUnitario.BorderWidthLeft = 0.50f;
            clPrecioUnitario.BorderWidthRight = 0.50f;
            clPrecioUnitario.BorderWidthTop = 0.50f;

            PdfPCell clPrecioNeto = new PdfPCell(new Phrase("Precio Neto", _standardFont));
            clPrecioNeto.BorderWidth = 0;
            clPrecioNeto.BorderWidthBottom = 0.50f;
            clPrecioNeto.BorderWidthLeft = 0.50f;
            clPrecioNeto.BorderWidthRight = 0.50f;
            clPrecioNeto.BorderWidthTop = 0.50f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clMaterial);
            tblPrueba.AddCell(clFecha);
            tblPrueba.AddCell(clKilos);
            tblPrueba.AddCell(clPrecioUnitario);
            tblPrueba.AddCell(clPrecioNeto);

            // Llenamos la tabla con información
            int TotalDeElementos = ListaMaterialesStatic.Count;
            int Contador = 0;
            foreach (var item in ListaMaterialesStatic)
            {
                Contador = Contador + 1;
                if (item.Descripcion != "")
                {
                    if (TotalDeElementos == Contador)
                    {
                        clMaterial = new PdfPCell(new Phrase(item.Descripcion, UltimoRegistro));
                        clMaterial.BorderWidth = 0;

                        string Fecha = Convert.ToString(item.FechaFactura.ToShortDateString());
                        clFecha = new PdfPCell(new Phrase(Fecha, UltimoRegistro));
                        clFecha.BorderWidth = 0;

                        string Kilos = Convert.ToString(item.Cantidad);
                        clKilos = new PdfPCell(new Phrase(Kilos, UltimoRegistro));
                        clKilos.BorderWidth = 0;

                        string PrecioUnitario = Convert.ToString(item.ValorUnitario);
                        clPrecioUnitario = new PdfPCell(new Phrase(PrecioUnitario, UltimoRegistro));
                        clPrecioUnitario.BorderWidth = 0;

                        string PrecioNeto = Convert.ToString(item.PrecioNeto);
                        clPrecioNeto = new PdfPCell(new Phrase(PrecioNeto, UltimoRegistro));
                        clPrecioNeto.BorderWidth = 0;

                        tblPrueba.AddCell(clMaterial);
                        tblPrueba.AddCell(clFecha);
                        tblPrueba.AddCell(clKilos);
                        tblPrueba.AddCell(clPrecioUnitario);
                        tblPrueba.AddCell(clPrecioNeto);
                    }
                    else
                    {
                        clMaterial = new PdfPCell(new Phrase(item.Descripcion, letraContenido));
                        clMaterial.BorderWidth = 0;

                        string Fecha = Convert.ToString(item.FechaFactura.ToShortDateString());
                        clFecha = new PdfPCell(new Phrase(Fecha, letraContenido));
                        clFecha.BorderWidth = 0;

                        string Kilos = Convert.ToString(item.Cantidad);
                        clKilos = new PdfPCell(new Phrase(Kilos, letraContenido));
                        clKilos.BorderWidth = 0;

                        string PrecioUnitario = Convert.ToString(item.ValorUnitario);
                        clPrecioUnitario = new PdfPCell(new Phrase(PrecioUnitario, letraContenido));
                        clPrecioUnitario.BorderWidth = 0;

                        string PrecioNeto = Convert.ToString(item.PrecioNeto);
                        clPrecioNeto = new PdfPCell(new Phrase(PrecioNeto, letraContenido));
                        clPrecioNeto.BorderWidth = 0;

                        tblPrueba.AddCell(clMaterial);
                        tblPrueba.AddCell(clFecha);
                        tblPrueba.AddCell(clKilos);
                        tblPrueba.AddCell(clPrecioUnitario);
                        tblPrueba.AddCell(clPrecioNeto);
                    }
                }
            }
            doc.Add(tblPrueba);
            doc.Close();
            writer.Close();
            string mensaje = "Se generó el PDF exitosamente en la carpeta" + " " + folderPath;
            string message2 = mensaje;
            const string caption2 = "Éxito";
            var result2 = MessageBox.Show(message2, caption2,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Asterisk);
        }
    }
}
