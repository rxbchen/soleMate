using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace soleMate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UnfilteredSearchPage : ContentPage
	{
        // Constructors
        public UnfilteredSearchPage() {
            InitializeComponent();
        }

        public UnfilteredSearchPage(Model.ShoeSearch shoeSearch, Model.SearchResult searchResult) {
            InitializeComponent();
            this.shoeSearch = shoeSearch;
            this.searchResult = searchResult;
            // TO DO: Refactor cause this is terrible lol
            if (shoeSearch.high_price > 500) {
                SearchResultText = $"Showing Search Results for: {shoeSearch.model}, Size {shoeSearch.size}, ${shoeSearch.low_price}-$500+";
            } else { 
                SearchResultText = $"Showing Search Results for: {shoeSearch.model}, Size {shoeSearch.size}, ${shoeSearch.low_price}-${shoeSearch.high_price}";
            }

            BindingContext = this;
        }

        // Private Variables
        private string SearchResultText { get; }
        private Model.ShoeSearch shoeSearch = new Model.ShoeSearch();
        private Model.SearchResult searchResult = new Model.SearchResult();

        // Private Methods
        private void createGrid() {

            var grid = new Grid
            {
                RowDefinitions = {

                }
            };

            //UnfilteredSearchLayout.Children.Add();
        }

    }
}