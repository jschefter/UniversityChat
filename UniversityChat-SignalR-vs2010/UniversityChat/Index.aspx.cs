using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityChat
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                UserName.Visible = true;
                UserName.Text = Page.User.Identity.Name;
            }
            else
            {
                UserName.Visible = false;
            }
        }
    }
}