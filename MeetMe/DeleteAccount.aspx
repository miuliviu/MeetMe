<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="DeleteAccount.aspx.cs" Inherits="DeleteAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/DeleteAccountStyle.css" rel="stylesheet" />
    <div class="titleBlock">
        <p>Delete Account</p>
    </div>
    <div class="allContent">
        <p class="questionStyle">Are you sure you want to delete your Meet Me account?</p>
        <p class="noteStyle"><b>Note:</b> By deleting your account all your gallery pictures and all your messages will be deleted!</p>
        <p class="enterPass">In order to delete your account you must enter your password.</p>
        <div class="pass1">
            <p class="pass1Style">Password</p>
            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" CssClass="textInputFashion"></asp:TextBox>
        </div>
        <div class="pass2">
            <p class="pass2Style">Confirm Password</p>
            <asp:TextBox ID="TextBoxConfirmPassword" runat="server" TextMode="Password" CssClass="textInputFashion"></asp:TextBox>
        </div>
        <div class="valDiv">
            <asp:CompareValidator ID="CompareValidatorPasswords" runat="server" ErrorMessage="Passwords must match!" ControlToCompare="TextBoxPassword" ControlToValidate="TextBoxConfirmPassword" ForeColor="Red" CssClass="passValidator"></asp:CompareValidator>
        </div>
        <p id="wrongPassword" runat="server" style="color:red; padding-top:10px;" visible="false">Wrong Password!</p>
        <div class="buttonArea">
            <asp:Button ID="ButtonYes" runat="server" Text="Yes!" CssClass="buttonFashionYes" OnClick="ButtonYes_Click" />
            <asp:Button ID="ButtonNo" runat="server" Text="No." CssClass="buttonFashionNo" OnClick="ButtonNo_Click" />
        </div>
    </div>
</asp:Content>

