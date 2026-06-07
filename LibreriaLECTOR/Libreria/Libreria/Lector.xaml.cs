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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Libreria
{
    /// <summary>
    /// Lógica de interacción para Lector.xaml
    /// </summary>
    public partial class Lector : Window
    {
        public Lector()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["Libreria.Properties.Settings.BilbliotecaConnectionString"].ConnectionString;
            conexionSQL = new SqlConnection(conexion);
            muestraTodosLosLectores();
        }

        SqlConnection conexionSQL;

        private void botonPrestamo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void muestraTodosLosLectores() {
            string consulta = "SELECT *, CONCAT ('ID:', Id_Lector, ' - Lector: ', Nombre, ' ', APaterno, ' ', AMaterno, ' - Telefono: ', Telefono, ' - Correo: ', Correo) AS INFO FROM Lector";
            SqlDataAdapter adaptadorSQL = new SqlDataAdapter(consulta, conexionSQL);
            using (adaptadorSQL) {
                DataTable tablaLectores = new DataTable();
                adaptadorSQL.Fill(tablaLectores);
                lstbx_allReaders.DisplayMemberPath = "INFO";
                lstbx_allReaders.SelectedValuePath = "Id_Lector";
                lstbx_allReaders.ItemsSource = tablaLectores.DefaultView;
            }
        }

        private void btn_add_reader_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "INSERT INTO Lector (Nombre, APaterno, AMaterno, Telefono, Correo, Domicilio) VALUES (@nombre, @paterno, @materno, @telefono, @correo, @dom)";
            SqlCommand comandoAdd = new SqlCommand(consulta, conexionSQL);
            conexionSQL.Open();
            comandoAdd.Parameters.AddWithValue("@nombre", txtbx_add_reader_input.Text);
            comandoAdd.Parameters.AddWithValue("@paterno", txtbx_add_readlast1_input.Text);
            comandoAdd.Parameters.AddWithValue("@materno", txtbx_add_reader_input.Text);
            comandoAdd.Parameters.AddWithValue("@telefono", txtbx_add_readphone_input.Text);
            comandoAdd.Parameters.AddWithValue("@correo", txtbx_add_reademail_input.Text);
            comandoAdd.Parameters.AddWithValue("@dom", txtbx_add_readaddress_input.Text);
            comandoAdd.ExecuteNonQuery();
            conexionSQL.Close();
            muestraTodosLosLectores();
            MessageBox.Show("Cliente agregado exitosamente.");
        }

        private void btn_delete_reader_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "DELETE FROM Lector WHERE Id_Lector = @LectorId";
            SqlCommand comandoDel = new SqlCommand(consulta, conexionSQL);
            conexionSQL.Open();
            comandoDel.Parameters.AddWithValue("@LectorId", lstbx_allReaders.SelectedValue);
            comandoDel.ExecuteNonQuery();
            conexionSQL.Close();
            muestraTodosLosLectores();
            MessageBox.Show("Lector eliminado exitosamente.");
        }

        private void btn_update_reader_Click(object sender, RoutedEventArgs e)
        {
            ActualizarLector update = new ActualizarLector((int) lstbx_allReaders.SelectedValue);
            update.Show();
            try
            {
                string consulta = "SELECT * FROM Lector WHERE Id_Lector = @LectorId";
                SqlCommand comandoSel = new SqlCommand(consulta, conexionSQL);
                SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comandoSel);
                using (adaptadorSQL) {
                    comandoSel.Parameters.AddWithValue("@LectorId", lstbx_allReaders.SelectedValue);
                    DataTable lectoresTable = new DataTable();
                    adaptadorSQL.Fill(lectoresTable);
                    update.txtbx_add_reader_input.Text = lectoresTable.Rows[0]["Nombre"].ToString();
                    update.txtbx_add_readlast1_input.Text = lectoresTable.Rows[0]["APaterno"].ToString();
                    update.txtbx_add_readlast2_input.Text = lectoresTable.Rows[0]["AMaterno"].ToString();
                    update.txtbx_add_readphone_input.Text = lectoresTable.Rows[0]["Telefono"].ToString();
                    update.txtbx_add_reademail_input.Text = lectoresTable.Rows[0]["Correo"].ToString();
                    update.txtbx_add_readaddress_input.Text = lectoresTable.Rows[0]["Domicilio"].ToString();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            muestraTodosLosLectores();
        }

        private void men_help_Click(object sender, RoutedEventArgs e)
        {
            //Por la seguridad de su cordura, no lean esta string, solo gocenla.
            string meme = "⣴⣶⣶⠿⠟⠛⠻⠷⣶⣶⣶⣤⣄⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⣿⣿⠁⠰⠆⠀⠀⠀⠀⠀⠈⠉⠛⠛⠿⣿⣶⣶⣦⣤⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⢹⣿⣆⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠙⠛⠿⢿⣿⣷⣶⣦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⢻⣿⣇⠀⠀⢠⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠿⢾⣿⣿⣶⣦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠹⣿⣦⡀⢸⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠛⠿⣿⣿⣶⣤⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠙⣿⣿⣦⠙⠻⠟⠛⠀⠀⠀⠀⠀⠀⣶⣿⢷⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⠛⢿⣿⣷⣦⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠈⠻⣿⣷⣀⡀⠀⠀⠀⠀⠀⠀⠀⠙⠻⣿⣿⡿⠀⠀⠀⠀⠀⠀⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠿⣿⣿⢶⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠈⠛⠿⣿⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣾⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠿⣿⣷⣦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠛⢿⣶⣤⣙⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣾⣿⡇⣀⣾⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠿⣿⣷⣶⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣿⡍⠛⠻⠶⠤⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠿⢿⣿⣶⣤⣀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣿⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⡿⠻⠛⣿⡿⠁⠀⠀⠀⠀⣀⣤⣤⣤⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠿⣿⣷⣦⣄⡀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠻⣷⣶⣶⡶⠀⠀⠀⠀⠀⣼⣿⡇⠀⢸⣿⠇⠀⣠⣴⣾⠟⠛⠉⠉⠉⠉⠛⠻⣶⣤⡂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⢿⣽\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣼⣿⣿⣿⣿⣿⣿⣿⣶⠃⠀⠀⠐⠿⠿⠀⣴⡿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣿⠿⣷⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣷⣀⣀⣀⡠⠀⠀⠀⠀⠀⠀⣼⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⢀⣿⡇⠀⣀⣀⣀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠺⠿⠿⠿⣿⡿⠿⠟⠛⠛⠀⠀⣀⣀⡀⠀⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⣿⢣⣾⣿⣥⣾⣿⣿⡉⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣿⡄⠀⣀⣤⣶⠿⠛⠛⠛⠻⣿⣧⠀⠀⠀⠀⠀⣠⣶⣿⣶⣤⣾⡟⠁⠈⢱⣿⠁⠈⢹⣿⣿⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣾⡿⠋⠀⠀⠀⠀⠀⠀⠈⢻⣧⠀⠀⠀⠀⣿⣿⣿⣿⠟⠁⠀⠀⣠⣿⡟⠀⢀⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡷⠶⠿⠿⠛⢋⡉⠁⠀⠀⣠⣾⡿⠋⠀⠀⣸⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⡷⣦⣤⣶⣿⣛⣋⣠⣴⡾⠟⠋⠀⠀⠀⣰⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⡄⠀⠀⢀⣤⣶⣶⣄⢀⣴⡿⠋⠀⠀⣸⡟⠛⠛⠋⠉⠁⠀⠀⠀⠀⠀⣠⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣇⠀⠀⠈⣿⣿⣿⣿⠿⠋⠀⠀⠀⣠⣿⠁⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⣿⣿⣿⡿⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⡟⠛⠛⠛⢛⡉⠁⠀⠀⠀⠀⣠⣾⡿⠁⠀⠀⠀⠀⠀⠀⠀⣠⣶⣿⣿⣿⣿⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠛⠛⠻⣿⣧⣤⣤⣤⣶⡾⠟⠉⠀⠀⠀⠀⠀⢀⣀⣴⣿⣿⣿⣿⣿⣿⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⡏⢩⡥⠄⠀⠀⠀⠀⣀⣠⣤⣶⣿⣿⣿⣿⣿⣿⣿⡿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣿⠿⢻⣿⣾⣷⣤⣴⣶⣾⣿⣿⣿⣿⣟⣟⣻⣿⣿⠿⠛⠉⠀⢀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⠛⠁⠀⠰⣿⡟⠉⠙⠛⠛⠛⠛⠛⠛⠛⠛⠛⠋⠉⣀⣀⣀⣀⣴⣿⠟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⣿⣿⠏⠀⠀⠀⠀⠀⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠛⠛⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⠏⠀⠀⠀⠀⠀⠀⠀⢿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀";
            MessageBox.Show(meme);
        }
    }
}
