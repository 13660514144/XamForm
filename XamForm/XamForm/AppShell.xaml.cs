using Autofac;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamForm.ViewModels;
using XamForm.Views;
using XamForm.WsSocket;

namespace XamForm
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));            
            
            MessagingCenter.Subscribe<object, string>(this, "SocketMsg", (sender, arg) =>
            {
                _ = DisplayAlert("CenterMessage", $"{arg}", "OK");
            });
            MessagingCenter.Subscribe<object, string>(this, "ErrMsg", (sender, arg) =>
            {
                _ = DisplayAlert("CenterMessage", $"{arg}", "OK");
            });
            /*
            var builder = new ContainerBuilder();
            builder.RegisterType<RefitApiViewModel>();
            IContainer _container = builder.Build();
            var viewModel = _container.Resolve<RefitApiViewModel>();
            */

            WsSocketMethod Ws = new WsSocketMethod();
            Ws.WsIint();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
