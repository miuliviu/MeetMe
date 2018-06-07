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

public partial class UserGallery : System.Web.UI.Page
{

    protected string USERID = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        FileUploadPicture.Attributes["onchange"] = "goToDB(this)";
        ButtonUpload.Attributes["onclick"] = "uploadPicture(); return false;";

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where email='" + Session["New"] + "'", conn);
        DataTable dt = new DataTable();
        sdaUserData.Fill(dt);
        USERID = dt.Rows[0]["UserID"].ToString();

        SqlDataAdapter sdaUsermedia = new SqlDataAdapter("select * from Media where userId='" + dt.Rows[0]["UserID"].ToString() + "'", conn);
        DataTable dtm = new DataTable();
        sdaUsermedia.Fill(dtm);

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
            imgToAdd.ImageUrl = "~/Media/"+rowMedia["imgName"];
            imgToAdd.AlternateText = "imageID"+ rowMedia["MediaID"];
            imgToAdd.CssClass = "imgGallery";

            //Creating the delete button
            Button deleteButton = new Button();
            deleteButton.ID = "ButtonDelete" + rowMedia["MediaID"];
            deleteButton.Text = "Delete";
            deleteButton.CssClass = "buttonFashionDelete";
            deleteButton.PostBackUrl = "DeleteMedia.aspx?picid="+ rowMedia["MediaID"];


            //Add to div
            divToAdd.Controls.Add(imgToAdd);
            divToAdd.Controls.Add(deleteButton);

            galleryContent.Controls.Add(divToAdd);
        }
    }

    protected void ButtonSendTheData_Click(object sender, EventArgs e)
    {

        FileInfo file = new FileInfo(Server.MapPath("~/Media/" + Path.GetFileName(FileUploadPicture.FileName)));
        if (file.Exists)
        {
            int ct = 1;
            while (true)
            {
                FileInfo file1 = new FileInfo(Server.MapPath("~/Media/("+ct+")" + Path.GetFileName(FileUploadPicture.FileName)));
                if (file1.Exists)
                {
                    ct++;
                }
                else
                {
                    FileUploadPicture.SaveAs(Server.MapPath("~/Media/(" + ct + ")" + Path.GetFileName(FileUploadPicture.FileName)));
                    break;
                }
            }

            //Insert in the DB
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
            conn.Open();
            string imageDB = "insert into Media(imgName, userId) values (@picName, @uID)";
            SqlCommand com = new SqlCommand(imageDB, conn);
            com.Parameters.AddWithValue("@picName", "("+ct+")"+Path.GetFileName(FileUploadPicture.FileName).ToString());
            com.Parameters.AddWithValue("@uID", USERID);
            com.ExecuteNonQuery();
            conn.Close();
        }
        else
        {
            FileUploadPicture.SaveAs(Server.MapPath("~/Media/" + Path.GetFileName(FileUploadPicture.FileName)));

            //Insert in the DB
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
            conn.Open();
            string imageDB = "insert into Media(imgName, userId) values (@picName, @uID)";
            SqlCommand com = new SqlCommand(imageDB, conn);
            com.Parameters.AddWithValue("@picName", Path.GetFileName(FileUploadPicture.FileName).ToString());
            com.Parameters.AddWithValue("@uID", USERID);
            com.ExecuteNonQuery();
            conn.Close();
        }

        //Reloading the page
        Response.Redirect("UserGallery.aspx");
    }
}