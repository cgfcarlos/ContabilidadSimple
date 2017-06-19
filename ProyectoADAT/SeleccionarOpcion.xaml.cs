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
    /// Lógica de interacción para SeleccionarOpcion.xaml
    /// </summary>
    public partial class SeleccionarOpcion : Window
    {
        CuentaBancaria c = new CuentaBancaria();
        public SeleccionarOpcion()
        {
            InitializeComponent();
        }

        private void btnInfoCuenta_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            foreach (CuentaBancaria item in MainWindow.user.CuentasBancarias)
            {
                MenuItem mi = new MenuItem();
                mi.Header = item.numerocuenta;
                mi.ToolTip = item.numerocuenta.ToString();
                mi.Click += menuItem_Click;
                cm.Items.Add(mi);
            }
            cm.Visibility = Visibility.Visible;
            cm.IsOpen = true;
            /*InfoCuenta info = new InfoCuenta(MainWindow.user.CuentasBancarias.FirstOrDefault());
            info.Show();
            this.Close();*/
        }

        private void menuItem_Click(object sender, System.EventArgs e)
        {
            if(sender.GetType() == typeof(MenuItem))
            {
                MenuItem mi = (MenuItem)sender;
                string cuenta = mi.Header.ToString();
                c = MainWindow.u.RepositorioCuentasBancarias.Single(a => a.numerocuenta == cuenta);
                InfoCuenta ic = new InfoCuenta(c);
                ic.Show();
                this.Close();
            }
        }

        private void menuItemG_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            string cuenta = mi.Header.ToString();
            c = MainWindow.u.RepositorioCuentasBancarias.Single(a => a.numerocuenta == cuenta);
            RealizarGasto rg = new RealizarGasto(c);
            rg.Show();
            this.Close();
        }

        private void menuItemI_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            string cuenta = mi.Header.ToString();
            c = MainWindow.u.RepositorioCuentasBancarias.Single(a => a.numerocuenta == cuenta);

            RealizarIngreso ri = new RealizarIngreso(c);
            ri.Show();
            this.Close();
        }
        private void menuItemD_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            string cuenta = mi.Header.ToString();
            c = MainWindow.u.RepositorioCuentasBancarias.Single(a => a.numerocuenta == cuenta);
            DepositosYAcciones dya = new DepositosYAcciones(c);
            dya.Show();
            this.Close();
        }

        private void btnGasto_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            foreach (CuentaBancaria item in MainWindow.user.CuentasBancarias)
            {
                MenuItem mi = new MenuItem();
                mi.Header = item.numerocuenta;
                mi.ToolTip = item.numerocuenta.ToString();
                mi.Click += menuItemG_Click;
                cm.Items.Add(mi);
            }
            cm.Visibility = Visibility.Visible;
            cm.IsOpen = true;
        }

        private void btnIngreso_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            foreach (CuentaBancaria item in MainWindow.user.CuentasBancarias)
            {
                MenuItem mi = new MenuItem();
                mi.Header = item.numerocuenta;
                mi.ToolTip = item.numerocuenta.ToString();
                mi.Click += menuItemI_Click;
                cm.Items.Add(mi);
            }
            cm.Visibility = Visibility.Visible;
            cm.IsOpen = true;
        }

        private void btnAccion_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            foreach (CuentaBancaria item in MainWindow.user.CuentasBancarias)
            {
                MenuItem mi = new MenuItem();
                mi.Header = item.numerocuenta;
                mi.ToolTip = item.numerocuenta.ToString();
                mi.Click += menuItemD_Click;
                cm.Items.Add(mi);
            }
            cm.Visibility = Visibility.Visible;
            cm.IsOpen = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
    }
}
