using BespokeFusion;
using ProyectoADAT.Model;
using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Lógica de interacción para RealizarIngreso.xaml
    /// </summary>
    public partial class RealizarIngreso : Window
    {
        CuentaBancaria c;
        Ingreso i = new Ingreso();
        public RealizarIngreso(CuentaBancaria c)
        {
            InitializeComponent();
            this.c = c;
            DataContext = i;
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
            if (!String.IsNullOrWhiteSpace(textBoxNombre.Text) && !String.IsNullOrWhiteSpace(textBoxTipo.Text) && !String.IsNullOrWhiteSpace(textBoxCuantia.Text) && datePickerFechaOp.SelectedDate != null && datePickerFechaVa.SelectedDate != null)
            {
                if (char.IsDigit(textBoxCuantia.Text[0]))
                {
                    i = new Ingreso();
                    i.nombreIngreso = textBoxNombre.Text;
                    i.tipoIngreso = textBoxTipo.Text;
                    decimal aux;
                    Decimal.TryParse(textBoxCuantia.Text.Replace('.', ','), out aux);
                    i.cuantia = Convert.ToDecimal(aux);
                    i.fechaOperacion = Convert.ToDateTime(datePickerFechaOp.Text);
                    i.fechaValor = Convert.ToDateTime(datePickerFechaVa.Text);
                    i.CuentaBancaria = c;
                    if (validado(i))
                    {
                        MainWindow.u.RepositorioIngresos.Create(i);
                        //InfoCuenta ic = new InfoCuenta(c);
                        //ic.Show();
                        this.Close();
                    }
                }
                else
                {
                    MaterialMessageBox.ShowError("Rellene correctamente los campos");
                }
                
            }
            else
            {
                MaterialMessageBox.ShowError("Rellene los campos");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InfoCuenta ic = new InfoCuenta(c);
            ic.Show();
            //this.Close();
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

        private void textBoxCuantia_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!textBoxCuantia.Text.Contains(','))
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

        private void datePickerFechaOp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(datePickerFechaOp.SelectedDate!=null)
                datePickerFechaVa.SelectedDate = datePickerFechaOp.SelectedDate;
        }
    }
}
