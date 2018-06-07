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
using System.Collections;

public partial class Dates : System.Web.UI.Page
{
    protected string UID = "";
    protected string RRUID = "";
    protected string RRFID = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where email='" + Session["New"].ToString() + "'", conn);
        DataTable dt = new DataTable();
        sdaUserData.Fill(dt);

        UID = dt.Rows[0]["UserID"].ToString();

        SqlDataAdapter sdaDates = new SqlDataAdapter("select * from Dates where userId='" + UID + "' or fId='"+UID+"'", conn);
        DataTable dtDR = new DataTable();
        sdaDates.Fill(dtDR);
        if (dtDR.Rows.Count > 0)
        {
            foreach (DataRow RowResult in dtDR.Rows)
            {
                if (RowResult["fId"].ToString() == UID)
                {
                    RRUID = RowResult["fId"].ToString();
                    RRFID = RowResult["userId"].ToString();
                }
                else
                {
                    RRFID = RowResult["fId"].ToString();
                    RRUID = RowResult["userId"].ToString();
                }
                SqlDataAdapter sdaFriend = new SqlDataAdapter("select * from Users where UserID='" + RRFID + "'", conn);
                DataTable dtFR = new DataTable();
                sdaFriend.Fill(dtFR);

                //Main line div
                HtmlGenericControl divToAdd = new HtmlGenericControl("div");
                divToAdd.Attributes.Add("class", "dateLine");

                //Parts if line div
                HtmlGenericControl divPresent = new HtmlGenericControl("div");
                divPresent.Attributes.Add("class", "presentationDiv");

                HtmlGenericControl divDateDetails = new HtmlGenericControl("div");
                divDateDetails.Attributes.Add("class", "detailsDiv");

                HtmlGenericControl divButtonArea = new HtmlGenericControl("div");
                divButtonArea.Attributes.Add("class", "divButtonArea");


                //Presentation part Controllers
                HtmlGenericControl pName = new HtmlGenericControl("p");
                pName.Attributes.Add("class", "pNameStyle");
                pName.InnerHtml = "You will date with <a href=\"ViewProfile.aspx?id=" + RRFID + "\">" + dtFR.Rows[0]["fname"].ToString() + " " + dtFR.Rows[0]["sname"].ToString() + "</a>";
                //Profile img
                SqlDataAdapter takeProfiPic = new SqlDataAdapter("select * from ProfilePictures where userId='" + RRFID + "'", conn);
                DataTable dtProf = new DataTable();
                takeProfiPic.Fill(dtProf);

                Image imgToAdd = new Image();
                imgToAdd.ID = "Image" + RRFID;
                if (dtProf.Rows.Count != 0)
                    imgToAdd.ImageUrl = "~/ProfilePictures/" + dtProf.Rows[0]["imageName"];
                else
                    imgToAdd.ImageUrl = "~/ProfilePictures/defaultProfilePicture.png";
                imgToAdd.AlternateText = "imageID" + RRFID;
                imgToAdd.CssClass = "imgProfile";
                HtmlGenericControl linkPic = new HtmlGenericControl("a");
                linkPic.Attributes.Add("class", "linkPicture");
                linkPic.Attributes.Add("href", "ViewProfile.aspx?id=" + RRFID);
                linkPic.Controls.Add(imgToAdd);



                //Add to presentation Div
                divPresent.Controls.Add(pName);
                divPresent.Controls.Add(linkPic);


                //Date Deatils part Controllers
                HtmlGenericControl pDateLocation = new HtmlGenericControl("p");
                pDateLocation.Attributes.Add("class", "pDateLocation");
                pDateLocation.InnerHtml = "<b>Location:</b> " + RowResult["dlocation"].ToString();

                HtmlGenericControl pDateTimeDate = new HtmlGenericControl("p");
                pDateTimeDate.Attributes.Add("class", "pDateTimeDate");
                pDateTimeDate.InnerHtml = "<b>Time:</b> " + RowResult["dtime"].ToString() + " <b>Date: </b>" + RowResult["ddate"].ToString();

                HtmlGenericControl pDateDescription = new HtmlGenericControl("p");
                pDateDescription.Attributes.Add("class", "pDateDescription");
                pDateDescription.InnerHtml = "<b>Description:</b> " + RowResult["ddescription"].ToString();

                //Separate sub-div for each p tag
                HtmlGenericControl divDetails1 = new HtmlGenericControl("p");
                divDetails1.Attributes.Add("class", "divDetails1");
                HtmlGenericControl divDetails2 = new HtmlGenericControl("p");
                divDetails2.Attributes.Add("class", "divDetails2");
                HtmlGenericControl divDetails3 = new HtmlGenericControl("p");
                divDetails3.Attributes.Add("class", "divDetails3");

                //Add P tags to sub-divs
                divDetails1.Controls.Add(pDateLocation);
                divDetails2.Controls.Add(pDateTimeDate);
                divDetails3.Controls.Add(pDateDescription);

                //Add sub-divs to Details Div
                divDateDetails.Controls.Add(divDetails1);
                divDateDetails.Controls.Add(divDetails2);
                divDateDetails.Controls.Add(divDetails3);

                //Creating buttons
                Button DeleteDateButton = new Button();
                DeleteDateButton.ID = "ButtonDelete" + RowResult["userId"].ToString();
                DeleteDateButton.Text = "Delete Date";
                DeleteDateButton.CssClass = "buttonFashionDeleteInv";
                DeleteDateButton.PostBackUrl = "DeleteDate.aspx?dId=" + RowResult["DateID"].ToString();


                //Add buttons to Button Area div
                divButtonArea.Controls.Add(DeleteDateButton);

                //ADD ALL PARTS TO LINE
                divToAdd.Controls.Add(divPresent);
                divToAdd.Controls.Add(divDateDetails);
                divToAdd.Controls.Add(divButtonArea);

                //ADD TO PAGE
                allDates.Controls.Add(divToAdd);

            }
        }
        else
        {
            HtmlGenericControl pNoDateRequest = new HtmlGenericControl("p");
            pNoDateRequest.Attributes.Add("class", "noDate");
            pNoDateRequest.InnerText = "You have no Dates to display.";
            allDates.Controls.Add(pNoDateRequest);
        }
    }   

}