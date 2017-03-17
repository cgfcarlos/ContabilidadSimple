using BespokeFusion;
using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                    string subject = "Reestablecer Contraseña";
                    string body = @"Bienvenido! \r Ha solicitado que le enviemos de nuevo la contraseña. \r Su contraseña es: " + MainWindow.u.RepositorioUsuarios.Single(a => a.emailUsuario == textBoxEmail.Text).passwordUsuario;
                    Process.Start(string.Format("mailto:{0}?subject={1}&body={2}", textBoxEmail.Text, subject, body));

                    /*MailMessage mail = new MailMessage("pedorapcarlos@gmail.com", MainWindow.u.RepositorioUsuarios.GetAll().Single(a => a.emailUsuario == textBoxEmail.Text).emailUsuario);
                    SmtpClient client = new SmtpClient("pedorapcarlos@gmail.com");
                    mail.Subject = "Reestablecer Contraseña";
                    mail.Body = @"Bienvenido! \r Ha solicitado que le enviemos de nuevo la contraseña. \r Su contraseña es: " + MainWindow.u.RepositorioUsuarios.Single(a => a.emailUsuario == textBoxEmail.Text).passwordUsuario;
                    client.Port = 465;
                    client.UseDefaultCredentials = true;
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Host = "smtp.google.com";
                    client.Send(mail);*/

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
