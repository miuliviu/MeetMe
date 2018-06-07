using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class DateInvite : System.Web.UI.Page
{

    protected string FID = "";
    protected string UID = "";
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

        SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where email='" + Session["New"].ToString() + "'", conn);
        DataTable dtU = new DataTable();
        sdaUserData.Fill(dtU);

        UID = dtU.Rows[0]["UserID"].ToString();
        TextBoxDescription.Attributes.Add("maxlength", TextBoxDescription.MaxLength.ToString());
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewProfile.aspx?id="+ FID);
    }

    protected void ButtonSendInvitation_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        conn.Open();
        string insertMsg = "insert into DateRequests(userId, fid, location, time, date, description, seen) values (@uid, @fid, @loc, @timp, @data, @desc, @sn)";
        SqlCommand com = new SqlCommand(insertMsg, conn);
        com.Parameters.AddWithValue("@uid", UID);
        com.Parameters.AddWithValue("@fid", FID);
        com.Parameters.AddWithValue("@loc", TextBoxLocation.Text.ToString());
        com.Parameters.AddWithValue("@timp", TextBoxTime.Text.ToString());
        com.Parameters.AddWithValue("@data", TextBoxDate.Text.ToString());
        com.Parameters.AddWithValue("@desc", TextBoxDescription.Text.ToString());
        com.Parameters.AddWithValue("@sn", "no");
        com.ExecuteNonQuery();

        Response.Redirect("DateInvitationSentConfirmation.aspx?fid="+FID);
    }
}