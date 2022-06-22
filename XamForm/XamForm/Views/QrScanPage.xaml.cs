
using Newtonsoft.Json.Linq;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Refit;
using ScanBarCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace XamForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrScanPage : ContentPage
    {
        public QrScanPage()
        {
            InitializeComponent();
            Title = "二位码 条码扫描";
        }
        public string CodeUrl = " https://www.mxnzp.com";
        public interface IGitHubApi
        {
            [Get("/api/barcode/goods/details?barcode={barcode}&app_id={app_id}&app_secret={app_secret}")]
            Task<string> GetDetails(string barcode,string app_id,string app_secret);
        }
        private async void scan_Clicked(object sender, EventArgs e)
        {
           
            var scanner = new MobileBarcodeScanner();
            var result = await scanner.Scan();
            if (null != result)
            {
                lab.Text = result.Text;
                await Xamarin.Essentials.TextToSpeech.SpeakAsync($"您扫描结果是{result.Text}");
                SetCodeInfo(result.Text);
            }
        }
        private async void SetCodeInfo(string Code)
        {
            string app_id = "ndmvjbe0rdeph9xd";
            string app_secret = "ZEhUWWFvcWVyb3lMYzgyaFJpVzJGUT09";
            string UrlGet = $"barcode={Code}&app_id={app_id}&app_secret={app_secret}";
            var gitHubApi = RestService.For<IGitHubApi>(CodeUrl);
            var s = await gitHubApi.GetDetails(Code, app_id, app_secret);
            JObject data = JObject.Parse(s.ToString());
            try
            {
                MessagingCenter.Send(new object(), "SocketMsg", $"{data["msg"]}");
                if ((int)data["code"] == 1)
                {
                    Qname.Text = $"商品名称:{data["data"]["goodsName"]}";
                    Qcode.Text = $"条码:{data["data"]["barcode"]}";
                    Qprice.Text = $"预估价格:{data["data"]["price"]}";
                    Qbrand.Text = $"品牌:{data["data"]["brand"]}";
                    Qfactory.Text = $"厂商:{data["data"]["supplier"]}";
                    Qnorms.Text = $"规格:{data["data"]["standard"]}";
                }                
            }
            catch (Exception ex)
            { 
            }
        }
        private async void CustomScanBarCodeBtn_OnClicked(object sender, EventArgs e)
        {
            if (await CheckPerssion())
            {
                var options = new ZXingScanOverlayOptions()
                {
                    ScanColor = Color.Green,
                    ShowFlash = true
                };
                var overlay = new ZXingScanOverlay(options);
                var csPage = new ZXingCustomScanPage(overlay);

                csPage.OnScanResult = (result) =>
                {
                    if (null != result)
                    {
                        CustomScanResult.Text = result.Text;
                        Xamarin.Essentials.TextToSpeech.SpeakAsync($"您扫描结果是{result.Text}");
                    }
                };

                await Navigation.PushAsync(csPage);
            }
        }

        private void BuildQrCdoeBtn_OnClicked(object sender, EventArgs e)
        {

            var barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 300;
            barcode.BarcodeOptions.Height = 300;
            barcode.BarcodeOptions.Margin = 10;
            barcode.BarcodeValue = "Xamarin.Forms";
            QrCodeSite.Children.Add(barcode);
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CheckPerssion()
        {
            var current = CrossPermissions.Current;
            var status = await current.CheckPermissionStatusAsync<CameraPermission>();
            if (Plugin.Permissions.Abstractions.PermissionStatus.Granted != status)
            {
                status = await current.RequestPermissionAsync<CameraPermission>();
            }
            return status == Plugin.Permissions.Abstractions.PermissionStatus.Granted;
        }
    }
}