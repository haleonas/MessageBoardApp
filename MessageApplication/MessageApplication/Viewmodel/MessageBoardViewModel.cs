using System;
using System.Collections.Generic;
using System.Windows.Input;
using MessageApplication.Models;
using MessageApplication.Views;
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
            });
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
            try
            {
                Posts = await App.Client.GetTable<Posts>().ToListAsync();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error","Couldn't retrieve posts","ok");
            }
        }

    }
}