using BespokeFusion;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ProyectoADAT;
using ProyectoADAT.DAL;
using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoADAT
{
    /// <summary>
    /// Lógica de interacción para Informes.xaml
    /// </summary>
    public partial class Informes : Window
    {
        Usuario user = new Usuario();
        public Informes(Usuario u)
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {


                Informe inf = new Informe();

                //informe.ViewerCore.ReportSource = inf;

                //string CadenaString = "Data Source=.;Initial Catalog=Contabilidad;Integrated Security=SSPI;";
                //SqlConnection Conection = new SqlConnection(CadenaString);

                //// Utilizar una variable para almacenar la instrucción SQL.

                //// metadata=res://*/Model1.csdl | res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.;initial catalog=CentroMedico;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient"*/
                //string SelectString = "SELECT Ingresoes.fechaOperacion, Ingresoes.nombreIngreso as 'Nombre', Ingresoes.fechaValor, Ingresoes.cuantia FROM Ingresoes join CuentaBancarias on Ingresoes.CuentaBancaria_CuentaBancariaId = CuentaBancariaId join Usuarios on UsuarioId = Usuario_UsuarioId Union select Gastoes.fechaOperacion, Gastoes.nombreGasto as 'Nombre', Gastoes.fechaValor, Gastoes.cuantia from Gastoes join CuentaBancarias on Gastoes.CuentaBancaria_CuentaBancariaId = CuentaBancariaId join Usuarios on UsuarioId = Usuario_UsuarioId WHERE UsuarioId ="+user.UsuarioId;

                //SqlDataAdapter Adaptador = new SqlDataAdapter(SelectString, Conection);

                

                //DataSet DS = new DataSet();
                
                //// Abrir la conexión.
                //Conection.Open();
                //Adaptador.Fill(DS);
                //Conection.Close();

                //DataTable dt = new DataTable();
                //dt.TableName = "Igresos_Gastos";
                //foreach (DataTable item in DS.Tables)
                //{
                //    dt.Columns.Add("Fecha Operacion", typeof(DateTime));
                //    dt.Columns.Add("Nombre", typeof(string));
                //    dt.Columns.Add("Fecha Valor", typeof(DateTime));
                //    dt.Columns.Add("Cuantia", typeof(decimal));
                //    foreach (DataRow item2 in item.Rows)
                //    {
                //        dt.Rows.Add();
                //    }
                //    int i = 0;
                //    foreach (DataRow item2 in item.Rows)
                //    {
                //        dt.Rows[i]["Fecha Operacion"] = item2.ItemArray[0];
                //        dt.Rows[i]["Nombre"]= item2.ItemArray[1];
                //        dt.Rows[i]["Fecha Valor"]= item2.ItemArray[2];
                //        dt.Rows[i]["Cuantia"]= item2.ItemArray[3];
                //    }
                //}

                //BindingSource bs = new BindingSource();
                //bs.DataSource = DS;
                //inf.RecordSelectionFormula = "{Usuarios.UsuarioId} = " + user.UsuarioId;
                //inf.SetDataSource(DS);
                informe.ViewerCore.ReportSource = inf;
                //informe.ViewerCore.RefreshReport();
            }
            catch(Exception ex)
            {
                MaterialMessageBox.ShowError("Ha ocurrido un problema inesperado");
            }
        }
    }
}
