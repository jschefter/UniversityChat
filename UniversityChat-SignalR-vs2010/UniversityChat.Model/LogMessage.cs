using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Model
{
    public class LogMessage
    {
        public int LogNumber { get; set; }
        public Guid ConnectionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}
