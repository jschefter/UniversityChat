using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityChat
{
    public class ChatChannel
    {
        // <connectionId, userName>
        private Dictionary<string, string> connectedUsers;

        public ChatChannel()
        {
            connectedUsers = new Dictionary<string, string>();
        }

        public void AddUser(string connectionId, string userName)
        {
            connectedUsers.Add(connectionId, userName);
        }

        public string RemoveUser(string connectionId)
        {
            string username = "unknown user";
            connectedUsers.TryGetValue(connectionId, out username);
            connectedUsers.Remove(connectionId);

            return username;
        }

        public string[] GetConnectedUsers()
        {
            return connectedUsers.Values.ToArray();
        }

        public bool ContainsUser(string connectionId)
        {
            if (connectedUsers.ContainsKey(connectionId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}