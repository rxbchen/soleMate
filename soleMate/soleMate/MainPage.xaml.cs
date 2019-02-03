using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace soleMate
{
    public partial class MainPage : ContentPage //This is the login page, for now
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void CredentialsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginCredentialsPage());
        }

        private void FacebookButtonClicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "HELLOHELLO";
        }
    }
}
