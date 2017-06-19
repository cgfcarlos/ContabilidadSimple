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
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        Usuario user;
        public Registro()
        {
            InitializeComponent();
            comboBoxNumCuentas.Items.Add("1");
            comboBoxNumCuentas.Items.Add("2");
            comboBoxNumCuentas.Items.Add("3");
            DataContext = user;
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxApellidos.Text) && !String.IsNullOrWhiteSpace(textBoxDNI.Text) && !String.IsNullOrWhiteSpace(textBoxEmail.Text) && !String.IsNullOrWhiteSpace(textBoxNick.Text) && !String.IsNullOrWhiteSpace(textBoxNombre.Text) && !String.IsNullOrWhiteSpace(textBoxPassword.Password) && !String.IsNullOrWhiteSpace(textBoxTlf.Text) && comboBoxNumCuentas.SelectedIndex>-1)
            {
                if (MainWindow.u.RepositorioUsuarios.Single(a => a.nickusuario==textBoxNick.Text)==null)
                {
                    user = new Usuario();
                    user.dniusuario = textBoxDNI.Text;
                    user.nombreusuario = textBoxNombre.Text;
                    user.apellidosusuario = textBoxApellidos.Text;
                    user.nickusuario = textBoxNick.Text;
                    user.passwordusuario = textBoxPassword.Password;
                    user.emailusuario = textBoxEmail.Text;
                    user.tlfusuario = textBoxTlf.Text;
                    if (validado(user))
                    {
                        MainWindow.u.RepositorioUsuarios.Create(user);
                        if (image.Source != null && !String.IsNullOrWhiteSpace(filename))
                        {
                            if (!File.Exists("./../../../Imagenes/" + filename))
                            {
                                File.Copy(image.Source.ToString(), "./../../../Imagenes/" + filename);
                            }
                            else
                            {
                                //File.Copy(image.Source.ToString(), System.Environment.CurrentDirectory + "../../../Imagenes/" + filename.Split('.')[0] + DateTime.Now.Millisecond + filename.Split('.')[1]);
                            }
                            //user.imagenUsuario = filename;
                        }
                        for (int i = 0; i < Convert.ToInt32(comboBoxNumCuentas.SelectedItem.ToString()); i++)
                        {
                            Usuario usuario = MainWindow.u.RepositorioUsuarios.Single(a => a.nickusuario == textBoxNick.Text);
                            RegistroCuenta rc = new RegistroCuenta(usuario);
                            rc.ShowDialog();
                        }
                        MainWindow.user = MainWindow.u.RepositorioUsuarios.Single(a => a.nickusuario == textBoxNick.Text);
                        if (!Directory.Exists(@".\..\..\Usuarios"))
                        {
                            Directory.CreateDirectory(@".\..\..\Usuarios");
                        }
                        MaterialMessageBox.Show("Se ha añadido el usuario correctamente", "Advertencia");
                        SeleccionarOpcion so = new SeleccionarOpcion();
                        MainWindow.user = MainWindow.u.RepositorioUsuarios.Single(a => a.nickusuario == textBoxNick.Text);
                        so.Show();
                        this.Close();
                    }
                }
                else
                {
                    MaterialMessageBox.ShowError("Ya existe ese nick o email en la base de datos, modifique los campos.");
                }
            }
            else
            {
                MaterialMessageBox.ShowError("Rellene los campos");
            }
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effects = DragDropEffects.All;
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string[] droppedfiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in droppedfiles)
            {
                image.Source = new BitmapImage(new Uri(file));
                filename = file;
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
        string filename = "";
        private void buttonImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.ShowDialog();
            filename = open.SafeFileName;
            if (!String.IsNullOrWhiteSpace(filename))
            {
                image.Source =new BitmapImage(new Uri(open.FileName));
            }
        }
        private void buttonAtras_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
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
}
