using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace AwaitSample2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {

            InitializeComponent();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e) {

            try {

                using (Stream stream = await GetWebPageAsync())
                using (var fileStream = new FileStream(@"c:\apps\msg.bin", FileMode.Create)) { 
                    await stream.CopyToAsync(fileStream);
                    StatusLabel.Content = "Done...";
                }
            }
            catch (Exception ex) {

                StatusLabel.Content = string.Format(
                    "An exception occured: {0}{1}Stack Trace:{1}{2}", 
                    ex.Message, 
                    Environment.NewLine, 
                    ex.StackTrace
                );
                StatusLabel.Foreground = Brushes.Red;
            }
        }

        private async Task<Stream> GetWebPageAsync() {
            
            using (var httpClient = new HttpClient()) {
                var stream = await httpClient.GetStreamAsync("http://localhost:9999");
                return stream;
            }
        }
    }
}
