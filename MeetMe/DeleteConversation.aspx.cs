using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class DeleteConversation : System.Web.UI.Page
{

    protected string UID = "";
    protected string FID = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        UID = Request.QueryString["id"].ToString();
        FID = Request.QueryString["fid"].ToString();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        conn.Open();
        string deleteConv = "delete from Mesaje where userId='" + UID + "' and fId='"+FID+"'";
        SqlCommand com1 = new SqlCommand(deleteConv, conn);
        com1.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("Chat.aspx");
    }
}