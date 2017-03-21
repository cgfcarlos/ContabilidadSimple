﻿using BespokeFusion;
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
using System.Windows.Shapes;

namespace ProyectoADAT
{
    /// <summary>
    /// Lógica de interacción para Historial.xaml
    /// </summary>
    public partial class Historial : Window
    {
        CuentaBancaria cuenta = new CuentaBancaria();
        public Historial(CuentaBancaria c)
        {
            InitializeComponent();
            this.cuenta = c;
            dataGridGastos.ItemsSource = cuenta.Gastos;
            dataGridIngresos.ItemsSource = cuenta.Ingresos;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InfoCuenta ic = new InfoCuenta(cuenta);
            ic.Show();
        }
        private void buttonDeleteFilaI_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridIngresos.SelectedIndex > -1)
            {
                Ingreso i = (Ingreso)dataGridIngresos.SelectedItem;
                if (i.fechaOperacion.Month != DateTime.Now.Month)
                {
                    MainWindow.u.RepositorioIngresos.Delete(i);
                    dataGridIngresos.ItemsSource = "";
                    dataGridIngresos.ItemsSource = cuenta.Ingresos;
                }
                else
                {
                    MaterialMessageBox.ShowError("No se puede borrar un ingreso del mes en curso");
                }
            }
        }
        private void buttonDeleteFilaG_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridGastos.SelectedIndex > -1)
            {
                Gasto g = (Gasto)dataGridGastos.SelectedItem;
                if (g.fechaOperacion.Month != DateTime.Now.Month)
                {
                    MainWindow.u.RepositorioGastos.Delete(g);
                    dataGridGastos.ItemsSource = "";
                    dataGridGastos.ItemsSource = cuenta.Gastos;
                }
                else
                {
                    MaterialMessageBox.ShowError("No se puede borrar un ingreso del mes en curso");
                }
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            if(datepickerInicio.Text!="" && datepickerFin.Text == "")
            {
                dataGridIngresos.ItemsSource = "";
                dataGridGastos.ItemsSource = "";
                dataGridGastos.ItemsSource = MainWindow.u.RepositorioGastos.GetAll().Where(a => a.CuentaBancaria==cuenta && a.fechaOperacion>=Convert.ToDateTime(datepickerInicio.Text));
                dataGridIngresos.ItemsSource = MainWindow.u.RepositorioIngresos.GetAll().Where(a => a.CuentaBancaria == cuenta && a.fechaOperacion>=Convert.ToDateTime(datepickerInicio.Text));
            }
            else if(datepickerFin.Text != "" && datepickerInicio.Text == "")
            {
                dataGridIngresos.ItemsSource = "";
                dataGridGastos.ItemsSource = "";
                dataGridGastos.ItemsSource = MainWindow.u.RepositorioGastos.GetAll().Where(a => a.CuentaBancaria == cuenta && a.fechaOperacion <= Convert.ToDateTime(datepickerFin.Text));
                dataGridIngresos.ItemsSource = MainWindow.u.RepositorioIngresos.GetAll().Where(a => a.CuentaBancaria == cuenta && a.fechaOperacion <= Convert.ToDateTime(datepickerFin.Text));
            }
            else if(datepickerFin.Text != "" && datepickerInicio.Text != "")
            {
                dataGridIngresos.ItemsSource = "";
                dataGridGastos.ItemsSource = "";
                dataGridGastos.ItemsSource = MainWindow.u.RepositorioGastos.GetAll().Where(a => a.CuentaBancaria == cuenta && a.fechaOperacion>=Convert.ToDateTime(datepickerInicio.Text) && a.fechaOperacion <= Convert.ToDateTime(datepickerFin.Text));
                dataGridIngresos.ItemsSource = MainWindow.u.RepositorioIngresos.GetAll().Where(a => a.CuentaBancaria == cuenta && a.fechaOperacion>=Convert.ToDateTime(datepickerInicio.Text) && a.fechaOperacion <= Convert.ToDateTime(datepickerFin.Text));
            }
            else
            {
                dataGridIngresos.ItemsSource = "";
                dataGridGastos.ItemsSource = "";
                dataGridGastos.ItemsSource = cuenta.Gastos;
                dataGridIngresos.ItemsSource = cuenta.Ingresos;
            }
            
        }

        private void textBoxConcepto_TextChanged(object sender, TextChangedEventArgs e)
        {

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
    }
}
