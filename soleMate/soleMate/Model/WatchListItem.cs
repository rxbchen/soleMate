namespace soleMate.Model {
    using System;
    using System.Collections.Generic;
    using soleMate.Service.API;

    public class WatchListItem {

        // Public Variables

        public string Model { get; }
        public int Size { get; }
        public int PriceMin { get; }
        public int PriceMax { get; }


        // Constructor

        public WatchListItem(string Model, int Size, int PriceMin, int PriceMax) {
            this.Model = Model;
            this.Size = Size;
            this.PriceMin = PriceMin;
            this.PriceMax = PriceMax;
        }

        // Public Methods

        public async System.Threading.Tasks.Task<SearchResult> SearchWatchListItem() {
            SearchResult searchResult = new SearchResult();

            ShoeSearch shoe = new ShoeSearch {
                model = Model,
                size = Size,
                low_price = PriceMin,
                high_price = PriceMax,
                sortLowToHigh = Constants.SearchDefaults.sortLowToHigh
            };

            try {
                HttpSearchRequests search = new HttpSearchRequests(App.RestClient);
                searchResult = await search.GetShoes(shoe);
            }
            catch (Exception) {
                //TODO: Handle Exception
                Console.WriteLine("Could not Search from server");
            }

            return searchResult;
        }

        public async void DeleteWatchListItemAsync(string username) {
            try {
                HttpWatchlistRequests delete = new HttpWatchlistRequests(App.RestClient);
                await delete.Delete(username, 
                                    this.Model, 
                                    this.Size,
                                    this.PriceMin,
                                    this.PriceMax);
            }
            catch (Exception) {
                //TODO: Handle Exception
                Console.WriteLine("Could not delete item from server");
            }
        }
    }
}

