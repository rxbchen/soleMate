namespace soleMate {
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;
    using Xamarin.Forms.Xaml;
    using soleMate.Model;
    using System.IO;
    using soleMate.Service.API;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilteredSearchResultsPage : ContentPage {

        // Public Variables

        public string SearchResultText { get; }
        public string SortPriceText { get; }
        public bool ResultsReturned { get; }

        // Private Variables

        private ShoeSearch shoeQuery = new ShoeSearch();
        private SearchResult searchResult = new SearchResult();
        private CredentialsAuthentication auth;
        private int num_shoes;
        private int num_rows;
        private bool savedWatchList = false;

        // Constructors 

        public FilteredSearchResultsPage(WatchListItem watchListItem) {
            InitializeComponent();
            BindingContext = this;
            num_shoes = 0;
            num_rows = 0;
        }

        public FilteredSearchResultsPage(ShoeSearch shoeQuery, SearchResult searchResult, CredentialsAuthentication auth) {
            InitializeComponent();

            SearchResultText = $"{shoeQuery.model}, Size {shoeQuery.size}, ${shoeQuery.low_price}-${shoeQuery.high_price}";

            if (shoeQuery.sortLowToHigh) {
                SortPriceText = $"Sort Price: {Constants.SearchDefaults.sortLowestText}";
            } else {
                SortPriceText = $"Sort Price: {Constants.SearchDefaults.sortHighestText}";
            }

            ResultsReturned = searchResult.ShoeList.Count != 0;

            num_shoes = searchResult.ShoeList.Count;
            num_rows = num_shoes / 2 + num_shoes % 2;


            BindingContext = this;
            this.shoeQuery = shoeQuery;
            this.searchResult = searchResult;
            this.auth = auth;

            SortPricePicker.ItemsSource = new Search().SortPriceList;

            StylePage();

            if (num_shoes != 0) {
                CreateGrid();
            } else {
                CreateEmptyState();
            }

        }

        // Private Methods

        private void CreateGrid() {
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

                    var image = new Image {
                        Source = DetermineImageSource(shoeResultNum),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HeightRequest = Constants.SearchItem.imageHeight,
                        WidthRequest = Constants.SearchItem.imageWidth,
                        ClassId = "" + shoeResultNum
                    };

                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();

                    tapGestureRecognizer.NumberOfTapsRequired = Constants.SearchItem.numberOfTapsRequired;
                    tapGestureRecognizer.Tapped += async (s, e) => {
                        var imageSender = (Image)s;
                        int imageID = Convert.ToInt32(imageSender.ClassId);

                        string action = await DisplayActionSheet("Open in browser?", "Cancel", null, "Yes");
                        if (action.Equals("Yes")) {
                            Console.WriteLine("Opening up page");
                            Device.OpenUri(new Uri(searchResult.ShoeList[imageID].Url));
                        }
                    };


                    image.GestureRecognizers.Add(tapGestureRecognizer);

                    var overlay = new BoxView {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        HeightRequest = Constants.SearchItem.overlayHeight,
                        WidthRequest = Constants.SearchItem.outlineWidth,
                        BackgroundColor = Color.FromHex(Constants.SearchItem.overlayBackgroundColour)
                    };

                    var outline = new Frame {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        HeightRequest = Constants.SearchItem.outlineHeight,
                        WidthRequest = Constants.SearchItem.outlineWidth,
                        BorderColor = Color.FromHex(Constants.SearchItem.outlineColour),
                        HasShadow = false,
                        Padding = new Thickness(0, 0, 0, 0)
                    };

                    var label = new Label {
                        Text = String.Format("${0}", searchResult.ShoeList[shoeResultNum].Price),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        TextColor = Color.White,
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(5, 0, 0, 0),
                    };

                    shoeResultNum += 1;

                    gridLayout.Children.Add(outline, colIndex, rowIndex);
                    gridLayout.Children.Add(image, colIndex, rowIndex);
                    gridLayout.Children.Add(overlay, colIndex, rowIndex);
                    gridLayout.Children.Add(label, colIndex, rowIndex);
                }
            }
        }

        private void CreateEmptyState() {
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());

            var mainText = new Label {
                Text = Constants.EmptyState.searchResultTextMain,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.FromHex(Constants.Text.red),
                FontAttributes = FontAttributes.Bold
            };

            var secondaryText = new Label {
                Text = Constants.EmptyState.searchResultTextSecondary,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            gridLayout.Children.Add(mainText, 0, 0);
            gridLayout.Children.Add(secondaryText, 0, 1);
        }

        private ImageSource DetermineImageSource(int imageID) {
            if (searchResult.ShoeList[imageID].Photo != null) {
                return searchResult.ShoeList[imageID].Photo;
            } 
            else {
                return ImageSource.FromFile("tempImage.png");
            }
        }

        private void StylePage() {
            AddToWatchListButton.TextColor = Color.White;
            AddToWatchListButton.BackgroundColor = Color.FromHex(Constants.Button.mainBackgroundColour);

            SortPricePicker.TextColor = Color.White;
            SortPricePicker.BackgroundColor = Color.FromHex(Constants.Button.thirdBackgroundColour);
        }

        private void HandleSortPriceSelectedIndexChanged(object sender, EventArgs args) {
            Picker picker = sender as Picker;
            string selectedItem = (string)picker.SelectedItem;
            string currentlySelected = shoeQuery.sortLowToHigh ? Constants.SearchDefaults.sortLowestText : Constants.SearchDefaults.sortHighestText;

            if (!selectedItem.Equals(currentlySelected)) {
                bool newSort = !shoeQuery.sortLowToHigh;
                shoeQuery.sortLowToHigh = newSort;

                // Reverse Sort
                searchResult.ReverseSorting();
                PopulateGrid();
            }
        }

        private async void WatchListClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new WatchListPage(auth));
        }

        private async void LogoutButtonClicked(object sender, EventArgs e) {
            string action = await DisplayActionSheet("Are you sure you want to logout?", "No", null, "Yes");
            if (action.Equals("Yes")) {
                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new SearchPage(auth));
            }
        }

        private async void AddToWatchListButtonClickedAsync(object sender, EventArgs e) {
            if (!savedWatchList) {
                //TODO: Populate Wish List
                // Call /addToWatchist end point with shoeQuery info, on success display alert

                HttpWatchlistRequests watchlist = new HttpWatchlistRequests(App.RestClient);
                bool addedToWatchList = await watchlist.AddToWatchList(auth.username, shoeQuery);

                if (addedToWatchList) {
                    await DisplayAlert("Saved to Watchlist", "", "OK");
                    AddToWatchListButton.BackgroundColor = Color.FromHex(Constants.Button.disabled);
                    savedWatchList = true;
                } else {
                    await DisplayAlert("Sorry! Could not save to Watchlist", "Try again later", "OK");
                }

            } else {
                await DisplayAlert("Already Saved to Watchlist", "", "OK");
            }
        }
    }
}