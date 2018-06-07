<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MeetMeLogReg.master" CodeFile="ForgotPass.aspx.cs" Inherits="_Default" %>

<asp:Content ID="forgotPassContentBody" ContentPlaceHolderID="contentBody" runat="server" >
    <link href="CSS/ForgotPassStyle.css" rel="stylesheet" />
    <div style="width:50%; float:left">
            <a href="Login.aspx"><img src="Images/Logo.png" alt="Error" style="padding-top:25%; margin-left:70px;" ></a>
    </div>
    <div style="width:50%; float:right; text-align: center;">
        <p class="titleFashion">Recover Password</p>
        <br />
        <p class="textMes"><b>Please use the form below to have your password emailed to you.</b></p>
        <table class="auto-style2">
            <tr>
                <td class="auto-style1"><b>Email address</b></td>
                <td class="auto-style8">
                    <asp:TextBox ID="TextBoxEmail" runat="server" Width="248px" CssClass="inputFashion"></asp:TextBox>
                </td>
                <td class="auto-style11">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="A valid email address has to be submitted." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Group1"></asp:RegularExpressionValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="An email address is required." ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <div class="buttonSection">
            <asp:Button ID="ButtonRecover" runat="server" OnClick="ButtonRecover_Click" Text="Recover" ValidationGroup="Group1" Width="125px" CssClass="buttonFashion1" />
            <asp:Button ID="ButtonBackLogin" runat="server" OnClick="ButtonBackLogin_Click" Text="Back to Login" Width="125px" CssClass="buttonFashion1" />
        </div>
    </div>
</asp:Content>    
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 110px;
        }
    </style>
</asp:Content>
    
