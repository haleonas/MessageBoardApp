using System;
using MessageApplication.Models;
using MessageApplication.Views;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace MessageApplication
{
    public partial class App : Application
    {
        
        public static MobileServiceClient _Client = new MobileServiceClient("https://messageapplication.azurewebsites.net");
        public static Users user = new Users();
        
        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
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