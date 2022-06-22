using HelperTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefitApiPage : ContentPage
    {
        private double width, height;
        public string PageMothed;
        public JObject data;
        public string Lastid;
        public string HomeUrl;
        public RefitApiPage()
        {
            InitializeComponent();
            Title = "Restful RefitApi";
            HomeUrl = "http://124.223.82.154:8222";
            PageMothed = "first";
            Lastid = string.Empty;
            Next.Clicked += async (sender, args) => { await NextPage(); };
            Pre.Clicked += async (sender, args) => { await PrePage(); };
            Next.IsEnabled = false;
            Pre.IsEnabled = false;
            FirstPage();
        }
        public interface IGitHubApi
        {
            [Post("/api/ListPage/GetPage")]
            //Task<string> GetPage(string json);
            Task<string> GetPage([Body(serializationMethod: BodySerializationMethod.Json)] string paylod);
            //Task<string> GetBody([Body] string json);
            
        }
        private string GetBody(string lastid,string pagemothed)
        {
            var o = new
            {
                IdCode = "62412c5f83e3ebef97021241",
                Role = "",
                DelFlg = 1,
                GroupFlg = "",
                LastId = lastid,
                PageNextOrPre = pagemothed,
                WhereCollection = new JArray(),
                rows = 10,
                pages = 10
            };            
            return JsonConvert.SerializeObject(o);
        }
        private async Task PrePage()
        {
            try
            {

                JArray Olddata = (JArray)data["data"];
                if (PageMothed == "pre")
                {
                    JObject J = (JObject)Olddata[Olddata.Count - 1];
                    Lastid = J["_id"].ToString();
                }
                else
                {
                    JObject J = (JObject)Olddata[0];
                    Lastid = J["_id"].ToString();
                }
                PageMothed = "pre";
                var gitHubApi = RestService.For<IGitHubApi>(HomeUrl);
                long t1 = TimeSpans.Timestamp();
                var s = await gitHubApi.GetPage(GetBody(Lastid, PageMothed));
                long t2 = TimeSpans.Timestamp();
                lab.Text = $@"{t2 - t1}";
                data = JObject.Parse(s.ToString());

                if ((int)data["code"] == 200)
                {
                    RowClear();
                    CreateBody(data);
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new object(), "SocketMsg", ex.Message.ToString());
            }
        }
        private async Task NextPage()
        {
            try
            {
                JArray Olddata = (JArray)data["data"];
                if (PageMothed == "next" || PageMothed == "first")
                {
                    JObject J = (JObject)Olddata[Olddata.Count - 1];
                    Lastid = J["_id"].ToString();
                }
                else
                {
                    JObject J = (JObject)Olddata[0];
                    Lastid = J["_id"].ToString();
                }
                PageMothed = "next";
                var gitHubApi = RestService.For<IGitHubApi>(HomeUrl);
                long t1 = TimeSpans.Timestamp();
                var s = await gitHubApi.GetPage(GetBody(Lastid, PageMothed));
                long t2 = TimeSpans.Timestamp();
                lab.Text = $@"{t2 - t1}";
                data = JObject.Parse(s.ToString());

                if ((int)data["code"] == 200)
                {
                    RowClear();
                    CreateBody(data);
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new object(), "SocketMsg", ex.Message.ToString());
            }
        }
        private async Task FirstPage()
        {

            try
            {               
                PageMothed = "first";
                var gitHubApi = RestService.For<IGitHubApi>(HomeUrl);
                long t1 = TimeSpans.Timestamp();
                var s = await gitHubApi.GetPage(GetBody(Lastid, PageMothed));
                long t2 = TimeSpans.Timestamp();
                lab.Text = $@"{t2 - t1}";
                data = JObject.Parse(s.ToString());
                
                MessagingCenter.Send(new object(), "SocketMsg", data["message"].ToString());
                if ((int)data["code"] == 200)
                {
                    RowClear();
                    CreateBody(data);
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new object(), "SocketMsg", ex.Message.ToString());
            }

        }
        private async void RowClear()
        {
            for (int x = BodyContect.Children.Count - 1; x >= 0; x--)
            {
                BodyContect.Children.RemoveAt(x);
            }
        }
        private async void CreateBody(JObject data)
        {
            JArray JsonArray = (JArray)data["data"];
            int len = JsonArray.Count;
            for (int x = 0; x < len; x++)
            {
                var t = new StackLayout()
                {
                    Margin = new Thickness(3, 3, 3, 3),
                    BackgroundColor = Color.FromHex("#808080"),
                    Orientation = StackOrientation.Vertical
                };
                
                JObject oo = (JObject)JsonArray[x];
                foreach (var item in oo)
                {
                    t.Children.Add(new Label() { Text = $"{item.Key}:{item.Value}  ", FontSize = 25, TextColor = Color.FromHex("#FF4500") });
                }
                
                BodyContect.Children.Add(t);
            }
            switch (PageMothed)
            {
                case "next":
                    if (10 == len) CreateBtn(true, true);
                    if (10 > len) CreateBtn(false, true);
                    break;
                case "pre":
                    if (10 == len) CreateBtn(true, true);
                    if (10 > len) CreateBtn(true, false);
                    break;
                case "first":
                    if (10 == len) CreateBtn(true, false);
                    break;
            }
        }
        private void CreateBtn(bool next, bool pre)
        {
            Next.IsEnabled = (next == true) ? true : false;
            Pre.IsEnabled = (pre == true) ? true : false;
        }
        protected override void OnSizeAllocated(double width, double height)
        {

            if (width != this.width || height != this.height)
            {
                if (width > height)
                {
                    outerStack.Orientation = StackOrientation.Horizontal;
                }
                else
                {
                    outerStack.Orientation = StackOrientation.Vertical;
                }

                this.width = width;
                this.height = height;
            }
            base.OnSizeAllocated(width, height);
        }
    }
}