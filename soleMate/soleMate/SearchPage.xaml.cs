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
	public partial class SearchPage : ContentPage
	{
		public SearchPage ()
		{
			InitializeComponent();
		}

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SearchPage());
        }

        private void CheckmarkButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SearchPage());
        }
    }
}