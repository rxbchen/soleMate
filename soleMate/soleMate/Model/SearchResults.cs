namespace soleMate.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    public class SearchResult
    {
        public ObservableCollection <Shoe> ShoeList { get; set; }

        public SearchResult()
        {
            ShoeList = new ObservableCollection<Shoe>();
        }

    }
}
