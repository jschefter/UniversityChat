using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityChat
{
    public static class ConnectedUsers
    {
        private static Dictionary<string, string> connectedUsers = new Dictionary<string, string>();

        public static void AddUser(string connectionId, string username)
        {
            connectedUsers.Add(connectionId, username);
        }

        public static string RemoveUser(string connectionId)
        {
            string username;
            connectedUsers.TryGetValue(connectionId, out username);
            connectedUsers.Remove(connectionId);

            return username;
        }

        public static string[] GetConnectedUsers()
        {
            return new List<string>(connectedUsers.Values).ToArray();
        }
    }
}