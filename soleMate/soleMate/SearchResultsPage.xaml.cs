﻿using soleMate.Model;
using soleMate.Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace soleMate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchResultsPage : ContentPage
	{
		public SearchResultsPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // Calls the GET shoes/ api
            HttpSearchRequests search = new HttpSearchRequests(App.RestClient);
            SearchResult searchResult = await search.GetAllShoes();

            listView.ItemsSource = searchResult.Shoes;
        }
    }
}