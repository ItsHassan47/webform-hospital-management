using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already authenticated and redirect if necessary
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("Default.aspx");
            }
        }

        private bool IsEmailUnique(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT COUNT(*) FROM Patients WHERE Email = @Email", connection);
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();

                int count = (int)command.ExecuteScalar();

                return count == 0;  // Returns true if the email is unique, false otherwise
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string gender = ddlGender.Value;
            string MedicalHistory = txtMedicalHistory.Value;
            string address = txtAddress.Text;

            if (!IsEmailUnique(email))
            {
                lblMessage.Text = "Email is already registered. Please use a different email.";
                return;
            }

            // Validate inputs
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Display an error message
                // You can use a Label control to display the error message
                lblMessage.Text = "Invalid email or password. Please try again.";
                return;
            }

            if (password != confirmPassword)
            {
                // Display an error message
                // You can use a Label control to display the error message
                lblMessage.Text = "Invalid email or password. Please try again.";
                return;
            }

            // Insert into the Patients table
            // Check if a picture was selected

            // Read the selected file into a byte array


            // Perform the database insert
            //string connectionString = "Data Source=C0FFEE-PC;Initial Catalog=HospitalMangement;Integrated Security=True;";
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Patients (Name, Email, Password, Gender, Address, Picture, MedicalHistory) VALUES (@Name, @Email, @Password, @Gender, @Address, @Picture, @MedicalHistory)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@MedicalHistory", MedicalHistory);


                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }



            // Redirect the user to a success page or perform any other necessary actions
            Session["PatientEmail"] = email;
            Response.Redirect("Patient-Management.aspx");
        }
    }
}
