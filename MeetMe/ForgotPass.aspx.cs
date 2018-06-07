using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonBackLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void ButtonRecover_Click(object sender, EventArgs e)
    {

        //String to save the password
        string passwordDB="";

        //String to save the first name
        string firstname = "";

        //Checking the email
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        conn.Open();
        string checkEmail = "select count(*) from Users where email='" + TextBoxEmail.Text + "'";
        SqlCommand com = new SqlCommand(checkEmail, conn);
        int count = Convert.ToInt32(com.ExecuteScalar().ToString());
        conn.Close();
        if (count == 1)
        {
            conn.Open();
            string retrievePassword= "select pass from Users where email='" + TextBoxEmail.Text + "'";
            SqlCommand passwordComm = new SqlCommand(retrievePassword, conn);
            passwordDB = passwordComm.ExecuteScalar().ToString().Replace(" ", "");

            string retrieveFirstName = "select fname from Users where email='" + TextBoxEmail.Text + "'";
            SqlCommand firstnameComm = new SqlCommand(retrieveFirstName, conn);
            firstname = firstnameComm.ExecuteScalar().ToString().Replace(" ", "");

            //Emailing the user with the password
            MailMessage mail = new MailMessage();
            mail.To.Add(TextBoxEmail.Text);
            mail.From = new MailAddress("noreplymeetme@gmail.com", "no-reply - MeetMe Support", System.Text.Encoding.UTF8);
            mail.Subject = "Recovery password for the MeetMe account";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            //mail.Body = "Hello "+firstname+"! Your password is: " + passwordDB;
            mail.Body = "<p>Hello " + firstname + "!</p><div>&nbsp;</div><div>This is an email received for completing the&nbsp;<em> Recover Password &nbsp;</em> form for your &nbsp;<em> MeetMe &nbsp;</em>&nbsp; account.</div><div>&nbsp;</div><div> Your password is: " + Decrypt(passwordDB).ToString()+"</div><div> &nbsp;</div><div>Thank you for using MeetMe!</div>";
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("noreplymeetme@gmail.com", "servicesupport");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);

            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
            }

            Response.Redirect("Login.aspx");
        }
        else
        {
            Response.Write("There is no User with this email address.");
        }

        
    }

    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}