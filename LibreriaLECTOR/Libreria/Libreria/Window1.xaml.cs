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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            string conexion = ConfigurationManager.ConnectionStrings["Libreria.Properties.Settings.BilbliotecaConnectionString"].ConnectionString;
            conexionSQL = new SqlConnection(conexion);
            MuestraBibliotecarios();
        }

        private void MuestraBibliotecarios()
        {
            string consulta = "SELECT *, CONCAT ('ID:', Id_Bibliotecario, ' - Bibliotecario: ', Nombre, ' ', APaterno, ' - RFC: ', RFC, ' - Telefono: ', Telefono) AS INFO FROM Bibliotecario";
            SqlDataAdapter adaptadorSQL = new SqlDataAdapter(consulta, conexionSQL);
            using (adaptadorSQL)
            {
                DataTable bibliotecariosTabla = new DataTable();
                adaptadorSQL.Fill(bibliotecariosTabla);
                todosLosBibliotecarios.DisplayMemberPath = "INFO";
                todosLosBibliotecarios.SelectedValuePath = "Id_Bibliotecario";
                todosLosBibliotecarios.ItemsSource = bibliotecariosTabla.DefaultView;
            }
        }

        SqlConnection conexionSQL;

        private void Window_Activated(object sender, EventArgs e)
        {
            MuestraBibliotecarios();
        }

        private void menuAyuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hay que llamar a un adulto, y los programadores de esta aplicacion son adultos, pero adultos chiquitos");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "INSERT INTO Bibliotecario (RFC, Nombre, APaterno, AMaterno, Telefono, Correo) VALUES (@rfc, @nombre, @apat, @amat, @tel, @correo)";

            SqlCommand miSqlCommand = new SqlCommand(consulta, conexionSQL);
            conexionSQL.Open();
            miSqlCommand.Parameters.AddWithValue("@rfc", input_rfc.Text);
            miSqlCommand.Parameters.AddWithValue("@nombre", input_nombre.Text);
            miSqlCommand.Parameters.AddWithValue("@apat", input_apaterno.Text);
            miSqlCommand.Parameters.AddWithValue("@amat", input_amaterno.Text);
            miSqlCommand.Parameters.AddWithValue("@tel", input_telefono.Text);
            miSqlCommand.Parameters.AddWithValue("@correo", input_email.Text);
            miSqlCommand.ExecuteNonQuery();
            conexionSQL.Close();

            MuestraBibliotecarios();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string consulta = "DELETE FROM Bibliotecario WHERE Id_Bibliotecario = @BibliotecarioId";
            SqlCommand comandoDel = new SqlCommand(consulta, conexionSQL);
            conexionSQL.Open();
            comandoDel.Parameters.AddWithValue("@BibliotecarioId", todosLosBibliotecarios.SelectedValue);

            try
            {
                comandoDel.ExecuteNonQuery();
                MessageBox.Show("Bibliotecario eliminado exitosamente");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("" + ex);
            }

            conexionSQL.Close();
            MuestraBibliotecarios();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ActualizaBibliotecario update = new ActualizaBibliotecario((int)todosLosBibliotecarios.SelectedValue);
                update.Show();
                string consulta = "SELECT * FROM Bibliotecario WHERE Id_Bibliotecario = @BibliotecarioId";
                SqlCommand comandoSel = new SqlCommand(consulta, conexionSQL);
                SqlDataAdapter adaptadorSQL = new SqlDataAdapter(comandoSel);
                using (adaptadorSQL)
                {
                    comandoSel.Parameters.AddWithValue("@BibliotecarioId", todosLosBibliotecarios.SelectedValue);
                    DataTable lectoresTable = new DataTable();
                    adaptadorSQL.Fill(lectoresTable);
                    update.txt_NombreBibl.Text = lectoresTable.Rows[0]["Nombre"].ToString();
                    update.txt_APatBibl.Text = lectoresTable.Rows[0]["APaterno"].ToString();
                    update.txt_AMatBibl.Text = lectoresTable.Rows[0]["AMaterno"].ToString();
                    update.txt_TelBibl.Text = lectoresTable.Rows[0]["Telefono"].ToString();
                    update.txt_CorreoBibl.Text = lectoresTable.Rows[0]["Correo"].ToString();
                    update.txtRFCBlibl.Text = lectoresTable.Rows[0]["RFC"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void botonPrestamo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
