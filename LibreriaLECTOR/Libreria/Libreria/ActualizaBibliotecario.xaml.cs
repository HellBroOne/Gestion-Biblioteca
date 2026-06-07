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
    /// Lógica de interacción para ActualizaBibliotecario.xaml
    /// </summary>
    public partial class ActualizaBibliotecario : Window
    {
        SqlConnection conexionSQL;
        private int identificadorBibliotecario;

        public ActualizaBibliotecario(int id)
        {
            InitializeComponent();    
            identificadorBibliotecario = id;
            string conexion = ConfigurationManager.ConnectionStrings["Libreria.Properties.Settings.BilbliotecaConnectionString"].ConnectionString;
            conexionSQL = new SqlConnection(conexion);
        }

        private void btn_updateCan_reader_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rellenaCampos(int id)
        {
            //SELECCIONAR AL NOMBRE
            string comandoName = "SELECT Bibliotecario.Nombre FROM Bibliotecario WHERE Id_Bibliotecario = " + id + "";
            SqlCommand selectName = new SqlCommand(comandoName, conexionSQL);
            //SELECCIONAR AL APELLIDO PATERNO
            string comandoPaterno = "SELECT Bibliotecario.APaterno FROM Bibliotecario WHERE Id_Bibliotecario = " + id + "";
            SqlCommand selectPaterno = new SqlCommand(comandoPaterno, conexionSQL);
            //SELECCIONAR AL APELLIDO MATERNO
            string comandoMaterno = "SELECT Bibliotecario.AMaterno FROM Bibliotecario WHERE Id_Bibliotecario = " + id + "";
            SqlCommand selectMaterno = new SqlCommand(comandoMaterno, conexionSQL);
            //SELECCIONAR AL TELEFONO
            string comandoTelefono = "SELECT Bibliotecario.Telefono FROM Bibliotecario WHERE Id_Bibliotecario = " + id + "";
            SqlCommand selectTelefono = new SqlCommand(comandoTelefono, conexionSQL);
            //SELECCIONAR AL CORREO
            string comandoCorreo = "SELECT Bibliotecario.Correo FROM Bibliotecario WHERE Id_Bibliotecario = " + id + "";
            SqlCommand selectCorreo = new SqlCommand(comandoCorreo, conexionSQL);
            //SELECCIONAR EL RFC
            string comandoRFC = "SELECT Bibliotecario.RFC FROM Bibliotecario WHERE Id_Bibliotecario = " + id + "";
            SqlCommand selectRFC = new SqlCommand(comandoRFC, conexionSQL);

            //MOSTRAR LOS RESULTADOS
            conexionSQL.Open();
            txt_NombreBibl.Text = (string)selectName.ExecuteScalar();
            txt_APatBibl.Text = (string)selectPaterno.ExecuteScalar();
            txt_AMatBibl.Text = (string)selectMaterno.ExecuteScalar();
            txt_TelBibl.Text = (string)selectTelefono.ExecuteScalar();
            txt_CorreoBibl.Text = (string)selectCorreo.ExecuteScalar();
            txtRFCBlibl.Text = (string)selectRFC.ExecuteScalar();
            conexionSQL.Close();
        }

        private void btn_updateOk_reader_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "UPDATE Bibliotecario SET Nombre = @nombre, APaterno = @paterno, AMaterno = @materno, Telefono = @telefono, Correo = @email, RFC = @RFC WHERE Id_Bibliotecario = " + identificadorBibliotecario + "";
            SqlCommand comandoUpdate = new SqlCommand(consulta, conexionSQL);
            conexionSQL.Open();
            comandoUpdate.Parameters.AddWithValue("@nombre", txt_NombreBibl.Text);
            comandoUpdate.Parameters.AddWithValue("@paterno", txt_APatBibl.Text);
            comandoUpdate.Parameters.AddWithValue("@materno", txt_AMatBibl.Text);
            comandoUpdate.Parameters.AddWithValue("@telefono", txt_TelBibl.Text);
            comandoUpdate.Parameters.AddWithValue("@email", txt_CorreoBibl.Text);
            comandoUpdate.Parameters.AddWithValue("@RFC", txtRFCBlibl.Text);
            comandoUpdate.ExecuteNonQuery();
            conexionSQL.Close();
            this.Close();
        }
    }
}
