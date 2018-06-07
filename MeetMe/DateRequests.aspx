<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="DateRequests.aspx.cs" Inherits="DateRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/DateRequestsStyle.css" rel="stylesheet" />
    <div class="titleBlock">
        <p>Date Requests</p>
    </div>
    <div id="allRequests" runat="server" class="">

    </div>
</asp:Content>

