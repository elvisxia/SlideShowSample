using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SlideShowSample
{
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer playlistTimer1a = null;
        List<string> Images1a = new List<string>();
        ThreadPoolTimer timer = null;
        String jsonText;
        int Index=0;
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            playlistTimer1a = new DispatcherTimer();
            jsonText = "{\"status\":true,"+
                "\"data\":["+
        "{\"image\":\"ms-appx:///images/image01.png\",\"url\":\"#\"},"+
        "{\"image\":\"ms-appx:///images/image02.png\",\"url\":\"#\"}," +
        "{\"image\":\"ms-appx:///images/image03.jpg\",\"url\":\"#\"}," +
        "{\"image\":\"ms-appx:///images/image04.jpg\",\"url\":\"#\"}"+
    "]}";
            base.OnNavigatedTo(e);
        }

        private void myBtn_Click(object sender, RoutedEventArgs e)
        {
            ImageSource1a();
        }
        


        private async void ImageSource1a()
        {
            try
            {
                //var httpClientHandler = new HttpClientHandler();
                //httpClientHandler.Credentials = new System.Net.NetworkCredential("username", "password");
                //var httpClient = new HttpClient(httpClientHandler);
                //string urlPath = "http://";
                //var values = new List<KeyValuePair<string, string>>
                //{
                //    new KeyValuePair<string, string>("platform","win"),
                //};
                //HttpResponseMessage response = await httpClient.PostAsync(urlPath, new FormUrlEncodedContent(values));

                //response.EnsureSuccessStatusCode();

                //string jsonText = await response.Content.ReadAsStringAsync();
                String jsonStr = "{\"name\":\"winffee\"}";
                JsonObject obj = null;
                JsonObject jsonObject = JsonObject.Parse(jsonText);
                //JsonObject jsonData1 = jsonObject["data"].GetObject();

                JsonArray jsonData1 = jsonObject["data"].GetArray();


                foreach (JsonValue groupValue1 in jsonData1)
                {
                    JsonObject groupObject1 = groupValue1.GetObject();

                    string image = groupObject1["image"].GetString();
                    string url = groupObject1["url"].GetString();
                    Images1a.Add(image);
                }
                   //I don't know what is Banner object used for but for this piece of codes, a banner object is not necessary.
                    //Banner file1 = new Banner();
                    //file1.Image = image;
                    //file1.URL = url;

                    playlistTimer1a = new DispatcherTimer();
                    playlistTimer1a.Interval = new TimeSpan(0, 0, 6);
                    playlistTimer1a.Tick += playlistTimer_Tick1a;
                    topBanner.Source = new BitmapImage(new Uri(Images1a[0]));//set the current image to the first one
                    playlistTimer1a.Start();
            }
            catch (HttpRequestException ex)
            {
                RequestException();
            }
        }

        int count1a = 0;

        private void playlistTimer_Tick1a(object sender, object e)
        {
            if (Images1a != null)
            {
                
                if (count1a < Images1a.Count)
                    count1a++;

                if (count1a >= Images1a.Count)
                    count1a = 0;
                topBanner.Source = new BitmapImage(new Uri(Images1a[count1a]));
                //ImageRotation1a();
            }
        }

        private void RequestException()
        {
            throw new NotImplementedException();
        }
    }
}
