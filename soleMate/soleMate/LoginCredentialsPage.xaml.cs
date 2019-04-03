
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
            bool isAuth = false;
            CredentialsAuthentication auth = new CredentialsAuthentication(UsernameField.Text, PasswordField.Text);
            if (Constants.LoginButton.loginAttempts >= 3)
            {
                await DisplayAlert("Exceeded number of login attempts", "Please check email for reset instructions", "OK");
                // Not actually disabled, just greyed out
                LoginButton.BackgroundColor = Color.FromHex(Constants.Button.disabled); 
            } 
            else
            {
                isAuth = await auth.ValidateUserAsync();
                if (isAuth)
                {
                    Constants.LoginButton.loginAttempts = 0;
                    await Navigation.PushAsync(new SearchPage());
                }
                else
                {
                    Constants.LoginButton.loginAttempts++;
                    await DisplayAlert("Invalid username/password!", "", "OK");
                }
            }

        }
    }
}