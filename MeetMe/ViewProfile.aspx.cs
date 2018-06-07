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
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class ViewProfile : System.Web.UI.Page
{

    protected string profilePictureName = "";
    protected string city = "";
    protected string description = "";
    protected string relationStatus = "";
    protected string age = "";
    protected string privacy = "";
    protected string interested = "";
    protected string hobby = "";
    protected string country = "";
    protected string gender = "";
    protected string viewUserID = "";
    protected string UserID = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        viewUserID = Request.QueryString["id"].ToString();

        if (Session["New"] != null)
        {
            //User Data
            string userEmail = Session["New"].ToString();
            string fname = "";
            string lname = "";


           //Creating DB Connection
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
            

            //Retrieving Session UserID
            SqlDataAdapter sdaSessionID = new SqlDataAdapter("select * from Users where email='" + userEmail + "'", conn);
            DataTable dtUID = new DataTable();
            sdaSessionID.Fill(dtUID);

            UserID = dtUID.Rows[0]["UserID"].ToString();

            


            //Show friend status
            SqlDataAdapter sdaCheckFriendStatus = new SqlDataAdapter("select * from Friends where fuserID='" + viewUserID + "' and userId='"+UserID+"'", conn);
            DataTable fStatus = new DataTable();
            sdaCheckFriendStatus.Fill(fStatus);
            if (fStatus.Rows.Count != 0)
            {
                friendStatusText.InnerText = "Friend";
            }

            SqlDataAdapter sdaCheckRequest = new SqlDataAdapter("select * from FriendRequests where userId='" + viewUserID + "' and requesterUserId='" + UserID + "'", conn);
            DataTable requestStatus = new DataTable();
            sdaCheckRequest.Fill(requestStatus);
            if (requestStatus.Rows.Count != 0)
            {
                friendStatusText.InnerText = "Friend Request Sent";
                ButtonChat.Visible = false;
                ButtonDate.Visible = false;
            }

            SqlDataAdapter sdaCheckRequest1 = new SqlDataAdapter("select * from FriendRequests where userId='" + UserID + "' and requesterUserId='" + viewUserID + "'", conn);
            DataTable requestStatus1 = new DataTable();
            sdaCheckRequest1.Fill(requestStatus1);
            if (requestStatus1.Rows.Count != 0)
            {
                friendStatusText.InnerText = "Requested to be Friends";
                ButtonChat.Visible = false;
                ButtonDate.Visible = false;
            }

            if (friendStatusText.InnerText == "Friend")
            {
                ButtonAddRemoveFriend.Text = "Remove Friend";
                ButtonAddRemoveFriend.CssClass = "buttonFashionRemoveFriend";
            }
            else
            {
                if (friendStatusText.InnerText == "Not a Friend")
                {
                    ButtonAddRemoveFriend.Text = "Add Friend";
                    ButtonAddRemoveFriend.CssClass = "buttonFashionAddFriend";
                    ButtonChat.Visible = false;
                    ButtonDate.Visible = false;
                }
                else
                {
                    if (friendStatusText.InnerText == "Requested to be Friends")
                    {
                        ButtonAddRemoveFriend.Text = "Accept Friend Request";
                        ButtonAddRemoveFriend.CssClass = "buttonFashionAcceptFriend";
                        ButtonChat.Visible = false;
                        ButtonDate.Visible = false;
                    }
                    else
                        ButtonAddRemoveFriend.Visible = false;
                }
            }

            //Retrieve User data
            SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where UserID='" + viewUserID + "'", conn);
            DataTable dt = new DataTable();
            sdaUserData.Fill(dt);

            //Retrieve User data
            SqlDataAdapter sdaProfilePicture = new SqlDataAdapter("select * from ProfilePictures where userId='" + viewUserID + "'", conn);
            DataTable dt1 = new DataTable();
            sdaProfilePicture.Fill(dt1);

            //Retrieve Data from ProfileDetails
            SqlDataAdapter sdaProfileDetailsExtracts = new SqlDataAdapter("select * from ProfileDetails where userId='" + viewUserID + "'", conn);
            DataTable dt2 = new DataTable();
            sdaProfileDetailsExtracts.Fill(dt2);

            //Retrieving Media Images
            SqlDataAdapter sdaGalleryIMG = new SqlDataAdapter("select * from Media where userId='" + viewUserID + "'", conn);
            DataTable dtGIMG = new DataTable();
            sdaGalleryIMG.Fill(dtGIMG);
            if (dtGIMG.Rows.Count != 0)
            {
                if (dt2.Rows[0]["privacy"].ToString().Trim() == "Public")
                    ImageButtonGallery.ImageUrl = "Media/" + dtGIMG.Rows[dtGIMG.Rows.Count - 1]["imgName"];
                else
                {
                    if(friendStatusText.InnerText == "Friend")
                        ImageButtonGallery.ImageUrl = "Media/" + dtGIMG.Rows[dtGIMG.Rows.Count - 1]["imgName"];
                    else
                        ImageButtonGallery.ImageUrl = "Media/defaultGallery.png";
                }
            }
            else
                ImageButtonGallery.ImageUrl = "Media/defaultGallery.png";

            //Saving the User Data
            fname = dt.Rows[0]["fname"].ToString();
            lname = dt.Rows[0]["sname"].ToString();
            country = dt.Rows[0]["country"].ToString();



            //Saving the User Data
            if (dt1.Rows.Count != 0)
            {
                if (dt2.Rows[0]["privacy"].ToString().Trim() == "Public")
                    profilePictureName = dt1.Rows[0]["imageName"].ToString();
                else
                {
                    if (friendStatusText.InnerText == "Friend")
                        profilePictureName = dt1.Rows[0]["imageName"].ToString();
                    else
                        profilePictureName = "defaultProfilePicture.png";
                }
            }
            else
                profilePictureName = "defaultProfilePicture.png";

            //Displaying the Profile Picture
            ImageProfilePicture.ImageUrl = "ProfilePictures/" + profilePictureName;

            //Display User Name
            nameText.InnerText = fname + " " + lname;

            

            //Saving Profile Details
            city = dt2.Rows[0]["city"].ToString();
            description = dt2.Rows[0]["description"].ToString();
            relationStatus = dt2.Rows[0]["rstatus"].ToString();

            privacy = dt2.Rows[0]["privacy"].ToString();
            age = dt2.Rows[0]["age"].ToString();
            hobby = dt2.Rows[0]["hobby"].ToString();
            interested = dt2.Rows[0]["interested"].ToString();
            gender = dt2.Rows[0]["gender"].ToString();

            if (dt2.Rows[0]["privacy"].ToString().Trim() != "Public")
            {
                if (friendStatusText.InnerText != "Friend")
                {
                    detailsTable.Visible = false;
                    privateText.Visible = true;
                }
            }



            //Generate Suggestions
            SqlDataAdapter takeUsers = new SqlDataAdapter("select * from Users where not UserID='" + UserID + "'", conn);
            DataTable dtAllUsers = new DataTable();
            takeUsers.Fill(dtAllUsers);

            SqlDataAdapter takeUserFriends = new SqlDataAdapter("select * from Friends where userId='" + UserID + "'", conn);
            DataTable dtUserFriends = new DataTable();
            takeUserFriends.Fill(dtUserFriends);

            ArrayList allFriends = new ArrayList();

            foreach (DataRow userFriend in dtUserFriends.Rows)
                allFriends.Add(userFriend["fuserID"].ToString());

            int ct = 0;

            foreach (DataRow userToAdd in dtAllUsers.Rows)
            {
                SqlDataAdapter checkForRequestSuggestion = new SqlDataAdapter("select * from FriendRequests where userId='" + userToAdd["UserID"].ToString() + "' and requesterUserId='" + UserID + "'", conn);
                DataTable dtcheckForRequestSuggestion = new DataTable();
                checkForRequestSuggestion.Fill(dtcheckForRequestSuggestion);

                SqlDataAdapter checkIfFriend = new SqlDataAdapter("select * from Friends where userId='" + userToAdd["UserID"].ToString() + "' and fuserID='" + viewUserID + "'", conn);
                DataTable dtcheckIfFriend = new DataTable();
                checkIfFriend.Fill(dtcheckIfFriend);

                if (dtcheckForRequestSuggestion.Rows.Count == 0 && dtcheckIfFriend.Rows.Count !=0)
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
                        SqlDataAdapter checkForRequestSuggestion1 = new SqlDataAdapter("select * from FriendRequests where userId='" + UserID + "' and requesterUserId='" + userToAdd["UserID"].ToString() + "'", conn);
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
                            AddNewFriendButton.PostBackUrl = "SendFriendRequest.aspx?id=" + userToAdd["UserID"].ToString() + "&suggViewUser=" + viewUserID;
                            buttonAreaSuggestion.Controls.Add(AddNewFriendButton);
                        }
                        else
                        {
                            Button AcceptFriendButton = new Button();
                            AcceptFriendButton.ID = "AcceptFriendSuggestion" + userToAdd["UserID"].ToString();
                            AcceptFriendButton.Text = "Accept Request";
                            AcceptFriendButton.CssClass = "buttonFashionAcceptFriendSuggestion";
                            AcceptFriendButton.PostBackUrl = "AddFriend.aspx?fid=" + userToAdd["UserID"].ToString() + "&id=" + UserID + "&suggViewUser=" + viewUserID;
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

    protected void ButtonAddRemoveFriend_Click(object sender, EventArgs e)
    {
        if (ButtonAddRemoveFriend.Text == "Add Friend")
            Response.Redirect("SendFriendRequest.aspx?id="+ viewUserID + "&userView=yes");
        if(ButtonAddRemoveFriend.Text == "Remove Friend")
            Response.Redirect("RemoveFriend.aspx?id=" + viewUserID + "&userView=yes");
        if (ButtonAddRemoveFriend.Text == "Accept Friend Request")
            Response.Redirect("AddFriend.aspx?fid=" + viewUserID + "&id=" + UserID + "&userView=yes");

    }

    protected void ImageButtonGallery_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ViewProfileGallery.aspx?id=" + viewUserID);
    }

    protected void ButtonChat_Click(object sender, EventArgs e)
    {
        Response.Redirect("Messages.aspx?id=" + UserID + "&fid="+ viewUserID);
    }

    protected void ButtonDate_Click(object sender, EventArgs e)
    {
        Response.Redirect("DateInvite.aspx?fid=" + viewUserID);
    }
}