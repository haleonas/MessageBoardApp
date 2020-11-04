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

        private readonly DisplayAlertService _displayAlertService;
        private readonly NavigationService _navigationService;

        public MessageBoardViewModel(INavigationService navigation,IDisplayAlertService displayAlertService)
        {
            _displayAlertService = (DisplayAlertService) displayAlertService;
            _navigationService = (NavigationService) navigation;
            Items = new ObservableCollection<Posts>();
            
            AddBtn = new Command(() =>
            {
                navigation.PushAsync(new AddMessagePage(navigation,displayAlertService));
                //Application.Current.MainPage.Navigation.PushAsync(new AddMessagePage(navigation));
            }, () => App.GetNetworkAccess() == NetworkAccess.Internet);
            ReloadListCmd = new Command( ReloadList);
        }

        private void ReloadList()
        {
            Busy = true;
            UpdateTable();
            Busy = false;
        }

        public List<Posts> Posts
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
                try
                {
                    Posts = await App.Client.GetTable<Posts>().ToListAsync();
                    using (var conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        //creates table if it doesn't exist, skips if it exists
                        conn.CreateTable<Posts>();
                        //deletes all posts even if empty
                        conn.DeleteAll<Posts>();
                        //insert all posts that was retrieved
                        foreach (var post in Posts)
                        {
                            conn.Insert(post);
                        }
                    }
                }
                catch (Exception)
                {
                    await _displayAlertService.DisplayAlert("Error","Couldn't retrieve posts","ok");
                }
            }
            else
            {
                using (var conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Posts>();
                    Posts = conn.Table<Posts>().ToList();
                }
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
        }
    }
}