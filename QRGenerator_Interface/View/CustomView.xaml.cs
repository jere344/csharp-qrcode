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
using QRGenerator;
using static System.Windows.Forms.DataFormats;


namespace QRGenerator_Interface.View
{
    /// <summary>
    /// Logique d'interaction pour CustomView.xaml
    /// </summary>
    public partial class CustomView : Window
    {
        public CustomView(QRCodeGenerator qrCode, string path, int scale)
        {
            InitializeComponent();

            DataContext = new ViewModel.VMCustom(qrCode, path, scale);
        }

        private void ImportImage_click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                Path_Logo.Text = filename;
            }
            ViewModel.VMCustom vm = (ViewModel.VMCustom)DataContext;
            vm.LogoPath = Path_Logo.Text;
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

            ViewModel.VMCustom vm = (ViewModel.VMCustom)DataContext;
            vm.PatternColor = "#" + cd.Color.R.ToString("X2") + cd.Color.G.ToString("X2") + cd.Color.B.ToString("X2");
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

            ViewModel.VMCustom vm = (ViewModel.VMCustom)DataContext;
            vm.BackgroundColor = "#" + cd.Color.R.ToString("X2") + cd.Color.G.ToString("X2") + cd.Color.B.ToString("X2");
        }

        private void ExportImage_click(object sender, RoutedEventArgs e)
        {
            ViewModel.VMCustom vm = (ViewModel.VMCustom)DataContext;
            string? error = vm.ExportImage();

            if (error is not null)
            {
                System.Windows.MessageBox.Show(error, "QR code generation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (vm.LastExportedPath is null)
            {
                System.Windows.MessageBox.Show("An error occured during the export", "QR code generation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            Window window = new Window();
            window.Title = vm.LastExportedPath;
            window.Width = 300;
            window.Height = 300;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // window.Content = new Image
            // {
            //     Source = new BitmapImage(new Uri(vm.Path))
            // };
            // instead of using URI, we directly read the filestream to avoid file locking
            using (System.IO.FileStream stream = new System.IO.FileStream(vm.LastExportedPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                window.Content = new Image
                {
                    Source = bitmap
                };
            }

            window.ShowDialog();
        }

        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }






        }
}
