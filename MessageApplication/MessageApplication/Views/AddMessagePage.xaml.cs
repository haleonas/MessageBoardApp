using MessageApplication.Services;
using MessageApplication.Viewmodel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessageApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMessagePage
    {
        public AddMessagePage(INavigationService navigation,IDisplayAlertService displayAlertService)
        {
            InitializeComponent();
            BindingContext = new AddMessageViewModel(navigation, displayAlertService);
        }
    }
}