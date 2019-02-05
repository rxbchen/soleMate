using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelliAbb.Xamarin.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace soleMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage() => InitializeComponent();

        // Private Variables
        private string model = "Yeezy";
        private int size = 8;
        private string chosen = "";

        private enum PriceRange { Tier1, Tier2, Tier3 };
        private PriceRange price_range = PriceRange.Tier1;

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SearchPage());
        }

        private void ModelSwitchToggled(object sender, ToggledEventArgs e)
        {
            chosen = " was chosen";
        }
    }
}