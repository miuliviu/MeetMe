using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["New"] != null)
        {
            Response.Redirect("UserProfile.aspx");
        }
    }

    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        conn.Open();
        string checkEmail = "select count(*) from Users where email='" + TextBoxEmail.Text + "'";
        SqlCommand com = new SqlCommand(checkEmail, conn);
        int count = Convert.ToInt32(com.ExecuteScalar().ToString());
        conn.Close();
        if (count == 1)
        {
            conn.Open();
            string checkPasswordQuerry = "select pass from Users where email='" + TextBoxEmail.Text + "'";
            SqlCommand passwordComm = new SqlCommand(checkPasswordQuerry, conn);
            string passwordDB = passwordComm.ExecuteScalar().ToString().Trim();//Replace(" ","");
            if (passwordDB.Equals(Encrypt(TextBoxPassword.Text).ToString().Trim()))
            {
                Session["New"] = TextBoxEmail.Text;

                Response.Redirect("UserProfile.aspx");
            }
            else
            {
                wrongUserPass.Visible = true;
            }
            conn.Close();
        }
        else
        {
            wrongUserPass.Visible = true;
        }
    }

    protected void ButtonRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registration.aspx");
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