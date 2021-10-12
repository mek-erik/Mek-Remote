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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using TVRequester.Responses;
namespace TVRequester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        private Host currentHost;

        public Host CurrentHost
        {
            get {
                if (dgrHosts.SelectedIndex != -1)
                {
                    return (Host)dgrHosts.SelectedItem;
                }
                else
                {
                    return null;
                }
                }
            //get { return currentHost; }
            set { currentHost = value; }
        }


        private HttpClient client = new HttpClient();

        private List<Host> hosts = new List<Host>();

        public List<Host> Hosts
        {
            get { return hosts; }
            set { hosts = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        private void LoadHosts()
        {
            Hosts = Host.GetHostsFromFile("hosts.cfg");
            dgrHosts.ItemsSource = Hosts;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WriteToLog("Application started.");
            LoadHosts();
            WriteToLog("Hosts loaded from file.");
        }

        private void WriteToLog(string message, bool isError = false)
        {
            string logText = DateTime.Now.ToShortDateString() + " " +  DateTime.Now.ToLongTimeString() + ": " + message;
            ListViewItem item = new ListViewItem();
            item.Content = logText;
            if (isError)
            {
                item.Foreground = Brushes.Red;
            }
            lstLog.Items.Add(item);
        }

        private async void btnOpenTeamViewer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GeneralResponse resp = await GetResponse(CurrentHost.Url + "tv/start");

                WriteToLog(resp.Description);
            }
            catch (Exception error)
            {

                WriteToLog(error.Message, true);
            }

        }

        private void dgrHosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentHost = ((Host)dgrHosts.SelectedItem);
        }
        public async Task<GeneralResponse> GetResponse(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.Timeout = TimeSpan.FromSeconds(2);
                HttpResponseMessage response;

                response = await client.GetAsync(uri);
                string text = await response.Content.ReadAsStringAsync();

                Responses.GeneralResponse generalResponse = Responses.GeneralResponse.GetGeneralResponse(text);
                return generalResponse;

            }
        }
        public async Task<BitmapImage>LoadImage(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.Timeout = TimeSpan.FromSeconds(2);
                HttpResponseMessage response;
                response = await client.GetAsync("http://" + CurrentHost.HostName + ":8000/tv/start");

            }
            return null;
        }

        private async void btnTakeScreenshot_Click(object sender, RoutedEventArgs e)
        {
            // Save screenshot
            GeneralResponse generalResponse = await GetResponse(CurrentHost.Url + "screenshot/save");
            if (generalResponse.StatusCode == 200)
            {
                WriteToLog("Screenshot taken succesfully.");
                string imageUrl = CurrentHost.Url + "screenshot/download/" + generalResponse.Description;
                ScreenshotWindow screenshotWindow = new ScreenshotWindow();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imageUrl, UriKind.Absolute);
                bitmap.EndInit();
                screenshotWindow.imgScreenshot.Source = bitmap;
                screenshotWindow.ShowDialog();
            }
            else
            {
                WriteToLog("Error: " + generalResponse.Description);
            }
            

        }
    }
}
