using System.Threading.Tasks;

namespace MessageApplication.Services
{
    public interface IDisplayAlertService
    {
        Task DisplayAlert(string title,string message, string cancel);
    }
}