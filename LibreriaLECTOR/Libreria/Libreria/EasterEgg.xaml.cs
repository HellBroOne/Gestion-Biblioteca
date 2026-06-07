using Microsoft.Win32;
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

namespace Libreria
{
    /// <summary>
    /// Lógica de interacción para EasterEgg.xaml
    /// </summary>
    public partial class EasterEgg : Window
    {
        MediaPlayer musik = new MediaPlayer();
        public EasterEgg()
        {
            InitializeComponent();
            musica();
        }

        private void musica() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                musik.Open(new Uri(openFileDialog.FileName));
                musik.Play();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            musik.Stop();
            this.Close();
        }
    }
}
