using iTextSharp.text;
using iTextSharp.text.pdf;
using ProyectoADAT.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Web;
using System.Web.UI;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using BespokeFusion;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;

namespace ProyectoADAT
{
    /// <summary>
    /// Lógica de interacción para InfoCuenta.xaml
    /// </summary>
    public partial class InfoCuenta : System.Windows.Window
    {
        CuentaBancaria c = new CuentaBancaria();
        List<IngresosYGastos> ig;
        decimal cuantiaRes;
        decimal aux;
        public InfoCuenta(CuentaBancaria cuenta)
        {
            InitializeComponent();
            this.c = cuenta;
            DataContext = c;
            cuantiaRes = c.saldo;
            aux = c.saldo;
            if (c.saldo < 0)
            {
                label_Copy2.Foreground = Brushes.Red;
            }
            else
            {
                label_Copy2.Foreground = Brushes.Green;
            }
            if (c.Usuario.imagenUsuario != null && !String.IsNullOrWhiteSpace(c.Usuario.imagenUsuario))
            {
                image.Source = new BitmapImage(new Uri(System.Environment.CurrentDirectory + "../../../Imagenes/" + c.Usuario.imagenUsuario));
            }
            Iniciar();
        }


        private void Asignar()
        {
            label_Copy2.Content = c.saldo;
            aux = c.saldo;
            if (c.saldo < 0)
            {
                label_Copy2.Foreground = Brushes.Red;
            }
            else
            {
                label_Copy2.Foreground = Brushes.Green;
            }
        }

        private void Iniciar()
        {
            //Mostrar datagrid
            ig = new List<IngresosYGastos>();
            decimal mediaIngresos = 0;
            decimal mediaGastos = 0;
            foreach (Gasto item in c.Gastos)
            {
                if (item.fechaValor.Month == DateTime.Now.Month)
                { 
                    IngresosYGastos inggas = new IngresosYGastos(item.nombreGasto, item.tipoGasto, item.cuantia, item.fechaOperacion, item.fechaValor);
                    ig.Add(inggas);
                    mediaGastos += item.cuantia;
                }
            }
            foreach (Ingreso item in c.Ingresos)
            {
                if (item.fechaValor.Month == DateTime.Now.Month)
                {
                    IngresosYGastos inggas = new IngresosYGastos(item.nombreIngreso, item.tipoIngreso, item.cuantia, item.fechaOperacion, item.fechaValor);
                    ig.Add(inggas);
                    mediaIngresos += item.cuantia;
                }
            }

            //Graficos de estadisticas media ingresos/gastos
            System.Windows.Forms.DataVisualization.Charting.Chart chart = this.FindName("MyWinformChart") as System.Windows.Forms.DataVisualization.Charting.Chart;
            List<Ingreso> ingresosMesActual = MainWindow.u.RepositorioIngresos.Get(a => a.fechaOperacion.Month == DateTime.Now.Month);
            List<Gasto> gastosMesActual = MainWindow.u.RepositorioGastos.Get(a => a.fechaOperacion.Month == DateTime.Now.Month);

            mediaGastos = mediaGastos*-1 ;
            //mediaIngresos = mediaIngresos / ingresosMesActual.Count;

            ig = ig.OrderBy(a => a.FechaOperacion).ToList();
            dataGrid.ItemsSource = ig;

            chart.Series["Ingresos"].Points.AddXY("Ingresos", mediaIngresos);
            chart.Series["Ingresos"].Points.AddXY("Gastos", mediaGastos);

            DataSet dataSet = MyWinformChart.DataManipulator.ExportSeriesValues("Ingresos");
            chart.DataSource = dataSet;

            //Lista de pdfs de la cuenta bancaria de un usuario
            cargarLista();

        }
        private void cargarLista()
        {
            //Cargar pdfs en listBox
            DirectoryInfo d = new DirectoryInfo(@".\..\..\Usuarios" + @"\" + c.numeroCuenta);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pdf");
            foreach (FileInfo item in Files)
            {
                listBox.Items.Add(item.Name);
            }
        }

        private void dataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(DataGridRow))
            {
                DataGridRow row = (DataGridRow)sender;
                row.Background = Brushes.Beige;
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex > -1)
            {
                string ruta = @"..\..\Usuarios\" + c.numeroCuenta + @"\" + listBox.SelectedItem.ToString();
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = "acroRd32.exe";
                myProcess.StartInfo.Arguments = ruta;
                myProcess.Start();
            }
        }

        private void MenuRealizarExtracto_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r =MaterialMessageBox.ShowWithCancel("¿Desea realizar el extracto de este mes?", "Advertencia");
            if (r == MessageBoxResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();
                    Byte[] bytes;
                    using (StringWriter sw = new StringWriter())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (Document document = new Document())
                            {
                                using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
                                {
                                    document.Open();
                                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                                    {
                                        int orderNo = 1234;
                                        string companyName = c.entidadCuenta;
                                        sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                                        sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Extracto Bancario</b></td></tr>");
                                        sb.Append("<tr><td colspan = '2'></td></tr>");
                                        sb.Append("<tr><td><b>Numero Operacion: </b>");
                                        sb.Append(orderNo);
                                        sb.Append("</td><td align = 'right'><b>Fecha: </b>");
                                        sb.Append(DateTime.Now.Date.ToShortDateString());
                                        sb.Append("</td></tr>");
                                        sb.Append("<tr><td colspan = '2'><b>Entidad Bancaria: </b>");
                                        sb.Append(companyName);
                                        sb.Append("</td></tr>");
                                        sb.Append("<tr><td colspan = '2'><b>Titular: </b>");
                                        sb.Append(c.titularCuenta);
                                        sb.Append("</td></td>");
                                        sb.Append("</table>");
                                        sb.Append("<br />");
                                        //sb2
                                        sb2.Append("<br />");

                                        sb2.Append("<table border = '1'>");
                                        sb2.Append("<tr>");
                                       
                                        sb2.Append("<th>Fecha</th>");
                                        sb2.Append("<th>Nombre</th>");
                                        sb2.Append("<th>Operación</th>");
                                        sb2.Append("<th>Cuantia</th>");
                                        sb2.Append("<th>Saldo</th>");
                                        
                                        sb2.Append("</tr>");
                                        
                                        foreach (IngresosYGastos item in dataGrid.Items)
                                        {
                                            sb2.Append("<tr>");
                                            sb2.Append("<td>");
                                            sb2.Append(item.FechaOperacion);
                                            sb2.Append("</td>");
                                            sb2.Append("<td>");
                                            sb2.Append(item.Nombre);
                                            sb2.Append("</td>");
                                            sb2.Append("<td>");
                                            sb2.Append(item.FechaValor);
                                            sb2.Append("</td>");
                                            sb2.Append("<td>");
                                            sb2.Append(item.Cuantia);
                                            sb2.Append("</td>");
                                            sb2.Append("<td>");
                                            sb2.Append((cuantiaRes+item.Cuantia));
                                            sb2.Append("</td>");
                                            sb2.Append("</tr>");
                                            cuantiaRes = cuantiaRes + item.Cuantia;
                                        }
                                        sb2.Append("</tr></table>");
                                    }
                                    using (HTMLWorker htmlWorker = new HTMLWorker(document))
                                    {
                                        StringReader sr1 = new StringReader(sb.ToString());

                                        htmlWorker.Parse(sr1);

                                        PdfContentByte cb = writer.DirectContent;
                                        cb.MoveTo(30, document.Top - 115f);
                                        cb.LineTo(563, document.Top - 115f);
                                        cb.Stroke();
                                        sr1 = new StringReader(sb2.ToString());
                                        htmlWorker.Parse(sr1);

                                    }
                                    document.Close();
                                }
                            }
                            bytes = ms.ToArray();
                        }
                    }
                    string testFile = "./../../Usuarios/" + c.numeroCuenta + "/" + "Extracto_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + ".pdf";
                    System.IO.File.WriteAllBytes(testFile, bytes);
                    c.saldo = cuantiaRes;
                    MainWindow.u.RepositorioCuentasBancarias.Update(c);
                    Asignar();
                    listBox.Items.Clear();
                    cargarLista();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void MenuExcel_Click(object sender, RoutedEventArgs e)
        {
            // Crear un objeto SqlConnection, y luego pasar la ConnectionString al constructor.            
            string CadenaString = "Data Source=.;Initial Catalog=Contabilidad;Integrated Security=SSPI;";
            SqlConnection Conection = new SqlConnection(CadenaString);

            // Utilizar una variable para almacenar la instrucción SQL.
            
            // metadata=res://*/Model1.csdl | res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.;initial catalog=CentroMedico;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient"*/
            string SelectString = "SELECT Ingresoes.fechaOperacion, Ingresoes.nombreIngreso, Ingresoes.fechaValor, Ingresoes.cuantia FROM Ingresoes UNION SELECT Gastoes.fechaOperacion, Gastoes.nombreGasto, Gastoes.fechaValor, Gastoes.cuantia FROM Gastoes";

            SqlDataAdapter Adaptador = new SqlDataAdapter(SelectString, Conection);

            DataSet DS = new DataSet();

            // Abrir la conexión.
            Conection.Open();
            Adaptador.Fill(DS);
            Conection.Close();

            // Creamos un objeto Excel.
            Microsoft.Office.Interop.Excel.Application Mi_Excel = default(Microsoft.Office.Interop.Excel.Application);
            // Creamos un objeto WorkBook. Para crear el documento Excel.           
            Workbook LibroExcel = default(Workbook);
            // Creamos un objeto WorkSheet. Para crear la hoja del documento.
            Worksheet HojaExcel = default(Worksheet);

            // Iniciamos una instancia a Excel, y Hacemos visibles para ver como se va creando el reporte, 
            // podemos hacerlo visible al final si se desea.
            Mi_Excel = new Microsoft.Office.Interop.Excel.Application();
            Mi_Excel.Visible = true;

            /* Ahora creamos un nuevo documento y seleccionamos la primera hoja del 
             * documento en la cual crearemos nuestro informe. 
             */
            // Creamos una instancia del Workbooks de excel.            
            LibroExcel = Mi_Excel.Workbooks.Add();
            // Creamos una instancia de la primera hoja de trabajo de excel            
            HojaExcel = LibroExcel.Worksheets[1];
            HojaExcel.Visible = XlSheetVisibility.xlSheetVisible;

            // Hacemos esta hoja la visible en pantalla 
            // (como seleccionamos la primera esto no es necesario
            // si seleccionamos una diferente a la primera si lo
            // necesitariamos).
            HojaExcel.Activate();

            // Crear el encabezado de nuestro informe.
            // La primera línea une las celdas y las convierte un en una sola.            
            HojaExcel.Range["A1:E1"].Merge();
            // La segunda línea Asigna el nombre del encabezado.
            HojaExcel.Range["A1:E1"].Value = "----------------------------------------------";
            // La tercera línea asigna negrita al titulo.
            HojaExcel.Range["A1:E1"].Font.Bold = true;
            // La cuarta línea signa un Size a titulo de 15.
            HojaExcel.Range["A1:E1"].Font.Size = 15;

            // Crear el subencabezado de nuestro informe
            HojaExcel.Range["A2:E2"].Merge();
            HojaExcel.Range["A2:E2"].Value = "EXTRACTO BANCARIO";
            HojaExcel.Range["A2:E2"].Font.Bold = true;
            HojaExcel.Range["A2:E2"].Font.Size = 13;

            Range objCelda = HojaExcel.Range["A3", Type.Missing];
            objCelda.Value = "Fecha";

            objCelda = HojaExcel.Range["B3", Type.Missing];
            objCelda.Value = "Concepto";

            objCelda = HojaExcel.Range["C3", Type.Missing];
            objCelda.Value = "Valor";

            objCelda = HojaExcel.Range["D3", Type.Missing];
            objCelda.Value = "Importe";

            objCelda = HojaExcel.Range["E3", Type.Missing];
            objCelda.Value = "Saldo";

            objCelda.EntireColumn.NumberFormat = "###,###,###.00";

            int i = 4;
            
            foreach (DataRow Row in DS.Tables[0].Rows)
            {
                // Asignar los valores de los registros a las celdas

                // Fecha Operacion
                HojaExcel.Cells[i, "A"] = Row.ItemArray[0];
                // Concepto
                HojaExcel.Cells[i, "B"] = Row.ItemArray[1];
                // Fecha Valor
                HojaExcel.Cells[i, "C"] = Row.ItemArray[2];
                // Importe
                HojaExcel.Cells[i, "D"] = Row.ItemArray[3];
                // Saldo
                HojaExcel.Cells[i, "E"] = aux+Convert.ToDecimal(Row.ItemArray[3]);

                aux=aux + Convert.ToDecimal(Row.ItemArray[3]);
                // Avanzamos una fila
                i++;
            }

            // Seleccionar todo el bloque desde A1 hasta D #de filas.
            Range Rango = HojaExcel.Range["A3:E" + (i - 1).ToString()];

            // Selecionado todo el rango especificado
            Rango.Select();

            // Ajustamos el ancho de las columnas al ancho máximo del
            // contenido de sus celdas
            Rango.Columns.AutoFit();

            // Asignar filtro por columna
            Rango.AutoFilter(1);

            // Crear un total general
            //LibroExcel.PrintPreview();
        }

        private void panelOculto_MouseLeave(object sender, MouseEventArgs e)
        {
            panelOculto.Visibility = Visibility.Hidden;
        }

        private void Menuplus_Click(object sender, RoutedEventArgs e)
        {
            panelOculto.Visibility = Visibility.Visible;
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            Configuracion conf = new Configuracion(c.Usuario);
            conf.Show();
            this.Close();
        }

        private void radioLineas_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.Chart chart = this.FindName("MyWinformChart") as System.Windows.Forms.DataVisualization.Charting.Chart;
            chart.Series[0].ChartType = SeriesChartType.Line;
            //chart.Series[1].ChartType = SeriesChartType.Line;
        }

        private void radioColumnas_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.Chart chart = this.FindName("MyWinformChart") as System.Windows.Forms.DataVisualization.Charting.Chart;
            chart.Series[0].ChartType = SeriesChartType.Column;
            //chart.Series[1].ChartType = SeriesChartType.Column;
        }

        private void radioCircular_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.Chart chart = this.FindName("MyWinformChart") as System.Windows.Forms.DataVisualization.Charting.Chart;
            chart.Series[0].ChartType = SeriesChartType.Pie;
            //chart.Series[0].Points.AddXY(chart.Series[1].Points[0].XValue, chart.Series[1].Points[0].YValues[0]);
            //chart.Series[1].ChartType = SeriesChartType.Pie;
        }

        private void radioBarras_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.Chart chart = this.FindName("MyWinformChart") as System.Windows.Forms.DataVisualization.Charting.Chart;
            chart.Series[0].ChartType = SeriesChartType.Bar;
            //chart.Series[1].ChartType = SeriesChartType.Bar;
        }

        private void radioRango_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.Chart chart = this.FindName("MyWinformChart") as System.Windows.Forms.DataVisualization.Charting.Chart;
            chart.Series[0].ChartType = SeriesChartType.Range;
            //chart.Series[1].ChartType = SeriesChartType.Range;
        }

        private void Atras_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarOpcion so = new SeleccionarOpcion();
            so.Show();
            this.Close();
        }

        private void MenuHistorial_Click(object sender, RoutedEventArgs e)
        {
            Historial h = new Historial(c);
            h.Show();
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DepositosYAcciones dp = new DepositosYAcciones(c);
            dp.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            RealizarGasto rg = new RealizarGasto(c);
            rg.Show();
            this.Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            RealizarIngreso ri = new RealizarIngreso(c);
            ri.Show();
            this.Close();
        }

        private void MenuPlantilla_Click(object sender, RoutedEventArgs e)
        {
            Plantilla p = new Plantilla();
            p.Show();
            this.Close();
        }
    }

    public class IngresosYGastos{

        private string nombre;
        private string tipo;
        private decimal cuantia;
        private DateTime fechaOperacion;
        private DateTime fechaValor;

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }

        public decimal Cuantia
        {
            get
            {
                return cuantia;
            }

            set
            {
                cuantia = value;
            }
        }

        public DateTime FechaOperacion
        {
            get
            {
                return fechaOperacion;
            }

            set
            {
                fechaOperacion = value;
            }
        }

        public DateTime FechaValor
        {
            get
            {
                return fechaValor;
            }

            set
            {
                fechaValor = value;
            }
        }

        public IngresosYGastos(string nombre, string tipo, decimal cuantia, DateTime fechaOperacion, DateTime fechaValor)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.cuantia = cuantia;
            this.fechaOperacion = fechaOperacion;
            this.fechaValor = fechaValor;
        }
    }
}
