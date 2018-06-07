using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class DeleteDate : System.Web.UI.Page
{
    protected string DateID = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        DateID = Request.QueryString["dId"].ToString();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        conn.Open();
        string deleteDateRequest = "delete from Dates where DateID='" + DateID + "'";
        SqlCommand com1 = new SqlCommand(deleteDateRequest, conn);
        com1.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("Dates.aspx");
    }
}