using System;
using System.Linq;
using System.Threading.Tasks;
using MessageApplication.Services;
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

        public static async Task<bool> Register(string username,string password,IDisplayAlertService displayAlertService)
        {
            var user = new Users
            {
                Username = username,
                Password = password
            };
            try
            {
                await App.Client.GetTable<Users>().InsertAsync(user);
                await displayAlertService.DisplayAlert("Success", "User registered", "Ok");
                //await Application.Current.MainPage.Navigation.PopAsync();
                return true;
            }
            catch (Exception)
            {
                await displayAlertService.DisplayAlert("Error", "Couldn't register user", "ok");
                return false;
            }
        }

        private static void SaveLoggedInUser()
        {
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Users>();
                conn.Insert(App.User);
            }
        }

        public static bool CheckLoggedInStatus()
        {
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Users>();
                var table = conn.Table<Users>().ToList();
                if (table.Count <= 0) return false;
                
                App.User = table[0];

                return true;
            }
        }
    }
}