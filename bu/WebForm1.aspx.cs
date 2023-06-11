using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static List<Tuple<string, bool>> chatMessages;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chatMessages = new List<Tuple<string, bool>>();
            }
            else
            {
                chatMessages = (List<Tuple<string, bool>>)ViewState["ChatMessages"];
            }

            RenderChatMessages();
        }

        protected void SendButton_Click(object sender, EventArgs e)
        {
            string message = messageInput.Value.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                // Determine if the message is from the patient or the doctor
                bool isPatient = true; // Change this logic as per your requirements
                if (!string.IsNullOrEmpty(Session["DoctorEmail"] as string))
                    isPatient = false;
                // Add the message to the chat messages collection
                chatMessages.Add(new Tuple<string, bool>(message, isPatient));

                // Render the updated chat messages
                RenderChatMessages();

                // Clear the input field
                messageInput.Value = "";
            }

            string message = messageInput.Value.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                // Generate a unique ID for the message (you may use a database-generated ID instead)
                string messageId = Guid.NewGuid().ToString();
                bool isPatient = true;

                //if (string.IsNullOrEmpty(Session["PatientEmail"] as string))
                //    Response.Redirect("Login.aspx");

                if (!string.IsNullOrEmpty(Session["DoctorEmail"] as string))
                    isPatient = false;
                // Determine if the message is from the patient or the doctor
                // Change this logic as per your requirements

                // Create the message HTML
                string messageHtml = $"<div id='{messageId}' class='message {(isPatient ? "patient" : "doctor")}'>{message}</div>";

                // Append the message to the chat box
                chatBox.InnerHtml += messageHtml;

                // Clear the input field
                messageInput.Value = "";
            }
        }

        private void RenderChatMessages()
        {
            chatBox.Controls.Clear();

            foreach (var chatMessage in chatMessages)
            {
                Label messageLabel = new Label();
                messageLabel.Text = chatMessage.Item1;
                messageLabel.CssClass = chatMessage.Item2 ? "patient" : "doctor";

                chatBox.Controls.Add(messageLabel);
                chatBox.Controls.Add(new LiteralControl("<br />"));
            }

            ViewState["ChatMessages"] = chatMessages;
        }
    }
}