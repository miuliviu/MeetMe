using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

public partial class DeleteMedia : System.Web.UI.Page
{
    protected string pictureID = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        pictureID = Request.QueryString["picid"].ToString();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Media where MediaID='" + pictureID + "'", conn);
        DataTable dt = new DataTable();
        sdaUserData.Fill(dt);

        //Deleting the actual profile picture
        FileInfo file = new FileInfo(Server.MapPath("~/Media/" + Path.GetFileName(dt.Rows[0]["imgName"].ToString())));
        if (file.Exists)//check file exsit or not
        {
            file.Delete();
        }

        //Delete old Picture from DB
        conn.Open();
        string deleteImageDB = "delete from Media where MediaID='" + pictureID + "'";
        SqlCommand com1 = new SqlCommand(deleteImageDB, conn);
        com1.ExecuteNonQuery();

        //Go back to Gallery
        Response.Redirect("UserGallery.aspx");
        
    }
}