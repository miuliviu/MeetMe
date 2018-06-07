using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class FriendRequests : System.Web.UI.Page
{
    protected string UserID = ""; 

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);

        SqlDataAdapter takeUserID = new SqlDataAdapter("select * from Users where email='" + Session["New"] + "'", conn);
        DataTable dtUserID = new DataTable();
        takeUserID.Fill(dtUserID);

        UserID = dtUserID.Rows[0]["UserID"].ToString();

        SqlDataAdapter takeRequests = new SqlDataAdapter("select * from FriendRequests where userId='" + UserID + "'", conn);
        DataTable dtFR = new DataTable();
        takeRequests.Fill(dtFR);

        foreach (DataRow rowResult in dtFR.Rows)
        {

            TableRow rowToBeAdded = new TableRow();

            SqlDataAdapter sdaSearchResultsProfileDetails = new SqlDataAdapter("select * from ProfileDetails where userId='" + rowResult["requesterUserId"].ToString() + "'", conn);
            DataTable dt2 = new DataTable();
            sdaSearchResultsProfileDetails.Fill(dt2);

            TableCell cellToAdd = new TableCell();
            cellToAdd.Text = "<a href='ViewProfile.aspx?id=" + dt2.Rows[0]["userId"].ToString() + "'>" + rowResult["requestFname"].ToString() + " " + rowResult["requestLname"] + " - " + dt2.Rows[0]["gender"].ToString() + " - " + dt2.Rows[0]["age"].ToString() + "</a>";

            SqlDataAdapter sdaSearchResultsProfilePictures = new SqlDataAdapter("select * from ProfilePictures where userId='" + dt2.Rows[0]["userId"].ToString() + "'", conn);
            DataTable dt1 = new DataTable();
            sdaSearchResultsProfilePictures.Fill(dt1);
            TableCell cellToAdd3 = new TableCell();
            if(dt1.Rows.Count !=0)
                cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + dt2.Rows[0]["userId"].ToString() + "' src='ProfilePictures/" + dt1.Rows[0]["imageName"].ToString() + "' />";
            else
                cellToAdd3.Text = "<img class='profileImages' id='ImageProfilePicture" + dt2.Rows[0]["userId"].ToString() + "' src='ProfilePictures/defaultProfilePicture.png' />";
            rowToBeAdded.Cells.Add(cellToAdd3);
            rowToBeAdded.Cells.Add(cellToAdd);

            //Creating the Button
            TableCell cellToAdd1 = new TableCell();
            Button addButton = new Button();
            addButton.ID = "ButtonAccept" + dt2.Rows[0]["userId"].ToString();
            addButton.Text = "Accept Friend Request";
            addButton.CssClass = "buttonFashionAcceptFriend";
            addButton.PostBackUrl = "AddFriend.aspx?fid=" + dt2.Rows[0]["userId"].ToString() + "&id="+UserID;
            cellToAdd1.Controls.Add(addButton);
            rowToBeAdded.Cells.Add(cellToAdd1);

            TableFriendRequestResults.Rows.Add(rowToBeAdded);
        }

        if (TableFriendRequestResults.Rows.Count == 0)
        {
            LabelNoRequestsMessage.Visible = true;
        }

        conn.Open();
        string updateFriendReq = "update FriendRequests set seen='yes' where userId='" + UserID + "'";
        SqlCommand com = new SqlCommand(updateFriendReq, conn);
        com.ExecuteNonQuery();
        conn.Close();
    }
}