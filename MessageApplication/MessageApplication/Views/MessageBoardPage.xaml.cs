using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageApplication.Viewmodel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessageApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageBoardPage : ContentPage
    {
        private readonly MessageBoardViewModel _messageBoardViewModel;
        public MessageBoardPage()
        {
            InitializeComponent();
            _messageBoardViewModel = new MessageBoardViewModel();
            BindingContext = _messageBoardViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _messageBoardViewModel.UpdateTable();
        }

       
    }
}