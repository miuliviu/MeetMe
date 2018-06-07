using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class AcceptDate : System.Web.UI.Page
{
    protected string UID = "";
    protected string FID = "";
    protected string DateID = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        UID = Request.QueryString["id"].ToString();
        FID = Request.QueryString["fid"].ToString();
        DateID = Request.QueryString["dId"].ToString();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaFriend = new SqlDataAdapter("select * from DateRequests where DateRequestID='" + DateID + "'", conn);
        DataTable dtFR = new DataTable();
        sdaFriend.Fill(dtFR);

        conn.Open();
        string addDate = "insert into Dates(dlocation, dtime, ddate, ddescription, userId, fId) values (@loc, @time, @date, @desc, @uid, @fid)";
        SqlCommand com = new SqlCommand(addDate, conn);
        com.Parameters.AddWithValue("@loc", dtFR.Rows[0]["location"].ToString());
        com.Parameters.AddWithValue("@time", dtFR.Rows[0]["time"].ToString());
        com.Parameters.AddWithValue("@date", dtFR.Rows[0]["date"].ToString());
        com.Parameters.AddWithValue("@desc", dtFR.Rows[0]["description"].ToString());
        com.Parameters.AddWithValue("@uid", dtFR.Rows[0]["userId"].ToString());
        com.Parameters.AddWithValue("@fid", dtFR.Rows[0]["fId"].ToString());
        com.ExecuteNonQuery();

        /*string addDate1 = "insert into Dates(dlocation, dtime, ddate, ddescription, userId, fId) values (@loc, @time, @date, @desc, @uid, @fid)";
        SqlCommand com2 = new SqlCommand(addDate1, conn);
        com2.Parameters.AddWithValue("@loc", dtFR.Rows[0]["location"].ToString());
        com2.Parameters.AddWithValue("@time", dtFR.Rows[0]["time"].ToString());
        com2.Parameters.AddWithValue("@date", dtFR.Rows[0]["date"].ToString());
        com2.Parameters.AddWithValue("@desc", dtFR.Rows[0]["description"].ToString());
        com2.Parameters.AddWithValue("@uid", dtFR.Rows[0]["fId"].ToString());
        com2.Parameters.AddWithValue("@fid", dtFR.Rows[0]["userId"].ToString());
        com2.ExecuteNonQuery();*/



        string deleteDateRequest = "delete from DateRequests where DateRequestID='"+ DateID +"'";
        SqlCommand com1 = new SqlCommand(deleteDateRequest, conn);
        com1.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("DateRequests.aspx");
    }
}