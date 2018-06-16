using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Backend.WebApi.Models;

namespace Backend.WebApi.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        { }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = DatabaseContext.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (user.Password != HashPassword(password))
                return null;

            // authentication successful
            return user;
        }

        public User Register(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = DatabaseContext.Users.SingleOrDefault(x => x.Username == username);

            // user with the given username already exists
            if (user != null)
                return null;

            // registration successful
            return Create(new User { Username = username, Password = HashPassword(password), Role = Role.User });
        }

        public User GetUserByName(string username) => 
            DatabaseContext.Users.FirstOrDefault(user => user.Username == username);

        private string HashPassword(string password)
        {
            using (var algorithm = SHA256.Create())
            {
                var bytes = algorithm.ComputeHash(Encoding.ASCII.GetBytes(password));
                return string.Concat(bytes.Select(b => b.ToString("x2")));
            }
        }
    }
}
