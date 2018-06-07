<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="Friends.aspx.cs" Inherits="Friends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/FriendsStyle.css" rel="stylesheet" />
    <div class="contentBody">
        <div class="titleBlock">
            <p>Your Friends</p>
        </div>
        <p id="noFriendsText" class="noFrText" runat="server" visible="False">You have no friends. Search and add new friends.</p>
        <asp:table runat="server" ID="TableFriends" CssClass="TableFriendsStyle"></asp:table>   
    </div>
</asp:Content>

