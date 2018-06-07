using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using TableDependency.SqlClient;
using TableDependency.EventArgs;
using TableDependency.Enums;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class UserProfile : System.Web.UI.Page
{
    protected string profilePictureName = "";
    protected string userID = "";
    protected string city = "";
    protected string description = "";
    protected string relationStatus = "";
    protected string age = "";
    protected string privacy = "";
    protected string interested = "";
    protected string hobby = "";
    protected string country = "";
    protected string gender = "";


    protected void Page_Load(object sender, EventArgs e)
    {


        FileUploadChangeProfilePicture.Attributes["onchange"] = "goToDB(this)";
        if (Session["New"] != null)
        {
            //User Data
            string userEmail = Session["New"].ToString();
            string fname = "";
            string lname = "";
            

            //Retrieve User data
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
            SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where email='" + userEmail + "'", conn);
            DataTable dt = new DataTable();
            sdaUserData.Fill(dt);

            //Saving the User Data
            userID = dt.Rows[0]["UserID"].ToString();
            fname = dt.Rows[0]["fname"].ToString();
            lname = dt.Rows[0]["sname"].ToString();
            country = dt.Rows[0]["country"].ToString();

            //Retrieve User data
            SqlDataAdapter sdaProfilePicture = new SqlDataAdapter("select * from ProfilePictures where userId='" + userID + "'", conn);
            DataTable dt1 = new DataTable();
            sdaProfilePicture.Fill(dt1);

            //Saving the User Data
            if (dt1.Rows.Count != 0)
                profilePictureName = dt1.Rows[0]["imageName"].ToString();
            else
                profilePictureName = "defaultProfilePicture.png";

            //Displaying the Profile Picture
            ImageProfilePicture.ImageUrl = "ProfilePictures/" + profilePictureName;

            //Retrieving Media Images
            SqlDataAdapter sdaGalleryIMG = new SqlDataAdapter("select * from Media where userId='" + userID + "'", conn);
            DataTable dtGIMG = new DataTable();
            sdaGalleryIMG.Fill(dtGIMG);
            if (dtGIMG.Rows.Count != 0)
                ImageButtonGallery.ImageUrl = "Media/" + dtGIMG.Rows[dtGIMG.Rows.Count - 1]["imgName"];
            else
                ImageButtonGallery.ImageUrl = "Media/defaultGallery.png";


            //Retrieve Data from ProfileDetails
            SqlDataAdapter sdaProfileDetailsExtracts = new SqlDataAdapter("select * from ProfileDetails where userId='" + userID + "'", conn);
            DataTable dt2 = new DataTable();
            sdaProfileDetailsExtracts.Fill(dt2);

            //Saving Profile Details
            city = dt2.Rows[0]["city"].ToString();
            description = dt2.Rows[0]["description"].ToString();
            relationStatus = dt2.Rows[0]["rstatus"].ToString();

            privacy = dt2.Rows[0]["privacy"].ToString();
            age = dt2.Rows[0]["age"].ToString();
            hobby = dt2.Rows[0]["hobby"].ToString();
            interested = dt2.Rows[0]["interested"].ToString();
            gender = dt2.Rows[0]["gender"].ToString();


            //Welcome the user
            welcomeText.InnerText = "Hi " + fname + " " + lname;

            //Generate Suggestions
            SqlDataAdapter takeUsers = new SqlDataAdapter("select * from Users where not UserID='" + userID + "'", conn);
            DataTable dtAllUsers = new DataTable();
            takeUsers.Fill(dtAllUsers);

            SqlDataAdapter takeUserFriends = new SqlDataAdapter("select * from Friends where userId='" + userID + "'", conn);
            DataTable dtUserFriends = new DataTable();
            takeUserFriends.Fill(dtUserFriends);

            ArrayList allFriends = new ArrayList();

            foreach (DataRow userFriend in dtUserFriends.Rows)
                allFriends.Add(userFriend["fuserID"].ToString());

            int ct = 0;

            foreach (DataRow userToAdd in dtAllUsers.Rows)
            {
                SqlDataAdapter checkForRequestSuggestion = new SqlDataAdapter("select * from FriendRequests where userId='" + userToAdd["UserID"].ToString() + "' and requesterUserId='" + userID + "'", conn);
                DataTable dtcheckForRequestSuggestion = new DataTable();
                checkForRequestSuggestion.Fill(dtcheckForRequestSuggestion);

                if (dtcheckForRequestSuggestion.Rows.Count == 0)
                {
                    if (!allFriends.Contains(userToAdd["UserID"].ToString()))
                    {
                        HtmlGenericControl divtoAdd = new HtmlGenericControl("div");
                        divtoAdd.Attributes.Add("class", "userBlock");

                        //Retrieve profile picture
                        SqlDataAdapter userAddProfiProfileDetails = new SqlDataAdapter("select * from ProfileDetails where userId='" + userToAdd["UserID"].ToString() + "'", conn);
                        DataTable dtAddProfileDetails = new DataTable();
                        userAddProfiProfileDetails.Fill(dtAddProfileDetails);

                        //Add profile Picture
                        SqlDataAdapter userAddProfiPic = new SqlDataAdapter("select * from ProfilePictures where userId='" + userToAdd["UserID"].ToString() + "'", conn);
                        DataTable dtAddProfPic = new DataTable();
                        userAddProfiPic.Fill(dtAddProfPic);

                        //Create image
                        Image imgToAdd = new Image();
                        imgToAdd.ID = "Image" + userToAdd["UserID"].ToString();
                        if (dtAddProfPic.Rows.Count != 0)
                        {
                            if (dtAddProfileDetails.Rows[0]["privacy"].ToString().Trim() == "Public")
                                imgToAdd.ImageUrl = "~/ProfilePictures/" + dtAddProfPic.Rows[0]["imageName"];
                            else
                                imgToAdd.ImageUrl = "~/ProfilePictures/defaultProfilePicture.png";
                        }
                        else
                            imgToAdd.ImageUrl = "~/ProfilePictures/defaultProfilePicture.png";
                        imgToAdd.AlternateText = "imageID" + userToAdd["UserID"].ToString();
                        imgToAdd.CssClass = "imgProfileSuggested";
                        HtmlGenericControl linkPic = new HtmlGenericControl("a");
                        linkPic.Attributes.Add("class", "linkPictureSuggested");
                        linkPic.Attributes.Add("href", "ViewProfile.aspx?id=" + userToAdd["UserID"].ToString());
                        linkPic.Controls.Add(imgToAdd);

                        //Create name with link
                        HtmlGenericControl linkNameSuggested = new HtmlGenericControl("a");
                        linkNameSuggested.Attributes.Add("class", "linkNameSuggestion");
                        linkNameSuggested.Attributes.Add("href", "ViewProfile.aspx?id=" + userToAdd["UserID"].ToString());
                        linkNameSuggested.InnerText = userToAdd["fname"].ToString() + " " + userToAdd["sname"].ToString();

                        //Check for his request
                        SqlDataAdapter checkForRequestSuggestion1 = new SqlDataAdapter("select * from FriendRequests where userId='" + userID + "' and requesterUserId='" + userToAdd["UserID"].ToString() + "'", conn);
                        DataTable dtcheckForRequestSuggestion1 = new DataTable();
                        checkForRequestSuggestion1.Fill(dtcheckForRequestSuggestion1);

                        HtmlGenericControl buttonAreaSuggestion = new HtmlGenericControl("div");
                        buttonAreaSuggestion.Attributes.Add("class", "buttonAreaStyle");

                        // Creating buttons
                        if (dtcheckForRequestSuggestion1.Rows.Count == 0)
                        {
                            Button AddNewFriendButton = new Button();
                            AddNewFriendButton.ID = "ButtonAddNewSuggestion" + userToAdd["UserID"].ToString();
                            AddNewFriendButton.Text = "Add Friend";
                            AddNewFriendButton.CssClass = "buttonFashionAddNewSuggestion";
                            AddNewFriendButton.PostBackUrl = "SendFriendRequest.aspx?id=" + userToAdd["UserID"].ToString() + "&suggUser=yes";
                            buttonAreaSuggestion.Controls.Add(AddNewFriendButton);
                        }
                        else
                        {
                            Button AcceptFriendButton = new Button();
                            AcceptFriendButton.ID = "AcceptFriendSuggestion" + userToAdd["UserID"].ToString();
                            AcceptFriendButton.Text = "Accept Request";
                            AcceptFriendButton.CssClass = "buttonFashionAcceptFriendSuggestion";
                            AcceptFriendButton.PostBackUrl = "AddFriend.aspx?fid=" + userToAdd["UserID"].ToString() + "&id=" + userID+ "&suggUser=yes";
                            buttonAreaSuggestion.Controls.Add(AcceptFriendButton);
                        }
                        //Add all to div
                        divtoAdd.Controls.Add(linkPic);
                        divtoAdd.Controls.Add(linkNameSuggested);
                        divtoAdd.Controls.Add(buttonAreaSuggestion);

                        suggestionDiv.Controls.Add(divtoAdd);

                        ct++;
                    }
                }

                if (ct == 3)
                    break;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void TableDep_OnError(object sender, TableDependency.EventArgs.ErrorEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void TableDep_OnChanged(object sender, TableDependency.EventArgs.RecordChangedEventArgs<Media> e)
    {
        throw new NotImplementedException();
    }

    protected void ButtonSendPictureToDB_Click(object sender, EventArgs e)
    {

        //Deleting the actual profile picture
        FileInfo file = new FileInfo(Server.MapPath("~/ProfilePictures/" + Path.GetFileName(profilePictureName)));
        if (file.Exists && profilePictureName!= "defaultProfilePicture.png")//check file exsit or not
        {
            file.Delete();
        }

        //Saving the New Profile Picture
        FileUploadChangeProfilePicture.SaveAs(Server.MapPath("~/ProfilePictures/" + Path.GetFileName(FileUploadChangeProfilePicture.FileName)));


        
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        conn.Open();

        //Delete old Picture from DB
        if (profilePictureName != "defaultProfilePicture.png")
        {
            string oldImageDB = "delete from ProfilePictures where imageName like '" + profilePictureName + "'";
            SqlCommand com1 = new SqlCommand(oldImageDB, conn);
            com1.ExecuteNonQuery();
        }

        

        //Insert in the DB
        string imageDB = "insert into ProfilePictures(imageName, userId) values (@picName, @uID)";
        SqlCommand com = new SqlCommand(imageDB, conn);
        com.Parameters.AddWithValue("@picName", Path.GetFileName(FileUploadChangeProfilePicture.FileName).ToString());
        com.Parameters.AddWithValue("@uID", userID);
        com.ExecuteNonQuery();
        conn.Close();

        //Changing the Profile Picture with the new one
        ImageProfilePicture.ImageUrl = "ProfilePictures/" + Path.GetFileName(FileUploadChangeProfilePicture.FileName);
        
        

    }

    protected void ButtonEditDetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditDetails.aspx");
    }

    protected void ButtonFriends_Click(object sender, EventArgs e)
    {
        Response.Redirect("Friends.aspx");
    }

    protected void ButtonGallery_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserGallery.aspx");
    }

    protected void ImageButtonGallery_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("UserGallery.aspx");
    }

    protected void ButtonDates_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dates.aspx");
    }
}