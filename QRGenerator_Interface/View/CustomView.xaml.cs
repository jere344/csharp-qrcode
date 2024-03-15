using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;


namespace QRGenerator_Interface.View
{
    /// <summary>
    /// Logique d'interaction pour CustomView.xaml
    /// </summary>
    public partial class CustomView : Window
    {
        public CustomView()
        {
            InitializeComponent();
        }

        private void ImportImage_click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                Path_Logo.Text = filename;
            }
        }

        private void ModelColor_click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.Color = System.Drawing.Color.Black;
            cd.FullOpen = true;
            cd.AnyColor = true;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Modele.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb( cd.Color.A,cd.Color.R,cd.Color.G,cd.Color.B));
            }
        }

        private void ContourColor_click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.Color = System.Drawing.Color.White;
            cd.FullOpen = true;
            cd.AnyColor = true;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Contour.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B));
            }
        }


    }
}
