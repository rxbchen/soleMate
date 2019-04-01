namespace soleMate.Model {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Search {

        // Public Variables
        public List<string> ModelList { get; set; }
        public List<int> ShoeSizeList { get; set; }
        public List<string> SortPriceList { get; set; }

        public string ChosenModel { get; set; }
        public int ChosenShoeSize { get; set; }
        public int ChosenLowPriceRange { get; set; }
        public int ChosenHighPriceRange { get; set; }
        public bool SortLowToHigh { get; set; }

        // Constructor

        public Search() {
            //TODO: Call to database to get these values
            //TODO: Some Constants/Enums need to be implemented

            // Models
            ModelList = new List<string> { "Yeezy", "Nike", "Adidas" };

            // Shoe Sizes - Standard Sizes
            ShoeSizeList = new List<int>();
            for (int i = 5; i < 11; i++) {
                ShoeSizeList.Add(i);
            }

            // Sort Price
            SortPriceList = new List<string> { Constants.SearchDefaults.sortLowestText, Constants.SearchDefaults.sortHighestText };

            // Defaults for Chosen
            ChosenModel = ModelList.First();
            ChosenShoeSize = ShoeSizeList.First();
            ChosenShoeSize = ShoeSizeList.First();
            ChosenLowPriceRange = Constants.SearchDefaults.lowPriceRange;
            ChosenHighPriceRange = Constants.SearchDefaults.highPriceRange;
            SortLowToHigh = Constants.SearchDefaults.sortLowToHigh;
        }

        // Private Methods

        private void UpdateSearchData() { 
            //Make a call to database
        }
    }
}
