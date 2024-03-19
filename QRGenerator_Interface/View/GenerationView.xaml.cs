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
using CommunityToolkit.Mvvm;
using Microsoft.Win32;
using System.Windows.Forms;
using QRGenerator;


namespace QRGenerator_Interface.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GenerationView : Window
    {
        public GenerationView()
        {
            InitializeComponent();

            DataContext = new ViewModel.VMGeneration();
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path.Text = dialog.SelectedPath;
            }
            else
            {
                Path.Text = null;
            } 
            // update the binding source
            Path.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateSource();
        }

        private void OpenCustomizationWindow_Click(object sender, RoutedEventArgs e)
        {
            // Open the custom in a new window
            var vm = ((ViewModel.VMGeneration)DataContext);
            QRCodeGenerator? qr = vm.GetLastGeneratedQRCode;
            if (qr is null)
            {
                System.Windows.MessageBox.Show("No QR code has been generated yet", "Customization", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            CustomView customView = new CustomView(qr, vm.SavePath, vm.Scale);
            customView.Show();
        }

        private void GenerateQRCode_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.VMGeneration vm = (ViewModel.VMGeneration)DataContext;
            string? error = vm.GenerateQRCode();

            if (error is not null)
            {
                System.Windows.MessageBox.Show(error, "QR code generation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            Window window = new Window();
            window.Title = "QR code preview";
            window.Width = 300;
            window.Height = 300;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Content = new Image
            {
                Source = new BitmapImage(new Uri(vm.SaveFolder + (vm.SaveFolder.EndsWith("\\") ? "" : "\\") + vm.FileName + ".png"))
            };
            window.Show();
        }

    }
}
