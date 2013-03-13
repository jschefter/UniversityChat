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
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshDropDownList();
            }
        }

        protected void RemoveChannel_Click(object sender, EventArgs e)
        {
            if (string.Equals(roomName.Text, "Select Class"))
            {
                resultLabel.Text = "No changes made.";
                return;
            }

            string connectionSource = ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
            SqlConnection connection = new SqlConnection(connectionSource);

            using (connection)
            {
                connection.Open();

                #region Remove Constraints

                // Remove room users constraint
                SqlCommand removeConstraintCommand;
                string sqlRemoveConstraintString = string.Format("ALTER TABLE [ucdatabase].[UniversityChat].[RoomUsers] DROP CONSTRAINT FK_UserRooms_Rooms;");
                removeConstraintCommand = new SqlCommand(sqlRemoveConstraintString, connection);
                SqlDataReader removeConstraintReader = removeConstraintCommand.ExecuteReader();
                removeConstraintReader.Close();

                #endregion

                #region Delete room

                // Delete room
                SqlCommand deleteRoomCommand;
                string sqlDeleteRoomString = string.Format("DELETE FROM [ucdatabase].[UniversityChat].[Rooms] WHERE [RoomName] = '{0}';", roomName.Text);
                deleteRoomCommand = new SqlCommand(sqlDeleteRoomString, connection);
                SqlDataReader deleteRoomReader = deleteRoomCommand.ExecuteReader();
                deleteRoomReader.Close();

                #endregion

                #region Add Constraints

                // Add room users constraint
                SqlCommand addConstraintCommand;
                string sqlAddConstraintString = string.Format("ALTER TABLE [ucdatabase].[UniversityChat].[RoomUsers] ADD CONSTRAINT FK_UserRooms_Rooms FOREIGN KEY([RoomId]) REFERENCES [UniversityChat].[Rooms](RoomId);");
                addConstraintCommand = new SqlCommand(sqlAddConstraintString, connection);
                SqlDataReader addConstraintReader = addConstraintCommand.ExecuteReader();
                addConstraintReader.Close();

                #endregion
            }
            connection.Close();

            // Show the admin what happened and refresh the drop down list
            resultLabel.Text = "Class name '" + roomName.Text + "' deleted.";
            RefreshDropDownList();
        }

        protected void RemoveUser_Click(object sender, EventArgs e)
        {
            if (string.Equals(roomName.Text, "Select User"))
            {
                resultLabel.Text = "No changes made.";
                return;
            }

            string connectionSource = ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
            SqlConnection connection = new SqlConnection(connectionSource);

            using (connection)
            {
                connection.Open();

                #region Remove Constraints

                // Remove room users constraint
                SqlCommand removeRoomUsersConstraintCommand;
                string sqlRemoveRoomUsersConstraintString = string.Format("ALTER TABLE [ucdatabase].[UniversityChat].[RoomUsers] DROP CONSTRAINT FK_UserRooms_Users;");
                removeRoomUsersConstraintCommand = new SqlCommand(sqlRemoveRoomUsersConstraintString, connection);
                SqlDataReader removeRoomUsersConstraintReader = removeRoomUsersConstraintCommand.ExecuteReader();
                removeRoomUsersConstraintReader.Close();

                // Remove history constraint
                SqlCommand removeHistoryConstraintCommand;
                string sqlRemoveHistoryConstraintString = string.Format("ALTER TABLE [ucdatabase].[UniversityChat].[History] DROP CONSTRAINT FK_History_Users;");
                removeHistoryConstraintCommand = new SqlCommand(sqlRemoveHistoryConstraintString, connection);
                SqlDataReader removeHistoryConstraintReader = removeHistoryConstraintCommand.ExecuteReader();
                removeHistoryConstraintReader.Close();

                #endregion

                #region Delete user

                // Delete User
                SqlCommand deleteUserCommand;
                string sqlDeleteUserString = string.Format("DELETE FROM [ucdatabase].[UniversityChat].[Users] WHERE [NickName] = '{0}';", userName.Text);
                deleteUserCommand = new SqlCommand(sqlDeleteUserString, connection);
                SqlDataReader deleteUserReader = deleteUserCommand.ExecuteReader();
                deleteUserReader.Close();

                #endregion

                #region Add Constraints

                // Add room users constraint
                SqlCommand addRoomUsersConstraintCommand;
                string sqlAddRoomUsersConstraintString = string.Format("ALTER TABLE [ucdatabase].[UniversityChat].[RoomUsers] ADD CONSTRAINT FK_UserRooms_Users FOREIGN KEY([UserId]) REFERENCES [UniversityChat].[Users](UserId);");
                addRoomUsersConstraintCommand = new SqlCommand(sqlAddRoomUsersConstraintString, connection);
                SqlDataReader addRoomUsersConstraintReader = addRoomUsersConstraintCommand.ExecuteReader();
                addRoomUsersConstraintReader.Close();

                // Add history constraint
                SqlCommand addHistoryConstraintCommand;
                string sqlAddHistoryConstraintString = string.Format("ALTER TABLE [ucdatabase].[UniversityChat].[History] ADD CONSTRAINT FK_History_Users FOREIGN KEY([UserId]) REFERENCES [UniversityChat].[Users](UserId);");
                addHistoryConstraintCommand = new SqlCommand(sqlAddHistoryConstraintString, connection);
                SqlDataReader addHistoryConstraintReader = addHistoryConstraintCommand.ExecuteReader();
                addHistoryConstraintReader.Close();

                #endregion
            }
            connection.Close();

            // Show the admin what happened and refresh the drop down list
            resultLabel.Text = "User name '" + userName.Text + "' deleted.";
            RefreshDropDownList();
        }

        private void RefreshDropDownList()
        {
            string connectionSource = ConfigurationManager.ConnectionStrings["ucdatabaseConnectionString2"].ToString();
            SqlConnection connection = new SqlConnection(connectionSource);

            using (connection)
            {
                connection.Open();

                #region Gets all the room

                SqlCommand roomCommand;
                string sqlRoomString = string.Format("SELECT [RoomName] FROM [ucdatabase].[UniversityChat].[Rooms];");
                roomCommand = new SqlCommand(sqlRoomString, connection);
                SqlDataReader roomReader = roomCommand.ExecuteReader();

                roomName.Items.Clear();
                int count = 1;
                roomName.Items.Insert(0, new ListItem("Select Class"));

                while (roomReader.Read())
                {
                    roomName.Items.Insert(count, new ListItem(roomReader[0].ToString()));
                    count++;
                }
                roomReader.Close();

                #endregion

                #region Gets all the users

                SqlCommand userCommand;
                string sqlUserString = string.Format("SELECT [NickName] FROM [ucdatabase].[UniversityChat].[Users];");
                userCommand = new SqlCommand(sqlUserString, connection);
                SqlDataReader userReader = userCommand.ExecuteReader();

                userName.Items.Clear();
                count = 1;
                userName.Items.Insert(0, new ListItem("Select User"));

                while (userReader.Read())
                {
                    userName.Items.Insert(count, new ListItem(userReader[0].ToString()));
                    count++;
                }
                userReader.Close();

                #endregion
            }
            connection.Close();
        }
    }
}
