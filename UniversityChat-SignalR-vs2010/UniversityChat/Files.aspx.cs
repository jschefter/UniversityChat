using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace UniversityChat
{
    public class UploadedFile
    {
        public UploadedFile(string id, string logTime, string fileName, string fileType, byte[] binaryData)
        {
            ID = id;
            LogTime = logTime;
            FileName = fileName;
            FileType = fileType;
            BinaryData = binaryData;
        }

        public string ID { get; set; }
        public string LogTime { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] BinaryData { get; set; }
    }

    public partial class Files : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<UploadedFile> files = new List<UploadedFile>();

            string connectionSource = ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
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
                    // LogDateTimeStamp, FileName, FileType, Binary Data
                    files.Add(new UploadedFile(fileReader[0].ToString(), fileReader[1].ToString(),
                                               fileReader[2].ToString(), fileReader[3].ToString(),
                                               (byte[]) fileReader[4]));
                }
                fileReader.Close();
            }
            connection.Close();

            // Bind the chat logs to the repeater
            LogRepeater.DataSource = files;
            LogRepeater.DataBind();
        }

        protected void downloadLink_Click(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            string connectionSource = ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
            SqlConnection connection = new SqlConnection(connectionSource);

            UploadedFile file;
            using (connection)
            {
                connection.Open();
                SqlCommand fileCommand;
                string sqlFileString =
                    string.Format("SELECT * FROM [ucdatabase].[UniversityChat].[File] WHERE [FileId] = '{0}';", id);
                fileCommand = new SqlCommand(sqlFileString, connection);
                SqlDataReader fileReader = fileCommand.ExecuteReader();

                fileReader.Read();
                file = new UploadedFile(fileReader[0].ToString(), fileReader[1].ToString(),
                                               fileReader[2].ToString(), fileReader[3].ToString(),
                                               (byte[]) fileReader[4]);
                fileReader.Close();
            }
            connection.Close();

            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.FileName.Replace(",", ""));
            BinaryWriter binaryWriter = new BinaryWriter(Response.OutputStream);
            binaryWriter.Write(file.BinaryData);
            Response.ContentType = file.FileType;
            Response.Flush();
            binaryWriter.Close();
            Response.End();
        }
    }
}