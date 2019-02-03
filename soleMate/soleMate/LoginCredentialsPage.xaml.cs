using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace soleMate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginCredentialsPage : ContentPage
	{
		public LoginCredentialsPage ()
		{
			InitializeComponent();
		}

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}