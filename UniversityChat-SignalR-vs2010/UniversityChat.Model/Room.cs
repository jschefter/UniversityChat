using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace UniversityChat.Model
{
    public class Room
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public string RoomDesc { get; set; }
        public Guid ClassId { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastUsedDate { get; set; }
        public Guid ModeratorId { get; set; }

        public Room()
        {
            RoomName = string.Empty;
            RoomDesc = string.Empty;
            StartDate = DateTime.Now;
            ExpirationDate = DateTime.Now.AddYears(1);
            LastUsedDate = DateTime.Now;
        }

        public Room(DataRow roomData)
        {
            Id = Guid.Parse(roomData["RoomId"].ToString());
            RoomName = roomData["RoomName"].ToString();
            RoomDesc = roomData["RoomDesc"].ToString();
            ClassId = Guid.Parse(roomData["ClassId"].ToString());
            IsPrivate = Convert.ToBoolean(roomData["IsPrivate"]);
            IsActive = Convert.ToBoolean(roomData["IsActive"]);

            ExpirationDate = DateTime.Parse(roomData["ExpirationDate"].ToString());
            StartDate = DateTime.Parse(roomData["StartDate"].ToString());
            LastUsedDate = DateTime.Parse(roomData["LastUsedDate"].ToString());

            ModeratorId = Guid.Parse(roomData["ModeratorId"].ToString());
        }
    }
}
