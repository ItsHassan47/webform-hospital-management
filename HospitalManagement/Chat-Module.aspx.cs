using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class Chat_Module : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadChatHistory();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadChatHistory()
        {
            string chatHistory = "";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string query = "SELECT MessageText FROM Chat ORDER BY Timestamp ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dtChat = new DataTable();
                        adapter.Fill(dtChat);

                        foreach (DataRow row in dtChat.Rows)
                        {
                            string messageText = row["MessageText"].ToString();
                            chatHistory += "<div>" + messageText + "</div>";
                        }
                    }
                }
            }

            return chatHistory;
        }

        [WebMethod]
        public static void SendMessage(string message)
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                string senderId = (context.Session["PatientID"]).ToString();
                string senderEmail = GetSenderEmail();
                string receiverId = (context.Session["DoctorID"]).ToString();
                DateTime timestamp = DateTime.Now;

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    string query = "INSERT INTO Chat (SenderId, ReceiverId, MessageText, Timestamp) VALUES (@SenderId, @ReceiverId, @MessageText, @Timestamp)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SenderId", senderId);
                        command.Parameters.AddWithValue("@ReceiverId", receiverId);
                        command.Parameters.AddWithValue("@MessageText", message);
                        command.Parameters.AddWithValue("@Timestamp", timestamp);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private static string GetSenderEmail()
        {
            // Get the doctor's email from the query parameter
            string patientEmail = HttpContext.Current.Request.QueryString["patientEmail"];

            // Return the doctor's email
            return patientEmail;
        }
    }
}
