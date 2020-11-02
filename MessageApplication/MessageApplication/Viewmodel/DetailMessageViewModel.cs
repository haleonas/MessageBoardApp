using MessageApplication.Models;
using Xamarin.Forms;

namespace MessageApplication.Viewmodel
{
    public class DetailMessageViewModel:BaseViewModel
    {
        private Posts _post;
        private string _message;
        private string _username;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public DetailMessageViewModel(Posts post)
        {
            _post = post;
            Message = post.Message;
            Username = post.Username;
            //Application.Current.MainPage.DisplayAlert("Success", _post.Message, "ok");
        }
    }
}