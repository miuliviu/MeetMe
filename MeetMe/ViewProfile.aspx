<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="ViewProfile.aspx.cs" Inherits="ViewProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/ViewUserStyle.css" rel="stylesheet" />
    <div class="contentBorder">
        <div class="leftPart">
            <br />
            <div class="nameDiv">
                <p id="nameText"  class="nameTextStyle" runat="server"></p>
            </div>
            <br />
            <asp:Image ID="ImageProfilePicture" runat="server" CssClass="imgProfilePicture" />
            <br />
            <hr class="hrFashion" />
            
            <br />
            
            <div class="friendStatusDiv">
                <p id="friendStatusText"  class="friendStatusTextStyle" runat="server">Not a Friend</p>
            </div>
            <br />
            <hr class="hrFashion" />
            <br />
            <asp:Button ID="ButtonAddRemoveFriend" runat="server" OnClick="ButtonAddRemoveFriend_Click" Text="Add/Remove Friend" CssClass="buttonFashionRemoveFriend" />
            <asp:Button runat="server" text="Send Message" ID="ButtonChat" OnClick="ButtonChat_Click" CssClass="buttonFashionSendMes" />
            <asp:Button ID="ButtonDate" runat="server" Text="Date" CssClass="buttonFashionDate" OnClick="ButtonDate_Click" />
            <br />
            <hr class="hrFashionLast" />
            <br />
            <div class="containerGal">
                <asp:imagebutton runat="server" ID="ImageButtonGallery" CssClass="galleryImg" Height="180px" OnClick="ImageButtonGallery_Click" Width="180px" ></asp:imagebutton>
                <div class="centeredText"><a href="ViewProfileGallery.aspx?id=<%=viewUserID %>">Gallery</a></div>
            </div>
        </div>
        
        <div class="rightPart">
            <div class="titleBlock">
                <p>Profile Details</p>
            </div>
            <table id="detailsTable" runat="server" class="profileDetailsTable">
                <tr><td><p><b>Description</b></p></td><td><%= description %></td></tr>
                <tr><td><p><b>City</b></p></td><td><%= city %></td></tr>
                <tr><td><p><b>Age</b></p></td><td><%= age %></td></tr>
                <tr><td><p><b>Hobby</b></p></td><td><%= hobby %></td></tr>
                <tr><td><p><b>Gender</b></p></td><td><%= gender %></td></tr>
                <tr><td><p><b>Interested in</b></p></td><td><%= interested %></td></tr>
                <tr><td><p><b>Relationship Status</b></p></td><td><%= relationStatus %></td></tr>
                <tr><td><p><b>Account Privacy</b></p></td><td><%= privacy %></td></tr>
            </table>
            <p id="privateText" runat="server" class="privateTextFashion" visible="false">Private Account</p>
            <br />
            <hr class="breakLineRightPart" />
            <br />
            <div class="suggestionTextFashion">
                <p class="peopleTitle">People you may know</p>
            </div>
            <div id="suggestionDiv" runat="server" class="suggestionDivFashion">

            </div>
        </div>
    </div>
</asp:Content>

