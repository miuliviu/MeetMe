using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class DateInvitationSentConfirmation : System.Web.UI.Page
{
    protected string FID = "";
    protected string friendName = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        FID = Request.QueryString["fid"].ToString();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaFRUserData = new SqlDataAdapter("select * from Users where UserID='" + FID + "'", conn);
        DataTable dtFR = new DataTable();
        sdaFRUserData.Fill(dtFR);

        friendName = dtFR.Rows[0]["fname"] + " " + dtFR.Rows[0]["sname"];

        LabelInvitationSent.InnerText = "Date Invitation sent to "+friendName+".";

        if (dtFR.Rows[0]["gender"].ToString().Trim() == "Male")
            ButtonBack.Text = "Return to his profile";
        else
            ButtonBack.Text = "Return to her profile";
    }

    protected void ButtonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewProfile.aspx?id="+FID);
    }
}