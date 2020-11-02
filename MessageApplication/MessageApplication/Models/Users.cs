using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace MessageApplication.Models
{
    public class Users
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static async Task<bool> Login(string username, string password)
        {
            var user = (await App.Client.GetTable<Users>().Where(u => u.Username == username).ToListAsync()).FirstOrDefault();
            if (user == null) return false;
            if (user.Password != password) return false;
            App.User = user;

            SaveLoggedInUser();

            return true;
        }

        private static void SaveLoggedInUser()
        {
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Users>();
                conn.Insert(App.User);
            }
        }
    }
}