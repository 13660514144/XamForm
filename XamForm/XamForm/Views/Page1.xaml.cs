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
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }
        decimal Jguo = 0.00M;
        async void Ondot(object sender, EventArgs args)
        {
            this.Cash.Text += ".";
        }
        async void Onclear(object sender, EventArgs args)
        {
            this.Cash.Text = "";
            this.Equ.Text = "";            
        }
        async void OnNum7(object sender, EventArgs args)
        {
            this.Cash.Text += "7";
        }
        async void OnNum8(object sender, EventArgs args)
        {
            this.Cash.Text += "8";
        }
        async void OnNum9(object sender, EventArgs args)
        {
            this.Cash.Text += "9";
        }
        async void OnNum4(object sender, EventArgs args)
        {
            this.Cash.Text += "4";
        }
        async void OnNum5(object sender, EventArgs args)
        {
            this.Cash.Text += "5";
        }
        async void OnNum6(object sender, EventArgs args)
        {
            this.Cash.Text += "6";
        }
        async void OnNum1(object sender, EventArgs args)
        {
            this.Cash.Text += "1";
        }
        async void OnNum2(object sender, EventArgs args)
        {
            this.Cash.Text += "2";
        }
        async void OnNum3(object sender, EventArgs args)
        {
            this.Cash.Text += "3";
        }
        async void OnNum0(object sender, EventArgs args)
        {
            this.Cash.Text += "0";
        }
        async void Onplus(object sender, EventArgs args)
        {
            this.Cash.Text += " + ";
        }
        async void Onreduce(object sender, EventArgs args)
        {
            this.Cash.Text += " - ";
        }
        async void Onride(object sender, EventArgs args)
        {
            this.Cash.Text += " * ";
        }
        async void Onexcept(object sender, EventArgs args)
        {
            this.Cash.Text += " / ";
        }
        async void Onect(object sender, EventArgs args)
        {
            //开始计算        
            string[] s = Cash.Text.Split(' ');

            Jguo = Convert.ToDecimal(s[0]);
            for (int x = 0; x < s.Length; x++)
            {
                if (x % 2 == 0 && x > 1)
                {
                    if (string.IsNullOrEmpty(s[x]))
                    {
                        await DisplayAlert("Error", "空值不运算", "OK");
                        return;
                    }
                    switch (s[x - 1])
                    {
                        case "+":
                        
                            Jguo += Convert.ToDecimal(s[x]);
                            break;
                        case "-":
                          
                            Jguo -= Convert.ToDecimal(s[x]);
                            break;
                        case "*":
                            
                            Jguo *= Convert.ToDecimal(s[x]);
                            break;
                        case "/":
                            if (Convert.ToDecimal(s[x])==0)
                            {
                                await DisplayAlert("Message", "除数0将导致溢出", "OK");
                            }
                            else
                            {
                                Jguo /= Convert.ToDecimal(s[x]);
                            }
                            break;
                    }
                }
            }
            Equ.Text = Jguo.ToString();            
        }
    }
}