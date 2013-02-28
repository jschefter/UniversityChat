using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityChat.Model;
using System.Data.Common;
using UniversityChat.Data.DataAccess;
using System.Data;
using UniversityChat.Data.DataHelpers;
using UniversityChat.Data.Repositories;

namespace UniversityChat.Chat
{
    public static class ChatChannels
    {
        private static RoomsRepository rooms = new RoomsRepository();
        private static RoomUsersRepository roomUsers = new RoomUsersRepository();

        /// <summary>
        /// adds a new room to the rooms repository.
        /// </summary>
        /// <param name="roomName">the name of the room to be added</param>
        public static void AddRoom(string roomName)
        {
            Room newRoom = new Room() { RoomName = roomName };
            rooms.Create(newRoom);
        }

        /// <summary>
        /// delete a room from the rooms repository.
        /// </summary>
        /// <param name="roomName">the name of the room to be deleted</param>
        public static void DeleteRoom(string roomName)
        {
            Room existingRoom = new Room() { RoomName = roomName };

            ICollection<User> usersOfRoom = GetUsersOfRoom(roomName);
            foreach (User user in usersOfRoom)
            {
                RemoveUserFromRoom(roomName, user);
                // TODO: users need to be kicked from the rooms they are in...
            }

            rooms.Delete(existingRoom);
        }

        /// <summary>
        /// Get the names of all existing rooms.
        /// </summary>
        /// <returns></returns>
        public static ICollection<string> GetRoomList()
        {
            List<string> roomNames = new List<string>();
            IList<Room> roomList = rooms.GetAll();
            foreach (Room room in roomList)
            {
                roomNames.Add(room.RoomName);
            }

            return roomNames.ToArray();
        }

        /// <summary>
        /// Add a user to an existing room
        /// </summary>
        /// <param name="roomName">the name of the room the user is being added to.</param>
        /// <param name="user">the user being added to room</param>
        public static void AddUserToRoom(string roomName, User user)
        {
            // get the room's Id.
            Guid roomId = rooms.GetGuidByName(roomName);

            if (!roomId.Equals(Guid.Empty))
            {
                RoomUser roomUser = new RoomUser() { RoomId = roomId, UserId = user.Id };
                roomUsers.Create(roomUser);
            }
        }

        /// <summary>
        /// Remove a user from an existing room.
        /// </summary>
        /// <param name="roomName">the name of the room the user is being removed from.</param>
        /// <param name="user">the user</param>
        public static void RemoveUserFromRoom(string roomName, User user)
        {
            // get the room's Id.
            Guid roomId = rooms.GetGuidByName(roomName);

            if (!roomId.Equals(Guid.Empty))
            {
                RoomUser roomUser = new RoomUser() { RoomId = roomId, UserId = user.Id };
                roomUsers.Delete(roomUser);
            }
        }

        /// <summary>
        /// gets the users currently connected to a room.
        /// </summary>
        /// <param name="roomName">the name of the room that user names should be gotten for.</param>
        /// <returns></returns>
        public static ICollection<User> GetUsersOfRoom(string roomName)
        {
            // get the room's Id.
            Guid roomId = rooms.GetGuidByName(roomName);

            ICollection<RoomUser> usersInRoom = roomUsers.GetByRoomId(roomId);

            List<User> users = new List<User>();
            foreach (RoomUser roomUser in usersInRoom)
            {
                User user = Users.GetUserByUserId(roomUser.UserId);
                users.Add(user);
            }

            return users;
        }

        public static string[] GetUsernamesInRoom(string roomName)
        {
            List<string> usernamesInRoom = new List<string>();

            ICollection<User> usersInRoom = GetUsersOfRoom(roomName);
            foreach (User user in usersInRoom)
            {
                usernamesInRoom.Add(user.NickName);
            }

            return usernamesInRoom.ToArray();
        }

        /// <summary>
        /// gets the names of the rooms a particular user is connected to.
        /// </summary>
        /// <param name="user">the user</param>
        /// <returns></returns>
        internal static ICollection<string> GetRoomNamesThatUserIsConnectedTo(User user)
        {
            ICollection<RoomUser> roomsUserIsIn = roomUsers.GetByUserId(user.Id);

            List<string> result = new List<string>();
            foreach (RoomUser roomUser in roomsUserIsIn)
            {
                result.Add(rooms.GetById(roomUser.RoomId).RoomName);
            }

            return result;
        }
    }
}