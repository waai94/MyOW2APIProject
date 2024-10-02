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
using System.Net.Http;
using System.Net;
using System.Diagnostics;


using System.Text.Json;

namespace newOW2TrackerWithAPI
{

    public class ow2tracker
    {
     public   string apiKey { get; set; }
        public string userName { get; set; }
        public string tagLine { get; set; }

        public ow2tracker(string api,string uname,string tag)
        {
            apiKey = api;
            userName = uname;
            tagLine = tag;
        }
    }

    public class userDataClass
    {
        public string userName { get; set; }
        public string userTag { get; set; }

        public string tankRanked { get; set; }
        public string dpsRanked { get; set; }

        public string supportRanked { get; set; }

        public userDataClass(string un,string ut,string tr,string dr,string sr)
        {
            userName = un;
            userTag = ut;
            tankRanked = tr;
            dpsRanked = dr;
            supportRanked = sr;
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string url = "https://overfast-api.tekrop.fr/players/%E9%99%B0%E9%99%BA%E8%87%AA%E6%92%AE%E3%82%8A%E5%A5%B3-1284/summary";
        ow2tracker tracker;
        userDataClass userDataClass;
        string urApiKey = "";
        string urlUserName = "";
        string urlTagLine = "";
        string playerUrl;
        string responceUserData;
        
        public  HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
           userDataClass= new userDataClass("", "", "", "", "");
            tracker = new ow2tracker("", "%E9%99%B0%E9%99%BA%E8%87%AA%E6%92%AE%E3%82%8A%E5%A5%B3", "1284");
            urApiKey = tracker.apiKey;
            urlUserName = tracker.userName;
            urlTagLine = tracker.tagLine;
            playerUrl = $"https://overfast-api.tekrop.fr/players/{urlUserName}-{urlTagLine}/summary";
            url = playerUrl;

            TryToConnectApi();
            //userNameLabel.Content = "HELLO";
            //Debug.WriteLine("Responce queue is done");
           
           

        }

        async void TryToConnectApi()
        {
            /*  WebRequest request = WebRequest.Create(url);
              HttpResponseMessage responce = await client.GetAsync(url);
              Debug.Write("Reques is " + responce);

              WebResponse res = request.GetResponse();
              Debug.WriteLine(res);
            */
            try
            {
                // GETリクエストを送信
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // ステータスコードが成功かどうかを確認

                // レスポンスを文字列として取得
                string responseBody = await response.Content.ReadAsStringAsync();
                responceUserData = responseBody;
                WhenConnectedApi();

                // 結果を出力
                Debug.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("\nException Caught!");
                Debug.WriteLine("Message: {0}", e.Message);
            }
            
         
        }
        //Apiの通信が終了したとき、
        void WhenConnectedApi()

         
        {
            using (JsonDocument doc = JsonDocument.Parse(responceUserData))
            {
                JsonElement root = doc.RootElement;

                // "username"項目を取得
                string user_name = root.GetProperty("username").GetString();
                

                userDataClass.userName = user_name;

                string _userIcon = root.GetProperty("avatar").GetString();
                LoadImageFromUrl(_userIcon);

                // 結果を表示
               // Debug.WriteLine($"Username: {username}");
            }


            userNameLabel.Content = userDataClass.userName;
        }



        void LoadImageFromUrl(string imageUrl)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imageUrl, UriKind.Absolute);
            bitmap.EndInit();
            WebImage.Source = bitmap;
        }
    }
}

