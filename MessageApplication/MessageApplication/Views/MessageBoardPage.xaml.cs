using System;
using MessageApplication.Services;
using MessageApplication.Viewmodel;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Xaml.Internals;

namespace MessageApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageBoardPage
    {
        private readonly MessageBoardViewModel _messageBoardViewModel;
        public MessageBoardPage(INavigationService navigation,IDisplayAlertService displayAlertService)
        {
            InitializeComponent();
            _messageBoardViewModel = new MessageBoardViewModel(navigation,displayAlertService);
            BindingContext = _messageBoardViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _messageBoardViewModel.UpdateTable();
        }

        protected override bool OnBackButtonPressed()
        {
            MessageBoardViewModel.LogoutUser();
            return base.OnBackButtonPressed();
        }
    }
}