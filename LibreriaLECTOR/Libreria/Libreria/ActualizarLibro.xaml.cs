using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Lógica de interacción para ActualizarLibro.xaml
    /// </summary>
    public partial class ActualizarLibro : Window
    {
        SqlConnection conexionSQL;
        private int identificadorLibro;
        public ActualizarLibro(int id)
        {
            InitializeComponent();
            identificadorLibro = id;
            string conexion = ConfigurationManager.ConnectionStrings["Libreria.Properties.Settings.BilbliotecaConnectionString"].ConnectionString;
            conexionSQL = new SqlConnection(conexion);
        }

        private void btn_updateCan_reader_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rellenaCampos(int id)
        {
            //SELECCIONAR EL TITULO
            string comandoTitulo = "SELECT Libro.Titulo FROM Libro WHERE Id_Libro = " + id + "";
            SqlCommand selectTitulo = new SqlCommand(comandoTitulo, conexionSQL);
            //SELECCIONAR EL GENERO
            string comandoGenero = "SELECT Libro.Genero FROM Libro WHERE Id_Libro = " + id + "";
            SqlCommand selectGenero = new SqlCommand(comandoGenero, conexionSQL);
            //SELECCIONAR EL AUTOR
            string comandoAutor = "SELECT Libro.Autor FROM Libro WHERE Id_Libro = " + id + "";
            SqlCommand selectAutor = new SqlCommand(comandoAutor, conexionSQL);
            //SELECCIONAR LA EDITORIAL
            string comandoEditorial = "SELECT Libro.Editorial FROM Libro WHERE Id_Libro = " + id + "";
            SqlCommand selectEditorial = new SqlCommand(comandoEditorial, conexionSQL);
            //SELECCIONAR LA CANTIDAD DE PAGINAS
            string comandoCantidadPag = "SELECT Libro.Cantidad_Paginas FROM Libro WHERE Id_Libro = " + id + "";
            SqlCommand selectCantidadPag = new SqlCommand(comandoCantidadPag, conexionSQL);
            //SELECCIONAR LA CANTIDAD
            string comandoCantidad = "SELECT Libro.Cantidad FROM Libro WHERE Id_Libro = " + id + "";
            SqlCommand selectCantidad = new SqlCommand(comandoCantidad, conexionSQL);

            //MOSTRAR LOS RESULTADOS
            conexionSQL.Open();
            txt_Titulo.Text = (string)selectTitulo.ExecuteScalar();
            txt_genero.Text = (string)selectGenero.ExecuteScalar();
            txt_autor.Text = (string)selectAutor.ExecuteScalar();
            txt_editorial.Text = (string)selectEditorial.ExecuteScalar();
            txt_cantidadPag.Text = (string)selectCantidadPag.ExecuteScalar();
            txt_cantidad.Text = (string)selectCantidad.ExecuteScalar();
            conexionSQL.Close();
        }

        private void btn_updateOk_reader_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "UPDATE Libro SET Titulo = @titulo, Genero = @genero, Autor = @autor, Editorial = @editorial, Cantidad_Paginas = @cantidad_pag, Cantidad = @cantidad WHERE Id_Libro = " + identificadorLibro + "";
            SqlCommand comandoUpdate = new SqlCommand(consulta, conexionSQL);
            conexionSQL.Open();
            comandoUpdate.Parameters.AddWithValue("@titulo", txt_Titulo.Text);
            comandoUpdate.Parameters.AddWithValue("@genero", txt_genero.Text);
            comandoUpdate.Parameters.AddWithValue("@autor", txt_autor.Text);
            comandoUpdate.Parameters.AddWithValue("@editorial", txt_editorial.Text);
            comandoUpdate.Parameters.AddWithValue("@cantidad_pag", txt_cantidadPag.Text);
            comandoUpdate.Parameters.AddWithValue("@cantidad", txt_cantidad.Text);
            comandoUpdate.ExecuteNonQuery();
            conexionSQL.Close();
            this.Close();
        }
    }
}
