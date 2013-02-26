using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Model
{
    public class Room
    {
        public decimal Id { get; set; }
        public string RoomName { get; set; }
        public string RoomDesc { get; set; }
        public decimal ClassId { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastUsedDate { get; set; }
        public decimal ModeratorId { get; set; }
    }
}
