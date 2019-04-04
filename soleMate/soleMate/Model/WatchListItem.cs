namespace soleMate.Model {
    using System;
    using System.Collections.Generic;
    using soleMate.Service.API;

    public class WatchListItem {

        // Public Variables

        public bool searchingWatchListItem = false;
        public string Model { get; }
        public int Size { get; }
        public int PriceMin { get; }
        public int PriceMax { get; }


        // Constructor

        public WatchListItem(string Model, int Size, int priceMin, int PriceMax) {
            this.Model = Model;
            this.Size = Size;
            this.PriceMin = priceMin;
            this.PriceMax = PriceMax;
        }

        // Public Methods

        public async void WatchListItemClickedAsync(CredentialsAuthentication auth) {
            ShoeSearch shoe = new ShoeSearch {
                model = Model,
                size = Size,
                low_price = PriceMin,
                high_price = PriceMax,
                sortLowToHigh = Constants.SearchDefaults.sortLowToHigh
            };

            try {
                searchingWatchListItem = true;
                HttpSearchRequests search = new HttpSearchRequests(App.RestClient);
                SearchResult searchResult = await search.GetShoes(shoe);

                //TODO: Figure out Activity Spinner

                FilteredSearchResultsPage unfilteredSearchPage = new FilteredSearchResultsPage(shoe, searchResult, auth);
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(unfilteredSearchPage);
            }
            catch (Exception)
            {
                //TODO: Handle Exception
                //await DisplayAlert("Sorry, something went wrong", "", "OK");
                Console.WriteLine("Exception Met");
            }
        }

        public async void DeleteWatchListItemAsync(string username)
        {
        
            //bool isDeleted = false;
            try
            {
                HttpWatchlistRequests delete = new HttpWatchlistRequests(App.RestClient);
                await delete.Delete(username, 
                                    this.Model, 
                                    this.Size,
                                    this.PriceMin,
                                    this.PriceMax);
            }
            catch (Exception)
            {
                //TODO: Handle Exception
                Console.WriteLine("Could not delete item from server");
            }
            //return isDeleted;
        }
    }
}

