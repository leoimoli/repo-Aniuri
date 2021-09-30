using Añuri.Dao;
using Añuri.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Añuri
{
    public partial class ReportesWF : Form
    {
        public ReportesWF()
        {
            InitializeComponent();
        }
        public static List<Reporte_Proveedores> listaProveedoresStatic;
        public static List<Reporte_Obras> listaObrasStatic;
        public static List<Reporte_Stock> listaStockStatic;
        public static List<Reporte_Stock> listaMaterialesStatic;
        private void ReportesWF_Load(object sender, EventArgs e)
        {
            List<Reporte_Proveedores> listaProveedores = new List<Reporte_Proveedores>();
            List<Reporte_Obras> listaObras = new List<Reporte_Obras>();
            List<Reporte_Stock> listaStock = new List<Reporte_Stock>();
            List<Reporte_Stock> listaMateriales = new List<Reporte_Stock>();

            ////// Grafico Proveedores
            listaProveedores = ReporteDao.BuscarTotalComprasRealizadasProveedores();
            if (listaProveedores.Count > 0)
            {
                listaProveedoresStatic = listaProveedores;
                DiseñoGraficoProveedores(listaProveedores);
            }
            else
            {
                btnExportarComprasproveedores.Visible = false;
            }
            ////// Grafico obras
            listaObras = ReporteDao.BuscarObrasPorMes();
            if (listaObras.Count > 0)
            {
                listaObrasStatic = listaObras;
                DiseñoGraficoObras(listaObras);
            }
            else
            { btnExportarObrasEnCurso.Visible = false; }

            ////// Grafico Stock
            listaStock = ReporteDao.BuscarMaterialesConMasStock();
            if (listaStock.Count > 0)
            {
                listaStockStatic = listaStock;
                DiseñoGraficoStock(listaStock);
            }
            else
            { btnExportarStock.Visible = false; }

            ////// Grafico Materiales mas utilizados
            listaMateriales = ReporteDao.BuscarMaterialesMasUtilizados();
            if (listaMateriales.Count > 0)
            {
                listaMaterialesStatic = listaMateriales;
                DiseñoGraficoMateriales(listaMateriales);
            }
            else
            { btnExportarStock.Visible = false; }
        }

        private void DiseñoGraficoMateriales(List<Reporte_Stock> listaMateriales)
        {
            List<string> Nombre = new List<string>();
            List<string> NombreValor = new List<string>();
            List<string> Total = new List<string>();
            int ContadorElementos = 0;
            foreach (var item in listaMateriales)
            {
                ContadorElementos = ContadorElementos + 1;
                if (ContadorElementos <= 6)
                {
                    Nombre.Add(item.Descripcion);
                    string total = Convert.ToString(item.Cantidad);
                    string totalFinal = total;
                    Total.Add(totalFinal);
                }
                else { break; }
            }
            chartMateriales.Series[0].Points.DataBindXY(Nombre, Total);
        }
        private void DiseñoGraficoStock(List<Reporte_Stock> listaStock)
        {
            List<string> Nombre = new List<string>();
            List<string> NombreValor = new List<string>();
            List<string> Total = new List<string>();
            int ContadorElementos = 0;
            foreach (var item in listaStock)
            {
                ContadorElementos = ContadorElementos + 1;
                if (ContadorElementos <= 6)
                {
                    Nombre.Add(item.Descripcion);
                    string total = Convert.ToString(item.Cantidad);
                    string totalFinal = total;
                    Total.Add(totalFinal);
                }
                else { break; }
            }
            chartStock.Series[0].Points.DataBindXY(Nombre, Total);

        }
        private void DiseñoGraficoObras(List<Reporte_Obras> listaObras)
        {
            List<string> Mes = new List<string>();
            List<string> Total = new List<string>();
            String Año = DateTime.Now.Year.ToString();
            foreach (var item in listaObras)
            {
                Mes.Add(item.mes);
                string total = Convert.ToString(item.TotalVentasPorMes);
                Total.Add(total);
            }
            chartObras.Series[0].Points.DataBindXY(Mes, Total);
        }
        private void DiseñoGraficoProveedores(List<Reporte_Proveedores> listaProveedores)
        {
            List<string> Nombre = new List<string>();
            List<string> Total = new List<string>();
            int ContadorElementos = 0;
            foreach (var item in listaProveedores)
            {
                ContadorElementos = ContadorElementos + 1;
                if (ContadorElementos <= 6)
                {
                    Nombre.Add(item.NombreEmpresa);
                    string total = Convert.ToString(item.TotalComprasEnPesos);
                    string totalFinal = "$" + " " + total;
                    Total.Add(total);
                }
            }
            chartProveedores.Series[0].Points.DataBindXY(Nombre, Total);
        }

        private void btnExportarObrasEnCurso_Click(object sender, EventArgs e)
        {
            if (listaObrasStatic.Count > 0)
            {
                foreach (var item in listaObrasStatic)
                {
                    dgvObra.Rows.Add(item.mes, item.TotalVentasPorMes);
                }
                dgvObra.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                dgvObra.MultiSelect = true;
                dgvObra.SelectAll();
                DataObject dataObj = dgvObra.GetClipboardContent();
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
            }
        }

        private void btnExportarMaterialesMasVendido_Click(object sender, EventArgs e)
        {
            if (listaMaterialesStatic.Count > 0)
            {
                foreach (var item in listaMaterialesStatic)
                {
                    dgvmateriales.Rows.Add(item.Descripcion, item.Cantidad);
                }
                dgvmateriales.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                dgvmateriales.MultiSelect = true;
                dgvmateriales.SelectAll();
                DataObject dataObj = dgvmateriales.GetClipboardContent();
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
            }
        }

        private void btnExportarStock_Click(object sender, EventArgs e)
        {
            if (listaStockStatic.Count > 0)
            {
                foreach (var item in listaStockStatic)
                {
                    dgvStock.Rows.Add(item.Descripcion, item.Cantidad);
                }
                dgvStock.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                dgvStock.MultiSelect = true;
                dgvStock.SelectAll();
                DataObject dataObj = dgvStock.GetClipboardContent();
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
            }
        }

        private void btnExportarComprasproveedores_Click(object sender, EventArgs e)
        {
            if (listaProveedoresStatic.Count > 0)
            {
                foreach (var item in listaProveedoresStatic)
                {
                    dgvproveedores.Rows.Add(item.NombreEmpresa, item.TotalComprasEnPesos);
                }
                dgvproveedores.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                dgvproveedores.MultiSelect = true;
                dgvproveedores.SelectAll();
                DataObject dataObj = dgvproveedores.GetClipboardContent();
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
            }
        }
    }
}
