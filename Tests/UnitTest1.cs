using System.Threading.Tasks;
using MessageApplication.Models;
using MessageApplication.Services;
using NUnit.Framework;
using Moq;
using Xamarin.Forms;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CheckIfUserCanLogin()
        {
            var result = await Users.Login("jesper", "jesper"); //will give true
            Assert.AreEqual(true,result);
        }
        
        [Test]
        public async Task CheckIfUserCanRegister()
        {
            var result = await Users.Register("test", "test");
            Assert.AreEqual(true,result);
        }

        [Test]
        public void CheckIfThereIsAUserLoggedIn()
        {
            
            //depends heavily if you have logged in before or not.
            var result = Users.CheckLoggedInStatus();
            Assert.AreEqual(false,result);
        }

        [Test]
        public void NavigationPushTest()
        {
            var mock = new Mock<INavigationService>();
            var navService = mock.Object;

            navService.PushAsync(null);
            mock.Verify(s => s.PushAsync(It.IsAny<Page>()));
        }

        [Test]
        public void NavigationPopTest()
        {
            var mock = new Mock<INavigationService>();
            var navService = mock.Object;

            navService.PopAsync();
            mock.Verify(s => s.PopAsync());
        }
    }
}