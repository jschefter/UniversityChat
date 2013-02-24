using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UniversityChat.Model;
using UniversityChat.Data;
using UniversityChat.Data.Repositories;

namespace UniversityChat.Tests.Repositories
{
    [TestFixture]
    public class UserReposiotryFixture
    {
        [Test]
        public void UserRepository_Validate_That_NewUser_Can_Be_Created()
        {
            User newUser = new User();
            newUser.FName = "Mark";
            newUser.LName = "McDonald";
            newUser.NickName = "mark2013";
            newUser.EmailAddress = "mark@gmail.com";
            newUser.RoleId = 1;

            IRepository<User> userRepository = new UsersRepository();
            userRepository.Create(newUser);
        }

        [Test]
        public void UserRepository_Validate_That_User_Can_Be_Deleted_ByNickName()
        {
            User newUser = new User();
            newUser.NickName = "mark2013";
            
            IRepository<User> userRepository = new UsersRepository();
            userRepository.Delete(newUser);
        }
    }
}
