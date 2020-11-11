using System.Windows.Input;
using MessageApplication.Models;
using MessageApplication.Services;
using Xamarin.Forms;

namespace Tests
{
    public class AddMessageStub
    {
        private string _message;
        
        public ICommand AddBtn { get; }
        public AddMessageStub(INavigationService navigation,IDisplayAlertService displayAlertService, PostsSpy post)
        {    
            AddBtn = new Command(async () =>
            {
                var result = await post.SendPosts(Message);
                if (result)
                {
                    await displayAlertService.DisplayAlert("Success", "Message added", "Ok");
                    await navigation.PopAsync();
                }
                else
                {
                    await displayAlertService.DisplayAlert("Error", "Message couldn't be added", "Ok");
                }
            });
        }

        public string Message
        {
            get => _message;
            set => _message = value;
        }
        
        
    }
}