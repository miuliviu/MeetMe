using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class EditDetails : System.Web.UI.Page
{
    protected string city = "";
    protected string description = "";
    protected string relationStatus = "";
    protected string age = "";
    protected string interested = "";
    protected string hobby = "";
    protected string country = "";
    protected string gender = "";
    protected string userID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["New"] != null)
        {
            string userEmail = Session["New"].ToString();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
            SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where email='" + userEmail + "'", conn);
            DataTable dt = new DataTable();
            sdaUserData.Fill(dt);

            userID = dt.Rows[0]["UserID"].ToString();

            SqlDataAdapter sdaUserProfileDetails = new SqlDataAdapter("select * from ProfileDetails where userId='" + userID + "'", conn);
            DataTable dt2 = new DataTable();
            sdaUserProfileDetails.Fill(dt2);


            gender = dt2.Rows[0]["gender"].ToString();
            city = dt2.Rows[0]["city"].ToString();
            description = dt2.Rows[0]["description"].ToString();
            relationStatus = dt2.Rows[0]["rstatus"].ToString();
            age = dt2.Rows[0]["age"].ToString();
            hobby = dt2.Rows[0]["hobby"].ToString();
            interested = dt2.Rows[0]["interested"].ToString();

            if (!IsPostBack)
            {
                TextBoxDescription.Text = description;
                TextBoxHobby.Text = hobby;
                TextBoxLocation.Text = city;
                TextBoxAge.Text = age;

                DropDownListGender.SelectedValue = gender.Replace(" ", "");
                DropDownListInterested.SelectedValue = interested.Replace(" ", "");
                if (relationStatus[0] == 'I')
                    DropDownListRelationship.SelectedValue = "In a relationship";
                else
                    DropDownListRelationship.SelectedValue = "Single";
            }

        }
        else {
            Response.Redirect("Login.aspx");
        }
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserProfile.aspx");
    }

    protected void ButtonSaveChanges_Click(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        try
        {

            SqlDataAdapter sdaUserData = new SqlDataAdapter("update ProfileDetails set " +
                "description='" + TextBoxDescription.Text.Replace("'", "''") + "', " +
                "rstatus='" + DropDownListRelationship.SelectedValue + "', " +
                "gender='" + DropDownListGender.Text + "', " +
                "city='" + TextBoxLocation.Text.Replace("'", "''") + "', " +
                "interested='" + DropDownListInterested.SelectedValue + "', " +
                "age='" + TextBoxAge.Text + "', " +
                "hobby='" + TextBoxHobby.Text.Replace("'", "''") + "' where userId='" + userID + "'", conn);
            DataTable dt = new DataTable();
            sdaUserData.Fill(dt);
        }
        catch (Exception err)
        {
            Console.WriteLine("Exception: " + err.ToString());
        }

        Response.Redirect("UserProfile.aspx");
    }
}