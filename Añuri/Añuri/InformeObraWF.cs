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

namespace Añuri
{
    public partial class InformeObraWF : Form
    {
        public int idObraSeleccionada;
        public string Obra;
        public InformeObraWF(int idObraSeleccionada, string Obra)
        {
            InitializeComponent();
            this.idObraSeleccionada = idObraSeleccionada;
            this.Obra = Obra;
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
            List<string> Total = new List<string>();
            foreach (var item in GraficoMaterialesEnPesos)
            {

                Nombre.Add(item.Descripcion + "( $ " + item.PrecioNeto + ")");
                string total = Convert.ToString(item.PrecioNeto);
                Total.Add(total);
            }
            chartEnPesos.Series[0].Points.DataBindXY(Nombre, Total);
        }
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
    }
}
