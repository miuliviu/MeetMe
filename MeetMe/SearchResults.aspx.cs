using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class SearchResults : System.Web.UI.Page
{

    protected string searchText = "";
    protected string searchCat = "";
    protected string UserIDSession = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");


        searchText = Request.QueryString["search"].ToString();
        searchCat = Request.QueryString["cat"].ToString();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);

        SqlDataAdapter takeUserID = new SqlDataAdapter("select * from Users where email='" + Session["New"] + "'", conn);
        DataTable dtUserID = new DataTable();
        takeUserID.Fill(dtUserID);

        UserIDSession = dtUserID.Rows[0]["UserID"].ToString();

        if (!IsPostBack)
        {
            //Generating data for search Name
            if (searchCat == "Name")
            {
                DataTable dt = new DataTable();
                
                string firstName = "";
                string surName = "";
                
                if (searchText.Contains(" "))
                {
                    string[] fullname = searchText.Split(' ');
                    firstName = fullname[0];
                    surName = fullname[1];
                    for (int i = 2; i < fullname.Length; i++)
                    {
                        surName += fullname[i];
                    }
                    SqlDataAdapter sdaSearchResults = new SqlDataAdapter("select * from Users where fname='" + firstName + "' and sname='" + surName + "'", conn);
                    sdaSearchResults.Fill(dt);
                }
                else
                { 

                    SqlDataAdapter sdaSearchResults = new SqlDataAdapter("select * from Users where fname='" + searchText + "' or sname='" + searchText + "'", conn);
                    sdaSearchResults.Fill(dt);
                }

                searchResText.InnerHtml = "Search Results for \"" + searchText.ToString()+"\""; 

                foreach (DataRow rowResult in dt.Rows)
                {

                    TableRow rowToBeAdded = new TableRow();

                    SqlDataAdapter sdaSearchResultsProfileDetails = new SqlDataAdapter("select * from ProfileDetails where userId='" + rowResult["UserID"].ToString() + "'", conn);
                    DataTable dt2 = new DataTable();
                    sdaSearchResultsProfileDetails.Fill(dt2);

                    SqlDataAdapter sdaFriendCheck1 = new SqlDataAdapter("select * from Friends where fuserID='" + UserIDSession + "' and userId='" + rowResult["UserID"].ToString() + "'", conn);
                    DataTable friendCheck1 = new DataTable();
                    sdaFriendCheck1.Fill(friendCheck1);

                    TableCell cellToAdd = new TableCell();
                    if (rowResult["email"].ToString() == Session["New"].ToString())
                        cellToAdd.Text = "<a href='UserProfile.aspx'>" + rowResult["fname"].ToString() + " " + rowResult["sname"] + " - " + dt2.Rows[0]["gender"].ToString() + " - " + dt2.Rows[0]["age"].ToString() + "</a>";
                    else
                    {
                        if (dt2.Rows[0]["privacy"].ToString().Trim() == "Public")
                            cellToAdd.Text = "<a href='ViewProfile.aspx?id=" + rowResult["UserID"].ToString() + "'>" + rowResult["fname"].ToString() + " " + rowResult["sname"] + " - " + dt2.Rows[0]["gender"].ToString() + " - " + dt2.Rows[0]["age"].ToString() + "</a>";
                        else
                        {
                            if(friendCheck1.Rows.Count != 0)
                                cellToAdd.Text = "<a href='ViewProfile.aspx?id=" + rowResult["UserID"].ToString() + "'>" + rowResult["fname"].ToString() + " " + rowResult["sname"] + " - " + dt2.Rows[0]["gender"].ToString() + " - " + dt2.Rows[0]["age"].ToString() + "</a>";
                            else
                                cellToAdd.Text = "<a href='ViewProfile.aspx?id=" + rowResult["UserID"].ToString() + "'>" + rowResult["fname"].ToString() + " " + rowResult["sname"] + "</a>";
                        }
                    }

                    SqlDataAdapter sdaSearchResultsProfilePictures = new SqlDataAdapter("select * from ProfilePictures where userId='" + rowResult["UserID"].ToString() + "'", conn);
                    DataTable dt1 = new DataTable();
                    sdaSearchResultsProfilePictures.Fill(dt1);
                    TableCell cellToAdd3 = new TableCell();
                    if (dt1.Rows.Count != 0)
                        if (dt2.Rows[0]["privacy"].ToString().Trim() == "Public")
                            cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + rowResult["UserID"].ToString() + "' src='ProfilePictures/" + dt1.Rows[0]["imageName"].ToString() + "' />";
                        else
                        {
                            if (friendCheck1.Rows.Count != 0)
                                cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + rowResult["UserID"].ToString() + "' src='ProfilePictures/" + dt1.Rows[0]["imageName"].ToString() + "' />";
                            else
                                cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + rowResult["UserID"].ToString() + "' src='ProfilePictures/defaultProfilePicture.png' />";
                        }
                    else
                        cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + rowResult["UserID"].ToString() + "' src='ProfilePictures/defaultProfilePicture.png' />";

                    rowToBeAdded.Cells.Add(cellToAdd3);
                    rowToBeAdded.Cells.Add(cellToAdd);

                    //Creating the Button
                    if (rowResult["email"].ToString() != Session["New"].ToString())
                    {
                        SqlDataAdapter sdaRequests = new SqlDataAdapter("select * from FriendRequests where requesterUserId='" + UserIDSession + "' and userId='"+ rowResult["UserID"].ToString() + "'", conn);
                        DataTable frCheck = new DataTable();
                        sdaRequests.Fill(frCheck);
                        if (frCheck.Rows.Count == 0)
                        {
                            SqlDataAdapter sdaFriendCheck = new SqlDataAdapter("select * from Friends where fuserID='" + UserIDSession + "' and userId='" + rowResult["UserID"].ToString() + "'", conn);
                            DataTable friendCheck = new DataTable();
                            sdaFriendCheck.Fill(friendCheck);
                            if (friendCheck.Rows.Count != 0)
                            {
                                TableCell cellToAdd1 = new TableCell();
                                Button addButton = new Button();
                                addButton.ID = "ButtonViewProfile" + rowResult["UserID"].ToString();
                                addButton.Text = "Friends";
                                addButton.CssClass = "buttonFashion3";
                                addButton.Enabled = false;
                                cellToAdd1.Controls.Add(addButton);
                                rowToBeAdded.Cells.Add(cellToAdd1);

                            }
                            else
                            {

                                SqlDataAdapter sdaReceiveRequest = new SqlDataAdapter("select * from FriendRequests where requesterUserId='" + rowResult["UserID"].ToString() + "' and userId='" + UserIDSession + "'", conn);
                                DataTable fRequests = new DataTable();
                                sdaReceiveRequest.Fill(fRequests);
                                if (fRequests.Rows.Count != 0)
                                {
                                    TableCell cellToAdd1 = new TableCell();
                                    Button addButton = new Button();
                                    addButton.ID = "ButtonViewProfile" + rowResult["UserID"].ToString();
                                    addButton.Text = "Accept Request";
                                    addButton.CssClass = "buttonFashionAccept1";
                                    addButton.PostBackUrl = "AddFriend.aspx?fid=" + rowResult["UserID"].ToString() + "&id="+ UserIDSession + "&search=" + searchText + "&cat=" + searchCat;
                                    cellToAdd1.Controls.Add(addButton);
                                    rowToBeAdded.Cells.Add(cellToAdd1);
                                }
                                else
                                {

                                    TableCell cellToAdd1 = new TableCell();
                                    Button addButton = new Button();
                                    addButton.ID = "ButtonViewProfile" + rowResult["UserID"].ToString();
                                    addButton.Text = "Add Friend";
                                    addButton.CssClass = "buttonFashion";
                                    addButton.PostBackUrl = "SendFriendRequest.aspx?id=" + rowResult["UserID"].ToString() + "&search=" + searchText + "&cat=" + searchCat;
                                    cellToAdd1.Controls.Add(addButton);
                                    rowToBeAdded.Cells.Add(cellToAdd1);
                                }
                            }
                        }
                        else
                        {
                            TableCell cellToAdd1 = new TableCell();
                            Button addButton = new Button();
                            addButton.ID = "ButtonViewProfile" + rowResult["UserID"].ToString();
                            addButton.Text = "Request Sent";
                            addButton.CssClass = "buttonFashion2";
                            addButton.Enabled = false;
                            cellToAdd1.Controls.Add(addButton);
                            rowToBeAdded.Cells.Add(cellToAdd1);
                 
                        }
                        
                    }
                    else
                    {
                        TableCell cellToAdd1 = new TableCell();
                        rowToBeAdded.Cells.Add(cellToAdd1);
                    }

                    TableSearchResults.Rows.Add(rowToBeAdded);
                }
            }
            //Generating data for search on City
            if (searchCat == "City")
            {
                SqlDataAdapter sdaSearchResults = new SqlDataAdapter("select * from ProfileDetails where city='" + searchText + "'", conn);
                DataTable dt = new DataTable();
                sdaSearchResults.Fill(dt);

                searchResText.InnerHtml = "Search Results for \"" + searchText.ToString() + "\"";

                foreach (DataRow rowResult in dt.Rows)
                {

                    TableRow rowToBeAdded = new TableRow();

                    SqlDataAdapter sdaSearchResultsProfileDetails = new SqlDataAdapter("select * from Users where UserID='" + rowResult["userId"].ToString() + "'", conn);
                    DataTable dt2 = new DataTable();
                    sdaSearchResultsProfileDetails.Fill(dt2);

                    SqlDataAdapter sdaFriendCheck1 = new SqlDataAdapter("select * from Friends where fuserID='" + UserIDSession + "' and userId='" + dt2.Rows[0]["UserID"].ToString() + "'", conn);
                    DataTable friendCheck1 = new DataTable();
                    sdaFriendCheck1.Fill(friendCheck1);

                    TableCell cellToAdd = new TableCell();
                    if (dt2.Rows[0]["email"].ToString() == Session["New"].ToString())
                        cellToAdd.Text = "<a href='UserProfile.aspx'>" + dt2.Rows[0]["fname"].ToString() + " " + dt2.Rows[0]["sname"].ToString() + " - " + dt2.Rows[0]["gender"].ToString() + " - " + rowResult["age"].ToString() + "</a>";
                    else
                    {
                        if (rowResult["privacy"].ToString().Trim() == "Public")
                            cellToAdd.Text = "<a href='ViewProfile.aspx?id=" + dt2.Rows[0]["UserID"].ToString() + "'>" + dt2.Rows[0]["fname"].ToString() + " " + dt2.Rows[0]["sname"].ToString() + " - " + dt2.Rows[0]["gender"].ToString() + " - " + rowResult["age"].ToString() + "</a>";
                        else
                        {
                            if(friendCheck1.Rows.Count != 0)
                                cellToAdd.Text = "<a href='ViewProfile.aspx?id=" + dt2.Rows[0]["UserID"].ToString() + "'>" + dt2.Rows[0]["fname"].ToString() + " " + dt2.Rows[0]["sname"].ToString() + " - " + dt2.Rows[0]["gender"].ToString() + " - " + rowResult["age"].ToString() + "</a>";
                            else
                                cellToAdd.Text = "<a href='ViewProfile.aspx?id=" + dt2.Rows[0]["UserID"].ToString() + "'>" + dt2.Rows[0]["fname"].ToString() + " " + dt2.Rows[0]["sname"].ToString() + "</a>";
                        }

                    }
                    SqlDataAdapter sdaSearchResultsProfilePictures = new SqlDataAdapter("select * from ProfilePictures where userId='" + rowResult["userId"].ToString() + "'", conn);
                    DataTable dt1 = new DataTable();
                    sdaSearchResultsProfilePictures.Fill(dt1);
                    TableCell cellToAdd3 = new TableCell();
                    if (dt1.Rows.Count != 0)
                    {
                        if (rowResult["privacy"].ToString().Trim() == "Public")
                            cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + rowResult["userId"].ToString() + "' src='ProfilePictures/" + dt1.Rows[0]["imageName"].ToString() + "' />";
                        else
                        {
                            if (friendCheck1.Rows.Count != 0)
                                cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + rowResult["userId"].ToString() + "' src='ProfilePictures/" + dt1.Rows[0]["imageName"].ToString() + "' />";
                            else
                                cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + rowResult["userId"].ToString() + "' src='ProfilePictures/defaultProfilePicture.png' />";
                        }
                    }
                    else
                        cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + rowResult["userId"].ToString() + "' src='ProfilePictures/defaultProfilePicture.png' />";

                    rowToBeAdded.Cells.Add(cellToAdd3);
                    rowToBeAdded.Cells.Add(cellToAdd);

                    //Creating the Button
                    if (dt2.Rows[0]["email"].ToString() != Session["New"].ToString())
                    {

                        SqlDataAdapter sdaRequests = new SqlDataAdapter("select * from FriendRequests where requesterUserId='" + UserIDSession + "' and userId='" + rowResult["userId"].ToString() + "'", conn);
                        DataTable frCheck = new DataTable();
                        sdaRequests.Fill(frCheck);
                        if (frCheck.Rows.Count == 0)
                        {
                            SqlDataAdapter sdaFriendCheck = new SqlDataAdapter("select * from Friends where fuserID='" + UserIDSession + "' and userId='" + rowResult["userId"].ToString() + "'", conn);
                            DataTable friendCheck = new DataTable();
                            sdaFriendCheck.Fill(friendCheck);
                            if (friendCheck.Rows.Count != 0)
                            {
                                TableCell cellToAdd1 = new TableCell();
                                Button addButton = new Button();
                                addButton.ID = "ButtonViewProfile" + rowResult["userId"].ToString();
                                addButton.Text = "Friends";
                                addButton.CssClass = "buttonFashion3";
                                addButton.Enabled = false;
                                cellToAdd1.Controls.Add(addButton);
                                rowToBeAdded.Cells.Add(cellToAdd1);
                            }
                            else
                            {
                                SqlDataAdapter sdaReceiveRequest = new SqlDataAdapter("select * from FriendRequests where requesterUserId='" + rowResult["userId"].ToString() + "' and userId='" + UserIDSession + "'", conn);
                                DataTable fRequests = new DataTable();
                                sdaReceiveRequest.Fill(fRequests);
                                if (fRequests.Rows.Count != 0)
                                {
                                    TableCell cellToAdd1 = new TableCell();
                                    Button addButton = new Button();
                                    addButton.ID = "ButtonViewProfile" + rowResult["userId"].ToString();
                                    addButton.Text = "Accept Request";
                                    addButton.CssClass = "buttonFashionAccept1";
                                    addButton.PostBackUrl = "AddFriend.aspx?fid=" + rowResult["userId"].ToString() + "&id=" + UserIDSession + "&search=" + searchText + "&cat=" + searchCat;
                                    cellToAdd1.Controls.Add(addButton);
                                    rowToBeAdded.Cells.Add(cellToAdd1);
                                }
                                else
                                {

                                    TableCell cellToAdd1 = new TableCell();
                                    Button addButton = new Button();
                                    addButton.ID = "ButtonViewProfile" + rowResult["userId"].ToString();
                                    addButton.Text = "Add Friend";
                                    addButton.CssClass = "buttonFashion";
                                    addButton.PostBackUrl = "SendFriendRequest.aspx?id=" + rowResult["userId"].ToString() + "&search=" + searchText + "&cat=" + searchCat;
                                    cellToAdd1.Controls.Add(addButton);
                                    rowToBeAdded.Cells.Add(cellToAdd1);
                                }
                            }
                        }
                        else
                        {
                            TableCell cellToAdd1 = new TableCell();
                            Button addButton = new Button();
                            addButton.ID = "ButtonViewProfile" + rowResult["userId"].ToString();
                            addButton.Text = "Request Sent";
                            addButton.CssClass = "buttonFashion2";
                            addButton.Enabled = false;
                            cellToAdd1.Controls.Add(addButton);
                            rowToBeAdded.Cells.Add(cellToAdd1);
                        }
                    }
                    else
                    {
                        TableCell cellToAdd1 = new TableCell();
                        rowToBeAdded.Cells.Add(cellToAdd1);
                    }

                    TableSearchResults.Rows.Add(rowToBeAdded);
                }
            }

            //Generating data for search on Friend Of
            if (searchCat == "Friend of")
            {
                SqlDataAdapter sdaSearchResults = new SqlDataAdapter("select * from Users", conn);
                DataTable dt = new DataTable();
                sdaSearchResults.Fill(dt);

                string fullName = "";

                searchResText.InnerHtml = "Search Results for \"" + searchText.ToString() + "\"";

                foreach (DataRow rowResult in dt.Rows)
                {
                    fullName = rowResult["fname"].ToString() + " " + rowResult["sname"].ToString();
                    if (fullName == searchText)
                    {
                        

                        SqlDataAdapter sdaFriends = new SqlDataAdapter("select * from Friends where userId='" + rowResult["UserID"].ToString() + "'", conn);
                        DataTable dt2 = new DataTable();
                        sdaFriends.Fill(dt2);

                        foreach (DataRow rowResultFriends in dt2.Rows)
                        {
                            TableRow rowToBeAdded = new TableRow();

                            SqlDataAdapter sdaFriendsUser = new SqlDataAdapter("select * from Users where userId='" + rowResultFriends["fuserId"].ToString() + "'", conn);
                            DataTable dt3 = new DataTable();
                            sdaFriendsUser.Fill(dt3);

                            SqlDataAdapter sdaFriendsUserProfileDetails = new SqlDataAdapter("select * from ProfileDetails where userId='" + rowResultFriends["fuserId"].ToString() + "'", conn);
                            DataTable dt4 = new DataTable();
                            sdaFriendsUserProfileDetails.Fill(dt4);

                            SqlDataAdapter sdaFriendCheck1 = new SqlDataAdapter("select * from Friends where fuserID='" + UserIDSession + "' and userId='" + dt3.Rows[0]["UserID"].ToString() + "'", conn);
                            DataTable friendCheck1 = new DataTable();
                            sdaFriendCheck1.Fill(friendCheck1);


                            TableCell cellToAdd = new TableCell();
                            if (dt3.Rows[0]["email"].ToString() == Session["New"].ToString())
                                cellToAdd.Text = "<a href='UserProfile.aspx'>" + dt3.Rows[0]["fname"].ToString() + " " + dt3.Rows[0]["sname"].ToString() + " - " + dt4.Rows[0]["gender"].ToString() + " - " + dt4.Rows[0]["age"].ToString() + "</a>";
                            else
                            {
                                if (dt4.Rows[0]["privacy"].ToString().Trim() == "Public")
                                    cellToAdd.Text = "<a href='ViewProfile.aspx?id=" + dt3.Rows[0]["UserID"].ToString() + "'>" + dt3.Rows[0]["fname"].ToString() + " " + dt3.Rows[0]["sname"].ToString() + " - " + dt4.Rows[0]["gender"].ToString() + " - " + dt4.Rows[0]["age"].ToString() + "</a>";
                                else
                                {
                                    if(friendCheck1.Rows.Count != 0)
                                        cellToAdd.Text = "<a href='ViewProfile.aspx?id=" + dt3.Rows[0]["UserID"].ToString() + "'>" + dt3.Rows[0]["fname"].ToString() + " " + dt3.Rows[0]["sname"].ToString() + " - " + dt4.Rows[0]["gender"].ToString() + " - " + dt4.Rows[0]["age"].ToString() + "</a>";
                                    else
                                        cellToAdd.Text = "<a href='ViewProfile.aspx?id=" + dt3.Rows[0]["UserID"].ToString() + "'>" + dt3.Rows[0]["fname"].ToString() + " " + dt3.Rows[0]["sname"].ToString() + "</a>";
                                }
                            }

                            SqlDataAdapter sdaSearchResultsProfilePictures = new SqlDataAdapter("select * from ProfilePictures where userId='" + dt4.Rows[0]["userId"].ToString() + "'", conn);
                            DataTable dt1 = new DataTable();
                            sdaSearchResultsProfilePictures.Fill(dt1);
                            TableCell cellToAdd3 = new TableCell();

                            if (dt1.Rows.Count != 0)
                            {
                                if (dt4.Rows[0]["privacy"].ToString().Trim() == "Public")
                                    cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + dt4.Rows[0]["userId"].ToString() + "' src='ProfilePictures/" + dt1.Rows[0]["imageName"].ToString() + "' />";
                                else
                                {
                                    if (friendCheck1.Rows.Count != 0)
                                        cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + dt4.Rows[0]["userId"].ToString() + "' src='ProfilePictures/" + dt1.Rows[0]["imageName"].ToString() + "' />";
                                    else
                                        cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + dt4.Rows[0]["userId"].ToString() + "' src='ProfilePictures/defaultProfilePicture.png' />";
                                }
                            }
                            else
                                cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + dt4.Rows[0]["userId"].ToString() + "' src='ProfilePictures/defaultProfilePicture.png' />";
                            rowToBeAdded.Cells.Add(cellToAdd3);
                            rowToBeAdded.Cells.Add(cellToAdd);

                            //Creating the Button 
                            if (dt3.Rows[0]["email"].ToString() != Session["New"].ToString())
                            {
                                SqlDataAdapter sdaRequests = new SqlDataAdapter("select * from FriendRequests where requesterUserId='" + UserIDSession + "' and userId='" + dt4.Rows[0]["userId"].ToString() + "'", conn);
                                DataTable frCheck = new DataTable();
                                sdaRequests.Fill(frCheck);
                                if (frCheck.Rows.Count == 0)
                                {
                                    SqlDataAdapter sdaFriendCheck = new SqlDataAdapter("select * from Friends where fuserID='" + UserIDSession + "' and userId='" + dt4.Rows[0]["userId"].ToString() + "'", conn);
                                    DataTable friendCheck = new DataTable();
                                    sdaFriendCheck.Fill(friendCheck);
                                    if (friendCheck.Rows.Count != 0)
                                    {
                                        TableCell cellToAdd1 = new TableCell();
                                        Button addButton = new Button();
                                        addButton.ID = "ButtonViewProfile" + dt4.Rows[0]["userId"].ToString();
                                        addButton.Text = "Friends";
                                        addButton.CssClass = "buttonFashion3";
                                        addButton.Enabled = false;                                    
                                        rowToBeAdded.Cells.Add(cellToAdd1);
                                        cellToAdd1.Controls.Add(addButton);
                                    }
                                    else
                                    {
                                        SqlDataAdapter sdaReceiveRequest = new SqlDataAdapter("select * from FriendRequests where requesterUserId='" + dt4.Rows[0]["userId"].ToString() + "' and userId='" + UserIDSession + "'", conn);
                                        DataTable fRequests = new DataTable();
                                        sdaReceiveRequest.Fill(fRequests);
                                        if (fRequests.Rows.Count != 0)
                                        {
                                            TableCell cellToAdd1 = new TableCell();
                                            Button addButton = new Button();
                                            addButton.ID = "ButtonViewProfile" + dt4.Rows[0]["userId"].ToString();
                                            addButton.Text = "Accept Request";
                                            addButton.CssClass = "buttonFashionAccept1";
                                            addButton.PostBackUrl = "AddFriend.aspx?fid=" + dt4.Rows[0]["userId"].ToString() + "&id=" + UserIDSession + "&search=" + searchText + "&cat=" + searchCat;
                                            rowToBeAdded.Cells.Add(cellToAdd1);
                                            cellToAdd1.Controls.Add(addButton);
                                        }
                                        else
                                        {

                                            TableCell cellToAdd1 = new TableCell();
                                            Button addButton = new Button();
                                            addButton.ID = "ButtonViewProfile" + dt4.Rows[0]["userId"].ToString();
                                            addButton.Text = "Add Friend";
                                            addButton.CssClass = "buttonFashion";
                                            addButton.PostBackUrl = "SendFriendRequest.aspx?id=" + dt4.Rows[0]["userId"].ToString() + "&search=" + searchText + "&cat=" + searchCat;
                                            rowToBeAdded.Cells.Add(cellToAdd1);
                                            cellToAdd1.Controls.Add(addButton);
                                        }
                                    }
                                }
                                else
                                {
                                    TableCell cellToAdd1 = new TableCell();
                                    Button addButton = new Button();
                                    addButton.ID = "ButtonViewProfile" + dt4.Rows[0]["userId"].ToString();
                                    addButton.Text = "Request Sent";
                                    addButton.CssClass = "buttonFashion2";
                                    addButton.Enabled = false;
                                    rowToBeAdded.Cells.Add(cellToAdd1);
                                    cellToAdd1.Controls.Add(addButton);
                                }
                            }
                            else
                            {
                                TableCell cellToAdd1 = new TableCell();
                                rowToBeAdded.Cells.Add(cellToAdd1);
                            }
                            
                            
                            TableSearchResults.Rows.Add(rowToBeAdded);
                        }
                    }
                }
            }




            //Populating the suggestion area
            SqlDataAdapter takeUsers = new SqlDataAdapter("select * from Users where not UserID='" + UserIDSession + "'", conn);
            DataTable dtAllUsers = new DataTable();
            takeUsers.Fill(dtAllUsers);

            SqlDataAdapter takeUserFriends = new SqlDataAdapter("select * from Friends where userId='" + UserIDSession + "'", conn);
            DataTable dtUserFriends = new DataTable();
            takeUserFriends.Fill(dtUserFriends);

            SqlDataAdapter takeUserDetails = new SqlDataAdapter("select * from ProfileDetails where userId='" + UserIDSession + "'", conn);
            DataTable dtUserDetails = new DataTable();
            takeUserDetails.Fill(dtUserDetails);

            ArrayList allFriends = new ArrayList();

            foreach (DataRow userFriend in dtUserFriends.Rows)
                allFriends.Add(userFriend["fuserID"].ToString());

            int ct = 0;

            foreach (DataRow userToAdd in dtAllUsers.Rows)
            {
                SqlDataAdapter checkForRequestSuggestion = new SqlDataAdapter("select * from FriendRequests where userId='" + userToAdd["UserID"].ToString() + "' and requesterUserId='"+ UserIDSession + "'", conn);
                DataTable dtcheckForRequestSuggestion = new DataTable();
                checkForRequestSuggestion.Fill(dtcheckForRequestSuggestion);

                SqlDataAdapter takeSugDetails = new SqlDataAdapter("select * from ProfileDetails where userId='" + userToAdd["UserID"].ToString() + "'", conn);
                DataTable dtSugDetails= new DataTable();
                takeSugDetails.Fill(dtSugDetails);

                if (dtcheckForRequestSuggestion.Rows.Count == 0 && dtSugDetails.Rows[0]["city"].ToString() == dtUserDetails.Rows[0]["city"].ToString())
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
                        SqlDataAdapter checkForRequestSuggestion1 = new SqlDataAdapter("select * from FriendRequests where userId='" + UserIDSession + "' and requesterUserId='" + userToAdd["UserID"].ToString() + "'", conn);
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
                            AddNewFriendButton.PostBackUrl = "SendFriendRequest.aspx?id=" + userToAdd["UserID"].ToString() + "&search=" + searchText + "&cat=" + searchCat;
                            buttonAreaSuggestion.Controls.Add(AddNewFriendButton);
                        }
                        else
                        {
                            Button AcceptFriendButton = new Button();
                            AcceptFriendButton.ID = "AcceptFriendSuggestion" + userToAdd["UserID"].ToString();
                            AcceptFriendButton.Text = "Accept Request";
                            AcceptFriendButton.CssClass = "buttonFashionAcceptFriendSuggestion";
                            AcceptFriendButton.PostBackUrl = "AddFriend.aspx?fid=" + userToAdd["UserID"].ToString() + "&id=" + UserIDSession + "&search=" + searchText + "&cat=" + searchCat;
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
    }

}