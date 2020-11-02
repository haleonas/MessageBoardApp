using Autofac;
using MessageApplication.Bootstrap;
using MessageApplication.Viewmodel;

namespace MessageApplication.Views
{
    public partial class LoginPage
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