using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class RemoveFriend : System.Web.UI.Page
{

    protected string friendToDelete = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        friendToDelete = Request.QueryString["id"].ToString();

        //Extract data from Users
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where email='" + Session["New"] + "'", conn);
        DataTable dt = new DataTable();
        sdaUserData.Fill(dt);
        string userID = dt.Rows[0]["UserID"].ToString();

        //Delete Friend in DB
        conn.Open();
        string RemoveFriend = "delete from Friends where (fuserID='" + friendToDelete + "' and userId='"+userID+ "') or (fuserID='" + userID + "' and userId='" + friendToDelete + "')";
        SqlCommand com1 = new SqlCommand(RemoveFriend, conn);
        com1.ExecuteNonQuery();
        conn.Close();

        if (Request.QueryString["profileFriends"] != null)
        {
            Response.Redirect("Friends.aspx");
        }

        if (Request.QueryString["userView"] != null)
        {
            Response.Redirect("ViewProfile.aspx?id=" + friendToDelete);
        }
        Response.Redirect("FriendRequests.aspx?id=" + friendToDelete);
    }
}