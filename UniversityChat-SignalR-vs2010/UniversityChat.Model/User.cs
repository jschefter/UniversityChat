using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Model
{
    public enum Roles { Admin, Mod, User, Guest };

    public class User
    {
        public User(string USER_NAME, string PASSWORD, 
            string FIRST_NAME = null, string LAST_NAME = null, 
            string EMAIL_ADDRESS = null)
        {
            NickName = USER_NAME;
            RoleId = (int)Roles.User;
            FName = FIRST_NAME;
            LName = LAST_NAME;
            EmailAddress = EMAIL_ADDRESS;
            PasswordHash = RetrievePasswordHash(PASSWORD);
            Id = RetrieveNextUserIndex();
        }
        
        protected decimal Id { get; set; }
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

        int RetrieveNextUserIndex()
        {
            // TODO: THIS
            return 0;
        }

        public bool VerifyAll()
        {
            // this should probably return an index for the user ID
            return true;
        }
    }
}
