using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace HospitalManagement
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["DoctorEmail"] as string))
            {
                Response.Redirect("~/Doctor-Management.aspx");
            }
            else if (!string.IsNullOrEmpty(Session["PatientEmail"] as string))
            {
                Response.Redirect("~/Patient-Management.aspx");
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}