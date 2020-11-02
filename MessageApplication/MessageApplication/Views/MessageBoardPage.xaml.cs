using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageApplication.Models;
using MessageApplication.Viewmodel;
using SQLite;
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

        protected override bool OnBackButtonPressed()
        {
            MessageBoardViewModel.LogoutUser();
            Environment.Exit(0);
            return base.OnBackButtonPressed();
        }
    }
}