using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityChat.Data.Repositories;
using UniversityChat.Model;

namespace UniversityChat.Chat
{
    public static class Users
    {
        private static UsersRepository usersRepository = new UsersRepository();
        private static Dictionary<Guid, string> connectedUsers = new Dictionary<Guid, string>();    // this maps connectionId to username for currently connected users.

        internal static void AddUser(Guid connectionId, string userName)
        {
            User newUser = new User(userName, "bogus-password");

            usersRepository.Create(newUser);
            connectedUsers.Add(connectionId, userName);
        }

        internal static void RemoveUser(Guid connectionId)
        {
            string userName = GetUserName(connectionId);

            User existingUser = new User(userName, "bogus-password");
            usersRepository.Delete(existingUser);

            connectedUsers.Remove(connectionId);
        }
        
        internal static string GetUserName(Guid connectionId)
        {
            string username = string.Empty;
            connectedUsers.TryGetValue(connectionId, out username);
            return username;
        }

        internal static User GetUserByConnectionId(Guid guid)
        {
            string userName = GetUserName(guid);
            User user = usersRepository.GetByName(userName);
            return user;
        }

        internal static User GetUserByUserId(Guid userId)
        {
            User user = usersRepository.GetByUserId(userId);

            return user;
        }

        internal static int CountOfConnectedUsers
        {
            get
            {
                return connectedUsers.Count;
            }
        }
    }
}