using BespokeFusion;
using Microsoft.Win32;
using ProyectoADAT.Model;
using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Window
    {
        Usuario user= new Usuario();
        string path="";
        public Configuracion(Usuario u)
        {
            InitializeComponent();
            this.user = u;
            DataContext = user;
            //filename = user.imagenUsuario;
            //if (user.imagenUsuario != null && filename!="")
            //{
            //    image.Source = new BitmapImage(new Uri(System.Environment.CurrentDirectory + "../../../Imagenes/" + u.imagenUsuario));
            //}
            textBoxPassword.Password = user.passwordusuario;
            dataGridCuentas.ItemsSource = user.CuentasBancarias;

        }

        
         private Boolean validado(Object obj)
        {

            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj, null, null);
            List<System.ComponentModel.DataAnnotations.ValidationResult> errors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            Validator.TryValidateObject(obj, validationContext, errors, true);

            if (errors.Count() > 0)
            {

                string mensageErrores = string.Empty;
                foreach (var error in errors)
                {
                    error.MemberNames.First();

                    mensageErrores += error.ErrorMessage + Environment.NewLine;
                }
                MaterialMessageBox.ShowError(mensageErrores); return false;
            }
            else
            {
                return true;


            }

        }

        private void buttonModificar_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxNick.Text) && !String.IsNullOrWhiteSpace(textBoxPassword.Password) && !String.IsNullOrWhiteSpace(textBoxEmail.Text))
            {


                MessageBoxResult r = MaterialMessageBox.ShowWithCancel("Desea modificar los datos?");
                if (r == MessageBoxResult.OK)
                {
                    user.passwordusuario = textBoxPassword.Password;
                    if (validado(user))
                    {
                        /*if (!File.Exists(System.Environment.CurrentDirectory + "../../../Imagenes/" + filename))
                        {
                            File.Copy(path, System.Environment.CurrentDirectory + "../../../Imagenes/" + filename);
                        }
                        else
                        {
                            File.Copy(path, System.Environment.CurrentDirectory + "../../../Imagenes/" + filename.Split('.')[0] + DateTime.Now.Millisecond + filename.Split('.')[1]);
                        }*/
                        MainWindow.u.RepositorioUsuarios.Update(user);
                    }
                }
            }
            else
            {
                MaterialMessageBox.ShowError("Rellene los campos");
            }
        }
        private void buttonDeleteFila_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCuentas.SelectedIndex > -1)
            {
                CuentaBancaria c = (CuentaBancaria)dataGridCuentas.SelectedItem;
                MessageBoxResult r = MaterialMessageBox.ShowWithCancel("Desea desvincular la cuenta corriente seleccionada?");
                if (r == MessageBoxResult.OK)
                {
                    Directory.Delete(System.Environment.CurrentDirectory + "../../../Usuarios/" + c.numerocuenta);
                    MainWindow.u.RepositorioCuentasBancarias.Delete(c);
                    dataGridCuentas.ItemsSource = "";
                    dataGridCuentas.ItemsSource = user.CuentasBancarias;
                }
                
            }
        }

        private void image_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonImagen.Visibility = Visibility.Visible;
        }

        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonImagen.Visibility = Visibility.Hidden;
        }

        private void buttonImagen_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonImagen.Visibility = Visibility.Visible;
        }
        string filename ="";
        private void buttonImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.ShowDialog();
            filename = open.SafeFileName;
            path = open.FileName;
            if (!String.IsNullOrWhiteSpace(filename))
            {
                image.Source = new BitmapImage(new Uri(open.FileName));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SeleccionarOpcion so = new SeleccionarOpcion();
            so.Show();
        }

        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            Color c = Color.FromScRgb(100, 139, 195, 74);
            SolidColorBrush aux = new SolidColorBrush(c);
            button.Background = aux;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            button.Background.Opacity = 0;
        }

        private void buttonInforme_Click(object sender, RoutedEventArgs e)
        {
            Informes inf = new Informes(user);
            inf.ShowDialog();
        }
    }
}
