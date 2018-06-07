using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class RateMeetMe : System.Web.UI.Page
{

    protected string UserID = "";
    protected string Stars = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        UserID = Request.QueryString["uid"].ToString();
        Stars = Request.QueryString["stars"].ToString();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);

        SqlDataAdapter sdaRating = new SqlDataAdapter("select * from Rating where userId='" + UserID + "'", conn);
        DataTable dtUrat = new DataTable();
        sdaRating.Fill(dtUrat);

        if (dtUrat.Rows.Count == 0)
        {
            conn.Open();
            string addRating = "insert into Rating(userId, stars) values (@UID, @Star)";
            SqlCommand com = new SqlCommand(addRating, conn);
            com.Parameters.AddWithValue("@UID", UserID);
            com.Parameters.AddWithValue("@Star", Stars);
            com.ExecuteNonQuery();
            conn.Close();
        }

        Response.Redirect("AccountSettings.aspx");

    }
}