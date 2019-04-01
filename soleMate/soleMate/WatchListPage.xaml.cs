namespace soleMate {
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;

    public partial class WatchListPage : ContentPage {
        public WatchListPage() {
            InitializeComponent();
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}
