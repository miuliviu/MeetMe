using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class AccountSettings : System.Web.UI.Page
{

    protected string UserID = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where email='" + Session["New"].ToString() + "'", conn);
        DataTable dtUData = new DataTable();
        sdaUserData.Fill(dtUData);

        UserID = dtUData.Rows[0]["UserID"].ToString();

        SqlDataAdapter sdaUserOptions = new SqlDataAdapter("select * from ProfileDetails where userId='" + UserID + "'", conn);
        DataTable dtUOPT = new DataTable();
        sdaUserOptions.Fill(dtUOPT);

        SqlDataAdapter sdaCheckRat= new SqlDataAdapter("select * from Rating where userId='" + UserID + "'", conn);
        DataTable dtCrat = new DataTable();
        sdaCheckRat.Fill(dtCrat);

        SqlDataAdapter GetAllRating = new SqlDataAdapter("select * from Rating", conn);
        DataTable dtRating = new DataTable();
        GetAllRating.Fill(dtRating);

        float averageRating=0;
        int sum = 0;

        foreach (DataRow row in dtRating.Rows)
        {
            sum += Int32.Parse(row["stars"].ToString());
        }

        averageRating = (float)sum / dtRating.Rows.Count;
        averageStarLeft.InnerHtml = averageRating.ToString("n1")+" / 5 ☆";

        if (dtCrat.Rows.Count != 0)
        {
            ratingStarsDiv.Visible = false;
            ratingLeftText.InnerHtml = "<b>Meet Me</b> Current Average User Rating";
        }
        else
            ratingAveDiv.Visible = false;

        if (!IsPostBack)
        {
            if (dtUOPT.Rows[0]["privacy"].ToString().Trim() == "Private")
                DropDownListPrivacyOption.Text = "Private";

            if (dtUOPT.Rows[0]["chat"].ToString().Trim() == "Off")
                DropDownListChatOption.Text = "Off";
        }
    }

    protected void ButtonApply_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        conn.Open();
        string updateOptions = "update ProfileDetails set privacy='"+DropDownListPrivacyOption.SelectedItem.ToString()+"', chat='"+DropDownListChatOption.SelectedItem.ToString()+"' where userId='" + UserID + "'";
        SqlCommand com = new SqlCommand(updateOptions, conn);
        com.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("UserProfile.aspx");
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserProfile.aspx");
    }

    protected void ButtonDeleteAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeleteAccount.aspx");
    }
}