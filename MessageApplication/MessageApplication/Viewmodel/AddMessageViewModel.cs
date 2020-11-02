using System;
using System.Windows.Input;
using MessageApplication.Models;
using Xamarin.Forms;

namespace MessageApplication.Viewmodel
{
    public class AddMessageViewModel:BaseViewModel
    {
        private string _message = "";

        public ICommand AddBtn { get; }

        public AddMessageViewModel()
        {    
            AddBtn = new Command(async () =>
            {
                var post = new Posts
                {
                    Message = Message,
                    Username = App.User.Username
                };
                try
                {
                    await App.Client.GetTable<Posts>().InsertAsync(post);
                    await Application.Current.MainPage.DisplayAlert("Success", "Message added", "Ok");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Message couldn't be added", "Ok");
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