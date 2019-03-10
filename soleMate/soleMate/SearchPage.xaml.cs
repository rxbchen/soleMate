
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
        private int low_price_range = 0;
        private int high_price_range = 100;

        // Private Methods

        private void IntializeSelectionData() {
            var modelList = new List<string>();
            modelList.Add("Yeezy");
            modelList.Add("Nike");
            modelList.Add("Adidas");

            ModelPicker.ItemsSource = modelList;

            var shoeSizeList = new List<int>();
            for (int i=5; i<11; i++) {
                shoeSizeList.Add(i);
            }

            SizePicker.ItemsSource = shoeSizeList;
        }

        private void StylePage() {
            ModelPicker.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            SizePicker.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            PriceRangeValue.BackgroundColor = Color.FromHex(Constants.InputField.backgroundColour);
            SearchButton.BackgroundColor = Color.FromHex(Constants.Button.mainBackgroundColour);

            SearchButton.HeightRequest = Constants.Button.height;
            SearchButton.WidthRequest = Constants.Button.widthShort;

            PriceSlider.MaximumTrackColor = Color.FromHex(Constants.Slider.maxTrackColour);
            PriceSlider.MinimumTrackColor = Color.FromHex(Constants.Slider.minTrackColour);
        }

        private void HandlePriceSliderValueChanged(object sender, ValueChangedEventArgs args) {
            double value = args.NewValue;
            PriceRangeValue.Text = String.Format("$0 - ${0}", value);
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e) {

            ShoeSearch shoe = new ShoeSearch {
                model = model,
                size = size,
                low_price = low_price_range,
                high_price = high_price_range
            };

            // Calls the GET shoes/ api
            HttpSearchRequests search = new HttpSearchRequests(App.RestClient);
            SearchResult searchResult = await search.GetAllShoes();

            UnfilteredSearchPage unfilteredSearchPage = new UnfilteredSearchPage(shoe, searchResult);
            await Navigation.PushAsync(unfilteredSearchPage);
        }
    }
}