﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityChat.Model
{
    public class User
    {
        public decimal Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public int RoleId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}