using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace soleMate.Model
{
    public class SearchResult
    {
        public ObservableCollection<Shoe> Shoes { get; set; }

        public SearchResult()
        {
            Shoes = new ObservableCollection<Shoe>();
        }

    }
}
