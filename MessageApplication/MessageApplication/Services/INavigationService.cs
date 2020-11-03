using System.Threading.Tasks;
using Xamarin.Forms;

namespace MessageApplication.Services
{
    public interface INavigationService
    {
        Task PushAsync(Page page);
        Task<Page> PopAsync();
    }
}