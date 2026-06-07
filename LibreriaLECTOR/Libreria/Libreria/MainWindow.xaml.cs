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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Libreria
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conexionSQL;
        public MainWindow()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["Libreria.Properties.Settings.BilbliotecaConnectionString"].ConnectionString;
            conexionSQL = new SqlConnection(conexion);
            muestraLectores();
            muestraLibros();
            muestraBibliotecarios();
            muestraPrestamos();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string quetaviendometiche = "este codigo solo se vera si estas datamineando"; 
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        //Ayuda
        private void botonAyudaS_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hay que llamar a un adulto, y los programadores son adultos pero mecates");
        }

        //Manda a la interfaz Bibliotecario
        private void botonBibliotecario_Click(object sender, RoutedEventArgs e)
        {
            Window1 bibliotecario = new Window1();
            bibliotecario.Show();
            this.Close();

        }

        //Manda a la interfaz Lector
        private void botonLector_Click(object sender, RoutedEventArgs e)
        {
            Lector lector = new Lector();
            lector.Show();
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            muestraLectores();
            muestraLibros();
            muestraBibliotecarios();
            muestraPrestamos();
        }

        //Manda a la interfaz Libro
        private void botonLibro_Click(object sender, RoutedEventArgs e)
        {
            Libro libro = new Libro();
            libro.Show();
            this.Close();
        }

        private void muestraLectores() {
            string consulta = "SELECT *, CONCAT ('ID:', Id_Lector, ' - Lector: ', Nombre, ' ', APaterno, ' ', AMaterno, ' ') AS INFO FROM Lector";
            SqlDataAdapter adaptadorSQL = new SqlDataAdapter(consulta, conexionSQL);
            using (adaptadorSQL)
            {
                DataTable tablaLectores = new DataTable();
                adaptadorSQL.Fill(tablaLectores);
                lista_Lectores.DisplayMemberPath = "INFO";
                lista_Lectores.SelectedValuePath = "Id_Lector";
                lista_Lectores.ItemsSource = tablaLectores.DefaultView;
            }
        }

        private void muestraBibliotecarios()
        {
            string consulta = "SELECT *, CONCAT ('ID:', Id_Bibliotecario, ' - Bibliotecario: ', Nombre, ' ', APaterno, ' ', AMaterno) AS INFO FROM Bibliotecario";
            SqlDataAdapter adaptadorSQL = new SqlDataAdapter(consulta, conexionSQL);
            using (adaptadorSQL)
            {
                DataTable bibliotecariosTabla = new DataTable();
                adaptadorSQL.Fill(bibliotecariosTabla);
                lista_Bib.DisplayMemberPath = "INFO";
                lista_Bib.SelectedValuePath = "Id_Bibliotecario";
                lista_Bib.ItemsSource = bibliotecariosTabla.DefaultView;
            }
        }
        private void muestraLibros()
        {
            string consulta = "SELECT *, CONCAT ('ID:', Id_Libro, ' - Titulo: ', Titulo) AS INFO FROM Libro";
            SqlDataAdapter adaptadorSQL = new SqlDataAdapter(consulta, conexionSQL);
            using (adaptadorSQL)
            {
                DataTable librosTabla = new DataTable();
                adaptadorSQL.Fill(librosTabla);
                lista_Libros.DisplayMemberPath = "INFO";
                lista_Libros.SelectedValuePath = "Id_Libro";
                lista_Libros.ItemsSource = librosTabla.DefaultView;
            }
        }
        private void muestraPrestamos()
        {
            string consulta = "SELECT *, CONCAT ('ID:', Id_Prestamo, ' - Inicio: ', Fecha_Inicial, ' - Duracion: ', Duracion_Dias, ' - Fin: ', Fecha_Final,' - ID del Lector: ', Id_Lector, ' - ID del Libro: ', Id_Libro, ' - ID del Bibliotecario: ', Id_Bibliotecario) AS INFO FROM Prestamo";
            SqlDataAdapter adaptadorSQL = new SqlDataAdapter(consulta, conexionSQL);
            using (adaptadorSQL)
            {
                DataTable prestamosTabla = new DataTable();
                adaptadorSQL.Fill(prestamosTabla);
                lista_prestamos.DisplayMemberPath = "INFO";
                lista_prestamos.SelectedValuePath = "Id_Prestamo";
                lista_prestamos.ItemsSource = prestamosTabla.DefaultView;
            }
        }

        private void register_borrow_Copy_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "INSERT INTO Prestamo (Fecha_Inicial, Duracion_Dias, Fecha_Final, Id_Lector, Id_Libro, Id_Bibliotecario)  VALUES (@fInicio, @dias, @fFinal, @Lector, @Libro, @Bibliotecario)";
            SqlCommand comando = new SqlCommand(consulta, conexionSQL);
            conexionSQL.Open();
            comando.Parameters.AddWithValue("@fInicio", dp_fecha.SelectedDate);
            comando.Parameters.AddWithValue("@dias", input_dias.Text);
            comando.Parameters.AddWithValue("@fFinal", dp_fecha.SelectedDate.Value.AddDays(int.Parse(input_dias.Text)));
            comando.Parameters.AddWithValue("@Lector", lista_Lectores.SelectedValue);
            comando.Parameters.AddWithValue("@Libro", lista_Libros.SelectedValue);
            comando.Parameters.AddWithValue("@Bibliotecario", lista_Bib.SelectedValue);
            comando.ExecuteNonQuery();
            conexionSQL.Close();
            muestraPrestamos();
        }

        private void Borrar_Prestamo_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "DELETE FROM Prestamo WHERE Id_Prestamo = @PrestamoId";
            SqlCommand comando = new SqlCommand(consulta, conexionSQL);
            conexionSQL.Open();
            comando.Parameters.AddWithValue("@PrestamoId", lista_prestamos.SelectedValue);
            comando.ExecuteNonQuery();
            conexionSQL.Close();
            muestraPrestamos();
            MessageBox.Show("Prestamo eliminado exitosamente.");
        }

        private void Actualizar_Prestamo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActualizaPrestamo actPres = new ActualizaPrestamo((int) lista_prestamos.SelectedValue);
                actPres.Show();
                string consulta = "SELECT * FROM Prestamo WHERE Id_Prestamo = @PresId";
                SqlCommand comandoSel = new SqlCommand(consulta, conexionSQL);
                SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comandoSel);
                using (adaptadorSQL)
                {
                    comandoSel.Parameters.AddWithValue("@PresId", lista_prestamos.SelectedValue);
                    DataTable prestamosTable = new DataTable();
                    adaptadorSQL.Fill(prestamosTable);
                    actPres.input_dias.Text = prestamosTable.Rows[0]["Duracion_Dias"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("WHATTHUHEEEEEEEEELLLL:\n"+ex.ToString());
            }
            //this.Close();
        }
    }
}
