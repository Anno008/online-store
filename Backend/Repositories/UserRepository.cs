using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Backend.Models;

namespace Backend.Repositories
{
    public class UserRepository
    {
        private readonly DatabaseContext databaseContext;

        public UserRepository(DatabaseContext databaseContext) =>
            this.databaseContext = databaseContext;

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            User user = null;
            user = databaseContext.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (user.Password != HashPassword(password))
                return null;

            // authentication successful
            return user;
        }

        public User Create(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            User user = null;
            user = databaseContext.Users.SingleOrDefault(x => x.Username == username);

            // user with the given username already exists
            if (user != null)
                return null;

            // registration successful
            var retVal = databaseContext.Users.Add(new User { Username = username, Password = HashPassword(password), Role = Role.User });
            databaseContext.SaveChanges();
            return retVal.Entity;
        }

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
