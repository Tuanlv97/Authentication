using BookStore.Data.Entities;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repositories
{
    public class UserReponsitory : IUserReponsitory
    {
        private BookStoreDBContext _context;

        public UserReponsitory(BookStoreDBContext context)
        {
            _context = context;

            if (!_context.Users.Any())
            {
                _context.Users.Add(new User
                {
                    UserId = Guid.NewGuid(),
                    FullName = "Le Anh Tuan",
                    UserName = "Anhtuan37",
                    PassWord = "e50de92f25c5d5466fb5771650ad2eece2ffeefe1e81dd9fa2d2533510f196cc",
                    FavoriteColor = "Green",
                    Role = "Admin",
                    GoogleId = "123"
                });
                _context.SaveChanges();
            }
        }

        public User GetByUserNameAndPassWord(string userName, string passWord)
        {
            var hashPWD = ComputeSha256Hash(passWord);
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName && x.PassWord == hashPWD);
            return user;
        }

        public User GetUserByGoogleIdentifier(string googleId)
        {
            var user = _context.Users.FirstOrDefault(x => x.GoogleId == googleId);
            return user;
        }
        private string ComputeSha256Hash(string rawData)
        {
            //Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
