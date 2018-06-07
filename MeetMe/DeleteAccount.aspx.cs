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
using System.Text;
using System.Security.Cryptography;

public partial class DeleteAccount : System.Web.UI.Page
{
    protected string UID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["New"] == null)
            Response.Redirect("Login.aspx");

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);

        SqlDataAdapter takeUserID = new SqlDataAdapter("select * from Users where email='" + Session["New"] + "'", conn);
        DataTable dtUserID = new DataTable();
        takeUserID.Fill(dtUserID);
        UID = dtUserID.Rows[0]["UserID"].ToString();
    }

    protected void ButtonYes_Click(object sender, EventArgs e)
    {
        //Create DB Conenction
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);

        //CheckPassword
        SqlDataAdapter takeUserID = new SqlDataAdapter("select * from Users where UserID='" + UID + "'", conn);
        DataTable dtUserID = new DataTable();
        takeUserID.Fill(dtUserID);

        if (dtUserID.Rows[0]["pass"].ToString().Trim().Equals(Encrypt(TextBoxPassword.Text.ToString()).Trim()))
        {
            conn.Open();
            //Delete from Users
            string deleteFromUsers = "delete from Users where UserID='" + UID + "'";
            SqlCommand comUsers = new SqlCommand(deleteFromUsers, conn);
            comUsers.ExecuteNonQuery();

            //Delete from ProfileDetails
            string deleteFromProfileDetails = "delete from ProfileDetails where userId='" + UID + "'";
            SqlCommand comProfileDetails = new SqlCommand(deleteFromProfileDetails, conn);
            comProfileDetails.ExecuteNonQuery();

            //Delete from ProfilePictures
            string deleteFromProfilePictures = "delete from ProfilePictures where userId='" + UID + "'";
            SqlCommand comProfilePictures = new SqlCommand(deleteFromProfilePictures, conn);
            comProfilePictures.ExecuteNonQuery();

            //Delete from Mesaje
            string deleteFromMesaje = "delete from Mesaje where userId='" + UID + "' or fId='"+ UID + "'";
            SqlCommand comMesaje = new SqlCommand(deleteFromMesaje, conn);
            comMesaje.ExecuteNonQuery();

            //Delete from Media
            string deleteFromMedia = "delete from Media where userId='" + UID + "'";
            SqlCommand comMedia = new SqlCommand(deleteFromMedia, conn);
            comMedia.ExecuteNonQuery();

            //Delete from Friends
            string deleteFromFriends = "delete from Friends where userId='" + UID + "' or fuserID='"+UID+"'";
            SqlCommand comFriends = new SqlCommand(deleteFromFriends, conn);
            comFriends.ExecuteNonQuery();

            //Delete from FriendRequests
            string deleteFromFriendRequests = "delete from FriendRequests where userId='" + UID + "' or requesterUserId='" + UID + "'";
            SqlCommand comFriendRequests = new SqlCommand(deleteFromFriendRequests, conn);
            comFriendRequests.ExecuteNonQuery();

            //Delete from Dates
            string deleteFromDates = "delete from Dates where userId='" + UID + "' or fId='" + UID + "'";
            SqlCommand comDates = new SqlCommand(deleteFromDates, conn);
            comDates.ExecuteNonQuery();

            //Delete from DateRequests
            string deleteDateRequests = "delete from DateRequests where userId='" + UID + "' or fId='" + UID + "'";
            SqlCommand comDateRequests = new SqlCommand(deleteDateRequests, conn);
            comDateRequests.ExecuteNonQuery();

            conn.Close();


            //Delete session and redirect to Login Page
            Session["New"] = null;
            Response.Redirect("Login.aspx");

        }
        else
        {
            wrongPassword.Visible = true;
        }
    }

    protected void ButtonNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountSettings.aspx");
    }

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
}