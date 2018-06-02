using Backend.WebApi.Models;

namespace Backend.WebApi.Repositories
{
    public class ChatRoomRepository : BaseRepository<ChatRoomMessage>
    {
        public ChatRoomRepository(DatabaseContext context) : base(context) { }
    }
}
