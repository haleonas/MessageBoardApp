using System;
using System.Windows.Input;
using MessageApplication.Models;
using MessageApplication.Views;
using Xamarin.Forms;

namespace MessageApplication.Viewmodel
{
    public class RegisterViewModel: BaseViewModel
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;

        public ICommand RegisterBtn { get; }

        public RegisterViewModel()
        {
            RegisterBtn = new Command(async () =>
            {
                //register user
                if (Username.Equals("") || Password != ConfirmPassword) return;
                var user = new Users
                {
                    Username = Username,
                    Password = Password
                };
                try
                {
                    await App._Client.GetTable<Users>().InsertAsync(user);
                    await App.Current.MainPage.DisplayAlert("Success", "User registered", "Ok");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Couldn't register user", "ok");
                }

            }, () =>
            {
                if (Username.Equals("") || Password.Equals("") || ConfirmPassword.Equals("")) return false;
                return Password == ConfirmPassword;
            });
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                RefreshButton((Command) RegisterBtn);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                RefreshButton((Command) RegisterBtn);
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                RefreshButton((Command) RegisterBtn);
            }
        }

        private void RefreshButton(Command btn)
        {
            btn.ChangeCanExecute();
        }
    }
}