
using MessageApplication.Services;
using MessageApplication.Viewmodel;
using Xamarin.Forms.Xaml;

namespace MessageApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage
    {
        public RegisterPage(INavigationService navigation,IDisplayAlertService displayAlertService)
        {
            InitializeComponent();
            var viewModel = new RegisterViewModel(navigation,displayAlertService);
            BindingContext = viewModel;
        }
    }
}