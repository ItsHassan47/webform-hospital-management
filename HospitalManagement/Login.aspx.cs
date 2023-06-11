using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace HospitalManagement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(Session["PatientEmail"] as string) || (!string.IsNullOrEmpty(Session["DoctorEmail"] as string)))
            //    Response.Redirect("Default.aspx");
            // Check if the user is already authenticated and redirect if necessary
            if (User.Identity.IsAuthenticated)
                Response.Redirect("Default.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            //@hitec - ims.edu.pk            
            string tableName = IsHitecEmail(email) ? "Doctors" : "Patients"; // Specify the table name for patients or doctors
            // Validate the user's credentials against the database
            bool isValid = ValidateCredentials(email, password, tableName);

            if (isValid)
                Response.Redirect(tableName == "Patients" ? "Patient-Management.aspx" : "Doctor-Management.aspx");
            else
                lblMessage.Text = "Invalid email or password. Please try again.";
        }

        private bool ValidateCredentials(string email, string password, string tableName)
        {
            //Insert the user into the appropriate table(patient or doctor)
            //Determine if the user is registering as a patient or doctor
            if (tableName == "Patients")
            {
                string query = "SELECT TOP 1 COUNT(*) FROM Patients WHERE Email = @Email AND Password = @Password";
                bool condition = ExecuteSelectQuery(query, email, password);
                Session["PatientEmail"] = email;
                return condition;
            }
            else if (tableName == "Doctors")
            {
                string query = "SELECT TOP 1 COUNT(*) FROM Doctors WHERE Email = @Email AND Password = @Password";
                bool condition = ExecuteSelectQuery(query, email, password);
                Session["DoctorEmail"] = email;
                return condition;
            }

            return false;
        }

        private static bool IsHitecEmail(string email)
        {
            string pattern = @"@hitec-ims\.edu\.pk$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        private bool ExecuteSelectQuery(string query, string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            connection.Open();

            int count = (int)command.ExecuteScalar();

            return count > 0;
        }
    }
}