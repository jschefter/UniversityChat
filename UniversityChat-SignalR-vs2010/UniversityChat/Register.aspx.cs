﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityChat.Model;
using UniversityChat.Data.Repositories;

namespace UniversityChat
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // TODO check and verify other fields
            if (CheckUserName() && CheckPasswords() && CheckEmailAddress())
            {
                // We want to create a new user with 'legal' (to this point) information
                User newUser = new User(TextUsername.Text, TextPassword.Text, TextFirstName.Text,
                    TextLastName.Text);

                if (newUser.VerifyAll())
                {
                    // We can return success to the user!

                    IRepository<User> userRepository = new UsersRepository();
                    userRepository.Create(newUser);

                    CloseWindow();
                }
                else
                {
                    // Something went wrong, we should fix it.
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }

        protected bool CheckUserName()
        {
            return !String.IsNullOrEmpty(TextUsername.Text) && TextUsername.Text.Length > 5;
        }

        protected bool CheckPasswords()
        {
            // TODO: we should also do very basic client side verification of length,
            // characters, validity, etc.
            if (TextPassword.Text == TextConfirmPassword.Text && !String.IsNullOrEmpty(TextPassword.Text))
            {
                UsernameLabel.ForeColor = System.Drawing.Color.Black;
                LastNameLabel.ForeColor = System.Drawing.Color.Black;
                return true;
            }
            else
            {
                UsernameLabel.ForeColor = System.Drawing.Color.Red;
                LastNameLabel.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }

        protected bool CheckEmailAddress()
        {
            string email = TextEmail.Text;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                // TODO: did we decide emial was not required?
                if (String.IsNullOrEmpty(email)) return true;

                return false;
            }
        }

        private void CloseWindow()
        {
            Response.Write("<script language='javascript'> { self.close() }</script>");
        }
    }
}