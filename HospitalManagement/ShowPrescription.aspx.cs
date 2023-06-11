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
    public partial class ShowPrescription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the prescription ID from the query string
                if (Session["PrescriptionId"] != null)
                {
                    int prescriptionId = Convert.ToInt32(Session["PrescriptionId"] as string);

                    // Retrieve the prescription information from the database
                    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    string query = "SELECT Pa.Name AS PatientName, D.Name AS DoctorName, M.Name AS MedicineName, P.Dosage, P.Instructions " +
               "FROM Prescription P " +
               "INNER JOIN Patients Pa ON P.PatientId = Pa.PatientId " +
               "INNER JOIN Doctors D ON P.DoctorId = D.DoctorId " +
               "INNER JOIN Medicine M ON P.MedicineId = M.MedicineId " +
               "WHERE P.PrescriptionId = @PrescriptionId";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PrescriptionId", prescriptionId);

                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.Read())
                            {
                                // Populate the labels with the prescription information
                                lblPatientName.Text = reader["PatientName"].ToString();
                                lblDoctorName.Text = reader["DoctorName"].ToString();
                                lblMedicineName.Text = reader["MedicineName"].ToString();
                                lblDosageValue.Text = reader["Dosage"].ToString();
                                lblInstructionsValue.Text = reader["Instructions"].ToString();
                            }

                            reader.Close();
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}