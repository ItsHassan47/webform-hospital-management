using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            if (!IsPostBack)
            {
                // Render the chat messages on the page
                RenderChatMessages();
            }
            RenderChatMessages();
        }

        protected void SendButton_Click(object sender, EventArgs e)
        {
            string message = messageInput.Value.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                string i = (Session["PatientID"] as string);
                int senderId = Int32.Parse((Session["PatientID"] as string)); // Change this to the appropriate sender ID (patient ID) 
                int receiverId = Int32.Parse(GetDoctorID(Session["DoctorEmailChat"] as string));

                // Insert the chat message into the "Chat" table
                InsertChatMessage(senderId, receiverId, message);

                // Render the chat messages on the page
                RenderChatMessages();

                // Clear the input field
                messageInput.Value = "";
            }
        }

        private void InsertChatMessage(int senderId, int receiverId, string message)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertQuery = $"INSERT INTO Chat (SenderId, ReceiverId, MessageText, Timestamp) VALUES (@SenderId, @ReceiverId, @MessageText, GETDATE())";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@SenderId", senderId);
                command.Parameters.AddWithValue("@ReceiverId", receiverId);
                command.Parameters.AddWithValue("@MessageText", message);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static int count = 0;

        private void RenderChatMessages()
        {
            chatBox.Controls.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT MessageText, SenderId FROM Chat ORDER BY Timestamp ASC";

                SqlCommand command = new SqlCommand(selectQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string message = reader.GetString(0);
                    int senderId = reader.GetInt32(1);

                    bool isPatient = (Session["DoctorEmail"] == null); // Assuming patient ID is 1

                    bool condition;
                    if (count % 2 == 0)
                    {
                        condition = false;
                        count += 1;
                    }
                    else
                    {
                        condition = true;
                        count += 1;
                    }

                    string messageId = Guid.NewGuid().ToString();
                    string messageHtml = $"<div id='{messageId}' class='message {(condition ? "patient" : "doctor")}'>{message}</div>";

                    // Append the message to the chat box
                    chatBox.InnerHtml += messageHtml;
                }

                reader.Close();

            }
        }

        private string GetDoctorID(string email)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            connection.Open();

            // SQL query to retrieve the ID based on the email
            string query = "SELECT DoctorId FROM Doctors WHERE Email = @Email";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);
            // Execute the query and retrieve the ID
            string id = ((int)command.ExecuteScalar()).ToString();

            return id;
        }
    }
}