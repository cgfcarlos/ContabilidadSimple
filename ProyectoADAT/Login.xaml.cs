using BespokeFusion;
using ProyectoADAT.DAL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoADAT
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static UnitOfWork u = new UnitOfWork();
        public static Usuario user = new Usuario();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            textblock.TextDecorations = TextDecorations.Underline;
            textblock.Cursor = Cursors.Hand;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            textblock.TextDecorations = null;
        }

        private void textblock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContraseñaOlvidada c = new ContraseñaOlvidada();
            c.ShowDialog();
        }

        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            if (u.RepositorioUsuarios.Single(a => a.nickUsuario == textBoxUser.Text || a.emailUsuario == textBoxUser.Text) != null)
            {
                if(u.RepositorioUsuarios.Single(a=>a.nickUsuario==textBoxUser.Text && a.passwordUsuario == passwordBox.Password) != null)
                {
                    user = u.RepositorioUsuarios.Single(a => a.nickUsuario == textBoxUser.Text);
                    SeleccionarOpcion so = new SeleccionarOpcion();
                    so.Show();
                    this.Close();
                }
                else
                {
                    MaterialMessageBox.ShowError("La contraseña y el usuario no son correctos.");
                }
            }
            else
            {
                MaterialMessageBox.ShowError("No existe el usuario "+textBoxUser.Text + " en la base de datos");
            }
        }

        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
            this.Close();
        }
    }
}
