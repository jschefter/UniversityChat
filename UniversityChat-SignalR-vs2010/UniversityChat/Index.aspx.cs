using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!uploadFile.HasFile)
                return;

            fileName.Text = "Uploaded " + uploadFile.FileName + "Content Type " + uploadFile.PostedFile.ContentType;

            byte[] fileSize = new byte[uploadFile.PostedFile.ContentLength];
            HttpPostedFile uploadedFile = uploadFile.PostedFile;
            uploadedFile.InputStream.Read(fileSize, 0, (int)uploadFile.PostedFile.ContentLength);

            string connectionSource = ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
            SqlConnection connection = new SqlConnection(connectionSource);

            using (connection)
            {
                connection.Open();

                SqlCommand uploadFileCommand = new SqlCommand();
                uploadFileCommand.CommandText = "INSERT INTO [ucdatabase].[UniversityChat].[File] ([UploadDate], [FileName], [MimeType], [BinaryData]) VALUES (@UploadDate, @FileName, @MimeType, @BinaryData)";
                uploadFileCommand.CommandType = CommandType.Text;
                uploadFileCommand.Connection = connection;

                SqlParameter uploadDate = new SqlParameter("@UploadDate", SqlDbType.DateTime);
                uploadDate.Value = DateTime.Now;
                uploadFileCommand.Parameters.Add(uploadDate);

                SqlParameter uploadFileName = new SqlParameter("@FileName", SqlDbType.Text);
                uploadFileName.Value = uploadFile.FileName;
                uploadFileCommand.Parameters.Add(uploadFileName);

                SqlParameter mimeType = new SqlParameter("@MimeType", SqlDbType.Text);
                mimeType.Value = uploadFile.PostedFile.ContentType;
                uploadFileCommand.Parameters.Add(mimeType);

                SqlParameter uploadData = new SqlParameter("@BinaryData", SqlDbType.Image);
                uploadData.Value = fileSize;
                uploadFileCommand.Parameters.Add(uploadData);

                SqlDataReader uploadFileCommandReader = uploadFileCommand.ExecuteReader();
                uploadFileCommandReader.Read();
                uploadFileCommandReader.Close();
            }
            connection.Close();
        }

        /*
        protected void ajaxUpload1_OnUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            //// Generate file path
            //string filePath = "~/Files/" + e.FileName;

            //// Save upload file to the file system
            //AjaxUpload1.SaveAs(Server.MapPath(filePath));

            // Read the file and convert it to Byte Array
            string filePath = Server.MapPath("~/Files/" + e.FileName);
            string filename = Path.GetFileName(filePath);

            AjaxUpload1.SaveAs(MapPath(filePath));

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            br.Close();
            fs.Close();

            //insert the file into database
            string strQuery = "insert into tblFiles(Name, ContentType, Data) values (@Name, @ContentType, @Data)";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename;
            cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = "image/jpeg";
            cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
            InsertUpdateData(cmd);
        }

        private bool InsertUpdateData(SqlCommand cmd)
        {
            String strConnString = System.Configuration.ConfigurationManager
            .ConnectionStrings["conString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private void ReadFile()
        {
            // Read the file and convert it to Byte Array
            string filePath = Server.MapPath("APP_DATA/TestDoc.docx");
            string filename = Path.GetFileName(filePath);

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            br.Close();
            fs.Close();
        }
        */
    }
}