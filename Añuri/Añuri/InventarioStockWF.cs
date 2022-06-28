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
    public partial class InventarioStockWF : Form
    {
        public InventarioStockWF()
        {
            InitializeComponent();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime Fecha = dtFechaHasta.Value;
                List<Stock> ListaStockAux = new List<Stock>();
                List<Stock> ListaStock = StockNeg.ListarMovimientosStockInventarioPorFecha(Fecha);
                if (ListaStock.Count > 0)
                {
                    dgvInventario.Visible = true;
                    btnExcel.Visible = true;
                    btnPdf.Visible = true;
                    ArmarGrillaStockDisponible(ListaStock);
                }
                else
                {
                    dgvInventario.Visible = false;
                    btnExcel.Visible = false;
                    btnPdf.Visible = false;
                    const string message = "Atención: No hay información para mostrar con el filtro ingresado.";
                    const string caption = "Atención:";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
            }
            catch (Exception ex)
            { }
        }
        public static List<Stock> ListaMaterialesStockDisponibleStatic;
        private void ArmarGrillaStockDisponible(List<Stock> ListaStock)
        {
            int idEntrada = 0;
            int TotalEntrada = 0;
            int TotalSalida = 0;
            int cantidadTotal = 0;
            int StockTotalFinal = 0;
            int Total = 0;
            List<Stock> ListaStockFinal = new List<Stock>();
            List<Stock> ListaStockFinal2 = new List<Stock>();
            List<int> ListaIdMaterial = new List<int>();

            int contadorElementos = 0;
            dgvInventario.Rows.Clear();
            foreach (var item in ListaStock)
            {
                //bool RecuentoStock = false;
                //bool existe = ListaIdMaterial.Any(x => x == item.idProducto);
                //if (existe == false)
                //{
                //    StockTotalFinal = 0;
                //    ListaIdMaterial.Add(item.idProducto);
                //    RecuentoStock = true;
                //}
                if (item.Descripcion == "TOTALES")
                {
                    ListaStockFinal2.Add(item);
                    break;
                }
                //// else para distinto de Totales
                else
                {
                    contadorElementos = contadorElementos + 1;
                    if (item.EstadoEntrada == 0 && dtFechaHasta.Value <= item.FechaCierre)
                    {
                        if (item.TipoMovimiento == "E" && dtFechaHasta.Value <= item.FechaMovimiento)
                        {
                            if (idEntrada != 0)
                            {
                                if (idEntrada == item.idMovimientoEntrada)
                                {
                                    if (item.TipoMovimiento == "E")
                                    {
                                        TotalEntrada = item.Cantidad;
                                    }
                                    if (item.TipoMovimiento == "S")
                                    {
                                        TotalSalida = TotalSalida + item.Cantidad;
                                    }
                                    Total = TotalEntrada - TotalSalida;
                                    item.Cantidad = Total;
                                    Stock _lista = new Stock();
                                    _lista.idProducto = item.idProducto;
                                    _lista.Descripcion = item.Descripcion;
                                    _lista.FechaFactura = item.FechaFactura;
                                    _lista.FechaCierre = item.FechaCierre;
                                    _lista.Cantidad = Total;
                                    StockTotalFinal = Total + StockTotalFinal;
                                    _lista.ValorUnitario = item.ValorUnitario;
                                    _lista.PrecioNeto = item.PrecioNeto;
                                    _lista.TipoMovimiento = item.TipoMovimiento;
                                    _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                                    ListaStockFinal.Add(_lista);
                                    idEntrada = item.idMovimientoEntrada;
                                    if (contadorElementos == ListaStock.Count)
                                    {
                                        if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                                        {
                                            ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                                        }
                                    }
                                }
                                //// else para caudno idmovimentoentrada es distinto a idEntrada
                                else
                                {
                                    cantidadTotal = 0;
                                    if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                                    {
                                        ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                                    }
                                    TotalEntrada = 0;
                                    TotalSalida = 0;
                                    Total = 0;
                                    if (item.TipoMovimiento == "E")
                                    {
                                        TotalEntrada = item.Cantidad;
                                    }
                                    if (item.TipoMovimiento == "S")
                                    {
                                        TotalSalida = TotalSalida + item.Cantidad;
                                    }
                                    Total = TotalEntrada - TotalSalida;
                                    item.Cantidad = Total;
                                    Stock _lista = new Stock();
                                    _lista.idProducto = item.idProducto;
                                    _lista.Descripcion = item.Descripcion;
                                    _lista.FechaFactura = item.FechaFactura;
                                    _lista.FechaCierre = item.FechaCierre;
                                    _lista.StockTotal = StockTotalFinal;
                                    _lista.Cantidad = Total;
                                    StockTotalFinal = Total + StockTotalFinal;
                                    _lista.ValorUnitario = item.ValorUnitario;
                                    _lista.PrecioNeto = item.PrecioNeto;
                                    _lista.TipoMovimiento = item.TipoMovimiento;
                                    _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                                    ListaStockFinal.Add(_lista);
                                    idEntrada = item.idMovimientoEntrada;
                                    ///// Valido si ya existe el material en la lista Final.
                                    if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                                    {
                                        ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                                    }
                                }
                            }

                            //// else para cuando idEntrada es distinto a 0
                            else
                            {
                                TotalEntrada = 0;
                                TotalSalida = 0;
                                Total = 0;
                                if (item.TipoMovimiento == "E")
                                {
                                    TotalEntrada = item.Cantidad;
                                }
                                Total = TotalEntrada - TotalSalida;
                                item.Cantidad = Total;
                                Stock _lista = new Stock();
                                _lista.idProducto = item.idProducto;
                                _lista.Descripcion = item.Descripcion;
                                _lista.FechaFactura = item.FechaFactura;
                                _lista.FechaCierre = item.FechaCierre;
                                _lista.Cantidad = Total;
                                StockTotalFinal = Total + StockTotalFinal;
                                _lista.ValorUnitario = item.ValorUnitario;
                                _lista.PrecioNeto = item.PrecioNeto;
                                _lista.TipoMovimiento = item.TipoMovimiento;
                                _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                                ListaStockFinal.Add(_lista);
                                idEntrada = item.idMovimientoEntrada;
                                if (contadorElementos == ListaStock.Count)
                                {
                                    if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                                    {
                                        ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                                    }
                                }
                            }
                        }
                    }
                    //// si no se da la condicion estado 0 y fecha
                    else
                    {
                        if (item.EstadoEntrada == 1)
                        {
                            if (idEntrada != 0)
                            {
                                if (idEntrada == item.idMovimientoEntrada)
                                {
                                    if (item.TipoMovimiento == "E")
                                    {
                                        TotalEntrada = item.Cantidad;
                                    }
                                    if (item.TipoMovimiento == "S")
                                    {
                                        TotalSalida = TotalSalida + item.Cantidad;
                                    }
                                    Total = TotalEntrada - TotalSalida;
                                    item.Cantidad = Total;
                                    cantidadTotal = cantidadTotal + item.Cantidad;
                                    //ListaStockFinal = ListaStock;
                                    Stock _lista = new Stock();
                                    _lista.idProducto = item.idProducto;
                                    _lista.Descripcion = item.Descripcion;
                                    _lista.FechaFactura = item.FechaFactura;
                                    _lista.FechaCierre = item.FechaCierre;
                                    _lista.Cantidad = Total;
                                    StockTotalFinal = Total + StockTotalFinal;
                                    _lista.ValorUnitario = item.ValorUnitario;
                                    _lista.PrecioNeto = item.PrecioNeto;
                                    _lista.TipoMovimiento = item.TipoMovimiento;
                                    _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                                    ListaStockFinal.Add(_lista);
                                    idEntrada = item.idMovimientoEntrada;
                                    if (contadorElementos == ListaStock.Count)
                                    {
                                        if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                                        {
                                            ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                                        }
                                        else
                                        {
                                            ListaStockFinal2.RemoveAll(r => r.idMovimientoEntrada == idEntrada);
                                            ListaStockFinal2.Add(item);
                                        }
                                    }
                                    else
                                    {
                                        if (ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                                        {
                                            ListaStockFinal2.RemoveAll(r => r.idMovimientoEntrada == idEntrada);
                                            ListaStockFinal2.Add(item);
                                        }
                                    }
                                }
                                //// else para caudno idmovimentoentrada es distinto a idEntrada
                                else
                                {
                                    if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                                    {
                                        ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                                    }
                                    TotalEntrada = 0;
                                    TotalSalida = 0;
                                    Total = 0;
                                    if (item.TipoMovimiento == "E")
                                    {
                                        TotalEntrada = item.Cantidad;
                                    }
                                    if (item.TipoMovimiento == "S")
                                    {
                                        TotalSalida = TotalSalida + item.Cantidad;

                                    }
                                    Total = TotalEntrada - TotalSalida;
                                    item.Cantidad = Total;
                                    cantidadTotal = cantidadTotal + item.Cantidad;
                                    Stock _lista = new Stock();
                                    _lista.idProducto = item.idProducto;
                                    _lista.Descripcion = item.Descripcion;
                                    _lista.FechaFactura = item.FechaFactura;
                                    _lista.FechaCierre = item.FechaCierre;
                                    _lista.StockTotal = StockTotalFinal;
                                    _lista.Cantidad = Total;
                                    StockTotalFinal = Total + StockTotalFinal;
                                    //if (RecuentoStock == true)
                                    //{
                                    //    _lista.StockTotal = StockTotalFinal;
                                    //}
                                    _lista.ValorUnitario = item.ValorUnitario;
                                    _lista.PrecioNeto = item.PrecioNeto;
                                    _lista.TipoMovimiento = item.TipoMovimiento;
                                    _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                                    ListaStockFinal.Add(_lista);
                                    idEntrada = item.idMovimientoEntrada;
                                    ///// Valido si ya existe el material en la lista Final.
                                    if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                                    {
                                        ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                                    }
                                }

                            }

                            //// else para cuando idEntrada es distinto a 0
                            else
                            {
                                TotalEntrada = 0;
                                TotalSalida = 0;
                                Total = 0;
                                if (item.TipoMovimiento == "E")
                                {
                                    TotalEntrada = item.Cantidad;

                                }
                                Total = TotalEntrada - TotalSalida;
                                item.Cantidad = Total;
                                cantidadTotal = cantidadTotal + item.Cantidad;
                                Stock _lista = new Stock();
                                _lista.idProducto = item.idProducto;
                                _lista.Descripcion = item.Descripcion;
                                _lista.FechaFactura = item.FechaFactura;
                                _lista.FechaCierre = item.FechaCierre;
                                _lista.StockTotal = StockTotalFinal;
                                _lista.Cantidad = Total;
                                StockTotalFinal = Total + StockTotalFinal;
                                _lista.ValorUnitario = item.ValorUnitario;
                                _lista.PrecioNeto = item.PrecioNeto;
                                _lista.TipoMovimiento = item.TipoMovimiento;
                                _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                                ListaStockFinal.Add(_lista);
                                idEntrada = item.idMovimientoEntrada;
                                if (contadorElementos == ListaStock.Count)
                                {
                                    if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                                    {
                                        ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            int cantidad = 0;
            int Contador = 0;
            List<int> Lista = new List<int>();
            List<string> ListaAux = new List<string>();
            int idProducto = 0;
            int posicion = 0;
            for (int i = 0; i < ListaStockFinal2.Count; i++)
            {
                if (Contador == 0)
                {
                    idProducto = ListaStockFinal2[i].idProducto;
                    Contador = Contador + 1;
                    cantidad = cantidad + ListaStockFinal2[i].Cantidad;
                    Lista.Add(idProducto);
                }
                else
                {
                    bool existe = Lista.Any(x => x == ListaStockFinal2[i].idProducto);
                    if (existe == false)
                    {
                        Contador = Contador + 1;
                        Lista.Add(ListaStockFinal2[i].idProducto);
                        ListaStockFinal2[posicion].StockTotal = cantidad;
                        cantidad = 0;
                        posicion = i;
                        cantidad = cantidad + ListaStockFinal2[i].Cantidad;
                    }
                    else
                    {
                        Contador = Contador + 1;
                        if (Contador == ListaStockFinal2.Count)
                        {
                            cantidad = cantidad + ListaStockFinal2[i].Cantidad;
                            ListaStockFinal2[posicion].StockTotal = cantidad;
                            //cantidad = 0;
                            //Contador = 0;
                        }
                        cantidad = cantidad + ListaStockFinal2[i].Cantidad;
                    }
                }
            }

            string fecha = "";
            Stock ultimo = new Stock();
            ultimo.Descripcion = "TOTALES";
            int Kilos = CalcularTotalKilos(ListaStockFinal2);
            decimal TotalPrecioNeto = CalculaPrecioNeto(ListaStockFinal2);
            decimal ImporteTotalMateriales = CalculaImporteTotalMateriales(ListaStockFinal2);
            ultimo.StockTotal = Convert.ToInt32(Kilos);
            ultimo.PrecioNeto = Convert.ToDecimal(TotalPrecioNeto);
            ultimo.ImporteTotalMateriales = ImporteTotalMateriales;
            ultimo.ValorUnitario = 0;
            ListaStockFinal2.Add(ultimo);
            ListaMaterialesStockDisponibleStatic = ListaStockFinal2;
            string Descripcion = "";
            int ContadorDescripcion = 0;
            decimal ImporteTotal = 0;
            string StockTotal = "";
            List<string> ListaDescripcion = new List<string>();
            List<string> ListaDescripcionAux = new List<string>();
            int contador = 0;
            foreach (var item in ListaStockFinal2)
            {
                contador = contador + 1;
                if (item.FechaFactura == Convert.ToDateTime("1/1/0001 00:00:00"))
                {
                    fecha = " ";
                }
                else
                {
                    fecha = item.FechaFactura.ToShortDateString();
                }


                decimal Importe = item.Cantidad * item.ValorUnitario;


                if (ContadorDescripcion == 0)
                {
                    Descripcion = item.Descripcion;
                    ContadorDescripcion = ContadorDescripcion + 1;
                    ListaDescripcion.Add(Descripcion);
                    StockTotal = Convert.ToString(item.StockTotal);
                    ImporteTotal = ImporteTotal + Importe;
                }
                else
                {
                    bool existe = ListaDescripcion.Any(x => x == item.Descripcion);
                    if (existe == false)
                    {
                        if (contador == ListaStockFinal2.Count - 1)
                        {
                            ListaDescripcion.Add(item.Descripcion);
                            Descripcion = item.Descripcion;
                            StockTotal = Convert.ToString(item.Cantidad);
                            ImporteTotal = 0;
                            ImporteTotal = ImporteTotal + Importe;
                        }
                        else
                        {
                            ListaDescripcion.Add(item.Descripcion);
                            Descripcion = item.Descripcion;
                            StockTotal = Convert.ToString(item.StockTotal);
                            ImporteTotal = 0;
                            ImporteTotal = ImporteTotal + Importe;
                        }
                    }
                    else
                    {
                        Descripcion = "";
                        StockTotal = "";
                        ImporteTotal = ImporteTotal + Importe;
                    }
                }
                if (item.Descripcion == "TOTALES")
                {
                    ImporteTotal = item.ImporteTotalMateriales;
                    StockTotal = Convert.ToString(item.StockTotal);
                }

                //Agrego Punto De Miles...
                string ValorUnitario = item.ValorUnitario.ToString("N", new CultureInfo("es-CL"));
                string ImporteDos = Importe.ToString("N", new CultureInfo("es-CL"));
                string ImporteTotalDos = ImporteTotal.ToString("N", new CultureInfo("es-CL"));

                //dgvInventario.Rows.Add(item.idProducto, Descripcion, StockTotal, item.Cantidad, fecha, item.ValorUnitario, Importe, ImporteTotal, "", "", "");
                dgvInventario.Rows.Add(item.idProducto, Descripcion, StockTotal, item.Cantidad, fecha, ValorUnitario, ImporteDos, ImporteTotalDos, "", "", "");
            }
            dgvInventario.ReadOnly = true;
        }

        private decimal CalculaImporteTotalMateriales(List<Stock> listaStockFinal2)
        {
            decimal ImporteTotal = 0;
            foreach (var item in listaStockFinal2)
            {
                int Cantidad = item.Cantidad;
                decimal precioUnitario = item.ValorUnitario;
                decimal Calculo = Cantidad * precioUnitario;
                ImporteTotal = ImporteTotal + Calculo;
            }
            return ImporteTotal;
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
        private int CalcularTotalKilos(List<Stock> listaMateriales)
        {
            List<int> Listakilos = new List<int>();
            int totalKilos = 0;
            for (int i = 0; i < listaMateriales.Count; i++)
            {
                bool existe = Listakilos.Any(x => x == listaMateriales[i].idProducto);
                if (existe == false)
                {
                    totalKilos += listaMateriales[i].Cantidad;
                    Listakilos.Add(listaMateriales[i].idProducto);
                }
                else
                {
                    totalKilos += listaMateriales[i].Cantidad;
                }
            }
            return totalKilos;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void InventarioStockWF_Load(object sender, EventArgs e)
        {

        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            ProgressBar();
            dgvInventario.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dgvInventario.MultiSelect = true;
            dgvInventario.SelectAll();
            DataObject dataObj = dgvInventario.GetClipboardContent();
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

        private void btnPdf_Click(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            Document doc = new Document(PageSize.LETTER);

            string folderPath = "C:\\Añuri-Archivos\\PDFs\\Reporte-Inventario\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string replaceWith = "";
            //material = material.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
            string ruta = folderPath;
            //string Periodo = "Reporte de Obra";
            string fecha = dtFechaHasta.Value.ToShortDateString();
            fecha = fecha.Replace("/", "-");
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(ruta + fecha + ".pdf", FileMode.Create));
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
            string TextoInicial = "Reporte inventario Fecha " + fecha;


            doc.Add(new Paragraph(" "));

            Paragraph p1 = new Paragraph(new Chunk(TextoInicial));
            p1.Alignment = Element.ALIGN_LEFT;
            doc.Add(new Paragraph(p1));
            doc.Add(new Paragraph(" "));



            // Creamos una tabla que contendrá las cabeceras
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(10);
            tblPrueba.WidthPercentage = 110;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clMaterial = new PdfPCell(new Phrase("Material", _standardFont));
            clMaterial.BorderWidth = 0;
            clMaterial.BorderWidthBottom = 0.50f;
            clMaterial.BorderWidthLeft = 0.50f;
            clMaterial.BorderWidthRight = 0.50f;
            clMaterial.BorderWidthTop = 0.50f;

            PdfPCell clKiloTotal = new PdfPCell(new Phrase("KG.Total", _standardFont));
            clKiloTotal.BorderWidth = 0;
            clKiloTotal.BorderWidthBottom = 0.50f;
            clKiloTotal.BorderWidthLeft = 0.50f;
            clKiloTotal.BorderWidthRight = 0.50f;
            clKiloTotal.BorderWidthTop = 0.50f;

            PdfPCell clKilos = new PdfPCell(new Phrase("Kilos", _standardFont));
            clKilos.BorderWidth = 0;
            clKilos.BorderWidthBottom = 0.50f;
            clKilos.BorderWidthLeft = 0.50f;
            clKilos.BorderWidthRight = 0.50f;
            clKilos.BorderWidthTop = 0.50f;

            PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
            clFecha.BorderWidth = 0;
            clFecha.BorderWidthBottom = 0.50f;
            clFecha.BorderWidthLeft = 0.50f;
            clFecha.BorderWidthRight = 0.50f;
            clFecha.BorderWidthTop = 0.50f;


            PdfPCell clPrecioUnitario = new PdfPCell(new Phrase("Precio Unitario", _standardFont));
            clPrecioUnitario.BorderWidth = 0;
            clPrecioUnitario.BorderWidthBottom = 0.50f;
            clPrecioUnitario.BorderWidthLeft = 0.50f;
            clPrecioUnitario.BorderWidthRight = 0.50f;
            clPrecioUnitario.BorderWidthTop = 0.50f;

            PdfPCell clImporte = new PdfPCell(new Phrase("Importe", _standardFont));
            clImporte.BorderWidth = 0;
            clImporte.BorderWidthBottom = 0.50f;
            clImporte.BorderWidthLeft = 0.50f;
            clImporte.BorderWidthRight = 0.50f;
            clImporte.BorderWidthTop = 0.50f;

            PdfPCell clImporteTotal = new PdfPCell(new Phrase("Importe Total", _standardFont));
            clImporteTotal.BorderWidth = 0;
            clImporteTotal.BorderWidthBottom = 0.50f;
            clImporteTotal.BorderWidthLeft = 0.50f;
            clImporteTotal.BorderWidthRight = 0.50f;
            clImporteTotal.BorderWidthTop = 0.50f;

            PdfPCell clIndice = new PdfPCell(new Phrase("Indice", _standardFont));
            clIndice.BorderWidth = 0;
            clIndice.BorderWidthBottom = 0.50f;
            clIndice.BorderWidthLeft = 0.50f;
            clIndice.BorderWidthRight = 0.50f;
            clIndice.BorderWidthTop = 0.50f;

            PdfPCell clRepos = new PdfPCell(new Phrase("V.Repos", _standardFont));
            clRepos.BorderWidth = 0;
            clRepos.BorderWidthBottom = 0.50f;
            clRepos.BorderWidthLeft = 0.50f;
            clRepos.BorderWidthRight = 0.50f;
            clRepos.BorderWidthTop = 0.50f;

            PdfPCell clValorFinal = new PdfPCell(new Phrase("Valor Final", _standardFont));
            clValorFinal.BorderWidth = 0;
            clValorFinal.BorderWidthBottom = 0.50f;
            clValorFinal.BorderWidthLeft = 0.50f;
            clValorFinal.BorderWidthRight = 0.50f;
            clValorFinal.BorderWidthTop = 0.50f;


            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clMaterial);
            tblPrueba.AddCell(clKiloTotal);
            tblPrueba.AddCell(clKilos);
            tblPrueba.AddCell(clFecha);
            tblPrueba.AddCell(clPrecioUnitario);
            tblPrueba.AddCell(clImporte);
            tblPrueba.AddCell(clImporteTotal);
            tblPrueba.AddCell(clIndice);
            tblPrueba.AddCell(clRepos);
            tblPrueba.AddCell(clValorFinal);


            // Llenamos la tabla con información
            string Descripcion = "";
            decimal ImporteTotal = 0;
            string StockTotal = "";
            List<string> ListaDescripcion = new List<string>();
            int TotalDeElementos = ListaMaterialesStockDisponibleStatic.Count;
            int Contador = 0;
            int Contador2 = 0;
            foreach (var item in ListaMaterialesStockDisponibleStatic)
            {
                Contador2 = Contador2 + 1;
                if (item.FechaFactura == Convert.ToDateTime("1/1/0001 00:00:00"))
                {
                    fecha = " ";
                }
                else
                {
                    fecha = item.FechaFactura.ToShortDateString();
                }

                decimal Importe = item.Cantidad * item.ValorUnitario;
                if (Contador == 0)
                {
                    Descripcion = item.Descripcion;
                    Contador = Contador + 1;
                    ListaDescripcion.Add(Descripcion);
                    StockTotal = Convert.ToString(item.StockTotal);
                    ImporteTotal = ImporteTotal + Importe;
                }
                else
                {
                    bool existe = ListaDescripcion.Any(x => x == item.Descripcion);
                    if (existe == false)
                    {
                        if (Contador2 == ListaMaterialesStockDisponibleStatic.Count - 1)
                        {
                            ListaDescripcion.Add(item.Descripcion);
                            Descripcion = item.Descripcion;
                            StockTotal = Convert.ToString(item.Cantidad);
                            ImporteTotal = 0;
                            ImporteTotal = ImporteTotal + Importe;
                        }
                        else
                        {
                            ListaDescripcion.Add(item.Descripcion);
                            Descripcion = item.Descripcion;
                            StockTotal = Convert.ToString(item.StockTotal);
                            ImporteTotal = 0;
                            ImporteTotal = ImporteTotal + Importe;
                        }

                    }
                    else
                    {
                        Descripcion = "";
                        StockTotal = "";
                        ImporteTotal = ImporteTotal + Importe;
                    }
                }
                if (item.Descripcion == "TOTALES")
                {
                    ImporteTotal = item.ImporteTotalMateriales;
                }

                if (item.Descripcion != "")
                {
                    if (TotalDeElementos == Contador2)
                    {
                        clMaterial = new PdfPCell(new Phrase(Descripcion, UltimoRegistro));
                        clMaterial.BorderWidth = 0;

                        string KiloTotal = Convert.ToString(StockTotal);
                        clKiloTotal = new PdfPCell(new Phrase(KiloTotal, UltimoRegistro));
                        clKiloTotal.BorderWidth = 0;

                        string Kilos = Convert.ToString(item.Cantidad);
                        clKilos = new PdfPCell(new Phrase(Kilos, UltimoRegistro));
                        clKilos.BorderWidth = 0;

                        string Fecha = Convert.ToString(fecha);
                        clFecha = new PdfPCell(new Phrase(Fecha, UltimoRegistro));
                        clFecha.BorderWidth = 0;

                        string PrecioUnitario = Convert.ToString(item.ValorUnitario);
                        clPrecioUnitario = new PdfPCell(new Phrase(PrecioUnitario, UltimoRegistro));
                        clPrecioUnitario.BorderWidth = 0;

                        string Impor = Convert.ToString(Importe);
                        clImporte = new PdfPCell(new Phrase(Impor, UltimoRegistro));
                        clImporte.BorderWidth = 0;

                        string ImporTotal = Convert.ToString(ImporteTotal);
                        clImporteTotal = new PdfPCell(new Phrase(ImporTotal, UltimoRegistro));
                        clImporteTotal.BorderWidth = 0;

                        string indice = "-";
                        clIndice = new PdfPCell(new Phrase(indice, UltimoRegistro));
                        clImporteTotal.BorderWidth = 0;

                        string repos = "-";
                        clRepos = new PdfPCell(new Phrase(repos, UltimoRegistro));
                        clImporteTotal.BorderWidth = 0;

                        string valorFinal = "-";
                        clValorFinal = new PdfPCell(new Phrase(valorFinal, UltimoRegistro));
                        clImporteTotal.BorderWidth = 0;

                        tblPrueba.AddCell(clMaterial);
                        tblPrueba.AddCell(clKiloTotal);
                        tblPrueba.AddCell(clKilos);
                        tblPrueba.AddCell(clFecha);
                        tblPrueba.AddCell(clPrecioUnitario);
                        tblPrueba.AddCell(clImporte);
                        tblPrueba.AddCell(clImporteTotal);
                        tblPrueba.AddCell(clIndice);
                        tblPrueba.AddCell(clRepos);
                        tblPrueba.AddCell(clValorFinal);
                    }
                    else
                    {
                        clMaterial = new PdfPCell(new Phrase(Descripcion, letraContenido));
                        clMaterial.BorderWidth = 0;

                        string KiloTotal = Convert.ToString(StockTotal);
                        clKiloTotal = new PdfPCell(new Phrase(KiloTotal, letraContenido));
                        clKiloTotal.BorderWidth = 0;

                        string Kilos = Convert.ToString(item.Cantidad);
                        clKilos = new PdfPCell(new Phrase(Kilos, letraContenido));
                        clKilos.BorderWidth = 0;

                        string Fecha = Convert.ToString(fecha);
                        clFecha = new PdfPCell(new Phrase(Fecha, letraContenido));
                        clFecha.BorderWidth = 0;

                        string PrecioUnitario = Convert.ToString(item.ValorUnitario);
                        clPrecioUnitario = new PdfPCell(new Phrase(PrecioUnitario, letraContenido));
                        clPrecioUnitario.BorderWidth = 0;

                        string Impor = Convert.ToString(Importe);
                        clImporte = new PdfPCell(new Phrase(Impor, letraContenido));
                        clImporte.BorderWidth = 0;

                        string ImporTotal = Convert.ToString(ImporteTotal);
                        clImporteTotal = new PdfPCell(new Phrase(ImporTotal, letraContenido));
                        clImporteTotal.BorderWidth = 0;

                        string indice = "-";
                        clIndice = new PdfPCell(new Phrase(indice, letraContenido));
                        clImporteTotal.BorderWidth = 0;

                        string repos = "-";
                        clRepos = new PdfPCell(new Phrase(repos, letraContenido));
                        clImporteTotal.BorderWidth = 0;

                        string valorFinal = "-";
                        clValorFinal = new PdfPCell(new Phrase(valorFinal, letraContenido));
                        clImporteTotal.BorderWidth = 0;

                        tblPrueba.AddCell(clMaterial);
                        tblPrueba.AddCell(clKiloTotal);
                        tblPrueba.AddCell(clKilos);
                        tblPrueba.AddCell(clFecha);
                        tblPrueba.AddCell(clPrecioUnitario);
                        tblPrueba.AddCell(clImporte);
                        tblPrueba.AddCell(clImporteTotal);
                        tblPrueba.AddCell(clIndice);
                        tblPrueba.AddCell(clRepos);
                        tblPrueba.AddCell(clValorFinal);
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

        /// <summary>
        /// /// 1 Inventario
        /// 2 Kilos
        /// 3 Pesos
        /// </summary>

        private void btnInicio_Click(object sender, EventArgs e)
        {
            InventarioStockWF _inventario = new InventarioStockWF();
            _inventario.Show();
            Hide();
        }
        private void btnMaterialesKilos_Click(object sender, EventArgs e)
        {
            InventarioMaterialesKilosWF _inventario = new InventarioMaterialesKilosWF();
            _inventario.Show();
            Hide();
        }
        private void btnMaterialesEnPesos_Click(object sender, EventArgs e)
        {
            InventarioMaterialesPesosWF _inventario = new InventarioMaterialesPesosWF();
            _inventario.Show();
            Hide();
        }
    }
}
