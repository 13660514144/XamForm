using HelperTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForm.Models;
using XamForm.Xrpc;

namespace XamForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XrpcPage : ContentPage
    {
        RpcSample RpcBind;
        //public ICommand CommandPre { get; }
        //public ICommand CommandNext { get; }
        public string PageMothed;
        public JObject data;
        public string lastid;
        public XrpcPage()
        {
            InitializeComponent();
            Title = "Xrpc Load Data";
            RpcBind = new RpcSample();
            //CommandPre = new Command(PrePage);
            //CommandNext = new Command(NextPage);
            PageMothed = "first";
            Next.Clicked += async(sender, args) =>{ await NextPage(); };
            Pre.Clicked += async (sender, args) => { await PrePage(); };
            Next.IsEnabled = false;
            Pre.IsEnabled = false;
            FirstPage();
        }
        private async Task PrePage()
        {
            try
            {
                
                JArray Olddata = (JArray)data["data"];
                if (PageMothed == "pre")
                {
                    JObject J = (JObject)Olddata[Olddata.Count - 1];
                    lastid = J["_id"].ToString();
                }
                else
                {
                    JObject J = (JObject)Olddata[0];
                    lastid = J["_id"].ToString();
                }
                PageMothed = "pre";
                RpcBind.PageMothed = "pre";
                RpcBind.lastid = lastid;
                long t1 = TimeSpans.Timestamp();
                var s = await RpcBind.GetMap();
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
                if (PageMothed == "next" || PageMothed=="first")
                {
                    JObject J = (JObject)Olddata[Olddata.Count - 1];
                    lastid = J["_id"].ToString();
                }               
                else
                {
                    JObject J = (JObject)Olddata[0];
                    lastid = J["_id"].ToString();
                }
                PageMothed = "next";
                RpcBind.PageMothed = "next";
                RpcBind.lastid = lastid;
                long t1 = TimeSpans.Timestamp();
                var s = await RpcBind.GetMap();
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
                RpcBind.PageMothed = "first";
                PageMothed = "first";
                long t1 = TimeSpans.Timestamp();
                var s = await RpcBind.GetMap();
                long t2= TimeSpans.Timestamp();
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
            int len = JsonArray.Count ;
            for (int x = 0; x < len; x++)
            {

                var t = new StackLayout()
                {
                    Margin = new Thickness(3, 3, 3, 3),
                    BackgroundColor = Color.FromHex("#FFEBCD"),
                    Orientation = StackOrientation.Vertical
                };
                
                JObject oo = (JObject)JsonArray[x];
                foreach (var item in oo)
                {
                    t.Children.Add(new Label() { Text = $"{item.Key}:{item.Value}  ", FontSize = 20, TextColor = Color.FromHex("#808000") });                    
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
    }
}