﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using UniversityChat.Chat;

namespace UniversityChat
{
    public partial class Index : System.Web.UI.Page
    {
        protected int userCount = Users.CountOfConnectedUsers;

        protected void btnUploadClick(object sender, EventArgs e) {
            HttpPostedFile file = Request.Files["myFile"];

            //check file was submitted
            if (file != null && file.ContentLength > 0) {
                string fname = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));
            }
        }
    }
}