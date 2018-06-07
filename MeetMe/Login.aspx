<%@ Page Language="C#" MasterPageFile="~/MeetMeLogReg.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>


 <asp:Content ID="loginContentBody" ContentPlaceHolderID="contentBody" runat="server" >
     <link href="CSS/LoginStyle.css" rel="stylesheet" />
        <div style="width:50%; float:left">
            <a href="Login.aspx"><img src="Images/Logo.png" alt="Error" style="padding-top:25%; margin-left:70px;" ></a>
        </div>
     <div style="width:50%; float:right; text-align: center;">
            <p class="titleFashion">Login</p>
            
            <br />
            <table class="auto-style2" style="float:right; padding-top:5%;">
                <tr class="auto-style6">
                    <td class="auto-style4"><b>E-mail</b></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TextBoxEmail" runat="server" Width="220px" CssClass="inputFashion"></asp:TextBox>
                    </td>
                    <td class="auto-style3">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Please enter an e-mail" ForeColor="Red" ValidationGroup="LoginValidation"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="auto-style6">
                    <td class="auto-style4"><b>Password</b></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" Width="220px" CssClass="inputFashion"></asp:TextBox>
                    </td>
                    <td class="auto-style11">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="Please enter a password" ForeColor="Red" ValidationGroup="LoginValidation"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="auto-style6">
                    <td class="auto-style4">&nbsp;</td>
                    <td><p id="wrongUserPass" runat="server" style="color:red;" visible="false">Wrong E-mail or Password!</p></td>
                    <td class="auto-style7">&nbsp;</td>
                </tr>
                <tr class="auto-style6">
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style9">
                        <asp:Button ID="ButtonLogin" runat="server" OnClick="ButtonLogin_Click" Text="Login" ValidationGroup="LoginValidation" Width="110px" style="height: 26px" Height="26px" CssClass="buttonFashion" />
                        <asp:Button ID="ButtonRegister" runat="server" OnClick="ButtonRegister_Click" Text="Register" Width="110px" Height="26px" CssClass="buttonFashion" />
                    </td>
                    <td class="auto-style7">&nbsp;</td>
                </tr>
                <tr class="auto-style6">
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style12">
                        <asp:HyperLink ID="HyperLinkForgot" runat="server" ForeColor="#0066FF" NavigateUrl="~/ForgotPass.aspx">Forgot your password?</asp:HyperLink>
                    </td>
                    <td class="auto-style7">&nbsp;</td>
                </tr>
            </table>
        </div>
   </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            margin-right: 5px;
            margin-left: 4px;
        }
        .auto-style2 {
            text-align: center;
        }
        .auto-style3 {
            text-align: left;
        }
        .auto-style4 {
            text-align: right;
        }
    </style>
</asp:Content>

