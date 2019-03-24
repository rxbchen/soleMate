namespace soleMate.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    public class SearchResult
    {
        // Public Variables

        public ObservableCollection<Shoe> ShoeList { get; set; }

        // Constructor

        public SearchResult() {
            ShoeList = new ObservableCollection<Shoe>();
        }

        // Public Functions

        public void ReverseSorting() { 
            for (int i = 0; i < ShoeList.Count; i++) {
                ShoeList.Move(ShoeList.Count - 1, i);
            }
        }

    }
}
