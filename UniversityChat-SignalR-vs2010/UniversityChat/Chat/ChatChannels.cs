using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityChat
{
    public static class ChatChannels
    {
        private static Dictionary<string, ChatChannel> channels = new Dictionary<string, ChatChannel>();

        /// <summary>
        /// If a channel doesn't already exist, create it.
        /// </summary>
        /// <param name="channelName">the name of the channel</param>
        public static void AddChannel(string channelName)
        {
            if (!channels.ContainsKey(channelName))
            {
                channels.Add(channelName, new ChatChannel());
            }
        }

        public static void DeleteChannel(string channelName)
        {
            channels.Remove(channelName);
        }

        public static ICollection<string> GetChannelList()
        {
            return channels.Keys.ToArray();
        }

        public static void AddUser(string channelName, string connectionId, string userName)
        {
            ChatChannel channel;
            if(channels.TryGetValue(channelName, out channel)) {
                channel.AddUser(connectionId, userName);
            }
            else {
                // TODO: channel doesn't exist!
            }
        }

        public static string[] GetConnectedUsers(string channelName)
        {
            ChatChannel channel;
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
            foreach (KeyValuePair<string,ChatChannel> channel in channels)
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
            ChatChannel channel;
            if(channels.TryGetValue(channelName, out channel)) {
                return channel.RemoveUser(connectionId);
            }
            else {
                return "unknown channel";
            }
        }
    }
}