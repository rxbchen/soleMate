using System;
using Xamarin.Forms;

namespace soleMate.Model {
    using soleMate.Service.API;
    public class CredentialsAuthentication : Behavior<Entry> {

        // Public Variabls

        public string username { get; }

        // Private Variables

        private string password;

        // Constructor

        public CredentialsAuthentication(string username, string password) {
            this.username = username;
            this.password = password;
        }

        // Public Methods

        // 
        public async System.Threading.Tasks.Task<bool> ValidateUserAsync() {

            //if (ValidateUsernameFormat() && ValidatePasswordFormat()) {
            //    //TODO: Call to backend to validate
            //}

            //return true; //TODO: Change to proper logic
            // Calls the POST /login api
            bool isAuth = false; 
            try
            {
                HttpLoginRequests login = new HttpLoginRequests(App.RestClient);
                isAuth = await login.Login(this.username, this.password);
            }
            catch (Exception)
            {
                //TODO: Handle Exception
                Console.WriteLine("Could not authenticate with the server");
            }
            return isAuth;
        }


        // Protected Methods
        protected override void OnAttachedTo(Entry entry) {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry) {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args) {
            double result;
            bool isValid = double.TryParse(args.NewTextValue, out result);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }


        // Private Methods

        private bool ValidateUsernameFormat() {

            if (username.Contains("@")) {
                return true;
            }

            return false;
        }

        private bool ValidatePasswordFormat() {
            return true;
        }


    }
}
