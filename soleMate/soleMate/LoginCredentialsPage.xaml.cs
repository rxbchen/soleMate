
namespace soleMate {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using soleMate.Model;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// LoginCredentialsPage summary
    /// </summary>

    public partial class LoginCredentialsPage : ContentPage {

        // Constructor

		public LoginCredentialsPage () {
			InitializeComponent();
            StylePage();
		}

        // Private Methods
        private void StylePage() {

            // InputFields
            UsernameField.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            UsernameField.HeightRequest = Constants.InputField.height;
            UsernameField.WidthRequest = Constants.InputField.width;

            PasswordField.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            PasswordField.HeightRequest = Constants.InputField.height;
            PasswordField.WidthRequest = Constants.InputField.width;

            // Buttons
            LoginButton.BackgroundColor = Color.FromHex(Constants.Button.mainBackgroundColour);
            LoginButton.HeightRequest = Constants.Button.height;
            LoginButton.WidthRequest = Constants.Button.widthShort;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e) {
            //await Navigation.PushAsync(new SearchPage());
            bool isAuth = false; 
            //await Navigation.PushAsync(new WatchListPage()); //TODO: Change back
            CredentialsAuthentication auth = new CredentialsAuthentication(UsernameField.Text, PasswordField.Text);
            isAuth = await auth.ValidateUserAsync(); 
            if (isAuth)
            {
                await Navigation.PushAsync(new SearchPage());
            }
            else
            {
                await DisplayAlert("Invalid username/password!", "", "OK");
            }
        }
    }
}