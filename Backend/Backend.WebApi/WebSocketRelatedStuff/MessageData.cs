using System;
using Backend.WebApi.Models;
using Newtonsoft.Json;

namespace Backend.WebApi.WebSocketRelatedStuff
{
    public class MessageData
    {
        public MessageData()
        {

        }

        public MessageData(ChatRoomMessage m)
        {
            Message = m.Message;
            User = m.User;
            Date = m.Date;
        }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
