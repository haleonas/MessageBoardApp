
using MessageApplication.Viewmodel;
using Xamarin.Forms.Xaml;

namespace MessageApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            var viewModel = new RegisterViewModel();
            BindingContext = viewModel;
        }
    }
}