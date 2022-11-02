using Añuri.Dao;
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
using System.Timers;
using System.Windows.Forms;

namespace Añuri
{
    public partial class InicioWF : Form
    {
        public InicioWF()
        {
            InitializeComponent();
        }

        private void InicioWF_Load(object sender, EventArgs e)
        {
            try
            {
                ///// Dia y Hora
                CheckForIllegalCrossThreadCalls = false;
                System.Timers.Timer t = new System.Timers.Timer(1000);
                t.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
                t.Start();
                String DiaDeLaSemana = DateTime.Now.DayOfWeek.ToString();
                string Dia = ValidarDia(DiaDeLaSemana);
                String FechaDia = DateTime.Now.Day.ToString();
                String Month = DateTime.Now.Month.ToString();
                string Mes = ValidarMes(Month);
                String Year = DateTime.Now.Year.ToString();
                int month = Convert.ToInt32(Month);
                int year = Convert.ToInt32(Year);
                lblDia.Text = Dia + "," + " " + FechaDia + " " + "de" + " " + Mes + " " + Year;

                ///// Armo Panel de Informacion
                int totalProvedores = DaoConsultasGenerales.ContadorProveedores();
                int Materiales = DaoConsultasGenerales.ContadorProductos();
                int Obras = DaoConsultasGenerales.ContadorObras();
                int Usuarios = DaoConsultasGenerales.ContadorUsuarios();
                if (Obras > 9999)
                {
                    lblContadorVentas.Text = "+ 10.000";
                }
                if (Obras > 99999)
                {
                    lblContadorVentas.Text = "+ 100.000";
                }
                if (Obras > 999999)
                {
                    lblContadorVentas.Text = "+ 1.000.000";
                }
                else
                {
                    lblContadorVentas.Text = Convert.ToString(Obras);
                }
                if (Materiales > 9999)
                {
                    lblContadorProdcutos.Text = "+ 10.000";
                }
                if (Materiales > 99999)
                {
                    lblContadorProdcutos.Text = "+ 100.000";
                }
                if (Materiales > 999999)
                {
                    lblContadorProdcutos.Text = "+ 1.000.000";
                }
                else
                {
                    lblContadorProdcutos.Text = Convert.ToString(Materiales);
                }
                if (totalProvedores > 9999)
                {
                    lblContadorProveedores.Text = "+ 10.000";
                }
                if (totalProvedores > 99999)
                {
                    lblContadorProveedores.Text = "+ 100.000";
                }
                if (totalProvedores > 999999)
                {
                    lblContadorProveedores.Text = "+ 1.000.000";
                }
                else
                {
                    lblContadorProveedores.Text = Convert.ToString(totalProvedores);
                }
                if (Usuarios > 9999)
                {
                    lblContadorUsuarios.Text = "+ 10.000";
                }
                if (Usuarios > 99999)
                {
                    lblContadorUsuarios.Text = "+ 100.000";
                }
                if (Usuarios > 999999)
                {
                    lblContadorUsuarios.Text = "+ 1.000.000";
                }
                else
                {
                    lblContadorUsuarios.Text = Convert.ToString(Usuarios);
                }
                ///// Completo Grilla con informacion
                List<Stock> ListaStockFaltante = new List<Stock>();
                ListaStockFaltante = StockNeg.ListaStockFaltante();
                if (ListaStockFaltante.Count > 0)
                {
                    foreach (var item in ListaStockFaltante)
                    {
                        dgvInicio.Rows.Add(item.idProducto, item.Descripcion, item.Cantidad);
                    }
                }
                dgvInicio.ReadOnly = true;
                /////  Achico font del dia, dependiendo el mes
                if (Mes == "Diciembre" || Mes == "Noviembre" || Mes == "Septiembre")
                {
                    Font fuente = new Font(label1.Font.FontFamily, 16);
                    lblDia.Font = fuente;
                }
                else
                {
                    Font fuente = new Font(label1.Font.FontFamily, 18);
                    lblDia.Font = fuente;
                }
            }
            catch (Exception ex)
            { }
        }
        private string ValidarDia(string diaDeLaSemana)
        {
            string Dia = "";
            if (diaDeLaSemana == "Monday")
            {
                Dia = "Lunes";
            }
            if (diaDeLaSemana == "Tuesday")
            {
                Dia = "Martes";
            }
            if (diaDeLaSemana == "Wednesday")
            {
                Dia = "Miercoles";
            }
            if (diaDeLaSemana == "Thursday")
            {
                Dia = "Jueves";
            }
            if (diaDeLaSemana == "Friday")
            {
                Dia = "Viernes";
            }
            if (diaDeLaSemana == "Saturday")
            {
                Dia = "Sábado";
            }
            if (diaDeLaSemana == "Sunday")
            {
                Dia = "Domingo";
            }
            return Dia;
        }
        private string ValidarMes(string Month)
        {
            string Mes = "";
            if (Month == "1")
            {
                Mes = "Enero";
            }
            if (Month == "2")
            {
                Mes = "Febrero";
            }
            if (Month == "3")
            {
                Mes = "Marzo";
            }
            if (Month == "4")
            {
                Mes = "Abril";
            }
            if (Month == "5")
            {
                Mes = "Mayo";
            }
            if (Month == "6")
            {
                Mes = "Junio";
            }
            if (Month == "7")
            {
                Mes = "Julio";
            }
            if (Month == "8")
            {
                Mes = "Agosto";
            }
            if (Month == "9")
            {
                Mes = "Septiembre";
            }
            if (Month == "10")
            {
                Mes = "Octubre";
            }
            if (Month == "11")
            {
                Mes = "Noviembre";
            }
            if (Month == "12")
            {
                Mes = "Diciembre";
            }
            return Mes;
        }
        private void timer1_Tick(object sender, ElapsedEventArgs el)
        {
            CheckForIllegalCrossThreadCalls = false;
            lblMaster_FechaHoraReal.Text = Convert.ToString(DateTime.Now.ToString("HH:mm:ss"));
        }
    }
}
