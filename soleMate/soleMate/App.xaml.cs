using soleMate.Service;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace soleMate
{
    public partial class App : Application
    {
        public NavigationPage LoginCredentialsPage { get; }
        public static RestClient RestClient { get; private set; }

        public App()
        {
            InitializeComponent();
            RestClient = new RestClient("http://10.0.2.2:8080");
            // Create initial screen - currently login page
            MainPage = new NavigationPage(new MainPage());
            LoginCredentialsPage = new NavigationPage(new LoginCredentialsPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
