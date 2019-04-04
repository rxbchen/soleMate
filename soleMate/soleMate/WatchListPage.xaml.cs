namespace soleMate {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using soleMate.Model;
    using soleMate.Service.API;
    using Xamarin.Forms;

    public partial class WatchListPage : ContentPage {

        // Private Variables

        private List<WatchListItem> watchList = new List<WatchListItem>();
        private CredentialsAuthentication auth;
        private int num_shoes;
        private int num_rows;

        // Constructor

        public WatchListPage(CredentialsAuthentication auth) {
            this.auth = auth;

            InitializeComponent();
            InitializeData();
            StylePage();
        }

        // Private Methods

        private void InitializeData() {
            Task.Run(async () => {
                try {
                    HttpWatchlistRequests watchListRequest = new HttpWatchlistRequests(App.RestClient);
                    watchList = await watchListRequest.GetWatchlist(auth.username);
                    num_shoes = watchList.Count;

                    Device.BeginInvokeOnMainThread(() => {
                        if (num_shoes != 0) {
                            CreateGrid();
                        }
                        else {
                            CreateEmptyState();
                        }
                    });
                } 
                catch (Exception e){
                    await DisplayAlert("Sorry, something went wrong", "", "OK");
                    Console.WriteLine("Exception Met");
                    Console.WriteLine(e.Message);
                }
            });
        }

        private void StylePage() {
            AddWatchListButton.HeightRequest = Constants.Button.imageHeight;
            AddWatchListButton.WidthRequest = Constants.Button.imageWidth;
        }

        private void CreateEmptyState() {
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());

            var mainText = new Label
            {
                Text = Constants.EmptyState.watchListTextMain,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.FromHex(Constants.Text.red),
                FontAttributes = FontAttributes.Bold
            };

            gridLayout.Children.Add(mainText, 0, 0);
        }

        private void CreateGrid() {
            num_rows = num_shoes / 2 + num_shoes % 2;

            // num_rows Rows
            for (int i = 0; i < num_rows; i++) {
                gridLayout.RowDefinitions.Add(new RowDefinition());
            }

            // 2 Columns
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());

            // Populate Grid
            PopulateGrid();
        }

        private void PopulateGrid() {

            // Clear Grid

            if ((gridLayout != null) && (gridLayout.Children != null)) {
                for (int i = 0; i < gridLayout.Children.Count; i++) {
                    gridLayout.Children.RemoveAt(i);
                }
            }

            int shoeResultNum = 0;
            for (int rowIndex = 0; rowIndex < num_rows; rowIndex++) {
                for (int colIndex = 0; colIndex < 2; colIndex++) {

                    if (shoeResultNum >= num_shoes) {
                        break;
                    }

                    var overlay = new BoxView
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        HeightRequest = Constants.WatchListItem.outlineHeight,
                        WidthRequest = Constants.WatchListItem.outlineWidth,
                        BackgroundColor = Color.FromHex(Constants.WatchListItem.overlayBackgroundColour),
                        ClassId = "" + shoeResultNum
                    };

                    var outline = new Frame
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        HeightRequest = Constants.WatchListItem.outlineHeight,
                        WidthRequest = Constants.WatchListItem.outlineWidth,
                        BorderColor = Color.FromHex(Constants.WatchListItem.outlineColour),
                        HasShadow = false,
                        Padding = new Thickness(0, 0, 0, 0)
                    };

                    var label = new Label
                    {
                        Text = String.Format("{0}\nSize: {1}\n${2} - ${3}", watchList[shoeResultNum].Model, watchList[shoeResultNum].Size, watchList[shoeResultNum].PriceMin, watchList[shoeResultNum].PriceMax),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Color.White,
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(5, 0, 0, 0),
                    };

                    // Display action sheet when a watchlist item is tapped
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.NumberOfTapsRequired = 1;
                    tapGestureRecognizer.Tapped += async (s, e) => {
                        var boxviewSender = (BoxView)s;
                        int boxviewID = Convert.ToInt32(boxviewSender.ClassId); 
                        string action = await DisplayActionSheet("Watchlist Item Actions", "Cancel", null, "Search", "Delete");
                        if (action.Equals("Search")) {
                            Console.WriteLine("Conducting a search on watchlist item");
                            WatchListItem currentWatchList = watchList[boxviewID];
                            SearchResult searchResult = await currentWatchList.SearchWatchListItem();

                            ShoeSearch shoe = new ShoeSearch {
                                model = currentWatchList.Model,
                                size = currentWatchList.Size,
                                low_price = currentWatchList.PriceMin,
                                high_price = currentWatchList.PriceMax,
                                sortLowToHigh = Constants.SearchDefaults.sortLowToHigh
                            };

                            FilteredSearchResultsPage FilteredSearchPage = new FilteredSearchResultsPage(shoe, searchResult, auth);
                            await Navigation.PushAsync(FilteredSearchPage);
                        }
                        else if (action.Equals("Delete")) {
                            Console.WriteLine("Deleting item from user's watchlist");
                            watchList[boxviewID].DeleteWatchListItemAsync(auth.username);
                            Console.WriteLine("Deleted"); 
                        }
                    };
                    overlay.GestureRecognizers.Add(tapGestureRecognizer);


                    shoeResultNum += 1;

                    gridLayout.Children.Add(outline, colIndex, rowIndex);
                    gridLayout.Children.Add(overlay, colIndex, rowIndex);
                    gridLayout.Children.Add(label, colIndex, rowIndex);
                }
            }

        }

        private async void AddWatchListButtonClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new AddWatchList(auth));
        }

        private async void SearchPageClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new SearchPage(auth));
        }

        private async void LogoutButtonClicked(object sender, EventArgs e) {
            string action = await DisplayActionSheet("Confirm logout?", "No", null, "Yes");
            if (action.Equals("Yes")) {
                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new SearchPage(auth));
            }
        }
    }
}
