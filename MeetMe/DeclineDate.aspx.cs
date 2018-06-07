using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class DeclineDate : System.Web.UI.Page
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
        conn.Open();
        string deleteDateRequest = "delete from DateRequests where DateRequestID='" + DateID + "'";
        SqlCommand com1 = new SqlCommand(deleteDateRequest, conn);
        com1.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("DateRequests.aspx");
    }
}