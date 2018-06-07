<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="AccountSettings.aspx.cs" Inherits="AccountSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/SettingsStyle.css" rel="stylesheet" />
    <div class="titleBlock">
        <p>Account Settings</p>
    </div>
    <div class="allContent">
        <div class="textArea">
        <p class="privacyText">Privacy Option</p>
        <hr class="line1" />
        <p class="chatText">Chat Helper</p>
        <hr class="line1" />
        <p class="chatText" id="ratingLeftText" runat="server">Please Rate <b>Meet Me</b></p>
        <hr class="line1" />
        <p class="deleteText">Delete Account</p>
        </div>
        <div class="buttonArea">
            <asp:DropDownList ID="DropDownListPrivacyOption" runat="server" CssClass="dropDownFashionPrivacy" AutoPostBack="True">
                <asp:ListItem>Public</asp:ListItem>
                <asp:ListItem>Private</asp:ListItem>
            </asp:DropDownList>
            <hr class="line2"/>
            <asp:dropdownlist runat="server" ID="DropDownListChatOption" CssClass="dropDownFashionChat" AutoPostBack="True">
                <asp:ListItem>On</asp:ListItem>
                <asp:ListItem>Off</asp:ListItem>
            </asp:dropdownlist>
            <hr class="line2"/>
            <div id="ratingStarsDiv" runat="server" class="rating">
                <a href="RateMeetMe.aspx?uid=<%= UserID%>&stars=5" title="Very Good">☆</a>
                <a href="RateMeetMe.aspx?uid=<%= UserID%>&stars=4" title="Good">☆</a>
                <a href="RateMeetMe.aspx?uid=<%= UserID%>&stars=3" title="Ok">☆</a>
                <a href="RateMeetMe.aspx?uid=<%= UserID%>&stars=2" title="Bad">☆</a>
                <a href="RateMeetMe.aspx?uid=<%= UserID%>&stars=1" title="Very Bad">☆</a>
            </div>
            <div id="ratingAveDiv" runat="server" class="ratAveDivFashion">
                <p class="averageStarFashion" id="averageStarLeft" runat="server">☆</p>
            </div>
            <hr class="line2"/>
            <asp:Button ID="ButtonDeleteAccount" runat="server" Text="Delete!" CssClass="buttonFashionDeleteAcc" OnClick="ButtonDeleteAccount_Click" />
        </div>
        <div class="bugEmailDiv">
            <p class="reportEmailText">For any inconvenient issues encountered feel free to email us at <b>noreplymeetme@gmail.com</b></p>
        </div>
        <div class="clearArea"></div>
        <div class="applySettingsArea">
            <asp:Button ID="ButtonApply" runat="server" Text="Apply" OnClick="ButtonApply_Click" CssClass="buttonFashionApply" />
            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CssClass="buttonFashionCancelAcc" OnClick="ButtonCancel_Click" />
        </div>
    </div>
</asp:Content>

