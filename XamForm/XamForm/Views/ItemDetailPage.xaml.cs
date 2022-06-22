using System.ComponentModel;
using Xamarin.Forms;
using XamForm.ViewModels;

namespace XamForm.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}