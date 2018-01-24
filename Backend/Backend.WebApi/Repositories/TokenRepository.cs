using System.Linq;
using Backend.WebApi.Models;

namespace Backend.WebApi.Repositories
{
    public class TokenRepository : BaseRepository<RefreshToken>
    {
        public TokenRepository(DatabaseContext context) : base(context) { }

        public bool TokenExists(string tokenId) =>
           databaseContext.Tokens.Any(token => token.TokenId == tokenId);
    }
}
