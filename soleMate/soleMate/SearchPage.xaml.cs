using System;
using soleMate.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace soleMate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage {
        // Constructor
        public SearchPage() {
            InitializeComponent();
        }

        // Private Variables

        // TO DO: Change naming of labels (ex. yeezyLabel)
        private string model = "Yeezy";
        private int size = 8;
        private int low_price_range = 0;
        private int high_price_range = 100;
        private bool tier1_chosen = false;
        private bool tier2_chosen = false;
        private bool tier3_chosen = false;

        // Private Methods

        private async void OnSearchButtonClicked(object sender, EventArgs e) {

            determinePriceRange();

            ShoeSearch shoe = new ShoeSearch {
                model = model,
                size = size,
                low_price = low_price_range,
                high_price = high_price_range
            };

            // TO DO: Make call to data base here
            //var searchResult = new SearchResult();
            //searchResult.ShoeList = new ObservableCollection<Shoe>() {
                    //new Shoe {id = 123, model = "Yeezy", price = 1000, size = 8, source = "ebay", title = "BEST YEEZY EVER", url = "https://google.com"},
                    //new Shoe {id = 124, model = "Yeezy", price = 200, size = 8, source = "craigslist", title = "YEET YEEZY", url = "https://google.com"},
                    //new Shoe {id = 125, model = "Yeezy", price = 300, size = 8, source = "ebay", title = "EZ YEEZY", url = "https://google.com"},
                    //new Shoe {id = 126, model = "Yeezy", price = 400, size = 8, source = "kijiji", title = "GET YOUR YEEZY", url = "https://google.com"},
                    //new Shoe {id = 127, model = "Yeezy", price = 700, size = 8, source = "craigslist", title = "PRETTY YEEZY", url = "https://google.com"}
                    //};

            UnfilteredSearchPage unfilteredSearchPage = new UnfilteredSearchPage(shoe);
            await Navigation.PushAsync(unfilteredSearchPage);
        }

        // TO DO: Refactor all toggles into one function and check for type
        // TO DO: Model should be a bool first, then you check and convert into String
        private void ModelSwitchToggledYeezy(object sender, ToggledEventArgs e) {
            if (e.Value) {
                model = "Yeezy";
                Yeezy.TextColor = Color.FromHex("#D33F49");
            } 
            else {
                Yeezy.TextColor = Color.Black;
            }
        }

        private void ModelSwitchToggledNike(object sender, ToggledEventArgs e) {
            if (e.Value)
            {
                model = "Nike";
                Nike.TextColor = Color.FromHex("#D33F49");
            }
            else
            {
                Nike.TextColor = Color.Black;
            }
        }

        private void ModelSwitchToggledAdidas(object sender, ToggledEventArgs e) {
            if (e.Value)
            {
                model = "Adidas";
                Adidas.TextColor = Color.FromHex("#D33F49");
            }
            else
            {
                Adidas.TextColor = Color.Black;
            }
        }

        // TO DO: Refactor all toggle functions into one and check for type
        // TO DO: Better way for price checks

        private void ModelSwitchToggledTier1(object sender, ToggledEventArgs e) {
            tier1_chosen = e.Value;

            if (tier1_chosen)
            {
                Tier1.TextColor = Color.FromHex("#D33F49");
            }
            else
            {
                Tier1.TextColor = Color.Black;
            }
        }

        private void ModelSwitchToggledTier2(object sender, ToggledEventArgs e) {
            tier2_chosen = e.Value;

            if (tier2_chosen)
            {
                Tier2.TextColor = Color.FromHex("#D33F49");

            }
            else
            {
                Tier2.TextColor = Color.Black;
            }
        }

        private void ModelSwitchToggledTier3(object sender, ToggledEventArgs e) {
            tier3_chosen = e.Value;

            if (tier3_chosen)
            {
                Tier3.TextColor = Color.FromHex("#D33F49");
            }
            else
            {
                Tier3.TextColor = Color.Black;
            }
        }

        // TO DO: Refactor into correctly picking prices
        private void determinePriceRange() { 
            if (tier1_chosen && tier2_chosen && tier3_chosen) {
                low_price_range = 0;
                high_price_range = 600; // Change logic
            } else { 
                low_price_range = 0;
                high_price_range = 100;
            }
        }
    }
}