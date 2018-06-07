using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class MeetMe : System.Web.UI.MasterPage
{
    protected string UID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Register Update Trigger
        ScriptManager1.RegisterAsyncPostBackControl(ButtonInv1);

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaUserData = new SqlDataAdapter("select * from Users where email='"+Session["New"].ToString()+"'", conn);
        DataTable dt = new DataTable();
        sdaUserData.Fill(dt);
        UID = dt.Rows[0]["UserID"].ToString();


        //Notification System
        SqlDataAdapter sdaMesaje = new SqlDataAdapter("select * from Mesaje where userId='" + UID + "'", conn);
        DataTable dtt = new DataTable();
        sdaMesaje.Fill(dtt);

        foreach (DataRow msgRes in dtt.Rows)
        {
            if (msgRes["seen"].ToString().Trim() == "no")
            {
                chatTab.Attributes.Add("style", "background-color:#b50000; border-radius: 5px;");

                break;
            }
        }

        SqlDataAdapter sdaFriendReq = new SqlDataAdapter("select * from FriendRequests where userId='" + UID + "'", conn);
        DataTable dt1 = new DataTable();
        sdaFriendReq.Fill(dt1);

        foreach (DataRow friendReqRes in dt1.Rows)
        {
            if (friendReqRes["seen"].ToString().Trim() == "no")
            {
                testDepTxt.Attributes.Add("style", "background-color:#b50000; border-radius: 5px;");

                break;
            }
        }

        SqlDataAdapter sdaDateReq = new SqlDataAdapter("select * from DateRequests where fId='" + UID + "'", conn);
        DataTable dt2 = new DataTable();
        sdaDateReq.Fill(dt2);

        foreach (DataRow dateReqRes in dt2.Rows)
        {
            if (dateReqRes["seen"].ToString().Trim() == "no")
            {
                dateReqTab.Attributes.Add("style", "background-color:#b50000; border-radius: 5px;");

                break;
            }
        }

    }

    protected void ButtonLogout_Click(object sender, EventArgs e)
    {
        Session["New"] = null;
        Response.Redirect("Login.aspx");
    }

    

    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchResults.aspx?search=" + TextBoxSearchBar.Text.ToString()+"&cat="+DropDownListSearchCategory.SelectedValue.ToString());
    }

    protected void ButtonInv1_Click(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetMeConnectionString"].ConnectionString);
        SqlDataAdapter sdaMesaje = new SqlDataAdapter("select * from Mesaje where userId='" + UID + "'", conn);
        DataTable dt = new DataTable();
        sdaMesaje.Fill(dt);

        foreach (DataRow msgRes in dt.Rows)
        {
            if (msgRes["seen"].ToString().Trim() == "no")
            {
                chatTab.Attributes.Add("style", "background-color:#b50000; border-radius: 5px;");
                
                break;
            }
        }

        SqlDataAdapter sdaFriendReq = new SqlDataAdapter("select * from FriendRequests where userId='" + UID + "'", conn);
        DataTable dt1 = new DataTable();
        sdaFriendReq.Fill(dt1);

        foreach (DataRow friendReqRes in dt1.Rows)
        {
            if (friendReqRes["seen"].ToString().Trim() == "no")
            {
                testDepTxt.Attributes.Add("style", "background-color:#b50000; border-radius: 5px;");

                break;
            }
        }

        SqlDataAdapter sdaDateReq = new SqlDataAdapter("select * from DateRequests where fId='" + UID + "'", conn);
        DataTable dt2 = new DataTable();
        sdaDateReq.Fill(dt2);

        foreach (DataRow dateReqRes in dt2.Rows)
        {
            if (dateReqRes["seen"].ToString().Trim() == "no")
            {
                dateReqTab.Attributes.Add("style", "background-color:#b50000; border-radius: 5px;");

                break;
            }
        }


    }
}
