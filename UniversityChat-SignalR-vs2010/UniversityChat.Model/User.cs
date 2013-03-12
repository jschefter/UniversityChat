using System;
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
            Password = userData["Password"].ToString();
            PasswordHash = RetrievePasswordHash(Password);
            Id = Guid.Parse(userData["UserId"].ToString());
        }

        public User(string USER_NAME, string PASSWORD, 
            string FIRST_NAME = "firstName", string LAST_NAME = "lastName", string EMAIL = null
            )
        {
            NickName = USER_NAME;
            RoleId = (int)Roles.User;
            FName = FIRST_NAME;
            LName = LAST_NAME;
            EmailAddress = (EMAIL != null) ? EMAIL : Guid.NewGuid().ToString();
            Password = PASSWORD;
            PasswordHash = RetrievePasswordHash(PASSWORD);
            Id = Guid.Empty;
        }
        
        public Guid Id { get; private set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string NickName { get; set; }
        public int RoleId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        private string PasswordHash { get; set; }

        string RetrievePasswordHash(string PASSWORD)
        {
            // TODO: THIS, also get the salt value from DB
            return PASSWORD;
        }

        public bool VerifyAll()
        {
            // this should probably return an index for the user ID
            return true;
        }

        public bool AuthenticateUser(User otherUser)
        {
            if (otherUser == null)
            {
                // didn't find a record in DB
                return false;
            }

            if (!this.NickName.Equals(otherUser.NickName))
            {
                return false;
            }

            if (!RetrievePasswordHash(this.PasswordHash).Equals(RetrievePasswordHash(otherUser.PasswordHash)))
            {
                return false;
            }

            // got through all checks, users have same login info...
            return true;
        }
    }
}
