namespace soleMate {
    using System;
    using System.Collections.Generic;
    using soleMate.Model;
    using Xamarin.Forms;

    public partial class AddWatchList : ContentPage {

        // Private Variables

        private Search Search { get; set; }

        // Constructor

        public AddWatchList() {
            InitializeComponent();
            IntializeSelectionData();
            StylePage();
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

            // Button Sizes

            AddButton.HeightRequest = Constants.Button.height;
            AddButton.WidthRequest = Constants.Button.widthShort;

            // Slider Colours

            PriceSlider.MaximumTrackColor = Color.FromHex(Constants.Slider.maxTrackColour);
            PriceSlider.MinimumTrackColor = Color.FromHex(Constants.Slider.minTrackColour);
        }

        // Picker Events

        private void HandleModelSelectedIndexChanged(object sender, EventArgs args) {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;

            Search.ChosenModel = (string)selectedItem; //TODO: Refactor to enum?
        }

        private void HandleSizeSelectedIndexChanged(object sender, EventArgs args) {
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

        private void AddButtonClicked(object sender, EventArgs e) {
            //TODO: Populate Wish List
            // Call /addToWatchist end point with shoeQuery info, on success display alert
            DisplayAlert("Saved to Watchlist", "", "OK");

            //TODO: Replace Alert with A pop up page
        }

        private async void OnWatchListButtonClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new WatchListPage());
        }

        private async void LogoutButtonClicked(object sender, EventArgs e) {
            await Navigation.PopToRootAsync();
            await Navigation.PushAsync(new SearchPage());
        }
    }
}
