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
using System.Data.SqlClient;
using System.Data;

namespace Libreria
{
    /// <summary>
    /// Lógica de interacción para ActualizarLector.xaml
    /// </summary>

    public partial class ActualizarLector : Window
    {
        SqlConnection conexionSQL;
        private int identificadorLector;

        public ActualizarLector(int id)
        {
            InitializeComponent();
            identificadorLector = id;
            string conexion = ConfigurationManager.ConnectionStrings["Libreria.Properties.Settings.BilbliotecaConnectionString"].ConnectionString;
            conexionSQL = new SqlConnection(conexion);
        }


        private void rellenaCampos(int id) {
            //SELECCIONAR AL NOMBRE
            string comandoName = "SELECT Lector.Nombre FROM Lector WHERE Id_Lector = "+id+"";
            SqlCommand selectName = new SqlCommand(comandoName, conexionSQL);
            //SELECCIONAR AL APELLIDO PATERNO
            string comandoPaterno = "SELECT Lector.APaterno FROM Lector WHERE Id_Lector = "+id+"";
            SqlCommand selectPaterno = new SqlCommand(comandoPaterno, conexionSQL);
            //SELECCIONAR AL APELLIDO MATERNO
            string comandoMaterno = "SELECT Lector.AMaterno FROM Lector WHERE Id_Lector = "+id+"";
            SqlCommand selectMaterno = new SqlCommand(comandoMaterno, conexionSQL);
            //SELECCIONAR AL TELEFONO
            string comandoTelefono = "SELECT Lector.Telefono FROM Lector WHERE Id_Lector = "+id+"";
            SqlCommand selectTelefono = new SqlCommand(comandoTelefono, conexionSQL);
            //SELECCIONAR AL CORREO
            string comandoCorreo = "SELECT Lector.Correo FROM Lector WHERE Id_Lector = "+id+"";
            SqlCommand selectCorreo = new SqlCommand(comandoCorreo, conexionSQL);
            //SELECCIONAR AL DOMICILIO
            string comandoDomicilio = "SELECT Lector.Domicilio FROM Lector WHERE Id_Lector = "+id+"";
            SqlCommand selectDomicilio = new SqlCommand(comandoDomicilio, conexionSQL);

            //MOSTRAR LOS RESULTADOS
            conexionSQL.Open();
            txtbx_add_reader_input.Text = (string)selectName.ExecuteScalar();
            txtbx_add_readlast1_input.Text = (string)selectPaterno.ExecuteScalar();
            txtbx_add_readlast2_input.Text = (string)selectMaterno.ExecuteScalar();
            txtbx_add_readphone_input.Text = (string)selectTelefono.ExecuteScalar();
            txtbx_add_reademail_input.Text = (string)selectCorreo.ExecuteScalar();
            txtbx_add_readaddress_input.Text = (string)selectDomicilio.ExecuteScalar();
            conexionSQL.Close();
        }



        private void btn_updateCan_reader_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_updateOk_reader_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "UPDATE Lector SET Nombre = @nombre, APaterno = @paterno, AMaterno = @materno, Telefono = @telefono, Correo = @email, Domicilio = @address WHERE Id_Lector = "+identificadorLector+"";
            SqlCommand comandoUpdate = new SqlCommand(consulta, conexionSQL);
            conexionSQL.Open();
            comandoUpdate.Parameters.AddWithValue("@nombre", txtbx_add_reader_input.Text);
            comandoUpdate.Parameters.AddWithValue("@paterno", txtbx_add_readlast1_input.Text);
            comandoUpdate.Parameters.AddWithValue("@materno", txtbx_add_readlast2_input.Text);
            comandoUpdate.Parameters.AddWithValue("@telefono", txtbx_add_readphone_input.Text);
            comandoUpdate.Parameters.AddWithValue("@email", txtbx_add_reademail_input.Text);
            comandoUpdate.Parameters.AddWithValue("@address", txtbx_add_readaddress_input.Text);
            comandoUpdate.ExecuteNonQuery();
            conexionSQL.Close();
            this.Close();
        }
    }
}
