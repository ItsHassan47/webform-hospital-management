using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class Appointments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAppointments();
            }
        }

        private void BindAppointments()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AppointmentId, PatientId, DoctorId, AppointmentDateTime, Status FROM Appointments";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                GridView1.DataSource = reader;
                GridView1.DataBind();
                reader.Close();
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindAppointments();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string appointmentId = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string status = ((DropDownList)row.Cells[4].Controls[1]).SelectedValue;

            // Update the status in the database
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Appointments SET Status = @Status WHERE AppointmentId = @AppointmentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            GridView1.EditIndex = -1;
            BindAppointments();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindAppointments();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string appointmentId = GridView1.DataKeys[e.RowIndex].Value.ToString();

            // Delete the appointment from the database
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Appointments WHERE AppointmentId = @AppointmentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            BindAppointments();
        }

        protected string GetAppointmentStatus(object status)
        {
            if (status != null)
            {
                string statusValue = status.ToString();
                if (statusValue == "Pending")
                {
                    return "Pending";
                }
                else if (statusValue == "Confirmed")
                {
                    return "Confirmed";
                }
                else if (statusValue == "Cancelled")
                {
                    return "Cancelled";
                }
            }
            return "";
        }
    }
}