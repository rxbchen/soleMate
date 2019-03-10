namespace soleMate {
    using System;
    using soleMate.Model;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using System.Collections.Generic;

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

            // TO DO: Make call to data base here
            var searchResult = new SearchResult();
            searchResult.ShoeList = new ObservableCollection<Shoe>() {
                    new Shoe {id = 123, model = "Yeezy", price = 1000, size = 8, source = "ebay", title = "BEST YEEZY EVER", url = "https://google.com"},
                    new Shoe {id = 124, model = "Yeezy", price = 200, size = 8, source = "craigslist", title = "YEET YEEZY", url = "https://google.com"},
                    new Shoe {id = 125, model = "Yeezy", price = 300, size = 8, source = "ebay", title = "EZ YEEZY", url = "https://google.com"},
                    new Shoe {id = 126, model = "Yeezy", price = 400, size = 8, source = "kijiji", title = "GET YOUR YEEZY", url = "https://google.com"},
                    new Shoe {id = 127, model = "Yeezy", price = 700, size = 8, source = "craigslist", title = "PRETTY YEEZY", url = "https://google.com"}
                    };

            UnfilteredSearchPage unfilteredSearchPage = new UnfilteredSearchPage(shoe, searchResult);
            await Navigation.PushAsync(unfilteredSearchPage);
        }
    }
}