using BespokeFusion;
using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Lógica de interacción para ContraseñaOlvidada.xaml
    /// </summary>
    public partial class ContraseñaOlvidada : Window
    {
        #region credencialesPrueba
        string email = "pedorapcarlos@hotmail.com";
        string password = "Meadero_0";
        #endregion

        public ContraseñaOlvidada()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.u.RepositorioUsuarios.GetAll().Where(a => a.emailUsuario == textBoxEmail.Text) != null)
            {
                try
                {
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587);
                    client.Credentials = new NetworkCredential(email, password);
                    client.EnableSsl = true;
                    MailMessage mail = new MailMessage("pedorapcarlos@hotmail.com", MainWindow.u.RepositorioUsuarios.Single(a => a.emailUsuario == textBoxEmail.Text).emailUsuario);
                    mail.Subject = "Reestablecer Contraseña";
                    mail.Body = @"Bienvenido!  Ha solicitado que le enviemos de nuevo la contraseña.  Su contraseña es: "+MainWindow.u.RepositorioUsuarios.Single(a => a.emailUsuario == textBoxEmail.Text).passwordUsuario;

                    client.Send(mail);

                    MaterialMessageBox.Show("Se ha enviado un correo a su bandeja de entrada.", "Informacón");
                }
                catch(Exception ex)
                {
                    MaterialMessageBox.ShowError(ex.ToString());
                }
            }
            else
            {
                MaterialMessageBox.ShowError("No existe ese email en la base de datos");
            }

            
        }
    }
}
