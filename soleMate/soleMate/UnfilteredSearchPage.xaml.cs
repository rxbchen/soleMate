using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using soleMate.Model;
using System.IO;

namespace soleMate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UnfilteredSearchPage : ContentPage
	{

        // Public Variables

        public string SearchResultText { get; }
        public string SortPriceText { get; }

        // Private Variables

        private ShoeSearch shoeQuery = new ShoeSearch();
        private SearchResult searchResult = new SearchResult();

        // Constructors 

        public UnfilteredSearchPage() {
            InitializeComponent();
            BindingContext = this;
        }

        public UnfilteredSearchPage(ShoeSearch shoeQuery, SearchResult searchResult) {
            InitializeComponent();

            SearchResultText = $"{shoeQuery.model}, Size {shoeQuery.size}, ${shoeQuery.low_price}-${shoeQuery.high_price}";
            SortPriceText = "Lowest"; //TODO: Change to shoeQuery.sortPrice after database talk

            BindingContext = this;
            this.shoeQuery = shoeQuery;
            this.searchResult = searchResult;

            CreateGrid();
        }

        // Private Methods

        private void CreateGrid() {
            int num_shoes = searchResult.ShoeList.Count;
            int num_rows = num_shoes / 2 + num_shoes % 2;

            // num_rows Rows
            for (int i = 0; i < num_rows; i++) {
                gridLayout.RowDefinitions.Add(new RowDefinition());
            }

            // 2 Columns
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());

            // Populate Grid
            int shoeResultNum = 0;
            for (int rowIndex = 0; rowIndex < num_rows; rowIndex++) { 
                for (int colIndex = 0; colIndex < 2; colIndex++) { 

                    if (shoeResultNum >= num_shoes) {
                        break; 
                    }

                    var image = new Image
                    {
                        Source = ImageSource.FromFile("tempImage.png"),//TODO: Crawler image from URL
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

                        Console.WriteLine("ID: " + imageID);

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
                        BackgroundColor = Color.FromHex(Constants.SearchItem.overlayBackgroundColour),
                        Opacity = Constants.SearchItem.opacity
                    };

                    var outline = new Frame {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        HeightRequest = Constants.SearchItem.outlineHeight,
                        WidthRequest = Constants.SearchItem.outlineWidth,
                        BorderColor = Color.FromHex(Constants.SearchItem.outlineColour),
                        HasShadow = false,
                        Padding = new Thickness(0,0,0,0)
                    };

                    var label = new Label {
                        Text = String.Format("${0}", searchResult.ShoeList[shoeResultNum].Price),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        TextColor = Color.FromHex(Constants.Text.green),
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
    }
}