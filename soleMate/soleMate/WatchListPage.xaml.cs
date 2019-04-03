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
        private int num_shoes;
        private int num_rows;

        // Constructor

        public WatchListPage() {
            InitializeComponent();
            InitializeData();

            if (num_shoes != 0) {
                CreateGrid();
            }
            else {
                CreateEmptyState();
            }
        }

        // Private Methods

        private void InitializeData() {
            WatchListItem item1 = new WatchListItem("Yeezy", 10, 0, 700);
            WatchListItem item2 = new WatchListItem("Yeezy", 9, 0, 700);
            WatchListItem item3 = new WatchListItem("Yeezy", 8, 0, 700);
            WatchListItem item4 = new WatchListItem("Yeezy", 5, 0, 700);

            watchList.Add(item1);
            watchList.Add(item2);
            watchList.Add(item3);
            watchList.Add(item4);

            num_shoes = watchList.Count;

            Task.Run(async () => { 
                try {
                    //TODO: Implement
                    //HttpSearchRequests search = new HttpSearchRequests(App.RestClient);
                    //SearchResult searchResult = await search.GetWishList();
                } 
                catch {
                    await DisplayAlert("Sorry, something went wrong", "", "OK");
                    Console.WriteLine("Exception Met");
                }
            });
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
                        HeightRequest = Constants.SearchItem.outlineHeight,
                        WidthRequest = Constants.SearchItem.outlineWidth,
                        BackgroundColor = Color.FromHex(Constants.SearchItem.overlayBackgroundColour),
                        Opacity = Constants.SearchItem.opacity
                    };

                    var outline = new Frame
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        HeightRequest = Constants.SearchItem.outlineHeight,
                        WidthRequest = Constants.SearchItem.outlineWidth,
                        BorderColor = Color.FromHex(Constants.SearchItem.outlineColour),
                        HasShadow = false,
                        Padding = new Thickness(0, 0, 0, 0)
                    };

                    var label = new Label
                    {
                        Text = String.Format("{0} \n Size: {1} \n ${2} - ${3}", watchList[shoeResultNum].Model, watchList[shoeResultNum].ShoeSize, watchList[shoeResultNum].LowPriceRange, watchList[shoeResultNum].HighPriceRange),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Color.White,
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(5, 0, 0, 0),
                    };

                    shoeResultNum += 1;

                    gridLayout.Children.Add(outline, colIndex, rowIndex);
                    gridLayout.Children.Add(overlay, colIndex, rowIndex);
                    gridLayout.Children.Add(label, colIndex, rowIndex);
                }
            }
        }

        private async void AddWatchListButtonClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new AddWatchList());
        }

        private async void SearchPageClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new SearchPage());
        }

        private async void LogoutButtonClicked(object sender, EventArgs e) {
            string action = await DisplayActionSheet("Are you sure you want to logout?", "No", null, "Yes");
            if (action.Equals("Yes")) {
                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new SearchPage());
            }
        }
    }
}
