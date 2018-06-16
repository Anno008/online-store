using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Backend.WebApi.Repositories;
using Backend.WebApi.WebSocketRelatedStuff;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ChatRoomMessagesController : Controller
    {
        private readonly ChatRoomRepository _chatRoomRepository;

        public ChatRoomMessagesController(ChatRoomRepository chatRoomRepository) =>
            _chatRoomRepository = chatRoomRepository;

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<List<MessageData>> Get() =>
            (await _chatRoomRepository.GetAllAsync()).Select(m => new MessageData(m)).ToList();
    }
}
