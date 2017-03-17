using BespokeFusion;
using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoADAT
{
    /// <summary>
    /// Lógica de interacción para DepositosYAcciones.xaml
    /// </summary>
    public partial class DepositosYAcciones : Window
    {
        CuentaBancaria c;
        public DepositosYAcciones(CuentaBancaria cuenta)
        {
            InitializeComponent();
            btnAceptar.IsEnabled = false;
            c = cuenta;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InfoCuenta ic = new InfoCuenta(c);
            ic.Show();
        }
        List<Prestamo> pS = new List<Prestamo>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxCuotas.Text) || !String.IsNullOrWhiteSpace(textBoxMontante.Text) || cmbInterés.SelectedIndex > -1)
            {


                double A = Convert.ToDouble(textBoxMontante.Text);
                double aux = Convert.ToDouble(textBoxMontante.Text);
                for (int i = 1; i <= Convert.ToInt32(textBoxCuotas.Text); i++)
                {
                    double elevado = Convert.ToDouble(textBoxCuotas.Text.ToString());
                    decimal value = Decimal.Parse(cmbInterés.Text.Substring(0, (cmbInterés.Text.Length - 1)));
                    value = value / 100;
                    double valor = Convert.ToDouble(value);
                    Prestamo p = new Prestamo();
                    p.NumPagos = i;
                    double v = valor + 1;
                    double pot = Math.Pow(v, Convert.ToDouble(textBoxCuotas.Text));
                    double v2 = pot - 1;
                    double pot2 = Math.Pow((1 + valor), Convert.ToDouble(textBoxCuotas.Text));
                    double dividendo = valor * (pot2);
                    double v3 = v2 / dividendo;
                    double res = Convert.ToDouble(textBoxMontante.Text) / v3;
                    p.Anualidad = res;
                    //p.Intereses = Convert.ToDouble(textBoxMontante.Text) * Convert.ToDouble(valor);

                    if (pS.Count > 0)
                    {
                        p.Intereses = valor * aux;
                        p.Amortizacion = p.Anualidad - p.Intereses;
                        Prestamo pAux = pS.Last();
                        p.CapitalAmortizado = pAux.CapitalAmortizado + p.Amortizacion;

                    }
                    else
                    {
                        p.Intereses = Convert.ToDouble(textBoxMontante.Text) * Convert.ToDouble(valor);
                        p.Amortizacion = p.Anualidad - p.Intereses;
                        p.CapitalAmortizado = p.Amortizacion;
                    }
                    //p.Amortizacion = p.Anualidad - p.Intereses;
                    p.CapitalPendiente = aux - p.Amortizacion;
                    aux = p.CapitalPendiente;
                    A = p.Amortizacion;
                    pS.Add(p);
                    btnAceptar.IsEnabled = true;
                }
                foreach (Prestamo item in pS)
                {
                    item.Amortizacion = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(item.Amortizacion), 0));
                    item.Anualidad = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(item.Anualidad), 0));
                    item.CapitalAmortizado = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(item.CapitalAmortizado), 0));
                    item.CapitalPendiente = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(item.CapitalPendiente), 0));
                    item.Intereses = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(item.Intereses), 0));
                }
                datagridPrestamo.ItemsSource = pS;
            }
            else
            {
                MaterialMessageBox.ShowError("Rellene los campos");
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxCuotas.Text) || !String.IsNullOrWhiteSpace(textBoxMontante.Text) || datePickerConcesion.SelectedDate != null || cmbInterés.SelectedIndex>-1)
            {

                MessageBoxResult r = MaterialMessageBox.ShowWithCancel("Desea Realizar el prestamo? \n Advertencia: Al aceptar se asignaran los gastos e ingresos correspondientes en su cuenta. El calculo se realizara al realizar los extractos de cada mes");
                if (r == MessageBoxResult.OK)
                {
                    DateTime fechagasto = Convert.ToDateTime(datePickerConcesion.Text);
                    Ingreso i = new Ingreso();
                    i.nombreIngreso = "Prestamo";
                    i.tipoIngreso = "Prestamo Bancario";
                    i.fechaOperacion = Convert.ToDateTime(datePickerConcesion.Text);
                    i.fechaValor = Convert.ToDateTime(datePickerConcesion.Text);
                    i.cuantia = Convert.ToDecimal(textBoxMontante.Text);
                    i.CuentaBancaria = this.c;
                    MainWindow.u.RepositorioIngresos.Create(i);
                    foreach (Prestamo item in pS)
                    {
                        Gasto g = new Gasto();
                        g.nombreGasto = "Prestamo";
                        g.tipoGasto = "Prestamo Bancario";
                        fechagasto = fechagasto.AddMonths(1);
                        g.fechaOperacion = fechagasto;
                        g.fechaValor = fechagasto;
                        g.cuantia = Convert.ToDecimal(item.Anualidad);
                        g.CuentaBancaria = this.c;
                        MainWindow.u.RepositorioGastos.Create(g);

                    }
                }
            }
        }

        private void datePickerConcesion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerConcesion.SelectedDate != null)
            {
                DateTime date = Convert.ToDateTime(datePickerConcesion.Text).AddYears(1);
                datePickerFinalizacion.SelectedDate = date;
            }
        }
        private void buttonAtras_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            Color c = Color.FromScRgb(100, 139, 195, 74);
            SolidColorBrush aux = new SolidColorBrush(c);
            buttonAtras.Background = aux;
        }

        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonAtras.Background.Opacity = 0;
        }
    }

    public class Prestamo
    {
        private int numPagos;
        private double anualidad;
        private double amortizacion;
        private double intereses;
        private double capitalAmortizado;
        private double capitalPendiente;

        public int NumPagos
        {
            get
            {
                return numPagos;
            }

            set
            {
                numPagos = value;
            }
        }

        public double Anualidad
        {
            get
            {
                return anualidad;
            }

            set
            {
                anualidad = value;
            }
        }

        public double Amortizacion
        {
            get
            {
                return amortizacion;
            }

            set
            {
                amortizacion = value;
            }
        }

        public double Intereses
        {
            get
            {
                return intereses;
            }

            set
            {
                intereses = value;
            }
        }

        public double CapitalAmortizado
        {
            get
            {
                return capitalAmortizado;
            }

            set
            {
                capitalAmortizado = value;
            }
        }

        public double CapitalPendiente
        {
            get
            {
                return capitalPendiente;
            }

            set
            {
                capitalPendiente = value;
            }
        }
    }
}
