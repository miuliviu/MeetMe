<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="Dates.aspx.cs" Inherits="Dates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/DatesStyle.css" rel="stylesheet" />
    <div class="titleBlock">
        <p>Dates</p>
    </div>
    <div id="allDates" runat="server" class="">

    </div>
</asp:Content>

