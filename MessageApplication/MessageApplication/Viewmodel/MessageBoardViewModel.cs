using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MessageApplication.Models;
using MessageApplication.Views;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MessageApplication.Viewmodel
{
    public class MessageBoardViewModel:BaseViewModel
    {
        private List<Posts> _posts;
        private Posts itemSelected;
        
        public ICommand AddBtn { get; }
        public ICommand ItemNavigation { get; }

        public MessageBoardViewModel()
        {
            AddBtn = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushAsync(new AddMessagePage());
            }, () => App.GetNetworkAccess() == NetworkAccess.Internet);
            ItemNavigation = new Command(() =>
            {
                Application.Current.MainPage.DisplayAlert("Success", "YAY", "WOOOO");
            });
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

        public Posts ItemSelected
        {
            get => itemSelected;
            set
            {
                itemSelected = value;
                if (itemSelected == null) return; 
                OnPropertyChanged();
                Application.Current.MainPage.Navigation.PushAsync(new DetailMessagePage(itemSelected));
            }
        }

        public async void UpdateTable()
        {
            if (App.GetNetworkAccess() == NetworkAccess.Internet)
            {
                try
                {
                    Posts = await App.Client.GetTable<Posts>().ToListAsync();
                    using (var conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.DeleteAll<Posts>();
                        conn.CreateTable<Posts>();
                        foreach (var post in Posts)
                        {
                            conn.Insert(post);
                        }
                    }
                }
                catch (Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Error","Couldn't retrieve posts","ok");
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
        }

        public static void LogoutUser()
        {
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.Delete(App.User);
            }
            
            App.User = new Users();
            //Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}