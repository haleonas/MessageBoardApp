using System.Threading.Tasks;
using Xamarin.Forms;

namespace MessageApplication.Services
{
    public class DisplayAlertService:IDisplayAlertService
    {
        public Task DisplayAlert(string title, string message, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}