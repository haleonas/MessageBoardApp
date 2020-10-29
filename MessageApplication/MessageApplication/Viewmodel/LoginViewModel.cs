using System.Windows.Input;
using MessageApplication.Views;
using Xamarin.Forms;

namespace MessageApplication.Viewmodel
{
    public class LoginViewModel:BaseViewModel
    {
        private string _username = string.Empty;
        private string _password = string.Empty;

        public ICommand LoginBtn { get; }
        public ICommand RegisterBtn { get;  }

        public LoginViewModel()
        {
            LoginBtn = new Command(async () =>
                {
                    //send request to server
                    //login if user is available
                    //App.Current.MainPage.Navigation.PushAsync(new MessageBoardPage());
                    App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "ok");
                }, () => !Username.Equals("") && !Password.Equals(""));
            
            RegisterBtn = new Command(() =>
            {
                App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            });
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                RefreshButton((Command)LoginBtn);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                RefreshButton((Command)LoginBtn);
            }
        }
        
        private void RefreshButton(Command btn)
        {
            btn.ChangeCanExecute();
        }
    }
}