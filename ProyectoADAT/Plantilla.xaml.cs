using Excel;
using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Lógica de interacción para Plantilla.xaml
    /// </summary>
    public partial class Plantilla : Window
    {
        CuentaBancaria cuenta = new CuentaBancaria();
        public Plantilla(CuentaBancaria c)
        {
            InitializeComponent();
            this.cuenta = c;
        }
        DataSet result;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = File.Open(System.Environment.CurrentDirectory+"../../../Plantilla/plantilla.xls", FileMode.Open, FileAccess.Read);
            IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(fs);
            reader.IsFirstRowAsColumnNames = true;
            result = reader.AsDataSet();
            cmbTablaExcel.Items.Clear();
            foreach (DataTable item in result.Tables)
            {
                cmbTablaExcel.Items.Add(item.TableName);
            }
            reader.Close();
        }

        List<PlantillaIngresos> listPI = new List<PlantillaIngresos>();
        List<PlantillaGastos> listPG = new List<PlantillaGastos>();

        private void cmbTablaExcel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTablaExcel.SelectedIndex > -1)
            {
                listPI.Clear();
                listPG.Clear();
                foreach (DataRow item in result.Tables[cmbTablaExcel.SelectedIndex].Rows)
                {
                    if (cmbTablaExcel.SelectedIndex == 0)//Ingreso
                    {
                        PlantillaIngresos pI = new PlantillaIngresos();
                        pI.ingresos = item[0].ToString();
                        pI.semana1 = item[1].ToString();
                        pI.semana2 = item[2].ToString();
                        pI.semana3 = item[3].ToString();
                        pI.semana4 = item[4].ToString();
                        pI.semana5 = item[5].ToString();
                        pI.semana6 = item[6].ToString();
                        pI.totales = item[7].ToString();
                        listPI.Add(pI);
                    }
                    else//Gasto
                    {
                        PlantillaGastos pG = new PlantillaGastos();
                        pG.gastos = item[0].ToString();
                        pG.semana1 = item[1].ToString();
                        pG.semana2 = item[2].ToString();
                        pG.semana3 = item[3].ToString();
                        pG.semana4 = item[4].ToString();
                        pG.semana5 = item[5].ToString();
                        pG.semana6 = item[6].ToString();
                        pG.totales = item[7].ToString();
                        listPG.Add(pG);
                    }
                }

                if (cmbTablaExcel.SelectedIndex == 0)
                {
                    dataGridExcel.ItemsSource = listPI;
                }
                else
                {
                    dataGridExcel.ItemsSource = listPG;
                }

            }
        }

        private void btnCalculadora_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InfoCuenta ic = new InfoCuenta(cuenta);
            ic.Show();
        }
    }

    public class PlantillaIngresos
    {
        public string ingresos { get; set; }
        public string semana1 { get; set; }
        public string semana2 { get; set; }
        public string semana3 { get; set; }
        public string semana4 { get; set; }
        public string semana5 { get; set; }
        public string semana6 { get; set; }
        public string totales { get; set; }

    }

    public class PlantillaGastos
    {
        public string gastos { get; set; }
        public string semana1 { get; set; }
        public string semana2 { get; set; }
        public string semana3 { get; set; }
        public string semana4 { get; set; }
        public string semana5 { get; set; }
        public string semana6 { get; set; }
        public string totales { get; set; }

    }
}
