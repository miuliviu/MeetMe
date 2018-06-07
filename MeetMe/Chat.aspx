<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/ChatStyle.css" rel="stylesheet" />
    <div class="titleBlock">
        <p>Chat Conversations</p>
    </div>
    <div class="allConversationsFashion" runat="server" id="allConversations">

    </div>
</asp:Content>

