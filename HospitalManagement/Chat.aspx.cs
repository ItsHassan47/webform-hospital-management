using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class Chat1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((Session["PatientEmail"] as string)) || string.IsNullOrEmpty((Session["DoctorEmail"] as string)))
            {
                // User is not authenticated, redirect to the login page
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                BindChatMessages();
            }
        }

        private void BindChatMessages()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string query = "SELECT SenderId, ReceiverId, MessageText, Timestamp FROM Chat ORDER BY Timestamp";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvChat.DataSource = dataTable;
                    gvChat.DataBind();
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string query = "INSERT INTO Chat (SenderId, ReceiverId, MessageText, Timestamp) VALUES (@SenderId, @ReceiverId, @MessageText, @Timestamp)";
            
            int senderId = Int32.Parse((Session["PatientID"] as string));
            int receiverId = Int32.Parse((Session["DoctorID"] as string));
            string messageText = txtPatientMessage.Text;
            DateTime timestamp = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderId", senderId);
                    command.Parameters.AddWithValue("@ReceiverId", receiverId);
                    command.Parameters.AddWithValue("@MessageText", messageText);
                    command.Parameters.AddWithValue("@Timestamp", timestamp);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            txtPatientMessage.Text = string.Empty; // Clear the input field
            BindChatMessages(); // Refresh the chat messages
        }
    }
}