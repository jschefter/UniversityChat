using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityChat.Model;
using UniversityChat.Data.Repositories;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace UniversityChat
{
    public class ChatLog
    {
        public ChatLog(string logTime, string userId, string text)
        {
            LogTime = logTime;
            UserId = userId;
            Text = text;
        }

        public string LogTime { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
    }

    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connectionSource =
                    ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
                SqlConnection connection = new SqlConnection(connectionSource);

                using (connection)
                {
                    connection.Open();
                    SqlCommand roomCommand;
                    string sqlRoomString = string.Format("SELECT [RoomName] FROM [ucdatabase].[UniversityChat].[Rooms];");
                    roomCommand = new SqlCommand(sqlRoomString, connection);
                    SqlDataReader roomReader = roomCommand.ExecuteReader();

                    int count = 1;
                    roomName.Items.Insert(0, new ListItem("All"));

                    while (roomReader.Read())
                    {
                        // LogDateTimeStamp, UserId, Text
                        roomName.Items.Insert(count, new ListItem(roomReader[0].ToString()));
                        count++;
                    }
                    roomReader.Close();
                }
                connection.Close();
            }
        }

        protected void buttonSubmitForm_Click(object sender, EventArgs e)
        {
            string connectionSource = ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
            SqlConnection connection = new SqlConnection(connectionSource);
            List<ChatLog> chatLogs = new List<ChatLog>();

            using (connection)
            {
                connection.Open();

                #region Getting the ID of the room

                // Get the id of the room
                string classId = "";
                if (roomName.SelectedIndex != 0)
                {
                    SqlCommand roomCommand;
                    string sqlRoomString =
                        string.Format(
                            "SELECT [RoomId] FROM [ucdatabase].[UniversityChat].[Rooms] WHERE [RoomName] = '{0}';",
                            roomName.Text);
                    roomCommand = new SqlCommand(sqlRoomString, connection);
                    SqlDataReader roomReader = roomCommand.ExecuteReader();

                    // If there is nothing to read, return
                    if (!roomReader.Read())
                    {
                        ClassName.Text = "No Log Found!";
                        LogRepeater.DataSource = null;
                        LogRepeater.DataBind();
                        return;
                    }
                    classId = roomReader[0].ToString();
                    roomReader.Close();
                }

                #endregion

                #region Getting the date range for the logs

                // Date Range
                string dateFrom = datepickerFrom.Text + " 12:00:00 AM";
                string dateTo = datepickerTo.Text + " 11:59:59 AM";

                SqlCommand command;
                string sqlString;
                if (string.IsNullOrEmpty(datepickerFrom.Text) || string.IsNullOrEmpty(datepickerTo.Text))
                {
                    if (classId == "")
                        sqlString = string.Format("SELECT [LogDateTimeStamp], [UserId], [Text] FROM [ucdatabase].[UniversityChat].[History] ORDER BY [LogDateTimeStamp];");
                    else
                        sqlString = string.Format("SELECT [LogDateTimeStamp], [UserId], [Text] FROM [ucdatabase].[UniversityChat].[History] WHERE [RoomId] = '{0}' ORDER BY [LogDateTimeStamp];", classId);
                }
                else
                {
                    if (classId == "")
                        sqlString = string.Format("SELECT [LogDateTimeStamp], [UserId]NickName, [Text] FROM [ucdatabase].[UniversityChat].[History] WHERE [LogDateTimeStamp] BETWEEN '{0}' AND '{1}' ORDER BY [LogDateTimeStamp];", dateFrom, dateTo);
                    else
                        sqlString = string.Format("SELECT [LogDateTimeStamp], [UserId], [Text] FROM [ucdatabase].[UniversityChat].[History] WHERE [LogDateTimeStamp] BETWEEN '{0}' AND '{1}' AND [RoomId] = '{2}' ORDER BY [LogDateTimeStamp];", dateFrom, dateTo, classId);
                }
                
                command = new SqlCommand(sqlString, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // LogDateTimeStamp, UserId, Text
                    chatLogs.Add(new ChatLog(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                }
                reader.Close();

                #endregion

                #region Getting the nick names from the user IDs

                for (int i = 0; i < chatLogs.Count; i++)
                {
                    SqlCommand nickNameCommand;
                    string sqlNickNameString =
                        string.Format(
                            "SELECT [NickName] FROM [ucdatabase].[UniversityChat].[Users] WHERE [UserId] = '{0}';",
                            chatLogs[i].UserId);
                    nickNameCommand = new SqlCommand(sqlNickNameString, connection);
                    SqlDataReader nickNameReader = nickNameCommand.ExecuteReader();

                    nickNameReader.Read();
                    chatLogs[i].UserId = nickNameReader[0].ToString();
                    nickNameReader.Close();
                }

                #endregion
            }
            connection.Close();

            // Bind the chat logs to the repeater
            LogRepeater.DataSource = chatLogs;
            LogRepeater.DataBind();

            if (roomName.SelectedIndex == 0)
                ClassName.Text = "Class Name: All";
            else
                ClassName.Text = "Class Name: " + roomName.Text;
        }
    }
}