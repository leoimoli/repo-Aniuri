using Añuri.Entidades;
using Añuri.Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Sico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Añuri
{
    public partial class InformeObraMensualWF : Form
    {
        public InformeObraMensualWF()
        {
            InitializeComponent();
        }

        private void InformeObraMensualWF_Load(object sender, EventArgs e)
        {
            try
            {
                cmbMes.Text = "Seleccione";
                int anio = DateTime.Now.Year;
                txtAño.Text = Convert.ToString(anio);
            }
            catch (Exception ex)
            { }
        }
        private int ValidarMes(string Mes)
        {
            int mes = 0;
            if (Mes == "ENERO")
            {
                mes = 1;
            }
            if (Mes == "FEBRERO")
            {
                mes = 2;
            }
            if (Mes == "MARZO")
            {
                mes = 3;
            }
            if (Mes == "ABRIL")
            {
                mes = 4;
            }
            if (Mes == "MAYO")
            {
                mes = 5;
            }
            if (Mes == "JUNIO")
            {
                mes = 6;
            }
            if (Mes == "JULIO")
            {
                mes = 7;
            }
            if (Mes == "AGOSTO")
            {
                mes = 8;
            }
            if (Mes == "SEPTIEMBRE")
            {
                mes = 9;
            }
            if (Mes == "OCTUBRE")
            {
                mes = 10;
            }
            if (Mes == "NOVIEMBRE")
            {
                mes = 11;
            }
            if (Mes == "DICIEMBRE")
            {
                mes = 12;
            }
            return mes;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        public static List<Stock> ListaDeObrasStatic;
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Stock> ListaAux = new List<Stock>();
                decimal TotalPrecioNeto = 0;
                int TotalKilos = 0;
                //int anio = DateTime.Now.Year;
                //txtAño.Text = Convert.ToString(anio);
                int anio = Convert.ToInt32(txtAño.Text);
                int mes = ValidarMes(cmbMes.Text);
                string fechaString = Convert.ToString("01" + "/" + mes + "/" + anio);
                DateTime FechaDesde = Convert.ToDateTime(fechaString);
                DateTime FechaHasta = FechaDesde.AddMonths(1).AddDays(-1);
                if (cmbMes.Text == "SELECCIONE" || txtAño.Text == "")
                {
                    const string message2 = "Atención: Debe indicar un año y mes para filtrar la busqueda.";
                    const string caption2 = "Atención";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
                dgvLista.Rows.Clear();
                List<Stock> ListaTipoMedicion = new List<Stock>();

                ///// Inicio Consulta para Grupo Perfileria
                string Titulo = "PERFILERIA";
                List<Stock> ListaDeObras = ObrasNeg.BuscarObrasPorMesPerfileria(FechaDesde, FechaHasta, 1);
                List<ObraReporte> ListaDeObra = GrillaPerfileria(ListaDeObras);
                int posicionSiguiente = DiseñoGrillaPerfileria(ListaDeObra, Titulo, 0);

                ///// Duplica Consulta para Grupo Chapas
                Titulo = "CHAPAS";
                List<Stock> ListaDeObrasChapas = ObrasNeg.BuscarObrasPorMesPerfileria(FechaDesde, FechaHasta, 2);
                List<ObraReporte> ListaDeObraChapas = GrillaPerfileria(ListaDeObrasChapas);
                DiseñoGrillaPerfileria(ListaDeObraChapas, Titulo, posicionSiguiente);

                List<Stock> ListaGraficoFinal = ListaDeObras.Concat(ListaDeObrasChapas).OrderBy(x => x.idObra).ToList();

                ListaDeObrasStatic = ListaGraficoFinal;
                DiseñoGraficoMaterialesEnPesos(ListaDeObrasStatic);
            }
            catch (Exception ex)
            { }

        }

        private int DiseñoGrillaPerfileria(List<ObraReporte> listaDeObra, string Titulo, int posicion)
        {
            int posicionSiguiente = 0;
            dgvLista.Rows.Add("", Titulo, "", "", "", "", "", "", "", "");
            posicionSiguiente++;
            dgvLista.Rows[posicion].Cells[1].Style.ForeColor = Color.Red;

            foreach (var item in listaDeObra)
            {
                string nombreObra = item.NombreObra;
                int cantidadKilos = 0;
                decimal montoKilos = 0;

                int cantidadUnidad = 0;
                decimal montoUnidad = 0;

                foreach (var Kilo in item.ListaKilos)
                {
                    cantidadKilos += Kilo.Cantidad;
                    montoKilos += Kilo.PrecioNeto;

                    //Agrego Punto De Miles...
                    string PrecioNeto = Kilo.PrecioNeto.ToString("N", new CultureInfo("es-CL"));
                  
                    dgvLista.Rows.Add(item.idObra, nombreObra, "", Kilo.Descripcion, DateTime.Now, Kilo.Cantidad, 0, PrecioNeto, 0, 1);
                    posicionSiguiente++;
                    nombreObra = string.Empty;
                }
                if (item.ListaKilos.Count > 0)
                {
                    //Agrego Punto De Miles...
                    string montoKilosString = montoKilos.ToString("N", new CultureInfo("es-CL"));
                    dgvLista.Rows.Add(item.idObra, nombreObra, "", "", DateTime.Now, cantidadKilos, 0, montoKilosString, 0, 1);
                    posicionSiguiente++;
                }

                foreach (var Unidad in item.ListaUnidad)
                {
                    cantidadUnidad += Unidad.Cantidad;
                    montoUnidad += Unidad.PrecioNeto;
                    //Agrego Punto De Miles...
                    string PrecioNetoString= Unidad.PrecioNeto.ToString("N", new CultureInfo("es-CL"));
                    dgvLista.Rows.Add(item.idObra, nombreObra, "", Unidad.Descripcion, DateTime.Now, Unidad.Cantidad, 0, PrecioNetoString, 0, 1);
                    posicionSiguiente++;
                    nombreObra = string.Empty;
                }
                if (item.ListaUnidad.Count > 0)
                {
                    //Agrego Punto De Miles...
                    string montoUnidadString = montoUnidad.ToString("N", new CultureInfo("es-CL"));
                    dgvLista.Rows.Add(item.idObra, nombreObra, "", "", DateTime.Now, cantidadUnidad, 0, montoUnidadString, 0, 1);
                    posicionSiguiente++;
                }
                dgvLista.Rows.Add("", "", "", "", "", "", "", "", "", "");
                posicionSiguiente++;
            }
            return posicionSiguiente;
        }
        private List<ObraReporte> GrillaPerfileria(List<Stock> listaDeObras)
        {
            List<ObraReporte> ListaObrasReportes = new List<ObraReporte>();
            if (listaDeObras.Count > 0)
            {
                btnExcel.Visible = true;
                btnPdf.Visible = true;
                ObraReporte _ObraReporte = new ObraReporte();
                foreach (var item in listaDeObras)
                {
                    bool Existe = ListaObrasReportes.Any(x => x.idObra == item.idObra);
                    if (Existe == false)
                    {
                        _ObraReporte = new ObraReporte();
                        _ObraReporte.idObra = item.idObra;
                        _ObraReporte.NombreObra = item.NombreObra;
                        _ObraReporte.ListaKilos = new List<KiloReporte>();
                        _ObraReporte.ListaUnidad = new List<UnidadReporte>();
                    }
                    if (item.TipoMedicion == "KILOS")
                    {
                        KiloReporte _KiloReporte = new KiloReporte();
                        _KiloReporte.idProducto = item.idProducto;
                        _KiloReporte.Descripcion = item.Descripcion;
                        _KiloReporte.Cantidad = item.Cantidad;
                        _KiloReporte.PrecioNeto = item.PrecioNeto;
                        //Agrego Punto De Miles...
                        string prueba = _KiloReporte.PrecioNeto.ToString("N", new CultureInfo("es-CL"));
                        _ObraReporte.ListaKilos.Add(_KiloReporte);
                    }
                    if (item.TipoMedicion == "UNIDAD")
                    {
                        UnidadReporte _UnidadReporte = new UnidadReporte();
                        _UnidadReporte.idProducto = item.idProducto;
                        _UnidadReporte.Descripcion = item.Descripcion;
                        _UnidadReporte.Cantidad = item.Cantidad;
                        _UnidadReporte.PrecioNeto = item.PrecioNeto;
                        //Agrego Punto De Miles...
                        _UnidadReporte.PrecioNeto = Convert.ToDecimal(_UnidadReporte.PrecioNeto.ToString("N", new CultureInfo("es-CL")));

                        _ObraReporte.ListaUnidad.Add(_UnidadReporte);
                    }
                    if (Existe == false)
                    {
                        ListaObrasReportes.Add(_ObraReporte);
                    }
                }
            }
            return ListaObrasReportes;
        }
        private void DiseñoGraficoMaterialesEnPesos(List<Stock> listaDeObrasStatic)
        {
            List<int> ListaIdObra = new List<int>();
            //ListaIdObra.Add(0);
            List<string> Nombre = new List<string>();
            List<string> NombreValor = new List<string>();
            List<string> Total = new List<string>();
            decimal Monto = 0;
            string NombreObra = "";
            int ContadorElementos = 0;
            int CountDeLista = listaDeObrasStatic.Count;
            int ListaIdObraContadora = 0;
            foreach (var item in listaDeObrasStatic)
            {
                if (item.idObra == 0 && item.idProducto == 0)
                {
                    continue;
                }
                else
                {
                    bool Existe = ListaIdObra.Any(x => x == item.idObra);
                    if (Existe == false && item.idObra != 0)
                    {
                        if (ContadorElementos > 0)
                        {
                            Nombre.Add(NombreObra);
                            string total = Convert.ToString(Monto);
                            string totalFinal = "$" + " " + total;
                            Total.Add(totalFinal);
                            Monto = 0;
                            NombreObra = "";
                            Monto = Monto + item.PrecioNeto;
                            NombreObra = item.NombreObra;
                            ListaIdObra.Add(item.idObra);
                            ContadorElementos = ContadorElementos + 1;
                            if (ContadorElementos == listaDeObrasStatic.Count)
                            {
                                Nombre.Add(NombreObra);
                                total = Convert.ToString(Monto);
                                totalFinal = "$" + " " + total;
                                Total.Add(totalFinal);
                            }
                        }
                        else
                        {
                            Monto = 0;
                            NombreObra = "";
                            Monto = Monto + item.PrecioNeto;
                            NombreObra = item.NombreObra;
                            ListaIdObra.Add(item.idObra);
                            ContadorElementos = ContadorElementos + 1;
                        }
                    }
                    else
                    {
                        if (item.idObra != 0)
                        {
                            ListaIdObraContadora = ListaIdObra.Count;
                            Monto = Monto + item.PrecioNeto;
                            ContadorElementos = ContadorElementos + 1;
                        }
                        else
                        { ContadorElementos = ContadorElementos + 1; }
                        if (listaDeObrasStatic.Count == ContadorElementos)
                        {
                            Nombre.Add(NombreObra);
                            string total = Convert.ToString(Monto);
                            string totalFinal = "$" + " " + total;
                            Total.Add(totalFinal);
                            Monto = 0;
                            NombreObra = "";
                            Monto = Monto + item.PrecioNeto;
                            NombreObra = item.NombreObra;
                            ListaIdObra.Add(item.idObra);
                        }
                    }
                }
            }
            chartEnPesos.Series[0].Points.DataBindXY(Nombre, Total);
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
        private void btnPdf_Click(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            Document doc = new Document(PageSize.LETTER);

            string folderPath = "C:\\Añuri-Archivos\\PDFs\\Reporte Obra Mensual\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string ruta = folderPath;
            //string Periodo = "Reporte de Obra";
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(ruta + cmbMes.Text + " " + "del Año" + " " + txtAño.Text + ".pdf", FileMode.Create));
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
            string TextoInicial = "Informe mensual de obras - " + cmbMes.Text + " " + "del Año" + " " + txtAño.Text;

            string replaceWith = "";
            doc.Add(new Paragraph(" "));

            Paragraph p1 = new Paragraph(new Chunk(TextoInicial));
            p1.Alignment = Element.ALIGN_LEFT;



            doc.Add(new Paragraph(p1));
            //doc.Add(new Paragraph(p2));
            doc.Add(new Paragraph(" "));

            //doc.Add(Chunk.NEWLINE);

            // Creamos una tabla que contendrá las cabeceras
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(4);
            tblPrueba.WidthPercentage = 110;

            // Configuramos el título de las columnas de la tabla

            PdfPCell clObra = new PdfPCell(new Phrase("Obra", _standardFont));
            clObra.BorderWidth = 0;
            clObra.BorderWidthBottom = 0.50f;
            clObra.BorderWidthLeft = 0.50f;
            clObra.BorderWidthRight = 0.50f;
            clObra.BorderWidthTop = 0.50f;

            PdfPCell clMaterial = new PdfPCell(new Phrase("Material", _standardFont));
            clMaterial.BorderWidth = 0;
            clMaterial.BorderWidthBottom = 0.50f;
            clMaterial.BorderWidthLeft = 0.50f;
            clMaterial.BorderWidthRight = 0.50f;
            clMaterial.BorderWidthTop = 0.50f;

            PdfPCell clKilos = new PdfPCell(new Phrase("Kilos", _standardFont));
            clKilos.BorderWidth = 0;
            clKilos.BorderWidthBottom = 0.50f;
            clKilos.BorderWidthLeft = 0.50f;
            clKilos.BorderWidthRight = 0.50f;
            clKilos.BorderWidthTop = 0.50f;


            PdfPCell clPrecioNeto = new PdfPCell(new Phrase("Precio Neto", _standardFont));
            clPrecioNeto.BorderWidth = 0;
            clPrecioNeto.BorderWidthBottom = 0.50f;
            clPrecioNeto.BorderWidthLeft = 0.50f;
            clPrecioNeto.BorderWidthRight = 0.50f;
            clPrecioNeto.BorderWidthTop = 0.50f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clObra);
            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clMaterial);
            //tblPrueba.AddCell(clFecha);
            tblPrueba.AddCell(clKilos);
            //tblPrueba.AddCell(clPrecioUnitario);
            tblPrueba.AddCell(clPrecioNeto);

            // Llenamos la tabla con información
            int TotalDeElementos = ListaDeObrasStatic.Count;
            int Contador = 0;
            List<int> ListaIdObra = new List<int>();
            foreach (var item in ListaDeObrasStatic)
            {
                //Agrego Punto De Miles...
                string ValorUnitario = item.ValorUnitario.ToString("N", new CultureInfo("es-CL"));
                string ValorNeto = item.PrecioNeto.ToString("N", new CultureInfo("es-CL"));
                Contador = Contador + 1;

                bool Existe = ListaIdObra.Any(x => x == item.idObra);
                if (Existe != true)
                {
                    clObra = new PdfPCell(new Phrase(item.NombreObra, letraContenido));
                    clObra.BorderWidth = 0;

                    clMaterial = new PdfPCell(new Phrase(item.Descripcion, letraContenido));
                    clMaterial.BorderWidth = 0;

                    string Kilos = Convert.ToString(item.Cantidad);
                    clKilos = new PdfPCell(new Phrase(Kilos, letraContenido));
                    clKilos.BorderWidth = 0;

                    string PrecioNeto = Convert.ToString(ValorNeto);
                    clPrecioNeto = new PdfPCell(new Phrase(PrecioNeto, letraContenido));
                    clPrecioNeto.BorderWidth = 0;

                    tblPrueba.AddCell(clObra);
                    tblPrueba.AddCell(clMaterial);
                    tblPrueba.AddCell(clKilos);
                    tblPrueba.AddCell(clPrecioNeto);

                    ListaIdObra.Add(item.idObra);
                }
                else
                {
                    clObra = new PdfPCell(new Phrase(" ", letraContenido));
                    clObra.BorderWidth = 0;

                    clMaterial = new PdfPCell(new Phrase(item.Descripcion, letraContenido));
                    clMaterial.BorderWidth = 0;

                    string Kilos = Convert.ToString(item.Cantidad);
                    clKilos = new PdfPCell(new Phrase(Kilos, letraContenido));
                    clKilos.BorderWidth = 0;

                    string PrecioNeto = Convert.ToString(ValorNeto);
                    clPrecioNeto = new PdfPCell(new Phrase(PrecioNeto, letraContenido));
                    clPrecioNeto.BorderWidth = 0;

                    tblPrueba.AddCell(clObra);
                    tblPrueba.AddCell(clMaterial);
                    tblPrueba.AddCell(clKilos);
                    tblPrueba.AddCell(clPrecioNeto);
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
