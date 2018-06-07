<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="SearchResults.aspx.cs" Inherits="SearchResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/SearchStyle.css" rel="stylesheet" />
    <div class="searchPageContent">
        <p id="searchResText" class="serachResText" runat="server">Search Results for</p>
        <asp:table runat="server" ID="TableSearchResults" CssClass="TableSearchResults"></asp:table>
        <hr class="lineStyle" />
        <div class="suggestionTextFashion">
            <p class="peopleTitle">People you may know</p>
        </div>
        <div id="suggestionDiv" runat="server" class="suggestionDivFashion">

        </div>
    </div>
</asp:Content>

