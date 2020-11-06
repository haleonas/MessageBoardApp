using System;
using System.Windows.Input;
using MessageApplication.Models;
using MessageApplication.Services;
using Xamarin.Forms;

namespace MessageApplication.Viewmodel
{
    public class AddMessageViewModel:BaseViewModel
    {
        private string _message = "";

        public ICommand AddBtn { get; }

        public AddMessageViewModel(INavigationService navigation,IDisplayAlertService displayAlertService)
        {    
            AddBtn = new Command(async () =>
            {
                var result = await Posts.SendPosts(Message);
                if (result)
                {
                    await displayAlertService.DisplayAlert("Success", "Message added", "Ok");
                    await navigation.PopAsync();
                }
                else
                {
                    await displayAlertService.DisplayAlert("Error", "Message couldn't be added", "Ok");
                }
            }, () => !Message.Equals(""));
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
                RefreshButton((Command) AddBtn);
            }
        }

        private void RefreshButton(Command btn)
        {
            btn.ChangeCanExecute();
        }
    }
}