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
    public partial class Nfc : ContentPage
    {
        public Nfc()
        {
            InitializeComponent();
        }
        public void SetNFCData(string nfcid)
        {
            //EvaluateJavascript不会刷新页面
            //调用H5页面的函数，赋值给文本框或变量都可以
            //string script = "javascript:SetNFCID('" + nfcid + "');";
            lab.Text = nfcid;
        }
    }
}