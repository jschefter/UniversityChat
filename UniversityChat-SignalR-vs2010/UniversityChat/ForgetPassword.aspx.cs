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
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CheckUserName())
            {
                // Send a link to reset the password to the email
            }
            else
            {
                CloseWindow();
            }
        }

        protected bool CheckUserName()
        {
            return !String.IsNullOrEmpty(TextUsername.Text) && TextUsername.Text.Length > 5;
        }

        private void CloseWindow()
        {
            Response.Write("<script language='javascript'> { self.close() }</script>");
        }
    }
}