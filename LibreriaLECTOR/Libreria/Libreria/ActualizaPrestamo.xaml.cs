using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace Libreria
{
    /// <summary>
    /// Lógica de interacción para ActualizaPrestamo.xaml
    /// </summary>
    public partial class ActualizaPrestamo : Window
    {
        int prestamoID;
        SqlConnection conexionSQL;
        public ActualizaPrestamo(int id)
        {
            prestamoID = id;
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["Libreria.Properties.Settings.BilbliotecaConnectionString"].ConnectionString;
            conexionSQL = new SqlConnection(conexion);
            muestraLectores();
            muestraLibros();
            muestraBibliotecarios();
        }

        private void muestraLectores()
        {
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

        private void Actualizar_Prestamo_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "UPDATE Prestamo SET Fecha_Inicial = @FIn, Duracion_Dias = @dias, Fecha_Final = @fFi, Id_Lector = @Lec, Id_Libro = @Lib, Id_Bibliotecario = @Bib WHERE Id_Prestamo = " + prestamoID + "";
            SqlCommand comandoUpdate = new SqlCommand(consulta, conexionSQL);
            conexionSQL.Open();
            comandoUpdate.Parameters.AddWithValue("@FIn", dp_fecha.SelectedDate.Value);
            comandoUpdate.Parameters.AddWithValue("@dias", input_dias.Text);
            comandoUpdate.Parameters.AddWithValue("@fFi", dp_fecha.SelectedDate.Value.AddDays(int.Parse(input_dias.Text)));
            comandoUpdate.Parameters.AddWithValue("@Lec", lista_Lectores.SelectedValue);
            comandoUpdate.Parameters.AddWithValue("@Lib", lista_Libros.SelectedValue);
            comandoUpdate.Parameters.AddWithValue("@Bib", lista_Bib.SelectedValue);
            comandoUpdate.ExecuteNonQuery();
            conexionSQL.Close();
            this.Close();
        }

        private void Borrar_Prestamo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_si_Click(object sender, RoutedEventArgs e)
        {
            EasterEgg eas = new EasterEgg();
            eas.Show();
        }
    }
}
