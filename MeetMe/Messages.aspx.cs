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

public partial class Messages : System.Web.UI.Page
{

    protected string friendUID = "";
    protected string UserID = "";
    protected string friendName = "";
    protected string profilePictureName = "";



    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        Page.MaintainScrollPositionOnPostBack = true;

        //Register Update Trigger
        ScriptManager1.RegisterAsyncPostBackControl(ButtonInv);
        

        //Receive data from url
        friendUID = Request.QueryString["fid"].ToString();
        UserID = Request.QueryString["id"].ToString();

        //Creating SQL Connection
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);

        //Get the Friend Name
        SqlDataAdapter sdaFrData = new SqlDataAdapter("select * from Users where UserID='"+friendUID+"'", conn);
        DataTable dtFrDt = new DataTable();
        sdaFrData.Fill(dtFrDt);

        friendName = dtFrDt.Rows[0]["fname"].ToString() + " " + dtFrDt.Rows[0]["sname"].ToString();

        //Get Profile Pic Name
        SqlDataAdapter sdaPic = new SqlDataAdapter("select * from ProfilePictures where userId='" + friendUID + "'", conn);
        DataTable dtPic = new DataTable();
        sdaPic.Fill(dtPic);

        if (dtPic.Rows.Count != 0)
            profilePictureName = dtPic.Rows[0]["imageName"].ToString();
        else
            profilePictureName = "defaultProfilePicture.png";

        ImageProfilePicture.ImageUrl = "ProfilePictures/" + profilePictureName;

        //Populate the chat screen
        
        SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Mesaje where userId='" + UserID + "' and fId='" + friendUID + "'", conn);
        DataTable dt = new DataTable();
        sdaUserData.Fill(dt);
        int keyCt = 0;

        foreach (DataRow msgRes in dt.Rows)
        {
            if (msgRes["type"].ToString().Trim() == "SEND")
            {
                messagesScreenID.InnerHtml += "<div class='sendMsg'><p>" + msgRes["messageText"].ToString() + "</p></div><div class='clearArea'></div>";
                keyCt++;    
            }
            else
            {
                messagesScreenID.InnerHtml += "<div class='receiveMsg'><p>" + msgRes["messageText"].ToString() + "</p></div><div class='clearArea'></div>";
            }
        }

        suggestionText.InnerText = "There are no Message Suggestions at the moment! You are doing great! Keep it that way!";

        //Retrieve keywords from DB
        SqlDataAdapter userKeyWords = new SqlDataAdapter("select * from Keywords where userId='" + UserID + "' and fId='" + friendUID + "'", conn);
        DataTable dtKey = new DataTable();
        userKeyWords.Fill(dtKey);

        //Message Suggestion 1 'Hello!'
        if (keyCt <= 5 && dtKey.Rows[0]["hello"].ToString().Trim()=="no")
        {
            suggestionText.InnerText = "Message Suggestion: Try saying: \"Hello!\"";
        }

        //Message Suggestion 2 'How was/is your day?'
        if ((keyCt >5 && keyCt <=15) && dtKey.Rows[0]["day"].ToString().Trim() == "no")
        {
            suggestionText.InnerText = "Message Suggestion: Try saying: \"How was/is your day?\"";
        }

        //Message Suggestion 3 'What hobby do you have?'
        if ((keyCt > 15 && keyCt <= 25) && dtKey.Rows[0]["hobby"].ToString().Trim() == "no")
        {
            suggestionText.InnerText = "Message Suggestion: Try saying: \"What hobby do you have?\"";
        }

        //Message Suggestion 4 'What are you doing for a living?'
        if ((keyCt > 25 && keyCt <= 30) && dtKey.Rows[0]["living"].ToString().Trim() == "no")
        {
            suggestionText.InnerText = "Message Suggestion: Try saying: \"What are you doing for a living?\"";
        }

        //Message Suggestion 5 'What kind of music do you listen to?'
        if ((keyCt > 30 && keyCt <= 40) && dtKey.Rows[0]["music"].ToString().Trim() == "no")
        {
            suggestionText.InnerText = "Message Suggestion: Try saying: \"What kind of music do you listen to?\"";
        }

        //Message Suggestion 6 'What was the last movie you watched?'
        if ((keyCt > 40 && keyCt <= 50) && dtKey.Rows[0]["movies"].ToString().Trim() == "no")
        {
            suggestionText.InnerText = "Message Suggestion: Try saying: \"'What was the last movie you watched?\"";
        }

        //Message Suggestion 7 'Do you like playing games?'
        if ((keyCt > 50 && keyCt <= 60) && dtKey.Rows[0]["games"].ToString().Trim() == "no")
        {
            suggestionText.InnerText = "Message Suggestion: Try saying: \"Do you like playing games?\"";
        }

        //Message Suggestion 8 'What type of food do you like?'
        if ((keyCt > 60 && keyCt <= 70) && dtKey.Rows[0]["food"].ToString().Trim() == "no")
        {
            suggestionText.InnerText = "Message Suggestion: Try saying: \"What type of food do you like?\"";
        }

        //Message Suggestion 9 'Your gallery pictures are beautiful!'
        if ((keyCt > 70 && keyCt <= 80) && dtKey.Rows[0]["pictures"].ToString().Trim() == "no")
        {
            suggestionText.InnerText = "Message Suggestion: Try saying: \"Your gallery pictures are beautiful!\"";
        }

        //Message Suggestion 10 'Let's date in the weekend!'
        if (keyCt > 80 && dtKey.Rows[0]["date"].ToString().Trim() == "no")
        {
            suggestionText.InnerText = "Message Suggestion: Try saying: \"Let's date in the weekend!\"";
        }

        //check for helper option
        SqlDataAdapter userProfDet = new SqlDataAdapter("select * from ProfileDetails where userId='" + UserID + "'", conn);
        DataTable dtUserOpt = new DataTable();
        userProfDet.Fill(dtUserOpt);
        if (dtUserOpt.Rows[0]["chat"].ToString().Trim() == "Off")
            SuggestionDiv.Visible = false;

    }

    protected void ButtonInv_Click(object sender, EventArgs e)
    {
        messagesScreenID.InnerHtml = "";

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Mesaje where userId='" + UserID + "' and fId='"+ friendUID + "'", conn);
        DataTable dt = new DataTable();
        sdaUserData.Fill(dt);

        foreach (DataRow msgRes in dt.Rows)
        {
            if (msgRes["type"].ToString().Trim() == "SEND")
            {
                messagesScreenID.InnerHtml += "<div class='sendMsg'><p>" + msgRes["messageText"].ToString() + "</p></div><div class='clearArea'></div>";
            }
            else
            {
                messagesScreenID.InnerHtml += "<div class='receiveMsg'><p>" + msgRes["messageText"].ToString() + "</p></div><div class='clearArea'></div>";
            }
        }
        conn.Open();
        string updateSeen = "update Mesaje set seen='yes' where userId='"+ UserID + "' and fId='"+ friendUID + "'";
        SqlCommand com = new SqlCommand(updateSeen, conn);
        com.ExecuteNonQuery();
        conn.Close();
        



    }

    protected void ButtonSendMsg_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        conn.Open();
        string insertMsg = "insert into Mesaje(fid, messageText, userId, type, seen) values (@frID, @msgTxt, @UID, @msgType, @sn)";
        SqlCommand com = new SqlCommand(insertMsg, conn);
        com.Parameters.AddWithValue("@frID", friendUID);
        com.Parameters.AddWithValue("@msgTxt", TextBoxInputMessage.Text.ToString());
        com.Parameters.AddWithValue("@UID", UserID);
        com.Parameters.AddWithValue("@msgType", "SEND");
        com.Parameters.AddWithValue("@sn", "yes");
        com.ExecuteNonQuery();

        string insertMsg1 = "insert into Mesaje(fid, messageText, userId, type, seen) values (@frID1, @msgTxt1, @UID1, @msgType1, @sn1)";
        SqlCommand com1 = new SqlCommand(insertMsg1, conn);
        com1.Parameters.AddWithValue("@frID1", UserID);
        com1.Parameters.AddWithValue("@msgTxt1", TextBoxInputMessage.Text.ToString());
        com1.Parameters.AddWithValue("@UID1", friendUID);
        com1.Parameters.AddWithValue("@msgType1", "RECEIVE");
        com1.Parameters.AddWithValue("@sn1", "no");
        com1.ExecuteNonQuery();

        //Check for keys
        SqlDataAdapter userKeyWords = new SqlDataAdapter("select * from Keywords where userId='" + UserID + "' and fId='" + friendUID + "'", conn);
        DataTable dtKey = new DataTable();
        userKeyWords.Fill(dtKey);

        //Process the message string
        string textToProcess = TextBoxInputMessage.Text.ToString().ToLower();
        var punctuation = textToProcess.Where(Char.IsPunctuation).Distinct().ToArray();
        var words = textToProcess.Split().Select(x => x.Trim(punctuation));

        foreach (string word in words)
        {
            //hello
            if ((word == "hello" || word == "hi" || word =="hey") && dtKey.Rows[0]["hello"].ToString().Trim()=="no")
            {
                string updateKeys = "update Keywords set hello='yes' where userId='" + UserID + "' and fId='" + friendUID + "'";
                SqlCommand comChangeKey = new SqlCommand(updateKeys, conn);
                comChangeKey.ExecuteNonQuery();
            }

            //day
            if ((word == "day") && dtKey.Rows[0]["day"].ToString().Trim() == "no")
            {
                string updateKeys = "update Keywords set day='yes' where userId='" + UserID + "' and fId='" + friendUID + "'";
                SqlCommand comChangeKey = new SqlCommand(updateKeys, conn);
                comChangeKey.ExecuteNonQuery();
            }

            //hobby
            if ((word == "hobby" || word =="hobbies") && dtKey.Rows[0]["hobby"].ToString().Trim() == "no")
            {
                string updateKeys = "update Keywords set hobby='yes' where userId='" + UserID + "' and fId='" + friendUID + "'";
                SqlCommand comChangeKey = new SqlCommand(updateKeys, conn);
                comChangeKey.ExecuteNonQuery();
            }

            //living
            if ((word == "living" || word=="work" || word=="job" || word== "occupation") && dtKey.Rows[0]["living"].ToString().Trim() == "no")
            {
                string updateKeys = "update Keywords set living='yes' where userId='" + UserID + "' and fId='" + friendUID + "'";
                SqlCommand comChangeKey = new SqlCommand(updateKeys, conn);
                comChangeKey.ExecuteNonQuery();
            }

            //music
            if ((word == "music" || word=="song" || word=="songs") && dtKey.Rows[0]["music"].ToString().Trim() == "no")
            {
                string updateKeys = "update Keywords set music='yes' where userId='" + UserID + "' and fId='" + friendUID + "'";
                SqlCommand comChangeKey = new SqlCommand(updateKeys, conn);
                comChangeKey.ExecuteNonQuery();
            }

            //movies
            if ((word == "movies" || word=="movie" || word=="film") && dtKey.Rows[0]["movies"].ToString().Trim() == "no")
            {
                string updateKeys = "update Keywords set movies='yes' where userId='" + UserID + "' and fId='" + friendUID + "'";
                SqlCommand comChangeKey = new SqlCommand(updateKeys, conn);
                comChangeKey.ExecuteNonQuery();
            }

            //games
            if ((word == "games" || word == "game") && dtKey.Rows[0]["games"].ToString().Trim() == "no")
            {
                string updateKeys = "update Keywords set games='yes' where userId='" + UserID + "' and fId='" + friendUID + "'";
                SqlCommand comChangeKey = new SqlCommand(updateKeys, conn);
                comChangeKey.ExecuteNonQuery();
            }

            //food
            if ((word == "food") && dtKey.Rows[0]["food"].ToString().Trim() == "no")
            {
                string updateKeys = "update Keywords set food='yes' where userId='" + UserID + "' and fId='" + friendUID + "'";
                SqlCommand comChangeKey = new SqlCommand(updateKeys, conn);
                comChangeKey.ExecuteNonQuery();
            }

            //pictures
            if ((word == "picture" || word=="pictures" || word=="photo" || word=="photos") && dtKey.Rows[0]["pictures"].ToString().Trim() == "no")
            {
                string updateKeys = "update Keywords set pictures='yes' where userId='" + UserID + "' and fId='" + friendUID + "'";
                SqlCommand comChangeKey = new SqlCommand(updateKeys, conn);
                comChangeKey.ExecuteNonQuery();
            }

            //food
            if ((word == "date" || word =="meet" || word=="meeting") && dtKey.Rows[0]["date"].ToString().Trim() == "no")
            {
                string updateKeys = "update Keywords set date='yes' where userId='" + UserID + "' and fId='" + friendUID + "'";
                SqlCommand comChangeKey = new SqlCommand(updateKeys, conn);
                comChangeKey.ExecuteNonQuery();
            }
        }

        conn.Close();

        Response.Redirect(Request.RawUrl);

    }

    protected void ButtonLogout_Click(object sender, EventArgs e)
    {
        Session["New"] = null;
        Response.Redirect("Login.aspx");
    }



    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchResults.aspx?search=" + TextBoxSearchBar.Text.ToString() + "&cat=" + DropDownListSearchCategory.SelectedValue.ToString());
    }
}