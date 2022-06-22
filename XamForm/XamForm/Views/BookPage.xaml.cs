using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookPage : ContentPage
    {
        public BookPage()
        {
            InitializeComponent();
        }

        private async void GetBook_Clicked(object sender, EventArgs e)
        {
            try
            {
                var contact = await Contacts.PickContactAsync();

                if (contact == null)
                    return;

                id.Text = contact.Id;
                namePrefix.Text = contact.NamePrefix;
                givenName.Text = contact.GivenName;
                middleName.Text = contact.MiddleName;
                familyName.Text = contact.FamilyName;
                nameSuffix.Text = contact.NameSuffix;
                displayName.Text = contact.DisplayName;
                phones.Text = contact.Phones[0].ToString(); // List of phone numbers
                emails.Text = contact.Emails[0].ToString(); // List of email addresses
            }
            catch (Exception ex)
            {
                // Handle exception here.
            }
        }
    }
}