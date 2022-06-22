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
    public partial class ListContect : ContentPage
    {
        public ListContect()
        {
            InitializeComponent();
            Title = "Web Browse/Api";
            InitLoad();
        }
        public async void InitLoad()
        {
            for (int x = BodyContect.Children.Count - 1; x >= 0; x--)
            {
                BodyContect.Children.RemoveAt(x);
            }
            List<Olist> L = new List<Olist>();
            for (int x = 0; x < 20; x++)
            {
                Olist o = new Olist();
                o.Id = $"id:{x}";
                L.Add(o);
            }
            for (int x = 0;x< L.Count; x++)
            {
                var t = new StackLayout()
                {
                    Margin = new Thickness(3, 3, 3, 3),
                    BackgroundColor = Color.FromHex("#916F8A"),
                    Orientation= StackOrientation.Horizontal
                };
                var s = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    
                    Padding = new Thickness(3, 3, 3, 3)
                };
     
                s.Children.Add(new Label() {Text="ID:", FontSize = 20, TextColor = Color.White });
                s.Children.Add(new Label() { Text = $"{L[x].Id}", FontSize = 20, TextColor = Color.White });
                t.Children.Add(s);

                var s1 = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    
                    Padding = new Thickness(3, 3, 3, 3)
                };
                s1.Children.Add(new Label() { Text = "Times:", FontSize = 20, TextColor = Color.White }) ;
                s1.Children.Add(new Label() { Text = $"{L[x].Times}", FontSize = 20, TextColor = Color.White});
                t.Children.Add(s1);
                this.BodyContect.Children.Add(t);
            }
            MessagingCenter.Send(new object(), "SocketMsg", "joso");
        }
   
        public class Olist
        {
            public string Times { get; set; } = DateTime.Now.ToString();
            public string Id { get; set; }
            
        }
    }
}