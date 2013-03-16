using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityChat.Model;
using UniversityChat.Data.Repositories;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace UniversityChat
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!CheckUserName())
            {
                resultLabel.Text = "Invalid nick name.";
                return;
            }
            string userEmail;
            string tempPassword = "password";

            string connectionSource = ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
            SqlConnection connection = new SqlConnection(connectionSource);

            using (connection)
            {
                connection.Open();

                #region Find the email of the nick name

                SqlCommand emailCommand;
                string sqlEmailString = string.Format("SELECT [Email] FROM [ucdatabase].[UniversityChat].[Users] WHERE [NickName] = '{0}';", TextUsername.Text);
                emailCommand = new SqlCommand(sqlEmailString, connection);
                SqlDataReader emailReader = emailCommand.ExecuteReader();

                if (emailReader.Read())
                    userEmail = emailReader[0].ToString();
                else
                {
                    resultLabel.Text = "Nickname not found.";
                    return;
                }
                emailReader.Close();

                #endregion

                #region Reset the password of the account

                SqlCommand passwordCommand;
                string sqlPasswordString = string.Format("UPDATE [ucdatabase].[UniversityChat].[Users] SET [Password] = '{0}' WHERE [NickName] = '{1}';", tempPassword, TextUsername.Text);
                passwordCommand = new SqlCommand(sqlPasswordString, connection);
                SqlDataReader passwordReader = passwordCommand.ExecuteReader();

                #endregion
            }
            connection.Close();

            #region Send the email with the temporary password

            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("admin@uc.com");
            mailMessage.To.Add(userEmail);
            mailMessage.Subject = "Password reset for University Chat";
            mailMessage.Body = "Your temporary password is '" + tempPassword + "', please remember to change it.";
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;

            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Send(mailMessage);

            resultLabel.Text = "Email sent.";

            #endregion
        }

        protected bool CheckUserName()
        {
            return !String.IsNullOrEmpty(TextUsername.Text) && TextUsername.Text.Length > 5;
        }
    }
}