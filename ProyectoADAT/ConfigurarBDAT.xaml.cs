using ProyectoADAT.DAL;
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
    /// Lógica de interacción para ConfigurarBDAT.xaml
    /// </summary>
    public partial class ConfigurarBDAT : Window
    {
        public ConfigurarBDAT()
        {
            InitializeComponent();
            //comboBoxInstancia.ItemsSource=

        }
        public static ConfigurarConexion c = new ConfigurarConexion();
        private void button_Click(object sender, RoutedEventArgs e)
        {
            c.NombreBDAT = textBoxNombreBDAT.Text;
        }
    }
}
