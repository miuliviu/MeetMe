<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="DateInvitationSentConfirmation.aspx.cs" Inherits="DateInvitationSentConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/DateConfirmStyle.css" rel="stylesheet" />
    <div class="titleBlock">
        <p>Date Invitation for <%= friendName %></p>
    </div>
     <p ID="LabelInvitationSent" runat="server" class="invitationSentText" ></p>
    <div class="buttonArea">
        <asp:Button ID="ButtonBack" runat="server" Text="Back to Profile" CssClass="buttonFashionBack" OnClick="ButtonBack_Click" />
    </div>
</asp:Content>

