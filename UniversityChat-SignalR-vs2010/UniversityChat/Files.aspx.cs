using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace UniversityChat
{
    public class UploadedFile
    {
        public UploadedFile(string logTime, string fileName, string fileType, string binaryData)
        {
            LogTime = logTime;
            FileName = fileName;
            FileType = fileType;
            BinaryData = binaryData;
        }

        public string LogTime { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string BinaryData { get; set; }
    }

    public partial class Files : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<UploadedFile> files = new List<UploadedFile>();

            string connectionSource =
                    ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
            SqlConnection connection = new SqlConnection(connectionSource);

            using (connection)
            {
                connection.Open();
                SqlCommand fileCommand;
                string sqlFileString = string.Format("SELECT * FROM [ucdatabase].[UniversityChat].[File];");
                fileCommand = new SqlCommand(sqlFileString, connection);
                SqlDataReader fileReader = fileCommand.ExecuteReader();

                while (fileReader.Read())
                {
                    // LogDateTimeStamp, UserId, Text
                    files.Add(new UploadedFile(fileReader[1].ToString(), fileReader[2].ToString(), fileReader[3].ToString(), fileReader[4].ToString()));
                }
                fileReader.Close();
            }
            connection.Close();

            // Bind the chat logs to the repeater
            LogRepeater.DataSource = files;
            LogRepeater.DataBind();
        }
    }
}