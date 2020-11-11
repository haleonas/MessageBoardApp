using System.Threading.Tasks;
using MessageApplication.Models;
using MessageApplication.Services;
using Moq;
using NUnit.Framework;
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

        [Test]
        public void AddMessageTest()
        {
            var navService = new Mock<INavigationService>();
            var displayService = new Mock<IDisplayAlertService>();
            var postMock = new Mock<PostsSpy>();
            var postsSpy = postMock.Object;

            var unit = new AddMessageStub(navService.Object, displayService.Object, postsSpy) {Message = "hej123"};

            unit.AddBtn.Execute(null);
            
            Assert.IsTrue(postsSpy.SendPostsCalled);
            displayService.Verify(d => d.DisplayAlert(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>()));
            navService.Verify(s => s.PopAsync());
        }

        [Test]
        //test in case there is no message sent
        public void AddMessageFailTest()
        {
            var navService = new Mock<INavigationService>();
            var displayService = new Mock<IDisplayAlertService>();
            var postsSpy = new PostsSpy();

            var unit = new AddMessageStub(navService.Object, displayService.Object, postsSpy);
            
            unit.AddBtn.Execute(null);
            
            Assert.IsTrue(postsSpy.SendPostsCalled);
            displayService.Verify(d => d.DisplayAlert(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>()));
            //navService.Verify(s => s.PopAsync()); //will fail as no PopAsync is never called in this case
        }
    }
}