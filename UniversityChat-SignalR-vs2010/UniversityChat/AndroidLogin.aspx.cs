using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityChat.Chat;

namespace UniversityChat
{
    public partial class AndroidLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string result = "Denied";

            if (Request.RequestType.Equals("POST"))
            {
                if (!string.IsNullOrEmpty(Request.Params["username"]) && !string.IsNullOrEmpty(Request.Params["password"]))
                {
                    string username = Request.Params["username"];
                    string password = Request.Params["password"];

                    UniversityChat.Model.User user = new UniversityChat.Model.User(username, password);

                    if (Users.AuthenticateUser(user))
                    {
                        result = "Authenticated";
                    }
                    else
                    {
                        // authentication failed!
                        // stay with "Denied" response...
                    }
                }
            }

            Response.Write(result);
            Response.End();
        }
    }
}