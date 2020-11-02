using MessageApplication.Models;
using MessageApplication.Viewmodel;
using Xamarin.Forms.Xaml;

namespace MessageApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailMessagePage
    {
        public DetailMessagePage(Posts post)
        {
            InitializeComponent();
            BindingContext = new DetailMessageViewModel(post);
        }
    }
}