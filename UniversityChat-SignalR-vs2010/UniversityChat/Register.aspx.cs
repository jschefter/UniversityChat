using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityChat.Model;
using UniversityChat.Data.Repositories;
using System.Net.Mail;
using UniversityChat.Chat;

namespace UniversityChat
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PanelRegisterUser.Visible = false;
            PanelUserRegistered.Visible = false;

            if (Request.IsAuthenticated)
            {
                PanelUserAuthenticated.Visible = true;
            }
            else if (Page.IsPostBack)
            {
                if (CheckUserName() && CheckPasswords() && CheckEmailAddress())
                {
                    // We want to create a new user with 'legal' (to this point) information
                    User newUser = new User(TextUsername.Text, TextPassword.Text, TextFirstName.Text,
                        TextLastName.Text);

                    if (Users.CreateUser(newUser))
                    {
                        // user was created...
                        PanelRegisterUser.Visible = false;
                        Username.Text = newUser.NickName;
                        PanelUserRegistered.Visible = true;
                    }
                    else
                    {
                        // could not create user.
                        UserExistsMessage.Visible = true;
                        PanelRegisterUser.Visible = true;
                    }
                }
                else
                {
                    // Something went wrong, tell the user to fix it.
                    InvalidMessage.Visible = true;
                    PanelRegisterUser.Visible = true;
                }
            }
            else
            {
                PanelRegisterUser.Visible = true;
            }
        }

        protected bool CheckFieldEmptyAndLength(TextBox field, Label label)
        {
            if (!String.IsNullOrEmpty(field.Text) && field.Text.Length > 5)
            {
                return true;
            }
            else
            {
                label.CssClass += "invalid";
                return false;
            }
        }

        protected bool CheckUserName()
        {
            return CheckFieldEmptyAndLength(TextUsername, UsernameLabel);
        }

        protected bool CheckPasswords()
        {
            bool passwordExists = CheckFieldEmptyAndLength(TextPassword, PasswordLabel);
            bool confirmPasswordExists = CheckFieldEmptyAndLength(TextConfirmPassword, ConfirmPasswordLabel);

            if (passwordExists && confirmPasswordExists)
            {
                return (TextPassword.Text.Equals(TextConfirmPassword.Text)) ? true : false;
            }

            return false;
        }

        protected bool CheckEmailAddress()
        {
            bool emailExists = CheckFieldEmptyAndLength(TextEmail, EmailLabel);

            if (emailExists)
            {
                string email = TextEmail.Text;
                try
                {
                    MailAddress addr = new MailAddress(email);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}