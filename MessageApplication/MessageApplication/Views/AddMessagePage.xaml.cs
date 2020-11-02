using MessageApplication.Viewmodel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessageApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMessagePage
    {
        public AddMessagePage()
        {
            InitializeComponent();
            BindingContext = new AddMessageViewModel();
        }
    }
}