using System;
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
                User newUser = new User(txtUserName.Text, txtPassword1.Text, txtFName.Text,
                    txtLName.Text, txtEmail.Text);

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
            return !String.IsNullOrEmpty(txtUserName.Text) && txtUserName.Text.Length > 5;
        }

        protected bool CheckPasswords()
        {
            // TODO: we should also do very basic client side verification of length,
            // characters, validity, etc.
            if (txtPassword1.Text == txtPassword2.Text && !String.IsNullOrEmpty(txtPassword1.Text))
            {
                lblPW1.ForeColor = System.Drawing.Color.Black;
                lblPW2.ForeColor = System.Drawing.Color.Black;
                return true;
            }
            else
            {
                lblPW1.ForeColor = System.Drawing.Color.Red;
                lblPW2.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }

        protected bool CheckEmailAddress()
        {
            string email = txtEmail.Text;
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