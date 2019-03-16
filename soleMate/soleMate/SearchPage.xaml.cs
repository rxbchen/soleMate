
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

            Search.ChosenModel = (string)selectedItem; //TODO: Refactor to enum?
        }

        private void HandleSizeSelectedIndexChanged(object sender, EventArgs args) {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem; //TODO: Size is currently int

            Search.ChosenShoeSize = (int)selectedItem;
        }

        private void HandleSortPriceSelectedIndexChanged(object sender, EventArgs args) {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem; 

            Search.ChosenSorting = (string)selectedItem; //TODO: Refactor to enum?
        }

        // Slider Event

        private void HandlePriceSliderValueChanged(object sender, ValueChangedEventArgs args) {
            Search.ChosenHighPriceRange = (int)Math.Round(args.NewValue);
            PriceRangeValue.Text = String.Format("$0 - ${0}", Search.ChosenHighPriceRange);
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e) {

            //TODO: Include chosen sorting, when calling the database
            ShoeSearch shoe = new ShoeSearch {
                model = Search.ChosenModel,
                size = Search.ChosenShoeSize,
                low_price = Search.ChosenLowPriceRange,
                high_price = Search.ChosenHighPriceRange
            };

            // Calls the GET shoes/ api
            HttpSearchRequests search = new HttpSearchRequests(App.RestClient);
            SearchResult searchResult = await search.GetAllShoes();

            UnfilteredSearchPage unfilteredSearchPage = new UnfilteredSearchPage(shoe, searchResult);
            await Navigation.PushAsync(unfilteredSearchPage);
        }
    }
}