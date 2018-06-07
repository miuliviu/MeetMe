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

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
            conn.Open();

            //Insert in Users
            string registerUser = "insert into Users(fname, sname, email, pass, country, gender) values (@first, @surname, @email, @pass, @country, @gender)";
            SqlCommand com = new SqlCommand(registerUser, conn);

            
            

            //Check Email
            string checkuser = "select count(*) from Users where email='" + TextBoxEmail.Text + "'";
            SqlCommand checkEmail = new SqlCommand(checkuser, conn);
            int temp = Convert.ToInt32(checkEmail.ExecuteScalar().ToString().Replace(" ",""));
            if (temp >= 1)
            {
                Response.Write("User already exists.");
            }
            else
            {

                com.Parameters.AddWithValue("@first", TextBoxFirstName.Text);
                com.Parameters.AddWithValue("@surname", TextBoxSurname.Text);
                com.Parameters.AddWithValue("@email", TextBoxEmail.Text);

                //Ecrypt Pass
                string ecnryptedPass = Encrypt(TextBoxPass.Text);
                com.Parameters.AddWithValue("@pass", ecnryptedPass);
                com.Parameters.AddWithValue("@country", DropDownListCountry.SelectedItem.ToString());
                com.Parameters.AddWithValue("@gender", DropDownListGender.SelectedItem.ToString());

                com.ExecuteNonQuery();

                //Insert in Profile Details
                string registerUserProfileDetails = "insert into ProfileDetails(description, rstatus, city, interested, age, hobby, privacy," +
                " userId, gender) values (@desc, @rst, @ct, @inter, @ani, @hob, @priv, @uID, @gen)";
                SqlCommand com1 = new SqlCommand(registerUserProfileDetails, conn);

                SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where email='" + TextBoxEmail.Text + "'", conn);
                DataTable dt = new DataTable();
                sdaUserData.Fill(dt);

                //Complete parameters for ProfileDetails
                com1.Parameters.AddWithValue("@desc", "Add description");
                com1.Parameters.AddWithValue("@rst", "Single");
                com1.Parameters.AddWithValue("@ct", "Add a City");
                if(DropDownListGender.SelectedItem.ToString() =="Male")
                    com1.Parameters.AddWithValue("@inter", "Woman");
                else
                    com1.Parameters.AddWithValue("@inter", "Man");
                com1.Parameters.AddWithValue("@ani", "0");
                com1.Parameters.AddWithValue("@hob", "Add a hobby");
                com1.Parameters.AddWithValue("@priv", "Public");
                com1.Parameters.AddWithValue("@uID", dt.Rows[0]["UserID"].ToString());
                com1.Parameters.AddWithValue("@gen", DropDownListGender.SelectedItem.ToString());

                com1.ExecuteNonQuery();

                Response.Write("Registration is Successful!");
                Response.Redirect("Login.aspx");

            }
            conn.Close();
        }
        catch(Exception ex)
        {
            Response.Write("Error: "+ex.ToString());
        }
    }

    protected void ButtonToLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
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