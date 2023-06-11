using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class Doctor_Management : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            string loggedInDoctorEmail = Session["DoctorEmail"] as string;
            if (string.IsNullOrEmpty(loggedInDoctorEmail))
            {
                // User is not authenticated, redirect to the login page
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                // Set the DoctorID session variable when the doctor signs in
                Session["DoctorID"] = GetDoctorID(Session["DoctorEmail"] as string); // Replace doctorID with the actual logged-in doctor's ID
                GridView1.DataBind(); // Bind the GridView control
                
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Update the doctor's information
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string name = ((TextBox)row.Cells[1].Controls[0]).Text;
            string email = ((TextBox)row.Cells[2].Controls[0]).Text;
            string password = ((TextBox)row.Cells[3].Controls[0]).Text;
            string specialization = ((TextBox)row.Cells[4].Controls[0]).Text;
            string qualification = ((TextBox)row.Cells[5].Controls[0]).Text;

            // Update the values in the data source
            SqlDataSource1.UpdateParameters["Name"].DefaultValue = name;
            SqlDataSource1.UpdateParameters["Email"].DefaultValue = email;
            SqlDataSource1.UpdateParameters["Password"].DefaultValue = password;
            SqlDataSource1.UpdateParameters["Specialization"].DefaultValue = specialization;
            SqlDataSource1.UpdateParameters["Qualification"].DefaultValue = qualification;
            SqlDataSource1.Update();

            GridView1.EditIndex = -1; // Exit edit mode
            GridView1.DataBind(); // Rebind the GridView
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Delete the doctor's information
            int doctorID = (int)GridView1.DataKeys[e.RowIndex].Value;

            // Delete the row from the data source            
            SqlDataSource1.DeleteParameters["@DoctorID"].DefaultValue = doctorID.ToString();
            SqlDataSource1.Delete();

            GridView1.DataBind(); // Rebind the GridView
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