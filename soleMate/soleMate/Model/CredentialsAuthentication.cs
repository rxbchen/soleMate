using System;
using Xamarin.Forms;

namespace soleMate.Model {
    public class CredentialsAuthentication : Behavior<Entry> { 

        // Private Variables

        private string username;
        private string password;

        // Constructor

        public CredentialsAuthentication(string username, string password) {
            this.username = username;
            this.password = password;
        }

        // Public Methods

        public bool ValidateUser() {

            if (ValidateUsernameFormat() && ValidatePasswordFormat()) {
                //TODO: Call to backend to validate
            }

            return true; //TODO: Change to proper logic
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
