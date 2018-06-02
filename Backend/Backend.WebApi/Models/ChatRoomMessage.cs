﻿using System;

namespace Backend.WebApi.Models
{
    public class ChatRoomMessage : BaseEntity
    {
        public string Message { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }

        public override void Update(BaseEntity e)
        {
            throw new NotImplementedException();
        }
    }
}
