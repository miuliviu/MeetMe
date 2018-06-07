using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class SendFriendRequest : System.Web.UI.Page
{
    protected string searchText = "";
    protected string searchCat = "";
    protected string friendReqUserId = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        if (Request.QueryString["search"] != null)
        {
            searchText = Request.QueryString["search"].ToString();
            searchCat = Request.QueryString["cat"].ToString();
        }
        friendReqUserId = Request.QueryString["id"].ToString();
        //Response.Redirect("SearchResults.aspx?search=" + searchText + "&cat=" + searchCat);

        //Extract data from Users
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where email='" + Session["New"] + "'", conn);
        DataTable dt = new DataTable();
        sdaUserData.Fill(dt);

        //Insert Friend Request in DB
        conn.Open();
        string freqDB = "insert into FriendRequests(requestFname, requestLname, requesterUserId, userId, seen) values (@rfname, @rlname, @ruserid, @userID, @sn)";
        SqlCommand com = new SqlCommand(freqDB, conn);
        com.Parameters.AddWithValue("@rfname", dt.Rows[0]["fname"]);
        com.Parameters.AddWithValue("@rlname", dt.Rows[0]["sname"]);
        com.Parameters.AddWithValue("@ruserid", dt.Rows[0]["UserID"]);
        com.Parameters.AddWithValue("@userID", friendReqUserId);
        com.Parameters.AddWithValue("@sn", "no");
        com.ExecuteNonQuery();
        conn.Close();

        if (Request.QueryString["suggViewUser"] != null)
            Response.Redirect("ViewProfile.aspx?id=" + Request.QueryString["suggViewUser"].ToString());
        

        if (Request.QueryString["suggUser"] != null)
            Response.Redirect("UserProfile.aspx");
        

        if (Request.QueryString["userView"] != null)
            Response.Redirect("ViewProfile.aspx?id="+ friendReqUserId);
        
        Response.Redirect("SearchResults.aspx?search=" + searchText + "&cat=" + searchCat);

    }
}