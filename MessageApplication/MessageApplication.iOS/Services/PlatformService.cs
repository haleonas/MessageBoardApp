using MessageApplication.Services;

namespace MessageApplication.iOS.Services
{
    public class PlatformService:IPlatformService
    {
        public string GetPlatform()
        {
            return "iOS";
        }
    }
}