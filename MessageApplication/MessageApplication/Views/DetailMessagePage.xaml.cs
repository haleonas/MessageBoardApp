using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageApplication.Models;
using MessageApplication.Viewmodel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessageApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailMessagePage : ContentPage
    {
        public DetailMessagePage(Posts post)
        {
            InitializeComponent();
            BindingContext = new DetailMessageViewModel(post);
        }
    }
}