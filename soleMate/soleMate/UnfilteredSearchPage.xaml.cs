using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace soleMate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UnfilteredSearchPage : ContentPage
	{
        // Constructors
        public UnfilteredSearchPage() {
            InitializeComponent();
            BindingContext = this;
        }

        public UnfilteredSearchPage(Model.ShoeSearch shoeQuery) {
            InitializeComponent();
            this.shoeQuery = shoeQuery;
            // TO DO: Refactor cause this is terrible lol
            if (shoeQuery.high_price > 500) {
                SearchResultText = $"Showing Search Results for: {shoeQuery.model}, Size {shoeQuery.size}, ${shoeQuery.low_price}-$500+";
            } else { 
                SearchResultText = $"Showing Search Results for: {shoeQuery.model}, Size {shoeQuery.size}, ${shoeQuery.low_price}-${shoeQuery.high_price}";
            }
            BindingContext = this;
        }

        // Private Variables
        private Model.ShoeSearch shoeQuery = new Model.ShoeSearch();
        private string SearchResultText { get; }
        //private Model.SearchResult searchResult = new Model.SearchResult();

        // Private Methods
        //private void createGrid() {
        //    int num_shoes = searchResult.ShoeList.Count;
        //    int num_rows = num_shoes / 2 + num_shoes % 2;

        //    // num_rows Rows
        //    for (int i = 0; i < num_rows; i++) {
        //        gridLayout.RowDefinitions.Add(new RowDefinition());
        //    }

        //    // 2 Columns
        //    gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
        //    gridLayout.ColumnDefinitions.Add(new ColumnDefinition());

        //    // Populate Grid
        //    foreach (var shoe in searchResult.ShoeList) { 
        //        for (int rowIndex = 0; rowIndex < num_rows; rowIndex++) { 
        //            for (int colIndex = 0; colIndex < 2; colIndex++) {
        //                var label = new Label {
        //                    Text = "${shoe.model}",
        //                    VerticalOptions = LayoutOptions.Center,
        //                    HorizontalOptions = LayoutOptions.Center
        //                };
        //                gridLayout.Children.Add(label, rowIndex, colIndex);
        //            }
        //        }
        //    }
        //}

    }
}