using BespokeFusion;
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
    /// Lógica de interacción para RegistroCuenta.xaml
    /// </summary>
    public partial class RegistroCuenta : Window
    {
        Usuario user=new Usuario();
        CuentaBancaria cuenta = new CuentaBancaria();
        public RegistroCuenta(Usuario u)
        {
            InitializeComponent();
            this.user = u;
            DataContext = cuenta;
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
            if (!String.IsNullOrWhiteSpace(textBoxBIC.Text) && !String.IsNullOrWhiteSpace(textBoxEntidad.Text) && !String.IsNullOrWhiteSpace(textBoxNumeroCuenta.Text) && !String.IsNullOrWhiteSpace(textBoxPaisDomiciliacion.Text) && !String.IsNullOrWhiteSpace(textBoxSaldo.Text) && !String.IsNullOrWhiteSpace(textBoxTipo.Text) && !String.IsNullOrWhiteSpace(textBoxTitular.Text))
            {


                if (MainWindow.u.RepositorioCuentasBancarias.Single(a => a.numerocuenta == textBoxNumeroCuenta.Text) == null)
                {
                    if(char.IsDigit(textBoxSaldo.Text[0]))
                        cuenta = new CuentaBancaria();
                        cuenta.numerocuenta = textBoxNumeroCuenta.Text;
                        cuenta.titularcuenta = textBoxTitular.Text;
                        cuenta.entidadcuenta = textBoxEntidad.Text;
                        cuenta.tipocuenta = textBoxTipo.Text;
                        cuenta.paisdomiciliacion = textBoxPaisDomiciliacion.Text;
                        cuenta.bic = textBoxBIC.Text;
                        
                        cuenta.saldo = Convert.ToDecimal(textBoxSaldo.Text);
                        cuenta.Usuario = user;
                    if (validado(cuenta))
                    {
                        MainWindow.u.RepositorioCuentasBancarias.Create(cuenta);
                        if (!Directory.Exists(@".\..\..\Usuarios\" + cuenta.numerocuenta))
                        {
                            Directory.CreateDirectory(@".\..\..\Usuarios\" + cuenta.numerocuenta);
                        }
                        this.Close();
                    }
                    else
                    {
                        MaterialMessageBox.ShowError("Rellene los campos correctamente");
                    }
                }
                else
                {
                    MaterialMessageBox.ShowError("La cuenta bancaria ya esta asignada a un usuario, modifique los campos");
                }

            }
            else
            {
                MaterialMessageBox.ShowError("Rellene los campos!");
            }
            
            
        }

        private void textBoxSaldo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!textBoxSaldo.Text.Contains(','))
            {
                if (!char.IsNumber(e.Text, e.Text.Length - 1) && !(e.Text[e.Text.Length - 1] == ','))
                    e.Handled = true;
            }
            else
            {
                if (!char.IsNumber(e.Text, e.Text.Length - 1))
                    e.Handled = true;
            }
        }

        private void textBoxSaldo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
