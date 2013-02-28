using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace UniversityChat.Model
{
    public class RoomUser
    {
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }

        public RoomUser()
        {
        }

        public RoomUser(DataRow roomUserData)
        {
            RoomId = Guid.Parse(roomUserData["RoomId"].ToString());
            UserId = Guid.Parse(roomUserData["UserId"].ToString());
        }
    }
}
