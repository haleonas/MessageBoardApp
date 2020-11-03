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
        private static readonly NavigationService Navigation = new NavigationService();
        private static readonly DisplayAlertService DisplayAlertService = new DisplayAlertService();
        public ICommand LoginBtn { get; }
        public ICommand RegisterBtn { get;  }

        public LoginViewModel(IPlatformService platformService)
        {
            Text = platformService.GetPlatform();
            OnlineStatus = App.GetNetworkAccess() == NetworkAccess.Internet ? "Online" : "Offline";
            
            LoginBtn = new Command(async () =>
            {
                if (Username.Equals("") || Password.Equals("")) return;
                if (await Users.Login(Username,Password))
                {
                    await Navigation.PushAsync(new MessageBoardPage(Navigation,DisplayAlertService));
                }
                else
                {
                    await DisplayAlertService.DisplayAlert("Error", "Something went wrong", "ok");
                }
            }, () => !Username.Equals("") && !Password.Equals("") && App.GetNetworkAccess() == NetworkAccess.Internet);
            
            RegisterBtn = new Command(async() =>
            {
                await Navigation.PushAsync(new RegisterPage(Navigation,DisplayAlertService));
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
                
                Navigation?.PushAsync(new MessageBoardPage(Navigation,DisplayAlertService));
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