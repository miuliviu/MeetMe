<%@ Page Title="" Language="C#" MasterPageFile="~/MeetMe.master" AutoEventWireup="true" CodeFile="ViewProfileGallery.aspx.cs" Inherits="ViewProfileGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <link href="CSS/ViewGallerySheet.css" rel="stylesheet" />
    <div class="titleBlock">
            <p><%= userName%>'s Gallery</p>
    </div>
    <div class="allContent">
        <p id="noPictureText" class="noPcText" runat="server" visible="False">There are no Pictures displayed.</p>
        <div class="contentBorder" id="galleryContent" runat="server">

        </div>
        <div class="clearArea"></div>
    </div>
</asp:Content>

