
namespace soleMate {
    using System;
    using soleMate.Model;
    using System.Collections.ObjectModel;
    using soleMate.Service.API;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SearchPage : ContentPage {

        // Private Variables

        private Search Search { get; set; }

        // Constructor

        public SearchPage() {
            InitializeComponent();
            IntializeSelectionData();
            StylePage();
        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            HttpSearchRequests search = new HttpSearchRequests(App.RestClient);
            Search.ModelList = await search.GetSupportedShoes();
            ModelPicker.ItemsSource = Search.ModelList;
        }

        // Private Methods

        private void IntializeSelectionData() {
            Search = new Search();
            ModelPicker.ItemsSource = Search.ModelList;
            SizePicker.ItemsSource = Search.ShoeSizeList;
            SortPricePicker.ItemsSource = Search.SortPriceList;
        }

        private void StylePage() {

            // Background Colours

            ModelPicker.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            SizePicker.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            SortPricePicker.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            PriceRangeValue.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            SearchButton.BackgroundColor = Color.FromHex(Constants.Button.mainBackgroundColour);

            // Enabling

            SearchButton.IsEnabled = true;
            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;

            // Button Sizes

            SearchButton.HeightRequest = Constants.Button.height;
            SearchButton.WidthRequest = Constants.Button.widthShort;

            // Slider Colours

            PriceSlider.MaximumTrackColor = Color.FromHex(Constants.Slider.maxTrackColour);
            PriceSlider.MinimumTrackColor = Color.FromHex(Constants.Slider.minTrackColour);
        }

        // Picker Events

        private void HandleModelSelectedIndexChanged(object sender, EventArgs args) {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;

            Search.ChosenModel = (string)selectedItem; 
        }

        private void HandleSizeSelectedIndexChanged(object sender, EventArgs args) {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;

            Search.ChosenShoeSize = (int)selectedItem;
        }

        private void HandleSortPriceSelectedIndexChanged(object sender, EventArgs args) {
            Picker picker = sender as Picker;
            string selectedItem = (string)picker.SelectedItem; 

            if (selectedItem.Equals(Constants.SearchDefaults.sortLowestText)) {
                Search.SortLowToHigh = true;
            }
            else {
                Search.SortLowToHigh = false;
            }
        }

        // Slider Event

        private void HandlePriceSliderValueChanged(object sender, ValueChangedEventArgs args) {
            Search.ChosenHighPriceRange = (int)Math.Round(args.NewValue);
            PriceRangeValue.Text = String.Format("$0 - ${0}", Search.ChosenHighPriceRange);
        }

        private async void OnSearchButtonClickedAsync(object sender, EventArgs e) {
            ShoeSearch shoe = new ShoeSearch {
                model = Search.ChosenModel,
                size = Search.ChosenShoeSize,
                low_price = Search.ChosenLowPriceRange,
                high_price = Search.ChosenHighPriceRange,
                sortLowToHigh = Search.SortLowToHigh
            };

            // Load Spinner

            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;
            SearchButton.IsEnabled = false;
            SearchButton.BackgroundColor = Color.FromHex(Constants.Button.disabled);

            // Calls the GET shoes/ api
            try {
                HttpSearchRequests search = new HttpSearchRequests(App.RestClient);
                SearchResult searchResult = await search.GetShoes(shoe);

                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
                SearchButton.IsEnabled = true;
                SearchButton.BackgroundColor = Color.FromHex(Constants.Button.mainBackgroundColour);

                FilteredSearchResultsPage unfilteredSearchPage = new FilteredSearchResultsPage(shoe, searchResult);
                await Navigation.PushAsync(unfilteredSearchPage);
            }
            catch (Exception) {
                //TODO: Handle Exception
                await DisplayAlert("Sorry, something went wrong", "", "OK");
                Console.WriteLine("Exception Met");
            }
        }

        private async void WatchListClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new WatchListPage());
        }

        private async void LogoutButtonClicked(object sender, EventArgs e) {
            await Navigation.PopToRootAsync();
            await Navigation.PushAsync(new SearchPage());
        }
    }
}