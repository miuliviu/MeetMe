using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class AddFriend : System.Web.UI.Page
{

    protected string UserID = "";
    protected string friendUserID = "";
    protected string searchCat = "";
    protected string searchTxt = "";


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        UserID = Request.QueryString["id"].ToString();
        friendUserID = Request.QueryString["fid"].ToString();
        if (Request.QueryString["search"] != null)
        {
            searchCat = Request.QueryString["cat"].ToString();
            searchTxt = Request.QueryString["search"].ToString();
        }

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);

        SqlDataAdapter takeUserID = new SqlDataAdapter("select * from Users where UserID='" + UserID + "'", conn);
        DataTable dtUserID = new DataTable();
        takeUserID.Fill(dtUserID);

        SqlDataAdapter takeFriendUserID = new SqlDataAdapter("select * from Users where UserID='" + friendUserID + "'", conn);
        DataTable dtFriendUserID = new DataTable();
        takeFriendUserID.Fill(dtFriendUserID);

        //Insert Friend Request in DB
        conn.Open();
        string freqDB1 = "insert into Friends(friendFname, friendLname, fuserID, userId) values (@rfname, @rlname, @fuserid, @userID)";
        SqlCommand com = new SqlCommand(freqDB1, conn);
        com.Parameters.AddWithValue("@rfname", dtFriendUserID.Rows[0]["fname"]);
        com.Parameters.AddWithValue("@rlname", dtFriendUserID.Rows[0]["sname"]);
        com.Parameters.AddWithValue("@fuserid", friendUserID);
        com.Parameters.AddWithValue("@userID", UserID);
        com.ExecuteNonQuery();

        string freqDB2 = "insert into Friends(friendFname, friendLname, fuserID, userId) values (@rfname, @rlname, @fuserid, @userID)";
        SqlCommand com1 = new SqlCommand(freqDB2, conn);
        com1.Parameters.AddWithValue("@rfname", dtUserID.Rows[0]["fname"]);
        com1.Parameters.AddWithValue("@rlname", dtUserID.Rows[0]["sname"]);
        com1.Parameters.AddWithValue("@fuserid", UserID);
        com1.Parameters.AddWithValue("@userID", friendUserID);
        com1.ExecuteNonQuery();

        //Check if there is a keyword entry and add if there is non
        SqlDataAdapter checkForKey = new SqlDataAdapter("select * from Keywords where userId='" + UserID + "' and fId='"+ friendUserID +"'", conn);
        DataTable dtcheckkey = new DataTable();
        checkForKey.Fill(dtcheckkey);

        if (dtcheckkey.Rows.Count == 0)
        {
            //Create Keyword Entry in DB
            string keyDB1 = "insert into Keywords(userId, fId, hello, day, hobby, living, music, movies, games, food, pictures, date) values" +
                " (@UID, @FID, @hi, @dai, @hob, @liv, @mus, @mov, @gam, @foo, @pic, @dat)";
            SqlCommand comKey1 = new SqlCommand(keyDB1, conn);
            comKey1.Parameters.AddWithValue("@UID", UserID);
            comKey1.Parameters.AddWithValue("@FID", friendUserID);
            comKey1.Parameters.AddWithValue("@hi", "no");
            comKey1.Parameters.AddWithValue("@dai", "no");
            comKey1.Parameters.AddWithValue("@hob", "no");
            comKey1.Parameters.AddWithValue("@liv", "no");
            comKey1.Parameters.AddWithValue("@mus", "no");
            comKey1.Parameters.AddWithValue("@mov", "no");
            comKey1.Parameters.AddWithValue("@gam", "no");
            comKey1.Parameters.AddWithValue("@foo", "no");
            comKey1.Parameters.AddWithValue("@pic", "no");
            comKey1.Parameters.AddWithValue("@dat", "no");
            comKey1.ExecuteNonQuery();

            string keyDB2 = "insert into Keywords(userId, fId, hello, day, hobby, living, music, movies, games, food, pictures, date) values" +
                " (@UID1, @FID1, @hi1, @dai1, @hob1, @liv1, @mus1, @mov1, @gam1, @foo1, @pic1, @dat1)";
            SqlCommand comKey2 = new SqlCommand(keyDB2, conn);
            comKey2.Parameters.AddWithValue("@UID1", friendUserID);
            comKey2.Parameters.AddWithValue("@FID1", UserID);
            comKey2.Parameters.AddWithValue("@hi1", "no");
            comKey2.Parameters.AddWithValue("@dai1", "no");
            comKey2.Parameters.AddWithValue("@hob1", "no");
            comKey2.Parameters.AddWithValue("@liv1", "no");
            comKey2.Parameters.AddWithValue("@mus1", "no");
            comKey2.Parameters.AddWithValue("@mov1", "no");
            comKey2.Parameters.AddWithValue("@gam1", "no");
            comKey2.Parameters.AddWithValue("@foo1", "no");
            comKey2.Parameters.AddWithValue("@pic1", "no");
            comKey2.Parameters.AddWithValue("@dat1", "no");
            comKey2.ExecuteNonQuery();
        }

        //Delete Friend Request
        string friendReq = "delete from FriendRequests where requesterUserId='" + friendUserID + "' and userId='"+ UserID +"'";
        SqlCommand com2 = new SqlCommand(friendReq, conn);
        com2.ExecuteNonQuery();

        conn.Close();

        if (Request.QueryString["suggViewUser"] != null)
            Response.Redirect("ViewProfile.aspx?id="+ Request.QueryString["suggViewUser"].ToString());

        if (Request.QueryString["suggUser"] != null)
            Response.Redirect("UserProfile.aspx");

        if (Request.QueryString["search"] != null)
            Response.Redirect("SearchResults.aspx?search=" + searchTxt + "&cat=" + searchCat);

        if (Request.QueryString["userView"] != null)
            Response.Redirect("ViewProfile.aspx?id=" + friendUserID);

        Response.Redirect("FriendRequests.aspx");


    }
}