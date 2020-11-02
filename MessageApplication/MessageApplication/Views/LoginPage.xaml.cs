using Autofac;
using MessageApplication.Bootstrap;
using MessageApplication.Viewmodel;
using Xamarin.Forms;

namespace MessageApplication.Views
{
    public partial class LoginPage : ContentPage
    {
        
        public LoginPage()
        {
            InitializeComponent();
            
            BindingContext = AppContainer.Container.Resolve<LoginViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitializeComponent();
            LoginViewModel.CheckIfLoggedIn();
        }
    }
}