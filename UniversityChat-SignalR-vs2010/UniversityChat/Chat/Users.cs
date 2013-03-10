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

        internal static void AddConnectedUser(Guid connectionId, string userName)
        {
            connectedUsers.Add(connectionId, userName);
        }

        internal static void RemoveUser(Guid connectionId)
        {
            string userName = GetUserName(connectionId);
            connectedUsers.Remove(connectionId);
        }
        
        internal static string GetUserName(Guid connectionId)
        {
            string username = string.Empty;
            connectedUsers.TryGetValue(connectionId, out username);
            return username;
        }

        internal static User GetConnectedUserByConnectionId(Guid guid)
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

        internal static bool AuthenticateUser(User user)
        {
            bool authentic = false;

            if (user.AuthenticateUser(usersRepository.GetByName(user.NickName)))
            {
                return true;
            }

            return authentic;
        }

        internal static bool CreateUser(User newUser)
        {
            return usersRepository.Create(newUser);
        }
    }
}