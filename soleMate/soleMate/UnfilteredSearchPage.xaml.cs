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
                     

                    // TO DO: Figure out proper image path
                    var image = new Image
                    {
                        Source = ImageSource.FromFile(Path.Combine(Directory.GetCurrentDirectory(), @"\Images\tempImage.png"))
                    };

                    var label = new Label {
                        Text = String.Format("${0}", searchResult.ShoeList[shoeResultNum].Price),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center
                    };

                    shoeResultNum += 1;

                    gridLayout.Children.Add(label, colIndex, rowIndex);
                    gridLayout.Children.Add(image, colIndex, rowIndex);
                }
            }
        }

    }
}