using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityChat.Chat;
using System.Web.Security;
//using System.Web.Security;

namespace UniversityChat
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected int userCount = Users.CountOfConnectedUsers;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LinkToLogin_Click(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void LinkToLogout_Click(object sender, EventArgs e)
        {
           Response.Redirect("~/Logout.aspx");
        }
    }
}