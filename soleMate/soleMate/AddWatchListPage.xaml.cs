namespace soleMate {
    using System;
    using System.Collections.Generic;
    using soleMate.Model;
    using soleMate.Service.API;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class AddWatchList : ContentPage {

        // Private Variables

        private Search Search { get; set; }
        private CredentialsAuthentication auth;
        private bool modelSelected = false;
        private bool sizeSelected = false;

        // Constructor

        public AddWatchList(CredentialsAuthentication auth) {
            InitializeComponent();
            IntializeSelectionData();
            StylePage();
            this.auth = auth;
        }

        // Private Methods

        private void IntializeSelectionData() {
            Search = new Search();
            ModelPicker.ItemsSource = Search.ModelList;
            SizePicker.ItemsSource = Search.ShoeSizeList;
        }

        private void StylePage() {

            // Background Colours

            ModelPicker.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            SizePicker.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            PriceRangeValue.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            AddButton.BackgroundColor = Color.FromHex(Constants.Button.mainBackgroundColour);

            // Enable
            AddButton.IsEnabled = true;
            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;

            // Button Sizes

            AddButton.HeightRequest = Constants.Button.height;
            AddButton.WidthRequest = Constants.Button.widthShort;

            // Slider Colours

            PriceSlider.MaximumTrackColor = Color.FromHex(Constants.Slider.maxTrackColour);
            PriceSlider.MinimumTrackColor = Color.FromHex(Constants.Slider.minTrackColour);
        }

        // Picker Events

        private void HandleModelSelectedIndexChanged(object sender, EventArgs args) {
            modelSelected = true;
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;

            Search.ChosenModel = (string)selectedItem; //TODO: Refactor to enum?
        }

        private void HandleSizeSelectedIndexChanged(object sender, EventArgs args) {
            sizeSelected = true;
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem; //TODO: Size is currently int

            Search.ChosenShoeSize = (int)selectedItem;
        }

        private void HandleSortPriceSelectedIndexChanged(object sender, EventArgs args) {
            Picker picker = sender as Picker;
            string selectedItem = (string)picker.SelectedItem;

            if (selectedItem.Equals(Constants.SearchDefaults.sortLowestText))
            {
                Search.SortLowToHigh = true;
            }
            else
            {
                Search.SortLowToHigh = false;
            }
        }

        // Slider Event

        private void HandlePriceSliderValueChanged(object sender, ValueChangedEventArgs args) {
            Search.ChosenHighPriceRange = (int)Math.Round(args.NewValue);
            PriceRangeValue.Text = String.Format("$0 - ${0}", Search.ChosenHighPriceRange);
        }

        private async void AddButtonClicked(object sender, EventArgs e) {
            // Validate fields are not empty
            if (!(modelSelected && sizeSelected)) {
                await DisplayAlert("Inputs are Missing", "Please make sure 'Model', and 'Size' are populated", "OK");
            }
            else{
                // Load Spinner

                activityIndicator.IsRunning = true;
                activityIndicator.IsVisible = true;
                AddButton.IsEnabled = false;
                AddButton.BackgroundColor = Color.FromHex(Constants.Button.disabled);

                ShoeSearch shoe = new ShoeSearch
                {
                    model = Search.ChosenModel,
                    size = Search.ChosenShoeSize,
                    low_price = Search.ChosenLowPriceRange,
                    high_price = Search.ChosenHighPriceRange,
                    sortLowToHigh = Search.SortLowToHigh
                };

                // Call /addToWatchist end point with shoeQuery info, on success display alert
                HttpWatchlistRequests watchlist = new HttpWatchlistRequests(App.RestClient);
                bool addedToWatchList = await watchlist.AddToWatchList(auth.username, shoe);

                await DisplayAlert("Saved to Watchlist", "", "OK");

                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
                AddButton.IsEnabled = true;
                AddButton.BackgroundColor = Color.FromHex(Constants.Button.mainBackgroundColour);

            }
        }

        private async void OnWatchListButtonClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new WatchListPage(auth));
        }

        private async void LogoutButtonClicked(object sender, EventArgs e) {
            string action = await DisplayActionSheet("Confirm logout?", "No", null, "Yes");
            if (action.Equals("Yes")) {
                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new SearchPage(auth));
            }
        }
    }
}
