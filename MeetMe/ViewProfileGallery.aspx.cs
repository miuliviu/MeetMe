using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class ViewProfileGallery : System.Web.UI.Page
{
    protected string userViewID = "";
    protected string userName = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        userViewID = Request.QueryString["id"].ToString();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaUsermedia = new SqlDataAdapter("select * from Media where userId='" + userViewID + "'", conn);
        DataTable dtm = new DataTable();
        sdaUsermedia.Fill(dtm);

        SqlDataAdapter sdaUserName = new SqlDataAdapter("select * from Users where userId='" + userViewID + "'", conn);
        DataTable dtUName = new DataTable();
        sdaUserName.Fill(dtUName);

        userName = dtUName.Rows[0]["fname"].ToString();

        if (dtm.Rows.Count == 0)
        {
            noPictureText.Visible = true;
        }

        foreach (DataRow rowMedia in dtm.Rows)
        {
            //Creating the div control
            HtmlGenericControl divToAdd = new HtmlGenericControl("div");
            divToAdd.Attributes.Add("class", "imageBlock");

            //Creating the Image
            Image imgToAdd = new Image();
            imgToAdd.ID = "Image" + rowMedia["MediaID"];
            imgToAdd.ImageUrl = "~/Media/" + rowMedia["imgName"];
            imgToAdd.AlternateText = "imageID" + rowMedia["MediaID"];
            imgToAdd.CssClass = "imgGallery";

            //Add to div
            divToAdd.Controls.Add(imgToAdd);

            galleryContent.Controls.Add(divToAdd);
        }
    }
}