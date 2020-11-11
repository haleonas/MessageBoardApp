using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace MessageApplication.Models
{
    public class Posts
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }

        public async Task<bool> SendPosts(string message)
        {
            var post = new Posts
            {
                Message = message,
                Username = App.User.Username
            };
            try
            {
                await App.Client.GetTable<Posts>().InsertAsync(post);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<Posts> RetrieveOfflineList()
        {
            List<Posts> posts;
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Posts>();
                posts = conn.Table<Posts>().ToList();
            }

            return posts;
        }

        public static async Task<List<Posts>> RetrieveList()
        {
            try
            {
                var postsList = await App.Client.GetTable<Posts>().ToListAsync();
                using (var conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    //creates table if it doesn't exist, skips if it exists
                    conn.CreateTable<Posts>();
                    //deletes all posts even if empty
                    conn.DeleteAll<Posts>();
                    //insert all posts that was retrieved
                    foreach (var post in postsList)
                    {
                        conn.Insert(post);
                    }
                }

                return postsList;
            }
            catch (Exception)
            {
                return new List<Posts>();
            }
        }
    }
}