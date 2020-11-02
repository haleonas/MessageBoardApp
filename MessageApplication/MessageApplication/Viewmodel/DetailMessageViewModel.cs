using MessageApplication.Models;

namespace MessageApplication.Viewmodel
{
    public class DetailMessageViewModel:BaseViewModel
    {
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
            Message = post.Message;
            Username = post.Username;
            //Application.Current.MainPage.DisplayAlert("Success", _post.Message, "ok");
        }
    }
}