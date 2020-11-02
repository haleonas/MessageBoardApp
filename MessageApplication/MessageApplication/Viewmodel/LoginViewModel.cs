using System.Windows.Input;
using MessageApplication.Models;
using MessageApplication.Services;
using MessageApplication.Views;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MessageApplication.Viewmodel
{
    public class LoginViewModel:BaseViewModel
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _text = string.Empty;
        private string _onlineStatus = string.Empty;
        private IPlatformService _platformService;

        public ICommand LoginBtn { get; }
        public ICommand RegisterBtn { get;  }

        public LoginViewModel(IPlatformService platformService)
        {
            _platformService = platformService;
            Text = _platformService.GetPlatform();
            
            OnlineStatus = App.GetNetworkAccess() == NetworkAccess.Internet ? "Online" : "Offline";
            
            LoginBtn = new Command(async () =>
            {
                if (Username.Equals("") || Password.Equals("")) return;
                if (await Users.Login(Username,Password))
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new MessageBoardPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "ok");
                }
            }, () => !Username.Equals("") && !Password.Equals("") && App.GetNetworkAccess() == NetworkAccess.Internet);
            
            RegisterBtn = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            }, () => App.GetNetworkAccess() == NetworkAccess.Internet); //can't register a new user without internet
        }

        public static void CheckIfLoggedIn()
        {
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Users>();
                var table = conn.Table<Users>().ToList();
                if (table.Count <= 0) return;
                
                App.User = table[0];
                Application.Current.MainPage.Navigation.PushAsync(new MessageBoardPage());
            }
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

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public string OnlineStatus
        {
            get => _onlineStatus;
            set
            {
                _onlineStatus = value;
                OnPropertyChanged();
            }
        }

        private void RefreshButton(Command btn)
        {
            btn.ChangeCanExecute();
        }
    }
}