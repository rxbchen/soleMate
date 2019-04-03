namespace soleMate.Model {
    using System;
    using System.Collections.Generic;
    using soleMate.Service.API;

    public class WatchListItem {

        // Public Variables

        public bool searchingWatchListItem = false;
        public string Model { get; }
        public int ShoeSize { get; }
        public int LowPriceRange { get; }
        public int HighPriceRange { get; }


        // Constructor

        public WatchListItem(string Model, int ShoeSize, int LowPriceRange, int HighPriceRange) {
            this.Model = Model;
            this.ShoeSize = ShoeSize;
            this.LowPriceRange = LowPriceRange;
            this.HighPriceRange = HighPriceRange;
        }

        // Public Methods

        public async void WatchListItemClickedAsync() {
            ShoeSearch shoe = new ShoeSearch {
                model = Model,
                size = ShoeSize,
                low_price = LowPriceRange,
                high_price = HighPriceRange,
                sortLowToHigh = Constants.SearchDefaults.sortLowToHigh
            };

            try {
                searchingWatchListItem = true;
                HttpSearchRequests search = new HttpSearchRequests(App.RestClient);
                SearchResult searchResult = await search.GetShoes(shoe);

                //TODO: Figure out Activity Spinner

                FilteredSearchResultsPage unfilteredSearchPage = new FilteredSearchResultsPage(shoe, searchResult);
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(unfilteredSearchPage);
            }
            catch (Exception)
            {
                //TODO: Handle Exception
                //await DisplayAlert("Sorry, something went wrong", "", "OK");
                Console.WriteLine("Exception Met");
            }
        }
    }
}

