using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MessageApplication.Models;
using MessageApplication.Services;
using MessageApplication.Views;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MessageApplication.Viewmodel
{
    public class MessageBoardViewModel:BaseViewModel
    {
        private List<Posts> _posts;
        private Posts _itemSelected;
        private bool _busy;
        
        public ObservableCollection<Posts> Items { get; }

        public ICommand AddBtn { get; }
        public ICommand ReloadListCmd { get;  }
        public ICommand LogoutCommand { get;  }

        private readonly NavigationService _navigationService;

        public MessageBoardViewModel(INavigationService navigation,IDisplayAlertService displayAlertService)
        {
            _navigationService = (NavigationService) navigation;
            Items = new ObservableCollection<Posts>();
            
            AddBtn = new Command(() =>
            {
                navigation.PushAsync(new AddMessagePage(navigation,displayAlertService, new Posts()));
            }, () => App.GetNetworkAccess() == NetworkAccess.Internet);
            ReloadListCmd = new Command(ReloadList);
            LogoutCommand = new Command(LogoutUser);
        }
        
        private void ReloadList()
        {
            Busy = true;
            UpdateTable();
            RefreshButton((Command)ReloadListCmd);
            Busy = false;
        }

        private List<Posts> Posts
        {
            get => _posts;
            set
            {
                _posts = value;
                OnPropertyChanged();
            }
        }

        public bool Busy
        {
            get => _busy;
            set
            {
                if (_busy == value) return;
                _busy = value;
                OnPropertyChanged();
            }
        }

        public Posts ItemSelected
        {
            get => _itemSelected;
            set
            {
                _itemSelected = value;
                if (_itemSelected == null) return; 
                OnPropertyChanged();
                _navigationService?.PushAsync(new DetailMessagePage(_itemSelected));
            }
        }

        public async void UpdateTable()
        {
            Items.Clear();
            if (App.GetNetworkAccess() == NetworkAccess.Internet)
            {
                Posts = await Models.Posts.RetrieveList();
            }
            else
            {
                Posts = Models.Posts.RetrieveOfflineList();
            }
            foreach (var post in Posts)
            {
                Items.Add(post);
            }
        }

        public static void LogoutUser()
        {
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.Delete(App.User);
            }
            
            App.User = new Users();
            Environment.Exit(0);
        }
        
        private void RefreshButton(Command btn)
        {
            btn.ChangeCanExecute();
        }
    }
}