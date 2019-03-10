namespace soleMate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using soleMate.Model;

    /// <summary>
    /// Login Page summary
    /// </summary>
    public partial class MainPage : ContentPage {

        // Constructor

        public MainPage() {
            InitializeComponent();
            StylePage();

        }

        // Private Methods

        private void StylePage() {

            // Buttons

            LoginCredentialsButton.BackgroundColor = Color.FromHex(Constants.Button.mainBackgroundColour);
            LoginCredentialsButton.HeightRequest = Constants.Button.height;
            LoginCredentialsButton.WidthRequest = Constants.Button.widthLong;

            LoginFacebookButton.BackgroundColor = Color.FromHex(Constants.Button.mainBackgroundColour);
            LoginFacebookButton.HeightRequest = Constants.Button.height;
            LoginFacebookButton.WidthRequest = Constants.Button.widthLong;

            SignUpButton.BackgroundColor = Color.FromHex(Constants.Button.secondaryBackgroundColour);
            SignUpButton.HeightRequest = Constants.Button.height;
            SignUpButton.WidthRequest = Constants.Button.widthLong;
        }

        private async void CredentialsButtonClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new LoginCredentialsPage());
        }

        private void FacebookButtonClicked(object sender, EventArgs e) {
            ((Button)sender).Text = "HELLOHELLO";
            //TODO: Facebook plugin
        }

        private void SignUpButtonClicked(object sender, EventArgs e) {
            ((Button)sender).Text = "YAS";
            //TODO: SignUp plugin
        }
    }
}
