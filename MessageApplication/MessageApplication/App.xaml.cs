using MessageApplication.Bootstrap;
using MessageApplication.Models;
using MessageApplication.Views;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace MessageApplication
{
    public partial class App
    {
        public static string DatabaseLocation = string.Empty;
        public static readonly MobileServiceClient Client = new MobileServiceClient("https://messageapplication.azurewebsites.net");
        public static Users User = new Users();
        
        public App(AppSetup setup,string databaseLocation)
        {
            InitializeComponent();

            AppContainer.Container = setup.CreateContainer();
            DatabaseLocation = databaseLocation;

            MainPage = new NavigationPage(new LoginPage());
        }

        public static NetworkAccess GetNetworkAccess()
        {
            return Connectivity.NetworkAccess;
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