<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="FriendRequests.aspx.cs" Inherits="FriendRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/FriendRequestStyle.css" rel="stylesheet" />
    <div class="contentBody">
        <div class="titleBlock">
            <p>Friend Requests</p>
        </div>
        <asp:table runat="server" ID="TableFriendRequestResults" CssClass="TableSearchResults"></asp:table>
        <asp:Label ID="LabelNoRequestsMessage" runat="server" Text="There are no Friend Requests at the moment." CssClass="noFriendReq" Visible="False"></asp:Label>
    </div>
</asp:Content>

