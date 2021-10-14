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

namespace Añuri
{
    public partial class InventarioMaterialesKilosWF : Form
    {
        public InventarioMaterialesKilosWF()
        {
            InitializeComponent();
        }

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

        private void InventarioMaterialesKilosWF_Load(object sender, EventArgs e)
        {
            try
            {
                txtMateriales.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteProductos.Autocomplete();
                txtMateriales.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtMateriales.AutoCompleteSource = AutoCompleteSource.CustomSource;
                int anio = DateTime.Now.Year;
                txtAño.Text = Convert.ToString(anio);
            }
            catch (Exception ex)
            { }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string año = txtAño.Text;
                string Material = txtMateriales.Text;
                if (año == "" && Material == "")
                {
                    const string message = "Atención: Debe ingresar algun filtro de busqueda.";
                    const string caption = "Atención:";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
                if (año == "")
                {
                    const string message = "Atención: El campo año es obligatorio.";
                    const string caption = "Atención:";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
                else
                {
                    List<MesProducto> ListaProductoMes = new List<MesProducto>();
                    List<int> ListaId = new List<int>();
                    List<int> ListaMes = new List<int>();
                    decimal SumaEntrada = 0;
                    decimal SumaSalida = 0;
                    decimal Total = 0;
                    List<Stock> ListaStock = StockNeg.ListarInventarioMaterialesKilos(año, Material);
                    if (ListaStock.Count > 0)
                    {

                        //var lista = ListaStock.GroupBy(t => new {Mes = t.FechaFactura.Month, idProducto = t.idProducto,  });
                        List<Stock> lista = new List<Stock>();
                        var data = ListaStock.Select(k => new { k.FechaFactura.Month, k.idProducto, k.TipoMovimiento, k.PrecioNeto }).GroupBy(x => new { x.Month, x.idProducto, x.TipoMovimiento, x.PrecioNeto }, (key, group) => new
                        {

                            mes = key.Month,
                            id = key.idProducto,
                            tipoMov = key.TipoMovimiento,
                            precioNeto = key.PrecioNeto,
                            //tCharge = group.Sum(k => k.)
                        }).ToList();

                        foreach (var item in data)
                        {
                            bool existe = ListaId.Any(x => x == item.id);
                            if (existe == false)
                            {
                                MesProducto _Lista = new MesProducto();
                                

                                SumaEntrada = 0;
                                SumaSalida = 0;
                                Total = 0;
                                ListaMes.Clear();
                                ListaId.Add(item.id);
                                ListaMes.Add(item.mes);
                                if (item.tipoMov == "E")
                                {
                                    SumaEntrada = SumaEntrada + item.precioNeto;
                                }
                                if (item.tipoMov == "S")
                                {
                                    SumaSalida = SumaSalida + item.precioNeto;
                                }
                                Total = SumaEntrada - SumaSalida;
                            }
                            else
                            {
                                bool existeMes = ListaMes.Any(x => x == item.mes);
                                if (existeMes == false)
                                {
                                    SumaEntrada = 0;
                                    SumaSalida = 0;
                                    Total = 0;
                                    ListaMes.Add(item.mes);
                                    if (item.tipoMov == "E")
                                    {
                                        SumaEntrada = SumaEntrada + item.precioNeto;
                                    }
                                    if (item.tipoMov == "S")
                                    {
                                        SumaSalida = SumaSalida + item.precioNeto;
                                    }
                                    Total = SumaEntrada - SumaSalida;
                                }
                                else
                                {
                                    if (item.tipoMov == "E")
                                    {
                                        SumaEntrada = SumaEntrada + item.precioNeto;
                                    }
                                    if (item.tipoMov == "S")
                                    {
                                        SumaSalida = SumaSalida + item.precioNeto;
                                    }
                                    Total = SumaEntrada - SumaSalida;
                                }
                            }
                        }
                    }

                  
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
