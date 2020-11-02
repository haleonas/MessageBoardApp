using MessageApplication.Services;

namespace MessageApplication.Android.Services
{
    public class PlatformService:IPlatformService
    {
        public string GetPlatform()
        {
            return "Android";
        }
    }
}