﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace UniversityChat.Model
{
    public enum Roles { Admin = 0, Mod = 1, User = 2, Guest = 3 };

    public class User
    {
        public User(DataRow userData)
        {
            NickName = userData["NickName"].ToString();
            RoleId = int.Parse(userData["UserRoleId"].ToString());
            FName = userData["FName"].ToString();
            LName = userData["LName"].ToString();
            EmailAddress = userData["Email"].ToString();
            PasswordHash = RetrievePasswordHash("bogus password");
            Id = Guid.Parse(userData["UserId"].ToString());
        }

        public User(string USER_NAME, string PASSWORD, 
            string FIRST_NAME = "firstName", string LAST_NAME = "lastName" 
            )
        {
            NickName = USER_NAME;
            RoleId = (int)Roles.User;
            FName = FIRST_NAME;
            LName = LAST_NAME;
            EmailAddress = Guid.NewGuid().ToString();
            PasswordHash = RetrievePasswordHash(PASSWORD);
            Id = Guid.Empty;
        }
        
        public Guid Id { get; private set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string NickName { get; set; }
        public int RoleId { get; set; }
        public string EmailAddress { get; set; }
        private string PasswordHash { get; set; }

        string RetrievePasswordHash(string PASSWORD)
        {
            // TODO: THIS, also get the salt value from DB
            return null;
        }

        public bool VerifyAll()
        {
            // this should probably return an index for the user ID
            return true;
        }
    }
}
