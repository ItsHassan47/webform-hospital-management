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
    public partial class Pescript_Med : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["DoctorEmail"] as string))
            {
                Response.Redirect("~/Login.aspx");
            }            

            if (!IsPostBack)
            {
                // Populate the DropDownList controls with data
                PopulatePatients();
                PopulateDoctors();
                PopulateMedicines();
            }
        }

        private void PopulatePatients()
        {
            // Retrieve patient data from the database and bind it to the ddlPatients DropDownList
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string query = "SELECT PatientId, Name FROM Patients";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    ddlPatients.DataSource = reader;
                    ddlPatients.DataTextField = "Name";
                    ddlPatients.DataValueField = "PatientId";
                    ddlPatients.DataBind();
                    ddlPatients.Items.Insert(0, new ListItem("Select Patient", ""));
                }
            }
        }

        private void PopulateDoctors()
        {
            // Retrieve doctor data from the database and bind it to the ddlDoctors DropDownList
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string query = "SELECT DoctorId, Name FROM Doctors";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    ddlDoctors.DataSource = reader;
                    ddlDoctors.DataTextField = "Name";
                    ddlDoctors.DataValueField = "DoctorId";
                    ddlDoctors.DataBind();
                    ddlDoctors.Items.Insert(0, new ListItem("Select Doctor", ""));
                }
            }
        }

        private void PopulateMedicines()
        {
            // Retrieve medicine data from the database and bind it to the ddlMedicines DropDownList
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string query = "SELECT MedicineId, Name FROM Medicine";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    ddlMedicines.DataSource = reader;
                    ddlMedicines.DataTextField = "Name";
                    ddlMedicines.DataValueField = "MedicineId";
                    ddlMedicines.DataBind();
                    ddlMedicines.Items.Insert(0, new ListItem("Select Medicine", ""));
                }
            }
        }

        protected void btnSavePrescription_Click(object sender, EventArgs e)
        {
            // Retrieve the selected values from the dropdown lists and other input controls
            string selectedPatientId = ddlPatients.SelectedValue;
            string selectedDoctorId = ddlDoctors.SelectedValue;
            string selectedMedicineId = ddlMedicines.SelectedValue;
            string dosage = txtDosage.Text;
            string instructions = txtInstructions.Text;

            // Perform the necessary database operations to save the prescription
            // Assuming you have a database connection and the appropriate methods to interact with the database

            // Example code to save the prescription into the Prescription table
            string query = "INSERT INTO Prescription (PatientId, DoctorId, MedicineId, Dosage, Instructions) VALUES (@PatientId, @DoctorId, @MedicineId, @Dosage, @Instructions)";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientId", selectedPatientId);
                    command.Parameters.AddWithValue("@DoctorId", selectedDoctorId);
                    command.Parameters.AddWithValue("@MedicineId", selectedMedicineId);
                    command.Parameters.AddWithValue("@Dosage", dosage);
                    command.Parameters.AddWithValue("@Instructions", instructions);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Session["PrescriptionId"] = GetPrescriptionID(selectedDoctorId, selectedMedicineId, selectedPatientId);
            // Display success or error message to the user
            // You can use a label or any other UI element to display the message
            lblMessage.Text = "Prescription saved successfully!";
        }

        protected void btnViewPrescription_Click(object sender, EventArgs e)
        {
            //// Retrieve the prescription ID from the CommandArgument of the clicked button
            //Button btnViewPrescription = (Button)sender;
            //int prescriptionId = Convert.ToInt32(btnViewPrescription.CommandArgument);

            // Redirect to the ShowPrescription.aspx page with the prescription ID in the query string
            Response.Redirect("ShowPrescription.aspx");
        }


        public string GetPrescriptionID(string doctorId, string medicineId, string patientId)
        {    

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PrescriptionId FROM Prescription WHERE DoctorId = @DoctorId AND MedicineId = @MedicineId AND PatientId = @PatientId;";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DoctorId", doctorId);
                command.Parameters.AddWithValue("@MedicineId", medicineId);
                command.Parameters.AddWithValue("@PatientId", patientId);

                connection.Open();
                var result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
            }

            return null;
        }


    }
}