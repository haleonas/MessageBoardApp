using System;
using MessageApplication.Viewmodel;
using Xamarin.Forms.Xaml;

namespace MessageApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageBoardPage
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

        protected override bool OnBackButtonPressed()
        {
            MessageBoardViewModel.LogoutUser();
            Environment.Exit(0);
            return base.OnBackButtonPressed();
        }
    }
}