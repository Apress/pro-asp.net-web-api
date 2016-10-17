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

namespace AwaitSample1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e) {

            using (Stream stream = await GetWebPageAsync())
            using (var fileStream = new FileStream(@"c:\apps\msg.bin", FileMode.Create)) {
                await stream.CopyToAsync(fileStream);
                StatusLabel.Content = "Done...";
            }
        }

        private async Task<Stream> GetWebPageAsync() {

            using (var httpClient = new HttpClient()) {
                var stream = await httpClient.GetStreamAsync("http://www.apress.com");
                return stream;
            }
        }
    }
}
