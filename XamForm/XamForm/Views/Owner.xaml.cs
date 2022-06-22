using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamForm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Owner : ContentPage
    {
        public Owner()
        {
            InitializeComponent();
            Title = "Web VIEW+API";
            this.BindingContext = new OwnerViewModel();
        }
    }
}