using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class Find_Doctor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDoctors();
            }
        }

        protected void BindDoctors()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Doctors";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    GridViewDoctors.DataSource = reader;
                    GridViewDoctors.DataBind();
                }
            }
        }


        //protected void ButtonChat_Click(object sender, EventArgs e)
        //{
        //    // Get the doctor's email from the data source (e.g., GridView, ListBox, etc.)
        //    string patientEmail = Session["PatientEmail"] as string;
        //    //if (string.IsNullOrEmpty(patientEmail))
        //    //{
        //    //    // User is not authenticated, redirect to the login page
        //    //    Response.Redirect("~/Login.aspx");
        //    //}

        //    // Redirect the user to the Chat.aspx page, passing the doctor's email as a query parameter
        //    Response.Redirect("webform1.aspx?doctorEmail=" + Server.UrlEncode(patientEmail));
        //}

        protected void ButtonChat_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            GridViewRow row = (GridViewRow)button.NamingContainer;
            int rowIndex = Convert.ToInt32(button.CommandArgument);

            // Retrieve the email from the GridView based on the row index
            string email = GridViewDoctors.Rows[rowIndex].Cells[2].Text;

            // Store the email in the session
            Session["DoctorEmailChat"] = email;

            // Check if the user is authenticated
            string patientEmail = Session["PatientEmail"] as string;
            if (string.IsNullOrEmpty(patientEmail) && string.IsNullOrEmpty(Session["DoctorEmail"] as string))
            {
                // User is not authenticated, redirect to the login page
                Response.Redirect("~/Login.aspx");
            }

            // Redirect the user to the Chat.aspx page, passing the doctor's email as a query parameter
            Response.Redirect("WebForm1.aspx?doctorEmail=" + Server.UrlEncode(email));
        }

        protected void ButtonAppointments_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            GridViewRow row = (GridViewRow)button.NamingContainer;
            int rowIndex = Convert.ToInt32(button.CommandArgument);

            // Retrieve the email from the GridView based on the row index
            string email = GridViewDoctors.Rows[rowIndex].Cells[2].Text;

            // Store the email in the session
            Session["DoctorEmailChat"] = email;

            // Check if the user is authenticated
            string patientEmail = Session["PatientEmail"] as string;
            //if (string.IsNullOrEmpty(patientEmail) && string.IsNullOrEmpty(Session["DoctorEmail"] as string))
            //{
            //    // User is not authenticated, redirect to the login page
            //    Response.Redirect("~/Login.aspx");
            //}

            // Redirect the user to the Chat.aspx page, passing the doctor's email as a query parameter
            Response.Redirect("Make-Appointment.aspx");
        }












        //private void LoadDoctors(string specialization)
        //{
        //    // Load all doctors initially
        //    var doctors = DoctorDAL.GetDoctors(specialization);
        //    if (doctors != null)
        //    {
        //        gvDoctors.DataSource = doctors;
        //        gvDoctors.DataBind();
        //    }
        //}

        //protected void ddlSpecialization_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedSpecialization = ddlSpecialization.SelectedValue;
        //    LoadDoctors(selectedSpecialization);
        //}

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    string selectedSpecialization = ddlSpecialization.SelectedValue;
        //    LoadDoctors(selectedSpecialization);
        //}

        //protected void btnChat_Click(object sender, EventArgs e)
        //{
        //    // Retrieve the doctor ID from the command argument of the chat button
        //    Button btnChat = (Button)sender;
        //    int doctorId = Convert.ToInt32(btnChat.CommandArgument);

        //    // Perform any necessary actions to initiate the chat with the doctor
        //    // For example, you can redirect to a chat page passing the doctorId in the query string
        //    Response.Redirect($"Chat.aspx?doctorId={doctorId}");
        //}

        //private void LoadDoctors(string specialization)
        //{
        //    DataTable dtDoctors = DoctorDAL.GetDoctorsBySpecialization(specialization);
        //    gvDoctors.DataSource = dtDoctors;
        //    gvDoctors.DataBind();
        //}

        //public class DoctorDAL
        //{
        //    // Other methods...

        //    public static List<string> GetQualifications()
        //    {
        //        List<string> qualifications = new List<string>();

        //        string connectionString = "YourConnectionString"; // Replace with your actual connection string
        //        string query = "SELECT DISTINCT Qualification FROM Doctors";

        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                connection.Open();
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        string qualification = reader["Qualification"].ToString();
        //                        qualifications.Add(qualification);
        //                    }
        //                }
        //            }
        //        }

        //        return qualifications;
        //    }
        //}












        /*
         *  protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Load doctors based on the selected specialization
            LoadDoctors(ddlSpecialization.SelectedValue);
        }

        protected void gvDoctors_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected doctor's email
            string doctorEmail = gvDoctors.SelectedRow.Cells[1].Text;

            // Redirect to the doctor's profile page
            Response.Redirect("Doctor-Profile.aspx?Email=" + doctorEmail);
        }

        protected void gvDoctors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the page index of the grid view to the page index of the clicked page
            gvDoctors.PageIndex = e.NewPageIndex;

            // Load doctors based on the selected specialization
            LoadDoctors(ddlSpecialization.SelectedValue);
        }
         * 
         */
    }
}