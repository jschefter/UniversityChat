using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

namespace UniversityChat
{
    public class TempUser
    {
        public TempUser(string nickName, string email, string password)
        {
            NickName = nickName;
            Email = email;
            Password = password;
        }

        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TempUser user;

                string connectionSource = ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
                SqlConnection connection = new SqlConnection(connectionSource);

                using (connection)
                {
                    connection.Open();
                    SqlCommand userCommand;
                    string sqlUserString = string.Format("SELECT [NickName], [Email], [Password] FROM [ucdatabase].[UniversityChat].[Users] WHERE [NickName] = '{0}';", Page.User.Identity.Name);
                    userCommand = new SqlCommand(sqlUserString, connection);
                    SqlDataReader userReader = userCommand.ExecuteReader();

                    userReader.Read();
                    user = new TempUser(userReader[0].ToString(), userReader[1].ToString(), userReader[2].ToString());
                    userReader.Close();
                }
                connection.Close();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string password;

            string connectionSource = ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
            SqlConnection connection = new SqlConnection(connectionSource);

            using (connection)
            {
                connection.Open();

                #region Verify that the user entered the right password

                SqlCommand passwordCommand;
                string sqlpasswordString = string.Format("SELECT [Password] FROM [ucdatabase].[UniversityChat].[Users] WHERE [NickName] = '{0}';", Page.User.Identity.Name);
                passwordCommand = new SqlCommand(sqlpasswordString, connection);
                SqlDataReader passwordReader = passwordCommand.ExecuteReader();

                passwordReader.Read();
                password = passwordReader[0].ToString();
                passwordReader.Close();

                // Check if the password matches the DB password
                if (!string.Equals(password, oldPasswordBox.Text))
                {
                    resultLabel.Text = "Invalid password.";
                    return;
                }
                else
                {
                    resultLabel.Text = "";
                }

                #endregion

                #region Check if the user wants to change his password

                if (CheckPasswords())
                {
                    SqlCommand passwordChangeCommand;
                    string sqlPasswordChangeString = string.Format("UPDATE [ucdatabase].[UniversityChat].[Users] SET [Password] = '{0}' WHERE [NickName] = '{1}';", passwordBox.Text, Page.User.Identity.Name);
                    passwordChangeCommand = new SqlCommand(sqlPasswordChangeString, connection);
                    SqlDataReader passwordChangeReader = passwordChangeCommand.ExecuteReader();
                    passwordChangeReader.Read();
                    passwordChangeReader.Close();

                    resultLabel.Text = "Password changed. ";
                }

                #endregion

                #region Check if the user wants to change his email address

                if (CheckEmailAddress())
                {
                    SqlCommand emailCommand;
                    string sqlEmailString = string.Format("UPDATE [ucdatabase].[UniversityChat].[Users] SET [Email] = '{0}' WHERE [NickName] = '{1}';", emailBox.Text, Page.User.Identity.Name);
                    emailCommand = new SqlCommand(sqlEmailString, connection);
                    SqlDataReader emailReader = emailCommand.ExecuteReader();
                    emailReader.Read();
                    emailReader.Close();

                    resultLabel.Text += "Email changed. ";
                }

                #endregion

                #region Check if the user wants to change his nickname
                /*
                if (CheckNickName())
                {
                    SqlCommand nickNameCommand;
                    string sqlNickNameString = string.Format("UPDATE [ucdatabase].[UniversityChat].[Users] SET [NickName] = '{0}' WHERE [NickName] = '{1}';", nickNameBox.Text, Page.User.Identity.Name);
                    nickNameCommand = new SqlCommand(sqlNickNameString, connection);
                    SqlDataReader nickNameReader = nickNameCommand.ExecuteReader();
                    nickNameReader.Read();
                    nickNameReader.Close();

                    resultLabel.Text += "Nick name changed.";
                }
                */
                #endregion
            }
            connection.Close();
        }

        protected bool CheckFieldEmptyAndLength(TextBox field, Label label)
        {
            if (!String.IsNullOrEmpty(field.Text) && field.Text.Length > 5)
            {
                return true;
            }
            else
            {
                label.CssClass += " invalid";
                return false;
            }
        }

        protected bool CheckNickName()
        {
            return CheckFieldEmptyAndLength(nickNameBox, nickNameLabel);
        }

        protected bool CheckPasswords()
        {
            bool passwordExists = CheckFieldEmptyAndLength(passwordBox, passwordLabel);
            bool confirmPasswordExists = CheckFieldEmptyAndLength(passwordConfirmBox, passwordConfirmLabel);

            if (passwordExists && confirmPasswordExists)
            {
                return passwordBox.Text.Equals(passwordConfirmBox.Text);
            }

            return false;
        }

        protected bool CheckEmailAddress()
        {
            bool emailExists = CheckFieldEmptyAndLength(emailBox, emailLabel);

            if (emailExists)
            {
                string email = emailBox.Text;
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