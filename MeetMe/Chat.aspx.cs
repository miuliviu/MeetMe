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

public partial class Chat : System.Web.UI.Page
{

    protected string UserID = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        ArrayList converID = new ArrayList();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);

        SqlDataAdapter takeUserID = new SqlDataAdapter("select * from Users where email='" + Session["New"] + "'", conn);
        DataTable dtUserID = new DataTable();
        takeUserID.Fill(dtUserID);

        UserID = dtUserID.Rows[0]["UserID"].ToString();

        SqlDataAdapter takeFriends = new SqlDataAdapter("select * from Friends where userId='" + UserID + "'", conn);
        DataTable dtFriends = new DataTable();
        takeFriends.Fill(dtFriends);

        SqlDataAdapter takeMessages = new SqlDataAdapter("select * from Mesaje where userId='" + UserID + "'", conn);
        DataTable dtMessages = new DataTable();
        takeMessages.Fill(dtMessages);

        if (dtMessages.Rows.Count > 0)
        {
            for (int i = dtMessages.Rows.Count - 1; i >= 0; i--)
            {
                foreach (DataRow RowFriend in dtFriends.Rows)
                {
                    if (dtMessages.Rows[i]["fid"].ToString() == RowFriend["fuserID"].ToString() && !converID.Contains(dtMessages.Rows[i]["fid"].ToString()))
                    {

                        //Add Friend to list
                        converID.Add(dtMessages.Rows[i]["fid"].ToString());

                        HtmlGenericControl divToAdd = new HtmlGenericControl("div");
                        divToAdd.Attributes.Add("class", "conversationLine");

                        SqlDataAdapter takeProfiPic = new SqlDataAdapter("select * from ProfilePictures where userId='" + RowFriend["fuserID"].ToString() + "'", conn);
                        DataTable dtProf = new DataTable();
                        takeProfiPic.Fill(dtProf);

                        //Image for friend in conv
                        Image imgToAdd = new Image();
                        imgToAdd.ID = "Image" + RowFriend["fuserID"].ToString();
                        if(dtProf.Rows.Count != 0)
                            imgToAdd.ImageUrl = "~/ProfilePictures/" + dtProf.Rows[0]["imageName"];
                        else
                            imgToAdd.ImageUrl = "~/ProfilePictures/defaultProfilePicture.png";
                        imgToAdd.AlternateText = "imageID" + RowFriend["fuserID"].ToString();
                        imgToAdd.CssClass = "imgProfile";

                        HtmlGenericControl pToAdd = new HtmlGenericControl("p");
                        pToAdd.Attributes.Add("class", "convText");

                        if (dtMessages.Rows[i]["type"].ToString().Trim() == "SEND")
                        {
                            pToAdd.InnerHtml = "Last message sent to <a href=\"ViewProfile.aspx?id=" + RowFriend["fuserID"].ToString() + "\">" + RowFriend["friendFname"].ToString() + " " + RowFriend["friendLname"].ToString() + "</a> :";
                        }
                        else
                        {
                            pToAdd.InnerHtml = "Last message received from <a href=\"ViewProfile.aspx?id=" + RowFriend["fuserID"].ToString() + "\">" + RowFriend["friendFname"].ToString() + " " + RowFriend["friendLname"].ToString() + "</a> :";
                        }

                        HtmlGenericControl pToAddMsg = new HtmlGenericControl("p");
                        pToAddMsg.Attributes.Add("class", "msgText");

                        pToAddMsg.InnerText = dtMessages.Rows[i]["messageText"].ToString();

                        HtmlGenericControl imgLinkToAdd = new HtmlGenericControl("a");
                        imgLinkToAdd.Attributes.Add("href", "ViewProfile.aspx?id=" + RowFriend["fuserID"].ToString());

                        imgLinkToAdd.Controls.Add(imgToAdd);

                        //Buttons to add;
                        Button msgButton = new Button();
                        msgButton.ID = "ButtonMessage" + RowFriend["fuserID"].ToString();
                        msgButton.Text = "Message";
                        msgButton.CssClass = "buttonFashionMessage";
                        msgButton.PostBackUrl = "Messages.aspx?id=" + UserID + "&fid=" + RowFriend["fuserID"].ToString();

                        Button deleteConvButton = new Button();
                        deleteConvButton.ID = "ButtonDelete" + RowFriend["fuserID"].ToString();
                        deleteConvButton.Text = "Delete Conversation";
                        deleteConvButton.CssClass = "buttonFashionDeleteConv";
                        deleteConvButton.PostBackUrl = "DeleteConversation.aspx?id=" + UserID + "&fid=" + RowFriend["fuserID"].ToString();

                        //Create div's
                        HtmlGenericControl imgDiv = new HtmlGenericControl("div");
                        HtmlGenericControl preMsgTxtDiv = new HtmlGenericControl("div");
                        HtmlGenericControl msgTxtDiv = new HtmlGenericControl("div");
                        HtmlGenericControl butMsgDiv = new HtmlGenericControl("div");
                        HtmlGenericControl butDelDiv = new HtmlGenericControl("div");

                        //Add Classes
                        imgDiv.Attributes.Add("class", "imgDiv");
                        preMsgTxtDiv.Attributes.Add("class", "preMsgTxtDiv");
                        msgTxtDiv.Attributes.Add("class", "msgTxtDiv");
                        butMsgDiv.Attributes.Add("class", "butMsgDiv");
                        butDelDiv.Attributes.Add("class", "butDelDiv");

                        //Add controlls
                        imgDiv.Controls.Add(imgLinkToAdd);
                        preMsgTxtDiv.Controls.Add(pToAdd);
                        msgTxtDiv.Controls.Add(pToAddMsg);
                        butMsgDiv.Controls.Add(msgButton);
                        butDelDiv.Controls.Add(deleteConvButton);


                        //Add Controls
                        divToAdd.Controls.Add(imgDiv);
                        divToAdd.Controls.Add(preMsgTxtDiv);
                        divToAdd.Controls.Add(msgTxtDiv);
                        divToAdd.Controls.Add(butMsgDiv);
                        divToAdd.Controls.Add(butDelDiv);

                        //Add to main Div
                        allConversations.Controls.Add(divToAdd);

                        //Exit iteration
                        break;
                    }
                }
            }
        }
        else
        {
            HtmlGenericControl pNoMsg = new HtmlGenericControl("p");
            pNoMsg.Attributes.Add("class", "noMessages");
            pNoMsg.InnerText = "There are no Conversations at the moment.";
            allConversations.Controls.Add(pNoMsg);
        }

        conn.Open();
        string updateMesaje = "update Mesaje set seen='yes' where userId='"+UserID+"'";
        SqlCommand com = new SqlCommand(updateMesaje, conn);
        com.ExecuteNonQuery();
        conn.Close();
    }
}