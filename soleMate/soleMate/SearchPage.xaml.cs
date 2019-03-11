
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

        // Constructor

        public SearchPage() {
            InitializeComponent();
            IntializeSelectionData();
            StylePage();
        }

        // Private Variables

        private string model = "Yeezy";
        private int size = 8;
        private const int lowPriceRange = 0;
        private int highPriceRange = 100;

        // Private Methods

        private void IntializeSelectionData() {

            // Model Picker

            var modelList = new List<string>();
            modelList.Add("Yeezy");
            modelList.Add("Nike");
            modelList.Add("Adidas");

            ModelPicker.ItemsSource = modelList;

            // Size Picker

            var shoeSizeList = new List<int>();
            for (int i=5; i<11; i++) {
                shoeSizeList.Add(i);
            }

            SizePicker.ItemsSource = shoeSizeList;
        }

        private void StylePage() {

            // Background Colours

            ModelPicker.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            SizePicker.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            PriceRangeValue.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            SearchButton.BackgroundColor = Color.FromHex(Constants.Button.mainBackgroundColour);

            // Button Sizes

            SearchButton.HeightRequest = Constants.Button.height;
            SearchButton.WidthRequest = Constants.Button.widthShort;

            // Slider Colours

            PriceSlider.MaximumTrackColor = Color.FromHex(Constants.Slider.maxTrackColour);
            PriceSlider.MinimumTrackColor = Color.FromHex(Constants.Slider.minTrackColour);
        }

        private void HandlePriceSliderValueChanged(object sender, ValueChangedEventArgs args) {
            highPriceRange = (int)Math.Round(args.NewValue);
            PriceRangeValue.Text = String.Format("$0 - ${0}", highPriceRange);
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e) {

            ShoeSearch shoe = new ShoeSearch {
                model = model,
                size = size,
                low_price = lowPriceRange,
                high_price = highPriceRange
            };

            // Calls the GET shoes/ api
            HttpSearchRequests search = new HttpSearchRequests(App.RestClient);
            SearchResult searchResult = await search.GetAllShoes();

            UnfilteredSearchPage unfilteredSearchPage = new UnfilteredSearchPage(shoe, searchResult);
            await Navigation.PushAsync(unfilteredSearchPage);
        }
    }
}