using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Internal;
using MessageApplication.Models;

namespace Tests
{
    public class PostsSpy: Posts
    {
        public bool SendPostsCalled;

        public override async Task<bool> SendPosts(string message)
        {
            SendPostsCalled = true;
            return await Task.FromResult(!message.IsNullOrEmpty());
        }
    }
}