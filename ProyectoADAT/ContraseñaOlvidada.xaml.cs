using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
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
                MailMessage mail = new MailMessage("carloscrg@hotmail.es", MainWindow.u.RepositorioUsuarios.GetAll().Single(a=>a.emailUsuario==textBoxEmail.Text).emailUsuario);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.google.com";
                mail.Subject = "Reestablecer Contraseña";
                mail.Body = "Bienvenido! \r Ha solicitado que le enviemos de nuevo la contraseña. \r Su contraseña es: "+ MainWindow.u.RepositorioUsuarios.Single(a => a.emailUsuario==textBoxEmail.Text).passwordUsuario;
                client.Send(mail);
            }

            
        }
    }
}
