using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityChat.Model;
using System.Data.Common;
using UniversityChat.Data.DataAccess;
using System.Data;

namespace UniversityChat
{
    public static class ChatChannels
    {
        private static Dictionary<string, Room> channels = new Dictionary<string, Room>();

        /// <summary>
        /// If a channel doesn't already exist, create it.
        /// </summary>
        /// <param name="channelName">the name of the channel</param>
        public static void AddChannel(string channelName)
        {
            if (!channels.ContainsKey(channelName))
            {
                channels.Add(channelName, new Room());
            }
        }

        public static void DeleteChannel(string channelName)
        {
            channels.Remove(channelName);
        }

        public static ICollection<string> GetChannelList()
        {
            DbCommand dbCommand = GenericDataAccess.CreateCommand(@"SELECT 1 FROM Test", null);
            DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);
            //return channels.Keys.ToArray();
        }

        public static void AddUser(string channelName, string connectionId, string userName)
        {
            Room channel;
            if(channels.TryGetValue(channelName, out channel)) {
                channel.AddUser(connectionId, userName);
            }
            else {
                // TODO: channel doesn't exist!
            }
        }

        public static string[] GetConnectedUsers(string channelName)
        {
            Room channel;
            if(channels.TryGetValue(channelName, out channel)) {
                return channel.GetConnectedUsers();
            }
            else {
                return null;
            }
        }

        // returns an array of the public channels
        internal static string[] GetChannels(string connectionId)
        {
            List<String> connectedChannels = new List<string>();
            foreach (KeyValuePair<string,Room> channel in channels)
            {
                if (channel.Value.ContainsUser(connectionId))
                {
                    connectedChannels.Add(channel.Key);
                }
            }

            return connectedChannels.ToArray();
        }

        internal static string RemoveUser(string channelName, string connectionId)
        {
            Room channel;
            if(channels.TryGetValue(channelName, out channel)) {
                return channel.RemoveUser(connectionId);
            }
            else {
                return "unknown channel";
            }
        }
    }
}