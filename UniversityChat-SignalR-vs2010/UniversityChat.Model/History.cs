using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Model
{
    public class History
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ConnectionId { get; set; }
        public Guid RoomId { get; set; }
        public string Text { get; set; }
        public DateTime LogDateTimeStamp { get; set; }
    }
}
