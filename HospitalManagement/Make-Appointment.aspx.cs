using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class Make_Appointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: Perform any initialization logic here
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get the input values from the text boxes
            int patientId = Convert.ToInt32(txtPatientId.Text);
            int doctorId = Convert.ToInt32(GetDoctorID(Session["DoctorEmailChat"] as string));
            DateTime appointmentDateTime = Convert.ToDateTime(txtAppointmentDateTime.Text);

            // Save the appointment to the database
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Appointments (PatientId, DoctorId, AppointmentDateTime, Status) VALUES (@PatientId, @DoctorId, @AppointmentDateTime, @Status)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientId", patientId);
                        command.Parameters.AddWithValue("@DoctorId", doctorId);
                        command.Parameters.AddWithValue("@AppointmentDateTime", appointmentDateTime);
                        command.Parameters.AddWithValue("@Status", "Pending");

                        connection.Open();
                        command.ExecuteNonQuery();
                        // Display success message
                        lblMessage.Text = "Appointment successfully made!";                        
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred while making the appointment: " + ex.Message;
                return;
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