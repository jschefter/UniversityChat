using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityChat.Chat;
using System.Web.Security;

namespace UniversityChat
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                AuthPanel.Visible = true;
            }
            else
            {
                AnonPanel.Visible = true;
            }

            if (Page.IsPostBack)
            {
                UniversityChat.Model.User user = new UniversityChat.Model.User(UserName.Text, Password.Text);

                if (Users.AuthenticateUser(user))
                {
                    FormsAuthentication.RedirectFromLoginPage(user.NickName, RememberMe.Checked);
                }
                else
                {
                    // authentication failed!
                    InvalidMessage.Visible = true;
                }
            }
        }
    }
}